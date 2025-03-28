namespace LauncherPrototype
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.GUI_progessbar = new System.Windows.Forms.ProgressBar();
            this.GUI_progesslabel = new System.Windows.Forms.Label();
            this.menustrip = new System.Windows.Forms.MenuStrip();
            this.ProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFromDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.startappToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GUI_startbtn = new System.Windows.Forms.Button();
            this.GUI_labelversion = new System.Windows.Forms.Label();
            this.menustrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // GUI_progessbar
            // 
            this.GUI_progessbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GUI_progessbar.Location = new System.Drawing.Point(12, 303);
            this.GUI_progessbar.Name = "GUI_progessbar";
            this.GUI_progessbar.Size = new System.Drawing.Size(466, 23);
            this.GUI_progessbar.TabIndex = 0;
            // 
            // GUI_progesslabel
            // 
            this.GUI_progesslabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GUI_progesslabel.AutoSize = true;
            this.GUI_progesslabel.Location = new System.Drawing.Point(13, 284);
            this.GUI_progesslabel.Name = "GUI_progesslabel";
            this.GUI_progesslabel.Size = new System.Drawing.Size(41, 13);
            this.GUI_progesslabel.TabIndex = 1;
            this.GUI_progesslabel.Text = "Статус";
            // 
            // menustrip
            // 
            this.menustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProductToolStripMenuItem});
            this.menustrip.Location = new System.Drawing.Point(0, 0);
            this.menustrip.Name = "menustrip";
            this.menustrip.Size = new System.Drawing.Size(628, 24);
            this.menustrip.TabIndex = 2;
            this.menustrip.Text = "menuStrip1";
            // 
            // ProductToolStripMenuItem
            // 
            this.ProductToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkUpdatesToolStripMenuItem,
            this.deleteFromDeviceToolStripMenuItem,
            this.toolStripSeparator1,
            this.startappToolStripMenuItem});
            this.ProductToolStripMenuItem.Name = "ProductToolStripMenuItem";
            this.ProductToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.ProductToolStripMenuItem.Text = "Контент";
            // 
            // checkUpdatesToolStripMenuItem
            // 
            this.checkUpdatesToolStripMenuItem.Name = "checkUpdatesToolStripMenuItem";
            this.checkUpdatesToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.checkUpdatesToolStripMenuItem.Text = "Проверить обновление";
            this.checkUpdatesToolStripMenuItem.Click += new System.EventHandler(this.CheckUpdatesToolStripMenuItem_Click);
            // 
            // deleteFromDeviceToolStripMenuItem
            // 
            this.deleteFromDeviceToolStripMenuItem.Name = "deleteFromDeviceToolStripMenuItem";
            this.deleteFromDeviceToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.deleteFromDeviceToolStripMenuItem.Text = "Удалить с устройства";
            this.deleteFromDeviceToolStripMenuItem.Click += new System.EventHandler(this.DeleteFromDeviceToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(201, 6);
            // 
            // startappToolStripMenuItem
            // 
            this.startappToolStripMenuItem.Enabled = false;
            this.startappToolStripMenuItem.Name = "startappToolStripMenuItem";
            this.startappToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.startappToolStripMenuItem.Text = "Запустить";
            this.startappToolStripMenuItem.Click += new System.EventHandler(this.StartappToolStripMenuItem_Click);
            // 
            // GUI_startbtn
            // 
            this.GUI_startbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GUI_startbtn.Enabled = false;
            this.GUI_startbtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GUI_startbtn.Location = new System.Drawing.Point(492, 284);
            this.GUI_startbtn.Name = "GUI_startbtn";
            this.GUI_startbtn.Size = new System.Drawing.Size(124, 42);
            this.GUI_startbtn.TabIndex = 3;
            this.GUI_startbtn.Text = "Запуск";
            this.GUI_startbtn.UseVisualStyleBackColor = true;
            this.GUI_startbtn.Click += new System.EventHandler(this.StartappToolStripMenuItem_Click);
            // 
            // GUI_labelversion
            // 
            this.GUI_labelversion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GUI_labelversion.AutoSize = true;
            this.GUI_labelversion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GUI_labelversion.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.GUI_labelversion.Location = new System.Drawing.Point(577, 5);
            this.GUI_labelversion.Name = "GUI_labelversion";
            this.GUI_labelversion.Size = new System.Drawing.Size(44, 13);
            this.GUI_labelversion.TabIndex = 4;
            this.GUI_labelversion.Text = "Версия";
            this.GUI_labelversion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 338);
            this.Controls.Add(this.GUI_labelversion);
            this.Controls.Add(this.GUI_startbtn);
            this.Controls.Add(this.GUI_progesslabel);
            this.Controls.Add(this.GUI_progessbar);
            this.Controls.Add(this.menustrip);
            this.MainMenuStrip = this.menustrip;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Прототип лаунчера";
            this.menustrip.ResumeLayout(false);
            this.menustrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar GUI_progessbar;
        private System.Windows.Forms.Label GUI_progesslabel;
        private System.Windows.Forms.MenuStrip menustrip;
        private System.Windows.Forms.ToolStripMenuItem ProductToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFromDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startappToolStripMenuItem;
        private System.Windows.Forms.Button GUI_startbtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label GUI_labelversion;
    }
}

