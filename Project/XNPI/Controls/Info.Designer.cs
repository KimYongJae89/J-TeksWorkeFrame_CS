namespace XNPI.Controls
{
    partial class Info
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
            this.grbx = new System.Windows.Forms.GroupBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.cbxDistance = new System.Windows.Forms.CheckBox();
            this.cbxSection = new System.Windows.Forms.CheckBox();
            this.cbxTime = new System.Windows.Forms.CheckBox();
            this.cbxInfo = new System.Windows.Forms.CheckBox();
            this.lbPipeNo = new System.Windows.Forms.Label();
            this.tbxPipeNo = new System.Windows.Forms.TextBox();
            this.tbxLine2 = new System.Windows.Forms.TextBox();
            this.tbxLine1 = new System.Windows.Forms.TextBox();
            this.lbLine2 = new System.Windows.Forms.Label();
            this.lbLine1 = new System.Windows.Forms.Label();
            this.grbx.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbx
            // 
            this.grbx.Controls.Add(this.btnApply);
            this.grbx.Controls.Add(this.cbxDistance);
            this.grbx.Controls.Add(this.cbxSection);
            this.grbx.Controls.Add(this.cbxTime);
            this.grbx.Controls.Add(this.cbxInfo);
            this.grbx.Controls.Add(this.lbPipeNo);
            this.grbx.Controls.Add(this.tbxPipeNo);
            this.grbx.Controls.Add(this.tbxLine2);
            this.grbx.Controls.Add(this.tbxLine1);
            this.grbx.Controls.Add(this.lbLine2);
            this.grbx.Controls.Add(this.lbLine1);
            this.grbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbx.Location = new System.Drawing.Point(0, 0);
            this.grbx.Name = "grbx";
            this.grbx.Size = new System.Drawing.Size(610, 140);
            this.grbx.TabIndex = 0;
            this.grbx.TabStop = false;
            this.grbx.Text = "Info";
            // 
            // btnApply
            // 
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Location = new System.Drawing.Point(487, 105);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 25);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // cbxDistance
            // 
            this.cbxDistance.AutoSize = true;
            this.cbxDistance.Location = new System.Drawing.Point(493, 78);
            this.cbxDistance.Name = "cbxDistance";
            this.cbxDistance.Size = new System.Drawing.Size(73, 16);
            this.cbxDistance.TabIndex = 3;
            this.cbxDistance.Text = "Distance";
            this.cbxDistance.UseVisualStyleBackColor = true;
            // 
            // cbxSection
            // 
            this.cbxSection.AutoSize = true;
            this.cbxSection.Location = new System.Drawing.Point(421, 78);
            this.cbxSection.Name = "cbxSection";
            this.cbxSection.Size = new System.Drawing.Size(66, 16);
            this.cbxSection.TabIndex = 3;
            this.cbxSection.Text = "Section";
            this.cbxSection.UseVisualStyleBackColor = true;
            // 
            // cbxTime
            // 
            this.cbxTime.AutoSize = true;
            this.cbxTime.Location = new System.Drawing.Point(301, 78);
            this.cbxTime.Name = "cbxTime";
            this.cbxTime.Size = new System.Drawing.Size(53, 16);
            this.cbxTime.TabIndex = 3;
            this.cbxTime.Text = "Time";
            this.cbxTime.UseVisualStyleBackColor = true;
            // 
            // cbxInfo
            // 
            this.cbxInfo.AutoSize = true;
            this.cbxInfo.Location = new System.Drawing.Point(251, 78);
            this.cbxInfo.Name = "cbxInfo";
            this.cbxInfo.Size = new System.Drawing.Size(44, 16);
            this.cbxInfo.TabIndex = 3;
            this.cbxInfo.Text = "Info";
            this.cbxInfo.UseVisualStyleBackColor = true;
            // 
            // lbPipeNo
            // 
            this.lbPipeNo.AutoSize = true;
            this.lbPipeNo.Location = new System.Drawing.Point(20, 80);
            this.lbPipeNo.Name = "lbPipeNo";
            this.lbPipeNo.Size = new System.Drawing.Size(54, 12);
            this.lbPipeNo.TabIndex = 2;
            this.lbPipeNo.Text = "Pipe No:";
            // 
            // tbxPipeNo
            // 
            this.tbxPipeNo.Location = new System.Drawing.Point(80, 76);
            this.tbxPipeNo.Name = "tbxPipeNo";
            this.tbxPipeNo.Size = new System.Drawing.Size(150, 21);
            this.tbxPipeNo.TabIndex = 1;
            // 
            // tbxLine2
            // 
            this.tbxLine2.Location = new System.Drawing.Point(80, 47);
            this.tbxLine2.Name = "tbxLine2";
            this.tbxLine2.Size = new System.Drawing.Size(480, 21);
            this.tbxLine2.TabIndex = 1;
            // 
            // tbxLine1
            // 
            this.tbxLine1.Location = new System.Drawing.Point(80, 18);
            this.tbxLine1.Name = "tbxLine1";
            this.tbxLine1.Size = new System.Drawing.Size(480, 21);
            this.tbxLine1.TabIndex = 1;
            // 
            // lbLine2
            // 
            this.lbLine2.AutoSize = true;
            this.lbLine2.Location = new System.Drawing.Point(31, 51);
            this.lbLine2.Name = "lbLine2";
            this.lbLine2.Size = new System.Drawing.Size(43, 12);
            this.lbLine2.TabIndex = 0;
            this.lbLine2.Text = "Line 2:";
            // 
            // lbLine1
            // 
            this.lbLine1.AutoSize = true;
            this.lbLine1.Location = new System.Drawing.Point(31, 22);
            this.lbLine1.Name = "lbLine1";
            this.lbLine1.Size = new System.Drawing.Size(43, 12);
            this.lbLine1.TabIndex = 0;
            this.lbLine1.Text = "Line 1:";
            // 
            // Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbx);
            this.Name = "Info";
            this.Size = new System.Drawing.Size(610, 140);
            this.Load += new System.EventHandler(this.Info_Load);
            this.grbx.ResumeLayout(false);
            this.grbx.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbx;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.CheckBox cbxDistance;
        private System.Windows.Forms.CheckBox cbxSection;
        private System.Windows.Forms.CheckBox cbxTime;
        private System.Windows.Forms.CheckBox cbxInfo;
        private System.Windows.Forms.Label lbPipeNo;
        private System.Windows.Forms.TextBox tbxPipeNo;
        private System.Windows.Forms.TextBox tbxLine2;
        private System.Windows.Forms.TextBox tbxLine1;
        private System.Windows.Forms.Label lbLine2;
        private System.Windows.Forms.Label lbLine1;
    }
}
