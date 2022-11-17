namespace XManager.Controls
{
    partial class LutControl
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
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.ImageCurvePanel = new System.Windows.Forms.Panel();
            this.YAxisPanel = new System.Windows.Forms.Panel();
            this.XAxisPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.ImageCurvePanel, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.YAxisPanel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.XAxisPanel, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(395, 267);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // ImageCurvePanel
            // 
            this.ImageCurvePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageCurvePanel.Location = new System.Drawing.Point(60, 0);
            this.ImageCurvePanel.Margin = new System.Windows.Forms.Padding(0);
            this.ImageCurvePanel.Name = "ImageCurvePanel";
            this.ImageCurvePanel.Size = new System.Drawing.Size(335, 242);
            this.ImageCurvePanel.TabIndex = 3;
            // 
            // YAxisPanel
            // 
            this.YAxisPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YAxisPanel.Location = new System.Drawing.Point(0, 0);
            this.YAxisPanel.Margin = new System.Windows.Forms.Padding(0);
            this.YAxisPanel.Name = "YAxisPanel";
            this.YAxisPanel.Size = new System.Drawing.Size(60, 242);
            this.YAxisPanel.TabIndex = 4;
            // 
            // XAxisPanel
            // 
            this.XAxisPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XAxisPanel.Location = new System.Drawing.Point(60, 242);
            this.XAxisPanel.Margin = new System.Windows.Forms.Padding(0);
            this.XAxisPanel.Name = "XAxisPanel";
            this.XAxisPanel.Size = new System.Drawing.Size(335, 25);
            this.XAxisPanel.TabIndex = 5;
            // 
            // LutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Name = "LutControl";
            this.Size = new System.Drawing.Size(395, 267);
            this.Load += new System.EventHandler(this.LutControl_Load);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel ImageCurvePanel;
        private System.Windows.Forms.Panel YAxisPanel;
        private System.Windows.Forms.Panel XAxisPanel;
    }
}
