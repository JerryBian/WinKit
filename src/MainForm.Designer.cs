namespace WinKit
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControlMain = new TabControl();
            tabPage1 = new TabPage();
            numAutoMouseMoverPixel = new NumericUpDown();
            numAutoMouseMoverInterval = new NumericUpDown();
            cbAutoMouseMoverEnabled = new CheckBox();
            tabPage2 = new TabPage();
            btnSave = new Button();
            tabControlMain.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAutoMouseMoverPixel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numAutoMouseMoverInterval).BeginInit();
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
            tabPage1.Controls.Add(numAutoMouseMoverPixel);
            tabPage1.Controls.Add(numAutoMouseMoverInterval);
            tabPage1.Controls.Add(cbAutoMouseMoverEnabled);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(747, 367);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // numAutoMouseMoverPixel
            // 
            numAutoMouseMoverPixel.Location = new Point(110, 252);
            numAutoMouseMoverPixel.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numAutoMouseMoverPixel.Name = "numAutoMouseMoverPixel";
            numAutoMouseMoverPixel.Size = new Size(120, 23);
            numAutoMouseMoverPixel.TabIndex = 2;
            numAutoMouseMoverPixel.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // numAutoMouseMoverInterval
            // 
            numAutoMouseMoverInterval.Location = new Point(110, 199);
            numAutoMouseMoverInterval.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            numAutoMouseMoverInterval.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numAutoMouseMoverInterval.Name = "numAutoMouseMoverInterval";
            numAutoMouseMoverInterval.Size = new Size(120, 23);
            numAutoMouseMoverInterval.TabIndex = 1;
            numAutoMouseMoverInterval.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // cbAutoMouseMoverEnabled
            // 
            cbAutoMouseMoverEnabled.AutoSize = true;
            cbAutoMouseMoverEnabled.Location = new Point(76, 95);
            cbAutoMouseMoverEnabled.Name = "cbAutoMouseMoverEnabled";
            cbAutoMouseMoverEnabled.Size = new Size(166, 19);
            cbAutoMouseMoverEnabled.TabIndex = 0;
            cbAutoMouseMoverEnabled.Text = "Enable Auto Mouse Mover";
            cbAutoMouseMoverEnabled.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(747, 367);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(695, 417);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += OnBtnSaveClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSave);
            Controls.Add(tabControlMain);
            Name = "MainForm";
            Text = "Form1";
            Shown += OnMainFormShown;
            tabControlMain.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAutoMouseMoverPixel).EndInit();
            ((System.ComponentModel.ISupportInitialize)numAutoMouseMoverInterval).EndInit();
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
    }
}
