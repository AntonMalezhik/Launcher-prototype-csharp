using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace LauncherPrototype
{
    public partial class Form1 : Form
    {
        // Переменные лаунчера ===================================================

        // Примечание, внутри ZIP архива, не должно быть вложенных папок (Вплане основная папка в папке)
        // ✔️ Правильно: PRODUCT_NAME.zip/PRODUCT_EXE
        // ❌ Неправильно: PRODUCT_NAME.zip/PRODUCT_NAME/PRODUCT_EXE

        // URL к папке на сервере, "/" на конце обязательно!
        public string SERVER_URL = "http://147.185.221.26:60821/Games Updates/";

        // Имя папки которая будет создана при скачивании контента
        // Оно же Название ZIP архива на сервере
        // Оно же и название для .txt файла на сервере с версией
        public string PRODUCT_NAME = "Project Zomboid Steam Launcher Legacy";

        // Путь к исполняемому файлы, используется как: PRODUCT_NAME/PRODUCT_EXE
        public string PRODUCT_EXE = "Launcher.exe";

        // =======================================================================

        public Form1()
        {
            InitializeComponent();
        }

        // Когда форма полностью показалась
        private async void Form1_Shown(object sender, EventArgs e)
        {

            // Задержка проверки в МС
            await Task.Delay(1000);

            // Проверка файлов при старте
            CheckAndDownloadServerFiles();
        }

        // Процедура управления состоянием кнопок запуска
        public void UpdateStartButtonsState(bool isEnable)
        {
            // По умолчанию кнопки отключены
            bool buttonsEnabled = false;

            // Проверяем условия для включения кнопок только если isEnable=true
            if (isEnable)
            {
                bool folderExists = Directory.Exists(PRODUCT_NAME);
                bool fileExists = folderExists && File.Exists(Path.Combine(PRODUCT_NAME, PRODUCT_EXE));

                buttonsEnabled = folderExists && fileExists;
            }

            // Устанавливаем состояние для всех связанных кнопок
            GUI_startbtn.Enabled = buttonsEnabled;
            startappToolStripMenuItem.Enabled = buttonsEnabled;
        }

        // Процедура удаления продукта
        public async void DeleteProduct()
        {

            // Отключаем кнопки запуска
            UpdateStartButtonsState(false);

            // Проверяем папки и файлы и удаляем их
            if (File.Exists("productversion.txt")) File.Delete("productversion.txt");
            if (File.Exists("tempProductFile.zip")) File.Delete("tempProductFile.zip");
            if (Directory.Exists(PRODUCT_NAME)) Directory.Delete(PRODUCT_NAME, true);

            // Уведомление для пользователя
            GUI_progesslabel.Text = "Удаление выполнено";

        }

        // Процедура распаковки продукта
        public async void UnpackProduct(string serverVersion)
        {

            // Уведомление для пользователя
            GUI_progesslabel.Text = "Начало распаковки...";

            // Начало скачивания с отслеживанием прогресса =========
            using (var archive = ZipFile.OpenRead("tempProductFile.zip"))
            {
                long totalBytes = archive.Entries.Sum(e => e.Length);
                long totalRead = 0;

                foreach (var entry in archive.Entries)
                {
                    string fullPath = Path.Combine(PRODUCT_NAME, entry.FullName);

                    // Это папка
                    if (entry.Length == 0)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                        continue;
                    }

                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                    using (var entryStream = entry.Open())
                    using (var fileStream = File.Create(fullPath))
                    {
                        byte[] buffer = new byte[8192];
                        int bytesRead;

                        while ((bytesRead = await entryStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead);
                            totalRead += bytesRead;

                            var progress = (int)((totalRead * 100) / totalBytes);
                            GUI_progessbar.Value = progress;
                            GUI_progesslabel.Text = $"Установка: {progress}%, {totalRead / (1024 * 1024)} MB из {totalBytes / (1024 * 1024)} MB";
                        }
                    }
                }
            }

            // Удаление архива после распаковки
            File.Delete("tempProductFile.zip");

            // Уведомление для пользователя
            GUI_progesslabel.Text = "Установка завершена";

            // Включаем кнопки
            UpdateStartButtonsState(true);

            // Включаем кнопку удаления с устройства
            deleteFromDeviceToolStripMenuItem.Enabled = Enabled;

            // Включаем кнопку обновления
            checkUpdatesToolStripMenuItem.Enabled = Enabled;

            // Обновляем версию
            File.WriteAllText("productversion.txt", serverVersion);

            // Отображения версии продукта
            GUI_labelversion.Text = serverVersion;

        }

        // Скачивание zip архива из сервера
        public async void DownloadZIPfromServer(string serverVersion)
        {
            var httpClient = new HttpClient();
            
            try {

                using (var response = await httpClient.GetAsync($"{SERVER_URL}{PRODUCT_NAME}.zip", HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    var totalBytes = response.Content.Headers.ContentLength ?? -1L;

                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var fileStream = new FileStream("tempProductFile.zip", FileMode.Create))
                    {
                        var buffer = new byte[8192];
                        var totalRead = 0L;
                        var bytesRead = 0;

                        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead);
                            totalRead += bytesRead;

                            if (totalBytes > 0)
                            {
                                var progress = (int)((totalRead * 100) / totalBytes);
                                GUI_progessbar.Value = progress;
                                GUI_progesslabel.Text = $"Скачивание: {progress}%, {totalRead / (1024 * 1024)} MB из {totalBytes / (1024 * 1024)} MB";
                            }
                        }
                    }
                }

                // Уведомление для пользователя
                GUI_progesslabel.Text = "Скачивание завершено";

                // Закрываем соединение с сервером
                httpClient.Dispose();

                // Начинаем распаковку
                UnpackProduct(serverVersion);

            } catch (Exception ex) {

                // Отображение ошибки для пользователя
                GUI_progesslabel.Text = $"Ошибка скачивания: {ex.Message}";
            }
        }

        // Процедура проверки и скачивания файлов с сервера
        public async void CheckAndDownloadServerFiles()
        {

            try {

                // Выключаем кнопки
                UpdateStartButtonsState(false);
                
                // Отключаем кнопку обновления
                checkUpdatesToolStripMenuItem.Enabled = false;

                // Проверка существования файла версии и его создание
                if (!File.Exists("productversion.txt")) File.Create("productversion.txt").Close();

                // Получаем версию продукта
                string localVersion = File.ReadAllText("productversion.txt").Trim();

                // Отображения версии продукта
                GUI_labelversion.Text = localVersion;

                // Уведомление пользователя
                GUI_progesslabel.Text = "Пожалуйста подождите, идет проверка сервера на доступность...";

                // Запрос на нужную папку на сервере
                var response = (HttpWebResponse) await WebRequest.Create(SERVER_URL).GetResponseAsync();

                // Уведомление пользователя
                GUI_progesslabel.Text = "Сервер в сети";

                try {

                    // Получаем версию из сервера
                    string serverVersion = await new HttpClient().GetStringAsync($"{SERVER_URL}{PRODUCT_NAME}.txt");

                    // Уведомление пользователя
                    GUI_progesslabel.Text = "Проверка обновлений...";

                    // Сравниваем версии и проверяем папку с файлом
                    if (localVersion == serverVersion && Directory.Exists(PRODUCT_NAME) && File.Exists($"{PRODUCT_NAME}/{PRODUCT_EXE}")){

                        // Уведомление пользователя
                        GUI_progesslabel.Text = "У вас последняя версия ✔️";

                        // Включаем кнопки
                        UpdateStartButtonsState(true);

                        // Включаем кнопку обновления
                        checkUpdatesToolStripMenuItem.Enabled = Enabled;

                    } else {

                        // Уведомление пользователя
                        GUI_progesslabel.Text = "Требуется обновление...";

                        // Спрашиваем пользователя
                        DialogResult result = MessageBox.Show("Доступно новое обновление! Хотите загрузить его прямо сейчас?", "Обновление", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        // Выбор ответа
                        switch (result)
                        {

                            // Если Да
                            case DialogResult.Yes:

                                // Отключаем кнопку удаления с устройства
                                deleteFromDeviceToolStripMenuItem.Enabled = false;

                                // Запуск удаления и скачивания обновления
                                DeleteProduct();

                                // Скачивание ZIP архива из сервера
                                DownloadZIPfromServer(serverVersion);

                                break;

                            // Если Нет
                            case DialogResult.No:

                                // Включаем кнопки
                                UpdateStartButtonsState(true);

                                // Проверяем, есть ли игровые файлы
                                if (Directory.Exists(PRODUCT_NAME) && File.Exists($"{PRODUCT_NAME}/{PRODUCT_EXE}"))
                                {

                                    // Уведомление пользователя
                                    GUI_progesslabel.Text = "Вы отказались от обновления, можно запускать";

                                } else {

                                    // Уведомление пользователя
                                    GUI_progesslabel.Text = "Вы отказались от обновления, контент не установлен";

                                }

                                // Включаем кнопку обновления
                                checkUpdatesToolStripMenuItem.Enabled = Enabled;

                                break;
                        }

                        // Выход из функции
                        return;

                    }

                } catch (Exception ex) {

                    // Уведомление пользователя
                    GUI_progesslabel.Text = $"Ошибка чтения версии: {ex.Message}";

                    // Включаем кнопки
                    UpdateStartButtonsState(true);

                    // Включаем кнопку обновления
                    checkUpdatesToolStripMenuItem.Enabled = Enabled;

                    // Выход из функции
                    return;

                }

            } catch {

                // Уведомление пользователя
                GUI_progesslabel.Text = "❌ Сервер не отвечает... Попробуйте обновиться позже";

                // Включаем кнопки
                UpdateStartButtonsState(true);

                // Включаем кнопку обновления
                checkUpdatesToolStripMenuItem.Enabled = Enabled;

                // Выходим из функции
                return;

            }
        }

        // Нажатие на кнопку удалить контент
        private void DeleteFromDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Отключаем кнопку обновления
            checkUpdatesToolStripMenuItem.Enabled = false;

            // Проверка директории на существование
            if (!Directory.Exists(PRODUCT_NAME)) {

                // Уведомление
                MessageBox.Show("Программа не нашла контент на удаление", "Удаление с устройства", MessageBoxButtons.OK);

                // Включаем кнопку обновления
                checkUpdatesToolStripMenuItem.Enabled = Enabled;

            } else {

                // Спрашиваем пользователя
                DialogResult result = MessageBox.Show("Вы уверены? Вы собираетесь удалить контент с устройства", "Удаление с устройства", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // Выбор ответа
                switch (result)
                {

                    // Если Да
                    case DialogResult.Yes:

                        // Удаление
                        DeleteProduct();

                        // Включаем кнопку обновления
                        checkUpdatesToolStripMenuItem.Enabled = Enabled;

                        break;
                }

            }
        }

        // Нажатие на кнопку проверки обновления вручную
        private void CheckUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Уведомление пользователя
            GUI_progesslabel.Text = "Запущена проверка обновления...";

            // Начало проверки обновления
            CheckAndDownloadServerFiles();

        }

        // Действие по запуску продукта
        private void StartappToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Уведомление пользователя
            GUI_progesslabel.Text = "✔️ Запуск";

            // Выключаем кнопки
            UpdateStartButtonsState(false);

            // Отключаем кнопку обновления
            checkUpdatesToolStripMenuItem.Enabled = false;

            try {

                // Запускаем в отдельном процессе
                string programPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{PRODUCT_NAME}/{PRODUCT_EXE}";

                // Запуск программы
                Process.Start(new ProcessStartInfo(programPath) { UseShellExecute = true, WorkingDirectory = Path.GetDirectoryName(programPath) });

                // Закрываем лаунчер
                Environment.Exit(0);

            } catch {

                // Уведомление пользователя
                GUI_progesslabel.Text = "❌ Неизвестная ошибка во время запуска!";

                // Включаем кнопку обновления
                checkUpdatesToolStripMenuItem.Enabled = Enabled;

            }

        }
    }
}
