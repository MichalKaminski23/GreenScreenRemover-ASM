namespace GreenScreenRemover
{
    partial class Menu
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.removeGreenScreenButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openFileButton = new System.Windows.Forms.Button();
            this.cButton = new System.Windows.Forms.RadioButton();
            this.asmButton = new System.Windows.Forms.RadioButton();
            this.dllPanel = new System.Windows.Forms.Panel();
            this.threadPanel = new System.Windows.Forms.Panel();
            this.thread16 = new System.Windows.Forms.RadioButton();
            this.thread64 = new System.Windows.Forms.RadioButton();
            this.thread32 = new System.Windows.Forms.RadioButton();
            this.thread8 = new System.Windows.Forms.RadioButton();
            this.thread4 = new System.Windows.Forms.RadioButton();
            this.thread2 = new System.Windows.Forms.RadioButton();
            this.thread1 = new System.Windows.Forms.RadioButton();
            this.beforePicture = new System.Windows.Forms.PictureBox();
            this.dllPanel.SuspendLayout();
            this.threadPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beforePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // removeGreenScreenButton
            // 
            this.removeGreenScreenButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.removeGreenScreenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.removeGreenScreenButton.Location = new System.Drawing.Point(843, 464);
            this.removeGreenScreenButton.Name = "removeGreenScreenButton";
            this.removeGreenScreenButton.Size = new System.Drawing.Size(125, 75);
            this.removeGreenScreenButton.TabIndex = 0;
            this.removeGreenScreenButton.Text = "Remove green screen!";
            this.removeGreenScreenButton.UseVisualStyleBackColor = false;
            this.removeGreenScreenButton.Click += new System.EventHandler(this.removeGreenScreenButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(848, 295);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Choose DLL:";
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.exitButton.Location = new System.Drawing.Point(843, 545);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(125, 75);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(835, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 60);
            this.label2.TabIndex = 6;
            this.label2.Text = "Choose number \r\nof threads \r\n(1-64):";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // openFileButton
            // 
            this.openFileButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.openFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.openFileButton.Location = new System.Drawing.Point(843, 383);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(125, 75);
            this.openFileButton.TabIndex = 9;
            this.openFileButton.Text = "Upload .bmp photo";
            this.openFileButton.UseVisualStyleBackColor = false;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // cButton
            // 
            this.cButton.AutoSize = true;
            this.cButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cButton.Location = new System.Drawing.Point(3, 3);
            this.cButton.Name = "cButton";
            this.cButton.Size = new System.Drawing.Size(38, 24);
            this.cButton.TabIndex = 10;
            this.cButton.TabStop = true;
            this.cButton.Text = "C";
            this.cButton.UseVisualStyleBackColor = true;
            // 
            // asmButton
            // 
            this.asmButton.AutoSize = true;
            this.asmButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.asmButton.Location = new System.Drawing.Point(3, 33);
            this.asmButton.Name = "asmButton";
            this.asmButton.Size = new System.Drawing.Size(62, 24);
            this.asmButton.TabIndex = 11;
            this.asmButton.TabStop = true;
            this.asmButton.Text = "ASM";
            this.asmButton.UseVisualStyleBackColor = true;
            // 
            // dllPanel
            // 
            this.dllPanel.Controls.Add(this.asmButton);
            this.dllPanel.Controls.Add(this.cButton);
            this.dllPanel.Location = new System.Drawing.Point(867, 318);
            this.dllPanel.Name = "dllPanel";
            this.dllPanel.Size = new System.Drawing.Size(74, 59);
            this.dllPanel.TabIndex = 14;
            // 
            // threadPanel
            // 
            this.threadPanel.Controls.Add(this.thread16);
            this.threadPanel.Controls.Add(this.thread64);
            this.threadPanel.Controls.Add(this.thread32);
            this.threadPanel.Controls.Add(this.thread8);
            this.threadPanel.Controls.Add(this.thread4);
            this.threadPanel.Controls.Add(this.thread2);
            this.threadPanel.Controls.Add(this.thread1);
            this.threadPanel.Location = new System.Drawing.Point(872, 81);
            this.threadPanel.Name = "threadPanel";
            this.threadPanel.Size = new System.Drawing.Size(69, 211);
            this.threadPanel.TabIndex = 15;
            // 
            // thread16
            // 
            this.thread16.AutoSize = true;
            this.thread16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.thread16.Location = new System.Drawing.Point(4, 124);
            this.thread16.Name = "thread16";
            this.thread16.Size = new System.Drawing.Size(45, 24);
            this.thread16.TabIndex = 4;
            this.thread16.TabStop = true;
            this.thread16.Tag = "16";
            this.thread16.Text = "16";
            this.thread16.UseVisualStyleBackColor = true;
            // 
            // thread64
            // 
            this.thread64.AutoSize = true;
            this.thread64.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.thread64.Location = new System.Drawing.Point(3, 184);
            this.thread64.Name = "thread64";
            this.thread64.Size = new System.Drawing.Size(45, 24);
            this.thread64.TabIndex = 6;
            this.thread64.TabStop = true;
            this.thread64.Tag = "64";
            this.thread64.Text = "64";
            this.thread64.UseVisualStyleBackColor = true;
            // 
            // thread32
            // 
            this.thread32.AutoSize = true;
            this.thread32.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.thread32.Location = new System.Drawing.Point(4, 154);
            this.thread32.Name = "thread32";
            this.thread32.Size = new System.Drawing.Size(45, 24);
            this.thread32.TabIndex = 5;
            this.thread32.TabStop = true;
            this.thread32.Tag = "32";
            this.thread32.Text = "32";
            this.thread32.UseVisualStyleBackColor = true;
            // 
            // thread8
            // 
            this.thread8.AutoSize = true;
            this.thread8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.thread8.Location = new System.Drawing.Point(4, 94);
            this.thread8.Name = "thread8";
            this.thread8.Size = new System.Drawing.Size(36, 24);
            this.thread8.TabIndex = 3;
            this.thread8.TabStop = true;
            this.thread8.Tag = "8";
            this.thread8.Text = "8";
            this.thread8.UseVisualStyleBackColor = true;
            // 
            // thread4
            // 
            this.thread4.AutoSize = true;
            this.thread4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.thread4.Location = new System.Drawing.Point(3, 64);
            this.thread4.Name = "thread4";
            this.thread4.Size = new System.Drawing.Size(36, 24);
            this.thread4.TabIndex = 2;
            this.thread4.TabStop = true;
            this.thread4.Tag = "4";
            this.thread4.Text = "4";
            this.thread4.UseVisualStyleBackColor = true;
            // 
            // thread2
            // 
            this.thread2.AutoSize = true;
            this.thread2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.thread2.Location = new System.Drawing.Point(4, 34);
            this.thread2.Name = "thread2";
            this.thread2.Size = new System.Drawing.Size(36, 24);
            this.thread2.TabIndex = 1;
            this.thread2.TabStop = true;
            this.thread2.Tag = "2";
            this.thread2.Text = "2";
            this.thread2.UseVisualStyleBackColor = true;
            // 
            // thread1
            // 
            this.thread1.AutoSize = true;
            this.thread1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.thread1.Location = new System.Drawing.Point(4, 4);
            this.thread1.Name = "thread1";
            this.thread1.Size = new System.Drawing.Size(36, 24);
            this.thread1.TabIndex = 0;
            this.thread1.TabStop = true;
            this.thread1.Tag = "1";
            this.thread1.Text = "1";
            this.thread1.UseVisualStyleBackColor = true;
            // 
            // beforePicture
            // 
            this.beforePicture.Location = new System.Drawing.Point(12, 9);
            this.beforePicture.Name = "beforePicture";
            this.beforePicture.Size = new System.Drawing.Size(800, 500);
            this.beforePicture.TabIndex = 16;
            this.beforePicture.TabStop = false;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(980, 632);
            this.Controls.Add(this.beforePicture);
            this.Controls.Add(this.threadPanel);
            this.Controls.Add(this.dllPanel);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.removeGreenScreenButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.Text = "GreenScreenRemover";
            this.dllPanel.ResumeLayout(false);
            this.dllPanel.PerformLayout();
            this.threadPanel.ResumeLayout(false);
            this.threadPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beforePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button removeGreenScreenButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.RadioButton cButton;
        private System.Windows.Forms.RadioButton asmButton;
        private System.Windows.Forms.Panel dllPanel;
        private System.Windows.Forms.Panel threadPanel;
        private System.Windows.Forms.RadioButton thread2;
        private System.Windows.Forms.RadioButton thread1;
        private System.Windows.Forms.RadioButton thread64;
        private System.Windows.Forms.RadioButton thread32;
        private System.Windows.Forms.RadioButton thread8;
        private System.Windows.Forms.RadioButton thread4;
        private System.Windows.Forms.RadioButton thread16;
        private System.Windows.Forms.PictureBox beforePicture;
    }
}

