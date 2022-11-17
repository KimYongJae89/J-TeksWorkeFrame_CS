namespace XNPI.Controls
{
    partial class ETC
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
            this.btnSetEnv = new System.Windows.Forms.Button();
            this.grbx.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbx
            // 
            this.grbx.Controls.Add(this.btnSetEnv);
            this.grbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbx.Location = new System.Drawing.Point(0, 0);
            this.grbx.Name = "grbx";
            this.grbx.Size = new System.Drawing.Size(90, 60);
            this.grbx.TabIndex = 0;
            this.grbx.TabStop = false;
            this.grbx.Text = "ETC";
            // 
            // btnSetEnv
            // 
            this.btnSetEnv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetEnv.Location = new System.Drawing.Point(8, 23);
            this.btnSetEnv.Name = "btnSetEnv";
            this.btnSetEnv.Size = new System.Drawing.Size(75, 25);
            this.btnSetEnv.TabIndex = 0;
            this.btnSetEnv.Text = "Set Env";
            this.btnSetEnv.UseVisualStyleBackColor = true;
            this.btnSetEnv.Click += new System.EventHandler(this.btnETC_Click);
            // 
            // ETC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbx);
            this.Name = "ETC";
            this.Size = new System.Drawing.Size(90, 60);
            this.Load += new System.EventHandler(this.ETC_Load);
            this.grbx.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbx;
        private System.Windows.Forms.Button btnSetEnv;
    }
}
