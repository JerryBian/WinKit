namespace WinKit
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabControlMain = new TabControl();
            tabPage1 = new TabPage();
            groupBox2 = new GroupBox();
            labelRemainingTime = new Label();
            labelMoveCount = new Label();
            cbAutoMouseMoverEnabled = new CheckBox();
            groupBox1 = new GroupBox();
            cbAutoMouseMoverClickEnabled = new CheckBox();
            label3 = new Label();
            numAutoMouseMoverDisableAfter = new NumericUpDown();
            label2 = new Label();
            numAutoMouseMoverInterval = new NumericUpDown();
            label1 = new Label();
            numAutoMouseMoverPixel = new NumericUpDown();
            tabPage2 = new TabPage();
            groupBox3 = new GroupBox();
            labelAutoShutdownStatus = new Label();
            cbAutoShutdownEnabled = new CheckBox();
            groupBox4 = new GroupBox();
            numAutoShutdownAfter = new NumericUpDown();
            label8 = new Label();
            btnSave = new Button();
            notifyIcon = new NotifyIcon(components);
            contextMenuStripTray = new ContextMenuStrip(components);
            exitToolStripMenuItem = new ToolStripMenuItem();
            tabControlMain.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAutoMouseMoverDisableAfter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numAutoMouseMoverInterval).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numAutoMouseMoverPixel).BeginInit();
            tabPage2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAutoShutdownAfter).BeginInit();
            contextMenuStripTray.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlMain
            // 
            tabControlMain.Controls.Add(tabPage1);
            tabControlMain.Controls.Add(tabPage2);
            tabControlMain.Location = new Point(19, 20);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(755, 395);
            tabControlMain.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(747, 367);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Auto Mouse Mover";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(labelRemainingTime);
            groupBox2.Controls.Add(labelMoveCount);
            groupBox2.Controls.Add(cbAutoMouseMoverEnabled);
            groupBox2.Location = new Point(15, 11);
            groupBox2.Margin = new Padding(2, 1, 2, 1);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2, 1, 2, 1);
            groupBox2.Size = new Size(720, 154);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Status";
            // 
            // labelRemainingTime
            // 
            labelRemainingTime.AutoSize = true;
            labelRemainingTime.ForeColor = Color.Olive;
            labelRemainingTime.Location = new Point(392, 69);
            labelRemainingTime.Name = "labelRemainingTime";
            labelRemainingTime.Size = new Size(0, 15);
            labelRemainingTime.TabIndex = 2;
            // 
            // labelMoveCount
            // 
            labelMoveCount.AutoSize = true;
            labelMoveCount.ForeColor = Color.FromArgb(0, 192, 0);
            labelMoveCount.Location = new Point(392, 36);
            labelMoveCount.Name = "labelMoveCount";
            labelMoveCount.Size = new Size(0, 15);
            labelMoveCount.TabIndex = 1;
            // 
            // cbAutoMouseMoverEnabled
            // 
            cbAutoMouseMoverEnabled.AutoSize = true;
            cbAutoMouseMoverEnabled.Location = new Point(24, 68);
            cbAutoMouseMoverEnabled.Name = "cbAutoMouseMoverEnabled";
            cbAutoMouseMoverEnabled.Size = new Size(166, 19);
            cbAutoMouseMoverEnabled.TabIndex = 0;
            cbAutoMouseMoverEnabled.Text = "Enable Auto Mouse Mover";
            cbAutoMouseMoverEnabled.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cbAutoMouseMoverClickEnabled);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(numAutoMouseMoverDisableAfter);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(numAutoMouseMoverInterval);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(numAutoMouseMoverPixel);
            groupBox1.Location = new Point(15, 178);
            groupBox1.Margin = new Padding(2, 1, 2, 1);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2, 1, 2, 1);
            groupBox1.Size = new Size(720, 188);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Configuration";
            // 
            // cbAutoMouseMoverClickEnabled
            // 
            cbAutoMouseMoverClickEnabled.AutoSize = true;
            cbAutoMouseMoverClickEnabled.Location = new Point(24, 45);
            cbAutoMouseMoverClickEnabled.Name = "cbAutoMouseMoverClickEnabled";
            cbAutoMouseMoverClickEnabled.Size = new Size(150, 19);
            cbAutoMouseMoverClickEnabled.TabIndex = 7;
            cbAutoMouseMoverClickEnabled.Text = "Enable Click after Move";
            cbAutoMouseMoverClickEnabled.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(168, 139);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(138, 15);
            label3.TabIndex = 6;
            label3.Text = "Disabled after in Minutes";
            // 
            // numAutoMouseMoverDisableAfter
            // 
            numAutoMouseMoverDisableAfter.Location = new Point(24, 136);
            numAutoMouseMoverDisableAfter.Margin = new Padding(2, 1, 2, 1);
            numAutoMouseMoverDisableAfter.Maximum = new decimal(new int[] { 525600, 0, 0, 0 });
            numAutoMouseMoverDisableAfter.Name = "numAutoMouseMoverDisableAfter";
            numAutoMouseMoverDisableAfter.Size = new Size(129, 23);
            numAutoMouseMoverDisableAfter.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(536, 84);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 4;
            label2.Text = "Pixels";
            // 
            // numAutoMouseMoverInterval
            // 
            numAutoMouseMoverInterval.Location = new Point(24, 83);
            numAutoMouseMoverInterval.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            numAutoMouseMoverInterval.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numAutoMouseMoverInterval.Name = "numAutoMouseMoverInterval";
            numAutoMouseMoverInterval.Size = new Size(120, 23);
            numAutoMouseMoverInterval.TabIndex = 1;
            numAutoMouseMoverInterval.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(168, 86);
            label1.Name = "label1";
            label1.Size = new Size(106, 15);
            label1.TabIndex = 3;
            label1.Text = "Interval in Seconds";
            // 
            // numAutoMouseMoverPixel
            // 
            numAutoMouseMoverPixel.Location = new Point(392, 83);
            numAutoMouseMoverPixel.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numAutoMouseMoverPixel.Name = "numAutoMouseMoverPixel";
            numAutoMouseMoverPixel.Size = new Size(120, 23);
            numAutoMouseMoverPixel.TabIndex = 2;
            numAutoMouseMoverPixel.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox3);
            tabPage2.Controls.Add(groupBox4);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(747, 367);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Auto Shutdown PC";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(labelAutoShutdownStatus);
            groupBox3.Controls.Add(cbAutoShutdownEnabled);
            groupBox3.Location = new Point(13, 6);
            groupBox3.Margin = new Padding(2, 1, 2, 1);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(2, 1, 2, 1);
            groupBox3.Size = new Size(720, 154);
            groupBox3.TabIndex = 8;
            groupBox3.TabStop = false;
            groupBox3.Text = "Status";
            // 
            // labelAutoShutdownStatus
            // 
            labelAutoShutdownStatus.AutoSize = true;
            labelAutoShutdownStatus.ForeColor = Color.FromArgb(0, 192, 0);
            labelAutoShutdownStatus.Location = new Point(382, 66);
            labelAutoShutdownStatus.Name = "labelAutoShutdownStatus";
            labelAutoShutdownStatus.Size = new Size(0, 15);
            labelAutoShutdownStatus.TabIndex = 1;
            // 
            // cbAutoShutdownEnabled
            // 
            cbAutoShutdownEnabled.AutoSize = true;
            cbAutoShutdownEnabled.Location = new Point(23, 66);
            cbAutoShutdownEnabled.Name = "cbAutoShutdownEnabled";
            cbAutoShutdownEnabled.Size = new Size(165, 19);
            cbAutoShutdownEnabled.TabIndex = 0;
            cbAutoShutdownEnabled.Text = "Enable Auto Shutdown PC";
            cbAutoShutdownEnabled.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(numAutoShutdownAfter);
            groupBox4.Controls.Add(label8);
            groupBox4.Location = new Point(13, 173);
            groupBox4.Margin = new Padding(2, 1, 2, 1);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(2, 1, 2, 1);
            groupBox4.Size = new Size(720, 188);
            groupBox4.TabIndex = 7;
            groupBox4.TabStop = false;
            groupBox4.Text = "Configuration";
            // 
            // numAutoShutdownAfter
            // 
            numAutoShutdownAfter.Location = new Point(23, 62);
            numAutoShutdownAfter.Maximum = new decimal(new int[] { 525600, 0, 0, 0 });
            numAutoShutdownAfter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numAutoShutdownAfter.Name = "numAutoShutdownAfter";
            numAutoShutdownAfter.Size = new Size(120, 23);
            numAutoShutdownAfter.TabIndex = 1;
            numAutoShutdownAfter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(162, 64);
            label8.Name = "label8";
            label8.Size = new Size(147, 15);
            label8.TabIndex = 3;
            label8.Text = "Shutdown after In Minutes";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(192, 255, 255);
            btnSave.Location = new Point(633, 430);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(137, 39);
            btnSave.TabIndex = 1;
            btnSave.Text = "Update Settings";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += OnBtnSaveClick;
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = contextMenuStripTray;
            notifyIcon.Icon = (Icon)resources.GetObject("$this.Icon");
            notifyIcon.Text = "WinKit";
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += OnNotifyIconDoubleClick;
            // 
            // contextMenuStripTray
            // 
            contextMenuStripTray.Items.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            contextMenuStripTray.Name = "contextMenuStripTray";
            contextMenuStripTray.Size = new Size(94, 26);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(93, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += OnExitMenuItemClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(778, 481);
            Controls.Add(btnSave);
            Controls.Add(tabControlMain);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WinKit";
            Shown += OnMainFormShown;
            Resize += OnMainFormResize;
            tabControlMain.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAutoMouseMoverDisableAfter).EndInit();
            ((System.ComponentModel.ISupportInitialize)numAutoMouseMoverInterval).EndInit();
            ((System.ComponentModel.ISupportInitialize)numAutoMouseMoverPixel).EndInit();
            tabPage2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAutoShutdownAfter).EndInit();
            contextMenuStripTray.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControlMain;
        private TabPage tabPage1;
        private CheckBox cbAutoMouseMoverEnabled;
        private TabPage tabPage2;
        private Button btnSave;
        private NumericUpDown numAutoMouseMoverInterval;
        private NumericUpDown numAutoMouseMoverPixel;
        private Label label2;
        private Label label1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label3;
        private NumericUpDown numAutoMouseMoverDisableAfter;
        private CheckBox cbAutoMouseMoverClickEnabled;
        private Label labelRemainingTime;
        private Label labelMoveCount;
        private GroupBox groupBox3;
        private Label labelAutoShutdownStatus;
        private CheckBox cbAutoShutdownEnabled;
        private GroupBox groupBox4;
        private NumericUpDown numAutoShutdownAfter;
        private Label label8;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStripTray;
        private ToolStripMenuItem exitToolStripMenuItem;
    }
}
