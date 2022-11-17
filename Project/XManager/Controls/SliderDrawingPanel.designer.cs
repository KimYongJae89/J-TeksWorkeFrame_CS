namespace XManager.Controls
{
    partial class SliderDrawingPanel
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SliderPanel = new System.Windows.Forms.Panel();
            this.StringPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.SliderPanel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.StringPanel, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(313, 108);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // SliderPanel
            // 
            this.SliderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SliderPanel.Location = new System.Drawing.Point(0, 0);
            this.SliderPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SliderPanel.Name = "SliderPanel";
            this.SliderPanel.Size = new System.Drawing.Size(313, 88);
            this.SliderPanel.TabIndex = 0;
            // 
            // StringPanel
            // 
            this.StringPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringPanel.Location = new System.Drawing.Point(0, 88);
            this.StringPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StringPanel.Name = "StringPanel";
            this.StringPanel.Size = new System.Drawing.Size(313, 20);
            this.StringPanel.TabIndex = 1;
            // 
            // SliderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SliderControl";
            this.Size = new System.Drawing.Size(313, 108);
            this.Load += new System.EventHandler(this.SliderDrawingPanel_Load);
            this.SizeChanged += new System.EventHandler(this.SliderDrawingPanel_SizeChanged);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel SliderPanel;
        private System.Windows.Forms.Panel StringPanel;
    }
}
