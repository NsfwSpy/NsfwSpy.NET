
namespace NsfwSpyNS.App
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtFileLocation = new System.Windows.Forms.TextBox();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblScore1 = new System.Windows.Forms.Label();
            this.lblScore2 = new System.Windows.Forms.Label();
            this.lblScore3 = new System.Windows.Forms.Label();
            this.lblScore4 = new System.Windows.Forms.Label();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.lblValue2 = new System.Windows.Forms.Label();
            this.lblValue3 = new System.Windows.Forms.Label();
            this.lblValue4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 105);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(406, 304);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.btnOpenFile.Location = new System.Drawing.Point(328, 76);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 1;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtFileLocation
            // 
            this.txtFileLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileLocation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtFileLocation.Location = new System.Drawing.Point(26, 76);
            this.txtFileLocation.Name = "txtFileLocation";
            this.txtFileLocation.PlaceholderText = "Enter a directory or url";
            this.txtFileLocation.Size = new System.Drawing.Size(296, 23);
            this.txtFileLocation.TabIndex = 2;
            this.txtFileLocation.TextChanged += new System.EventHandler(this.txtFileLocation_TextChangedAsync);
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.InitialImage = null;
            this.picLogo.Location = new System.Drawing.Point(157, 12);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(115, 58);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 3;
            this.picLogo.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog";
            this.openFileDialog1.Filter = "Files|*.jpg;*.jpeg;*.png;";
            // 
            // lblScore1
            // 
            this.lblScore1.AutoSize = true;
            this.lblScore1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblScore1.Location = new System.Drawing.Point(83, 431);
            this.lblScore1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblScore1.Name = "lblScore1";
            this.lblScore1.Size = new System.Drawing.Size(62, 20);
            this.lblScore1.TabIndex = 5;
            this.lblScore1.Text = "Neutral";
            // 
            // lblScore2
            // 
            this.lblScore2.AutoSize = true;
            this.lblScore2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblScore2.Location = new System.Drawing.Point(83, 461);
            this.lblScore2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblScore2.Name = "lblScore2";
            this.lblScore2.Size = new System.Drawing.Size(100, 20);
            this.lblScore2.TabIndex = 6;
            this.lblScore2.Text = "Pornography";
            // 
            // lblScore3
            // 
            this.lblScore3.AutoSize = true;
            this.lblScore3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblScore3.Location = new System.Drawing.Point(83, 491);
            this.lblScore3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblScore3.Name = "lblScore3";
            this.lblScore3.Size = new System.Drawing.Size(41, 20);
            this.lblScore3.TabIndex = 7;
            this.lblScore3.Text = "Sexy";
            // 
            // lblScore4
            // 
            this.lblScore4.AutoSize = true;
            this.lblScore4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblScore4.Location = new System.Drawing.Point(83, 521);
            this.lblScore4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblScore4.Name = "lblScore4";
            this.lblScore4.Size = new System.Drawing.Size(55, 20);
            this.lblScore4.TabIndex = 8;
            this.lblScore4.Text = "Hentai";
            // 
            // lblValue1
            // 
            this.lblValue1.AutoSize = true;
            this.lblValue1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValue1.Location = new System.Drawing.Point(231, 431);
            this.lblValue1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(0, 20);
            this.lblValue1.TabIndex = 9;
            // 
            // lblValue2
            // 
            this.lblValue2.AutoSize = true;
            this.lblValue2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValue2.Location = new System.Drawing.Point(231, 461);
            this.lblValue2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblValue2.Name = "lblValue2";
            this.lblValue2.Size = new System.Drawing.Size(0, 20);
            this.lblValue2.TabIndex = 10;
            // 
            // lblValue3
            // 
            this.lblValue3.AutoSize = true;
            this.lblValue3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValue3.Location = new System.Drawing.Point(231, 491);
            this.lblValue3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblValue3.Name = "lblValue3";
            this.lblValue3.Size = new System.Drawing.Size(0, 20);
            this.lblValue3.TabIndex = 11;
            // 
            // lblValue4
            // 
            this.lblValue4.AutoSize = true;
            this.lblValue4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValue4.Location = new System.Drawing.Point(231, 521);
            this.lblValue4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblValue4.Name = "lblValue4";
            this.lblValue4.Size = new System.Drawing.Size(0, 20);
            this.lblValue4.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(430, 568);
            this.Controls.Add(this.lblValue4);
            this.Controls.Add(this.lblValue3);
            this.Controls.Add(this.lblValue2);
            this.Controls.Add(this.lblValue1);
            this.Controls.Add(this.lblScore4);
            this.Controls.Add(this.lblScore3);
            this.Controls.Add(this.lblScore2);
            this.Controls.Add(this.lblScore1);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.txtFileLocation);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "NsfwSpy";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txtFileLocation;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblScore1;
        private System.Windows.Forms.Label lblScore2;
        private System.Windows.Forms.Label lblScore3;
        private System.Windows.Forms.Label lblScore4;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.Label lblValue2;
        private System.Windows.Forms.Label lblValue3;
        private System.Windows.Forms.Label lblValue4;
    }
}

