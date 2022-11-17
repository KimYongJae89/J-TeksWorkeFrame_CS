namespace ImageManipulator.Forms
{
    partial class MetaInfoForm
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
        //private void InitializeComponent()
        //{
        //    this.components = new System.ComponentModel.Container();
        //    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        //    this.Text = "MetaInfoForm";
        //}

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MetaInfoForm));
            this.tabMeta = new System.Windows.Forms.TabControl();
            this.tabPgGeneral = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbFileSize_Byte = new System.Windows.Forms.Label();
            this.lbDataModifiedTime = new System.Windows.Forms.Label();
            this.lbFilePath = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPgFormat = new System.Windows.Forms.TabPage();
            this.lbFmt = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbCRSize = new System.Windows.Forms.Label();
            this.lbCRPixel = new System.Windows.Forms.Label();
            this.lbCRRes = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbPixel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbYRes = new System.Windows.Forms.Label();
            this.lbXRes = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPgImage = new System.Windows.Forms.TabPage();
            this.lbImgFmt = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbClrSpace = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.lbImgHeight = new System.Windows.Forms.Label();
            this.lbImgWidth = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPgSequenceInfo = new System.Windows.Forms.TabPage();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancle = new System.Windows.Forms.Button();
            this.btnTagView = new System.Windows.Forms.Button();
            this.tabMeta.SuspendLayout();
            this.tabPgGeneral.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPgFormat.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPgImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMeta
            // 
            this.tabMeta.Controls.Add(this.tabPgGeneral);
            this.tabMeta.Controls.Add(this.tabPgFormat);
            this.tabMeta.Controls.Add(this.tabPgImage);
            this.tabMeta.Controls.Add(this.tabPgSequenceInfo);
            this.tabMeta.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabMeta.Location = new System.Drawing.Point(0, 0);
            this.tabMeta.Name = "tabMeta";
            this.tabMeta.SelectedIndex = 0;
            this.tabMeta.Size = new System.Drawing.Size(474, 135);
            this.tabMeta.TabIndex = 0;
            // 
            // tabPgGeneral
            // 
            this.tabPgGeneral.Controls.Add(this.groupBox1);
            this.tabPgGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPgGeneral.Name = "tabPgGeneral";
            this.tabPgGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgGeneral.Size = new System.Drawing.Size(466, 109);
            this.tabPgGeneral.TabIndex = 0;
            this.tabPgGeneral.Text = "General";
            this.tabPgGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbFileSize_Byte);
            this.groupBox1.Controls.Add(this.lbDataModifiedTime);
            this.groupBox1.Controls.Add(this.lbFilePath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Infomation";
            // 
            // lbFileSize_Byte
            // 
            this.lbFileSize_Byte.AutoSize = true;
            this.lbFileSize_Byte.Location = new System.Drawing.Point(118, 61);
            this.lbFileSize_Byte.Name = "lbFileSize_Byte";
            this.lbFileSize_Byte.Size = new System.Drawing.Size(50, 12);
            this.lbFileSize_Byte.TabIndex = 5;
            this.lbFileSize_Byte.Text = "FileSize";
            // 
            // lbDataModifiedTime
            // 
            this.lbDataModifiedTime.AutoSize = true;
            this.lbDataModifiedTime.Location = new System.Drawing.Point(118, 40);
            this.lbDataModifiedTime.Name = "lbDataModifiedTime";
            this.lbDataModifiedTime.Size = new System.Drawing.Size(107, 12);
            this.lbDataModifiedTime.TabIndex = 4;
            this.lbDataModifiedTime.Text = "DataModifiedTime";
            // 
            // lbFilePath
            // 
            this.lbFilePath.AutoSize = true;
            this.lbFilePath.Location = new System.Drawing.Point(118, 19);
            this.lbFilePath.Name = "lbFilePath";
            this.lbFilePath.Size = new System.Drawing.Size(54, 12);
            this.lbFilePath.TabIndex = 3;
            this.lbFilePath.Text = "File Path";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "File Size (Byte):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Date Modified:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Path:";
            // 
            // tabPgFormat
            // 
            this.tabPgFormat.Controls.Add(this.lbFmt);
            this.tabPgFormat.Controls.Add(this.groupBox3);
            this.tabPgFormat.Controls.Add(this.groupBox2);
            this.tabPgFormat.Controls.Add(this.label4);
            this.tabPgFormat.Location = new System.Drawing.Point(4, 22);
            this.tabPgFormat.Name = "tabPgFormat";
            this.tabPgFormat.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgFormat.Size = new System.Drawing.Size(466, 109);
            this.tabPgFormat.TabIndex = 1;
            this.tabPgFormat.Text = "Format";
            this.tabPgFormat.UseVisualStyleBackColor = true;
            // 
            // lbFmt
            // 
            this.lbFmt.AutoSize = true;
            this.lbFmt.Location = new System.Drawing.Point(68, 8);
            this.lbFmt.Name = "lbFmt";
            this.lbFmt.Size = new System.Drawing.Size(40, 12);
            this.lbFmt.TabIndex = 8;
            this.lbFmt.Text = "format";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbCRSize);
            this.groupBox3.Controls.Add(this.lbCRPixel);
            this.groupBox3.Controls.Add(this.lbCRRes);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.lbPixel);
            this.groupBox3.Location = new System.Drawing.Point(235, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(220, 74);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Current Resolution";
            // 
            // lbCRSize
            // 
            this.lbCRSize.AutoSize = true;
            this.lbCRSize.Location = new System.Drawing.Point(117, 54);
            this.lbCRSize.Name = "lbCRSize";
            this.lbCRSize.Size = new System.Drawing.Size(11, 12);
            this.lbCRSize.TabIndex = 10;
            this.lbCRSize.Text = "0";
            // 
            // lbCRPixel
            // 
            this.lbCRPixel.AutoSize = true;
            this.lbCRPixel.Location = new System.Drawing.Point(117, 36);
            this.lbCRPixel.Name = "lbCRPixel";
            this.lbCRPixel.Size = new System.Drawing.Size(11, 12);
            this.lbCRPixel.TabIndex = 9;
            this.lbCRPixel.Text = "0";
            // 
            // lbCRRes
            // 
            this.lbCRRes.AutoSize = true;
            this.lbCRRes.Location = new System.Drawing.Point(117, 18);
            this.lbCRRes.Name = "lbCRRes";
            this.lbCRRes.Size = new System.Drawing.Size(11, 12);
            this.lbCRRes.TabIndex = 8;
            this.lbCRRes.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "Size:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "Resolution:";
            // 
            // lbPixel
            // 
            this.lbPixel.AutoSize = true;
            this.lbPixel.Location = new System.Drawing.Point(8, 36);
            this.lbPixel.Name = "lbPixel";
            this.lbPixel.Size = new System.Drawing.Size(37, 12);
            this.lbPixel.TabIndex = 7;
            this.lbPixel.Text = "Pixel:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbYRes);
            this.groupBox2.Controls.Add(this.lbXRes);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(9, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 74);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reported Resolution";
            // 
            // lbYRes
            // 
            this.lbYRes.AutoSize = true;
            this.lbYRes.Location = new System.Drawing.Point(59, 36);
            this.lbYRes.Name = "lbYRes";
            this.lbYRes.Size = new System.Drawing.Size(35, 12);
            this.lbYRes.TabIndex = 7;
            this.lbYRes.Text = "YRes";
            // 
            // lbXRes
            // 
            this.lbXRes.AutoSize = true;
            this.lbXRes.Location = new System.Drawing.Point(59, 18);
            this.lbXRes.Name = "lbXRes";
            this.lbXRes.Size = new System.Drawing.Size(35, 12);
            this.lbXRes.TabIndex = 6;
            this.lbXRes.Text = "XRes";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "Y-Res:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "X-Res:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "Format:";
            // 
            // tabPgImage
            // 
            this.tabPgImage.Controls.Add(this.lbImgFmt);
            this.tabPgImage.Controls.Add(this.label14);
            this.tabPgImage.Controls.Add(this.lbClrSpace);
            this.tabPgImage.Controls.Add(this.lbType);
            this.tabPgImage.Controls.Add(this.lbImgHeight);
            this.tabPgImage.Controls.Add(this.lbImgWidth);
            this.tabPgImage.Controls.Add(this.label13);
            this.tabPgImage.Controls.Add(this.label12);
            this.tabPgImage.Controls.Add(this.label11);
            this.tabPgImage.Controls.Add(this.label10);
            this.tabPgImage.Location = new System.Drawing.Point(4, 22);
            this.tabPgImage.Name = "tabPgImage";
            this.tabPgImage.Size = new System.Drawing.Size(466, 109);
            this.tabPgImage.TabIndex = 2;
            this.tabPgImage.Text = "Image";
            this.tabPgImage.UseVisualStyleBackColor = true;
            // 
            // lbImgFmt
            // 
            this.lbImgFmt.AutoSize = true;
            this.lbImgFmt.Location = new System.Drawing.Point(68, 9);
            this.lbImgFmt.Name = "lbImgFmt";
            this.lbImgFmt.Size = new System.Drawing.Size(40, 12);
            this.lbImgFmt.TabIndex = 17;
            this.lbImgFmt.Text = "format";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 12);
            this.label14.TabIndex = 16;
            this.label14.Text = "Format:";
            // 
            // lbClrSpace
            // 
            this.lbClrSpace.AutoSize = true;
            this.lbClrSpace.Location = new System.Drawing.Point(296, 34);
            this.lbClrSpace.Name = "lbClrSpace";
            this.lbClrSpace.Size = new System.Drawing.Size(21, 12);
            this.lbClrSpace.TabIndex = 15;
            this.lbClrSpace.Text = "Clr";
            // 
            // lbType
            // 
            this.lbType.AutoSize = true;
            this.lbType.Location = new System.Drawing.Point(68, 34);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(13, 12);
            this.lbType.TabIndex = 14;
            this.lbType.Text = "T";
            // 
            // lbImgHeight
            // 
            this.lbImgHeight.AutoSize = true;
            this.lbImgHeight.Location = new System.Drawing.Point(68, 74);
            this.lbImgHeight.Name = "lbImgHeight";
            this.lbImgHeight.Size = new System.Drawing.Size(13, 12);
            this.lbImgHeight.TabIndex = 13;
            this.lbImgHeight.Text = "H";
            // 
            // lbImgWidth
            // 
            this.lbImgWidth.AutoSize = true;
            this.lbImgWidth.Location = new System.Drawing.Point(68, 54);
            this.lbImgWidth.Name = "lbImgWidth";
            this.lbImgWidth.Size = new System.Drawing.Size(15, 12);
            this.lbImgWidth.TabIndex = 12;
            this.lbImgWidth.Text = "W";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(211, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 12);
            this.label13.TabIndex = 11;
            this.label13.Text = "Color Space:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 74);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 12);
            this.label12.TabIndex = 10;
            this.label12.Text = "Height:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 12);
            this.label11.TabIndex = 9;
            this.label11.Text = "Width:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "Type:";
            // 
            // tabPgSequenceInfo
            // 
            this.tabPgSequenceInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPgSequenceInfo.Name = "tabPgSequenceInfo";
            this.tabPgSequenceInfo.Size = new System.Drawing.Size(466, 109);
            this.tabPgSequenceInfo.TabIndex = 3;
            this.tabPgSequenceInfo.Text = "Sequence Info";
            this.tabPgSequenceInfo.UseVisualStyleBackColor = true;
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(316, 139);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 1;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancle
            // 
            this.btn_Cancle.Location = new System.Drawing.Point(394, 139);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancle.TabIndex = 2;
            this.btn_Cancle.Text = "Cancle";
            this.btn_Cancle.UseVisualStyleBackColor = true;
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // btnTagView
            // 
            this.btnTagView.AutoSize = true;
            this.btnTagView.Location = new System.Drawing.Point(4, 139);
            this.btnTagView.Name = "btnTagView";
            this.btnTagView.Size = new System.Drawing.Size(75, 23);
            this.btnTagView.TabIndex = 3;
            this.btnTagView.Text = "Tag View";
            this.btnTagView.UseVisualStyleBackColor = true;
            this.btnTagView.Click += new System.EventHandler(this.btnTagView_Click);
            // 
            // MetaInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 167);
            this.Controls.Add(this.btnTagView);
            this.Controls.Add(this.btn_Cancle);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.tabMeta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MetaInfoForm";
            this.Text = "Image Properties";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MetaInfoForm_Load);
            this.tabMeta.ResumeLayout(false);
            this.tabPgGeneral.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPgFormat.ResumeLayout(false);
            this.tabPgFormat.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPgImage.ResumeLayout(false);
            this.tabPgImage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.TabControl tabMeta;
        private System.Windows.Forms.TabPage tabPgGeneral;
        private System.Windows.Forms.TabPage tabPgFormat;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancle;
        private System.Windows.Forms.TabPage tabPgImage;
        private System.Windows.Forms.TabPage tabPgSequenceInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbPixel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbFileSize_Byte;
        private System.Windows.Forms.Label lbDataModifiedTime;
        private System.Windows.Forms.Label lbFilePath;
        private System.Windows.Forms.Label lbCRSize;
        private System.Windows.Forms.Label lbCRPixel;
        private System.Windows.Forms.Label lbCRRes;
        private System.Windows.Forms.Label lbYRes;
        private System.Windows.Forms.Label lbXRes;
        private System.Windows.Forms.Label lbClrSpace;
        private System.Windows.Forms.Label lbType;
        private System.Windows.Forms.Label lbImgHeight;
        private System.Windows.Forms.Label lbImgWidth;
        private System.Windows.Forms.Label lbFmt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbImgFmt;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnTagView;
    }
}