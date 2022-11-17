namespace KiyControls.Controls
{
    partial class LogView
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
            this.tabLog = new System.Windows.Forms.TabControl();
            this.tabPgLog = new System.Windows.Forms.TabPage();
            this.tbxLog = new System.Windows.Forms.TextBox();
            this.tabLog.SuspendLayout();
            this.tabPgLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.tabPgLog);
            this.tabLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLog.Location = new System.Drawing.Point(0, 0);
            this.tabLog.Margin = new System.Windows.Forms.Padding(0);
            this.tabLog.Name = "tabLog";
            this.tabLog.SelectedIndex = 0;
            this.tabLog.Size = new System.Drawing.Size(400, 300);
            this.tabLog.TabIndex = 0;
            // 
            // tabPgLog
            // 
            this.tabPgLog.Controls.Add(this.tbxLog);
            this.tabPgLog.Location = new System.Drawing.Point(4, 22);
            this.tabPgLog.Margin = new System.Windows.Forms.Padding(0);
            this.tabPgLog.Name = "tabPgLog";
            this.tabPgLog.Size = new System.Drawing.Size(392, 274);
            this.tabPgLog.TabIndex = 0;
            this.tabPgLog.Text = "Log";
            this.tabPgLog.UseVisualStyleBackColor = true;
            // 
            // tbxLog
            // 
            this.tbxLog.BackColor = System.Drawing.Color.Black;
            this.tbxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxLog.Font = new System.Drawing.Font("굴림", 10F);
            this.tbxLog.Location = new System.Drawing.Point(0, 0);
            this.tbxLog.Margin = new System.Windows.Forms.Padding(0);
            this.tbxLog.Multiline = true;
            this.tbxLog.Name = "tbxLog";
            this.tbxLog.ReadOnly = true;
            this.tbxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxLog.Size = new System.Drawing.Size(392, 274);
            this.tbxLog.TabIndex = 0;
            // 
            // LogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabLog);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LogView";
            this.Size = new System.Drawing.Size(400, 300);
            this.Load += new System.EventHandler(this.LogView_Load);
            this.tabLog.ResumeLayout(false);
            this.tabPgLog.ResumeLayout(false);
            this.tabPgLog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabLog;
        private System.Windows.Forms.TabPage tabPgLog;
        private System.Windows.Forms.TextBox tbxLog;
    }
}
