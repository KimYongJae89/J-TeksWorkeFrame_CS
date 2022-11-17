namespace XManager.Forms
{
    partial class BootUp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BootUp));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_TCPConnect = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbl_DarkCalibration = new System.Windows.Forms.Label();
            this.lbl_PU_Open = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_CreateProcessChain = new System.Windows.Forms.Label();
            this.lbl_ResumeCalibration = new System.Windows.Forms.Label();
            this.lbl_LoadReferences = new System.Windows.Forms.Label();
            this.lblLoadingText = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Wait.png");
            this.imageList.Images.SetKeyName(1, "OK.png");
            this.imageList.Images.SetKeyName(2, "Error.png");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("HY중고딕", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(421, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 19);
            this.label6.TabIndex = 9;
            this.label6.Text = "Loading...";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 30);
            this.label8.TabIndex = 12;
            this.label8.Text = "Start Acquisition...";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(3, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 30);
            this.label4.TabIndex = 10;
            this.label4.Text = "Load References....";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 30);
            this.label7.TabIndex = 11;
            this.label7.Text = "Resume Calibration...";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.tableLayoutPanel2.Controls.Add(this.lbl_TCPConnect, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbl_DarkCalibration, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbl_PU_Open, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label12, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(574, 9);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(183, 130);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // lbl_TCPConnect
            // 
            this.lbl_TCPConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TCPConnect.ImageIndex = 0;
            this.lbl_TCPConnect.ImageList = this.imageList;
            this.lbl_TCPConnect.Location = new System.Drawing.Point(155, 0);
            this.lbl_TCPConnect.Name = "lbl_TCPConnect";
            this.lbl_TCPConnect.Size = new System.Drawing.Size(25, 30);
            this.lbl_TCPConnect.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(146, 30);
            this.label11.TabIndex = 0;
            this.label11.Text = "TCP IP Connect....";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.Location = new System.Drawing.Point(3, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(146, 30);
            this.label13.TabIndex = 1;
            this.label13.Text = "Process Unit Open....";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_DarkCalibration
            // 
            this.lbl_DarkCalibration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_DarkCalibration.ImageIndex = 0;
            this.lbl_DarkCalibration.ImageList = this.imageList;
            this.lbl_DarkCalibration.Location = new System.Drawing.Point(155, 60);
            this.lbl_DarkCalibration.Name = "lbl_DarkCalibration";
            this.lbl_DarkCalibration.Size = new System.Drawing.Size(25, 30);
            this.lbl_DarkCalibration.TabIndex = 7;
            // 
            // lbl_PU_Open
            // 
            this.lbl_PU_Open.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_PU_Open.ImageIndex = 0;
            this.lbl_PU_Open.ImageList = this.imageList;
            this.lbl_PU_Open.Location = new System.Drawing.Point(155, 30);
            this.lbl_PU_Open.Name = "lbl_PU_Open";
            this.lbl_PU_Open.Size = new System.Drawing.Size(25, 30);
            this.lbl_PU_Open.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.Location = new System.Drawing.Point(3, 60);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 30);
            this.label12.TabIndex = 3;
            this.label12.Text = "Dark Calibration....";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lbl_CreateProcessChain, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.lbl_ResumeCalibration, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_LoadReferences, 1, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(574, 145);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(183, 130);
            this.tableLayoutPanel3.TabIndex = 11;
            // 
            // lbl_CreateProcessChain
            // 
            this.lbl_CreateProcessChain.ImageIndex = 0;
            this.lbl_CreateProcessChain.ImageList = this.imageList;
            this.lbl_CreateProcessChain.Location = new System.Drawing.Point(155, 60);
            this.lbl_CreateProcessChain.Name = "lbl_CreateProcessChain";
            this.lbl_CreateProcessChain.Size = new System.Drawing.Size(21, 30);
            this.lbl_CreateProcessChain.TabIndex = 14;
            // 
            // lbl_ResumeCalibration
            // 
            this.lbl_ResumeCalibration.ImageIndex = 0;
            this.lbl_ResumeCalibration.ImageList = this.imageList;
            this.lbl_ResumeCalibration.Location = new System.Drawing.Point(155, 0);
            this.lbl_ResumeCalibration.Name = "lbl_ResumeCalibration";
            this.lbl_ResumeCalibration.Size = new System.Drawing.Size(21, 30);
            this.lbl_ResumeCalibration.TabIndex = 13;
            // 
            // lbl_LoadReferences
            // 
            this.lbl_LoadReferences.ImageIndex = 0;
            this.lbl_LoadReferences.ImageList = this.imageList;
            this.lbl_LoadReferences.Location = new System.Drawing.Point(155, 30);
            this.lbl_LoadReferences.Name = "lbl_LoadReferences";
            this.lbl_LoadReferences.Size = new System.Drawing.Size(21, 30);
            this.lbl_LoadReferences.TabIndex = 8;
            // 
            // lblLoadingText
            // 
            this.lblLoadingText.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblLoadingText.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblLoadingText.ForeColor = System.Drawing.Color.Black;
            this.lblLoadingText.Location = new System.Drawing.Point(12, 178);
            this.lblLoadingText.Name = "lblLoadingText";
            this.lblLoadingText.Size = new System.Drawing.Size(251, 23);
            this.lblLoadingText.TabIndex = 12;
            this.lblLoadingText.Text = ".";
            this.lblLoadingText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(16, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(520, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // BootUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(555, 210);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblLoadingText);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BootUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XManager BootUp";
            this.Load += new System.EventHandler(this.UroBootUp_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_TCPConnect;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label lbl_PU_Open;
        private System.Windows.Forms.Label lbl_DarkCalibration;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_LoadReferences;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_ResumeCalibration;
        private System.Windows.Forms.Label lbl_CreateProcessChain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblLoadingText;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}