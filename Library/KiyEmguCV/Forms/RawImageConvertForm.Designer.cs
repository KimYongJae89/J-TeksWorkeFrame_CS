namespace KiyEmguCV.Forms
{
    partial class RawImageConvertForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radBtn24BC = new System.Windows.Forms.RadioButton();
            this.radBtn16BG = new System.Windows.Forms.RadioButton();
            this.radBtn8BG = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nmrImgHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nmrImgWidth = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbCalcFileSize = new System.Windows.Forms.Label();
            this.lbSrcFileSize = new System.Windows.Forms.Label();
            this.lbSrcFilePath = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrImgHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrImgWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(300, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(300, 40);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radBtn24BC);
            this.groupBox1.Controls.Add(this.radBtn16BG);
            this.groupBox1.Controls.Add(this.radBtn8BG);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(110, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pixel Depth";
            // 
            // radBtn24BC
            // 
            this.radBtn24BC.AutoSize = true;
            this.radBtn24BC.Location = new System.Drawing.Point(6, 71);
            this.radBtn24BC.Name = "radBtn24BC";
            this.radBtn24BC.Size = new System.Drawing.Size(94, 16);
            this.radBtn24BC.TabIndex = 2;
            this.radBtn24BC.TabStop = true;
            this.radBtn24BC.Text = "24-Bit (RGB)";
            this.radBtn24BC.UseVisualStyleBackColor = true;
            this.radBtn24BC.CheckedChanged += new System.EventHandler(this.radBtn_CheckedChanged);
            // 
            // radBtn16BG
            // 
            this.radBtn16BG.AutoSize = true;
            this.radBtn16BG.Location = new System.Drawing.Point(6, 47);
            this.radBtn16BG.Name = "radBtn16BG";
            this.radBtn16BG.Size = new System.Drawing.Size(55, 16);
            this.radBtn16BG.TabIndex = 1;
            this.radBtn16BG.TabStop = true;
            this.radBtn16BG.Text = "16-Bit";
            this.radBtn16BG.UseVisualStyleBackColor = true;
            this.radBtn16BG.CheckedChanged += new System.EventHandler(this.radBtn_CheckedChanged);
            // 
            // radBtn8BG
            // 
            this.radBtn8BG.AutoSize = true;
            this.radBtn8BG.Checked = true;
            this.radBtn8BG.Location = new System.Drawing.Point(6, 23);
            this.radBtn8BG.Name = "radBtn8BG";
            this.radBtn8BG.Size = new System.Drawing.Size(49, 16);
            this.radBtn8BG.TabIndex = 0;
            this.radBtn8BG.TabStop = true;
            this.radBtn8BG.Text = "8-Bit";
            this.radBtn8BG.UseVisualStyleBackColor = true;
            this.radBtn8BG.CheckedChanged += new System.EventHandler(this.radBtn_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.nmrImgHeight);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.nmrImgWidth);
            this.groupBox2.Location = new System.Drawing.Point(125, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Image Characteristics";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Image Height:";
            // 
            // nmrImgHeight
            // 
            this.nmrImgHeight.Location = new System.Drawing.Point(95, 52);
            this.nmrImgHeight.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nmrImgHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrImgHeight.Name = "nmrImgHeight";
            this.nmrImgHeight.Size = new System.Drawing.Size(61, 21);
            this.nmrImgHeight.TabIndex = 1;
            this.nmrImgHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrImgHeight.ValueChanged += new System.EventHandler(this.nmrImgSize_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Image Width:";
            // 
            // nmrImgWidth
            // 
            this.nmrImgWidth.Location = new System.Drawing.Point(95, 23);
            this.nmrImgWidth.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nmrImgWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrImgWidth.Name = "nmrImgWidth";
            this.nmrImgWidth.Size = new System.Drawing.Size(61, 21);
            this.nmrImgWidth.TabIndex = 0;
            this.nmrImgWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrImgWidth.ValueChanged += new System.EventHandler(this.nmrImgSize_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Calculated File Size (Byte):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Source File Size (Byte):";
            // 
            // lbCalcFileSize
            // 
            this.lbCalcFileSize.AutoSize = true;
            this.lbCalcFileSize.Location = new System.Drawing.Point(176, 128);
            this.lbCalcFileSize.Name = "lbCalcFileSize";
            this.lbCalcFileSize.Size = new System.Drawing.Size(11, 12);
            this.lbCalcFileSize.TabIndex = 8;
            this.lbCalcFileSize.Text = "0";
            // 
            // lbSrcFileSize
            // 
            this.lbSrcFileSize.AutoSize = true;
            this.lbSrcFileSize.Location = new System.Drawing.Point(176, 169);
            this.lbSrcFileSize.Name = "lbSrcFileSize";
            this.lbSrcFileSize.Size = new System.Drawing.Size(11, 12);
            this.lbSrcFileSize.TabIndex = 9;
            this.lbSrcFileSize.Text = "0";
            // 
            // lbSrcFilePath
            // 
            this.lbSrcFilePath.AutoSize = true;
            this.lbSrcFilePath.Location = new System.Drawing.Point(10, 191);
            this.lbSrcFilePath.Name = "lbSrcFilePath";
            this.lbSrcFilePath.Size = new System.Drawing.Size(69, 12);
            this.lbSrcFilePath.TabIndex = 10;
            this.lbSrcFilePath.Text = "SrcFilePath";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.ForeColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(12, 153);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(281, 3);
            this.panel1.TabIndex = 11;
            // 
            // RawImageConvertForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 217);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbSrcFilePath);
            this.Controls.Add(this.lbSrcFileSize);
            this.Controls.Add(this.lbCalcFileSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RawImageConvertForm";
            this.Text = "Raw Image Information";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.RawImageConvertForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrImgHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrImgWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radBtn24BC;
        private System.Windows.Forms.RadioButton radBtn16BG;
        private System.Windows.Forms.RadioButton radBtn8BG;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nmrImgHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmrImgWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbCalcFileSize;
        private System.Windows.Forms.Label lbSrcFileSize;
        private System.Windows.Forms.Label lbSrcFilePath;
        private System.Windows.Forms.Panel panel1;
    }
}