namespace XNPI.Controls
{
    partial class Tool
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbManual = new System.Windows.Forms.Label();
            this.btnImage = new System.Windows.Forms.Button();
            this.btnAVI = new System.Windows.Forms.Button();
            this.lbDuration = new System.Windows.Forms.Label();
            this.nmrDuration = new System.Windows.Forms.NumericUpDown();
            this.lbMinute = new System.Windows.Forms.Label();
            this.lbAvg = new System.Windows.Forms.Label();
            this.nmrAvg = new System.Windows.Forms.NumericUpDown();
            this.btnAvgApply = new System.Windows.Forms.Button();
            this.lbAvgStatus = new System.Windows.Forms.Label();
            this.lbRawImg = new System.Windows.Forms.Label();
            this.btnTiffSave = new System.Windows.Forms.Button();
            this.btnLoadImg = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nmrDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrAvg)).BeginInit();
            this.SuspendLayout();
            // 
            // lbManual
            // 
            this.lbManual.AutoSize = true;
            this.lbManual.Location = new System.Drawing.Point(37, 25);
            this.lbManual.Name = "lbManual";
            this.lbManual.Size = new System.Drawing.Size(47, 12);
            this.lbManual.TabIndex = 0;
            this.lbManual.Text = "Manual";
            // 
            // btnImage
            // 
            this.btnImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImage.Location = new System.Drawing.Point(19, 45);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(82, 25);
            this.btnImage.TabIndex = 1;
            this.btnImage.Text = "Image";
            this.btnImage.UseVisualStyleBackColor = true;
            // 
            // btnAVI
            // 
            this.btnAVI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAVI.Location = new System.Drawing.Point(19, 76);
            this.btnAVI.Name = "btnAVI";
            this.btnAVI.Size = new System.Drawing.Size(82, 25);
            this.btnAVI.TabIndex = 2;
            this.btnAVI.Text = "AVI";
            this.btnAVI.UseVisualStyleBackColor = true;
            // 
            // lbDuration
            // 
            this.lbDuration.AutoSize = true;
            this.lbDuration.Location = new System.Drawing.Point(35, 112);
            this.lbDuration.Name = "lbDuration";
            this.lbDuration.Size = new System.Drawing.Size(51, 12);
            this.lbDuration.TabIndex = 3;
            this.lbDuration.Text = "Duration";
            // 
            // nmrDuration
            // 
            this.nmrDuration.Location = new System.Drawing.Point(20, 132);
            this.nmrDuration.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmrDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrDuration.Name = "nmrDuration";
            this.nmrDuration.Size = new System.Drawing.Size(54, 21);
            this.nmrDuration.TabIndex = 4;
            this.nmrDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmrDuration.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lbMinute
            // 
            this.lbMinute.AutoSize = true;
            this.lbMinute.Location = new System.Drawing.Point(77, 136);
            this.lbMinute.Name = "lbMinute";
            this.lbMinute.Size = new System.Drawing.Size(26, 12);
            this.lbMinute.TabIndex = 5;
            this.lbMinute.Text = "Min";
            // 
            // lbAvg
            // 
            this.lbAvg.AutoSize = true;
            this.lbAvg.Location = new System.Drawing.Point(33, 191);
            this.lbAvg.Name = "lbAvg";
            this.lbAvg.Size = new System.Drawing.Size(55, 12);
            this.lbAvg.TabIndex = 6;
            this.lbAvg.Text = "Average:";
            // 
            // nmrAvg
            // 
            this.nmrAvg.Location = new System.Drawing.Point(7, 211);
            this.nmrAvg.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmrAvg.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrAvg.Name = "nmrAvg";
            this.nmrAvg.Size = new System.Drawing.Size(54, 21);
            this.nmrAvg.TabIndex = 7;
            this.nmrAvg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmrAvg.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // btnAvgApply
            // 
            this.btnAvgApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAvgApply.Location = new System.Drawing.Point(63, 209);
            this.btnAvgApply.Name = "btnAvgApply";
            this.btnAvgApply.Size = new System.Drawing.Size(50, 25);
            this.btnAvgApply.TabIndex = 8;
            this.btnAvgApply.Text = "Apply";
            this.btnAvgApply.UseVisualStyleBackColor = true;
            // 
            // lbAvgStatus
            // 
            this.lbAvgStatus.AutoSize = true;
            this.lbAvgStatus.Location = new System.Drawing.Point(32, 238);
            this.lbAvgStatus.Name = "lbAvgStatus";
            this.lbAvgStatus.Size = new System.Drawing.Size(56, 12);
            this.lbAvgStatus.TabIndex = 9;
            this.lbAvgStatus.Text = "Avg Proc";
            // 
            // lbRawImg
            // 
            this.lbRawImg.AutoSize = true;
            this.lbRawImg.Location = new System.Drawing.Point(25, 326);
            this.lbRawImg.Name = "lbRawImg";
            this.lbRawImg.Size = new System.Drawing.Size(70, 12);
            this.lbRawImg.TabIndex = 10;
            this.lbRawImg.Text = "RAW Image";
            // 
            // btnTiffSave
            // 
            this.btnTiffSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTiffSave.Location = new System.Drawing.Point(19, 346);
            this.btnTiffSave.Name = "btnTiffSave";
            this.btnTiffSave.Size = new System.Drawing.Size(82, 25);
            this.btnTiffSave.TabIndex = 11;
            this.btnTiffSave.Text = "TIFF Save";
            this.btnTiffSave.UseVisualStyleBackColor = true;
            // 
            // btnLoadImg
            // 
            this.btnLoadImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadImg.Location = new System.Drawing.Point(19, 377);
            this.btnLoadImg.Name = "btnLoadImg";
            this.btnLoadImg.Size = new System.Drawing.Size(82, 25);
            this.btnLoadImg.TabIndex = 12;
            this.btnLoadImg.Text = "Load Image";
            this.btnLoadImg.UseVisualStyleBackColor = true;
            // 
            // Tool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLoadImg);
            this.Controls.Add(this.btnTiffSave);
            this.Controls.Add(this.lbRawImg);
            this.Controls.Add(this.lbAvgStatus);
            this.Controls.Add(this.btnAvgApply);
            this.Controls.Add(this.nmrAvg);
            this.Controls.Add(this.lbAvg);
            this.Controls.Add(this.lbMinute);
            this.Controls.Add(this.nmrDuration);
            this.Controls.Add(this.lbDuration);
            this.Controls.Add(this.btnAVI);
            this.Controls.Add(this.btnImage);
            this.Controls.Add(this.lbManual);
            this.Name = "Tool";
            this.Size = new System.Drawing.Size(120, 425);
            this.Load += new System.EventHandler(this.Tool_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmrDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrAvg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbManual;
        private System.Windows.Forms.Button btnImage;
        private System.Windows.Forms.Button btnAVI;
        private System.Windows.Forms.Label lbDuration;
        private System.Windows.Forms.NumericUpDown nmrDuration;
        private System.Windows.Forms.Label lbMinute;
        private System.Windows.Forms.Label lbAvg;
        private System.Windows.Forms.NumericUpDown nmrAvg;
        private System.Windows.Forms.Button btnAvgApply;
        private System.Windows.Forms.Label lbAvgStatus;
        private System.Windows.Forms.Label lbRawImg;
        private System.Windows.Forms.Button btnTiffSave;
        private System.Windows.Forms.Button btnLoadImg;
    }
}
