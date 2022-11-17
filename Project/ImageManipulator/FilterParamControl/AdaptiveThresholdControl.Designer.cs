namespace ImageManipulator.FilterParamControl
{
    partial class AdaptiveThresholdControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nupdnWeight = new System.Windows.Forms.NumericUpDown();
            this.nupdnBlockSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupdnWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupdnBlockSize)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nupdnWeight);
            this.groupBox1.Controls.Add(this.nupdnBlockSize);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 207);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Option";
            // 
            // nupdnWeight
            // 
            this.nupdnWeight.Location = new System.Drawing.Point(94, 73);
            this.nupdnWeight.Name = "nupdnWeight";
            this.nupdnWeight.Size = new System.Drawing.Size(79, 21);
            this.nupdnWeight.TabIndex = 13;
            this.nupdnWeight.ValueChanged += new System.EventHandler(this.nupdnWeight_ValueChanged);
            // 
            // nupdnBlockSize
            // 
            this.nupdnBlockSize.Location = new System.Drawing.Point(94, 46);
            this.nupdnBlockSize.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nupdnBlockSize.Name = "nupdnBlockSize";
            this.nupdnBlockSize.Size = new System.Drawing.Size(79, 21);
            this.nupdnBlockSize.TabIndex = 12;
            this.nupdnBlockSize.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nupdnBlockSize.ValueChanged += new System.EventHandler(this.nupdnBlockSize_ValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 23);
            this.label3.TabIndex = 11;
            this.label3.Text = "Weight :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 23);
            this.label2.TabIndex = 10;
            this.label2.Text = "Block Size :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxType
            // 
            this.cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Location = new System.Drawing.Point(94, 20);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(79, 20);
            this.cbxType.TabIndex = 9;
            this.cbxType.SelectedIndexChanged += new System.EventHandler(this.cbxType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "Type :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AdaptiveThresholdControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "AdaptiveThresholdControl";
            this.Size = new System.Drawing.Size(242, 207);
            this.Load += new System.EventHandler(this.AdaptiveThresholdControl_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nupdnWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupdnBlockSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nupdnWeight;
        private System.Windows.Forms.NumericUpDown nupdnBlockSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.Label label1;
    }
}
