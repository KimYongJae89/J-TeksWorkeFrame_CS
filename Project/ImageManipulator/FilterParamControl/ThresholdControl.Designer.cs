namespace ImageManipulator.FilterParamControl
{
    partial class ThresholdControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbxOtsuEnable = new System.Windows.Forms.CheckBox();
            this.nupdnValue = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupdnValue)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxOtsuEnable);
            this.groupBox1.Controls.Add(this.nupdnValue);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 239);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Option";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Value :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxOtsuEnable
            // 
            this.cbxOtsuEnable.AutoSize = true;
            this.cbxOtsuEnable.Location = new System.Drawing.Point(15, 61);
            this.cbxOtsuEnable.Name = "cbxOtsuEnable";
            this.cbxOtsuEnable.Size = new System.Drawing.Size(49, 16);
            this.cbxOtsuEnable.TabIndex = 3;
            this.cbxOtsuEnable.Text = "Auto";
            this.cbxOtsuEnable.UseVisualStyleBackColor = true;
            this.cbxOtsuEnable.CheckedChanged += new System.EventHandler(this.cbxOtsuEnable_CheckedChanged);
            // 
            // nupdnValue
            // 
            this.nupdnValue.Location = new System.Drawing.Point(74, 26);
            this.nupdnValue.Name = "nupdnValue";
            this.nupdnValue.Size = new System.Drawing.Size(91, 21);
            this.nupdnValue.TabIndex = 2;
            this.nupdnValue.ValueChanged += new System.EventHandler(this.nupdnValue_ValueChanged);
            this.nupdnValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nupdnValue_KeyUp);
            // 
            // ThresholdControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ThresholdControl";
            this.Size = new System.Drawing.Size(249, 239);
            this.Load += new System.EventHandler(this.ThresholdControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupdnValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbxOtsuEnable;
        private System.Windows.Forms.NumericUpDown nupdnValue;
    }
}
