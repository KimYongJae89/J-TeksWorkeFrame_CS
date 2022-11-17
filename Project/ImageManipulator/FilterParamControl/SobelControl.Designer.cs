namespace ImageManipulator.FilterParamControl
{
    partial class SobelControl
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
            this.tbxApertureSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxYorder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxXorder = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxApertureSize);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbxYorder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbxXorder);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 130);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Option";
            // 
            // tbxApertureSize
            // 
            this.tbxApertureSize.Location = new System.Drawing.Point(106, 88);
            this.tbxApertureSize.Name = "tbxApertureSize";
            this.tbxApertureSize.Size = new System.Drawing.Size(65, 21);
            this.tbxApertureSize.TabIndex = 5;
            this.tbxApertureSize.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbxApertureSize_KeyUp);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "ApertureSize :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Xorder :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxYorder
            // 
            this.tbxYorder.Location = new System.Drawing.Point(106, 58);
            this.tbxYorder.Name = "tbxYorder";
            this.tbxYorder.Size = new System.Drawing.Size(65, 21);
            this.tbxYorder.TabIndex = 3;
            this.tbxYorder.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbxYorder_KeyUp);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Yorder :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxXorder
            // 
            this.tbxXorder.Location = new System.Drawing.Point(106, 28);
            this.tbxXorder.Name = "tbxXorder";
            this.tbxXorder.Size = new System.Drawing.Size(65, 21);
            this.tbxXorder.TabIndex = 2;
            this.tbxXorder.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbxXorder_KeyUp);
            // 
            // SobelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "SobelControl";
            this.Size = new System.Drawing.Size(198, 130);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxYorder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxXorder;
        private System.Windows.Forms.TextBox tbxApertureSize;
        private System.Windows.Forms.Label label3;
    }
}
