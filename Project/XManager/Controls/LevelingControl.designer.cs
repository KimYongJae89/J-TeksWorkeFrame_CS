namespace XManager.Controls
{
    partial class LevelingControl
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.DisplayPanel = new System.Windows.Forms.Panel();
            this.SliderPanel = new System.Windows.Forms.Panel();
            this.YaxisPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nupdnMax = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nupdnMin = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupdnMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupdnMin)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.YaxisPanel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(427, 348);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.DisplayPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.SliderPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(50, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(327, 348);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // DisplayPanel
            // 
            this.DisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayPanel.Location = new System.Drawing.Point(0, 0);
            this.DisplayPanel.Margin = new System.Windows.Forms.Padding(0);
            this.DisplayPanel.Name = "DisplayPanel";
            this.DisplayPanel.Size = new System.Drawing.Size(327, 268);
            this.DisplayPanel.TabIndex = 0;
            // 
            // SliderPanel
            // 
            this.SliderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SliderPanel.Location = new System.Drawing.Point(0, 268);
            this.SliderPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SliderPanel.Name = "SliderPanel";
            this.SliderPanel.Size = new System.Drawing.Size(327, 40);
            this.SliderPanel.TabIndex = 1;
            // 
            // YaxisPanel
            // 
            this.YaxisPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YaxisPanel.Location = new System.Drawing.Point(0, 0);
            this.YaxisPanel.Margin = new System.Windows.Forms.Padding(0);
            this.YaxisPanel.Name = "YaxisPanel";
            this.YaxisPanel.Size = new System.Drawing.Size(50, 348);
            this.YaxisPanel.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(377, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(50, 348);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.nupdnMax);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.nupdnMin);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 311);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(321, 34);
            this.panel2.TabIndex = 2;
            // 
            // nupdnMax
            // 
            this.nupdnMax.Location = new System.Drawing.Point(175, 6);
            this.nupdnMax.Name = "nupdnMax";
            this.nupdnMax.Size = new System.Drawing.Size(67, 21);
            this.nupdnMax.TabIndex = 11;
            this.nupdnMax.ValueChanged += new System.EventHandler(this.nupdnMax_ValueChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(125, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 23);
            this.label2.TabIndex = 10;
            this.label2.Text = "Max :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nupdnMin
            // 
            this.nupdnMin.Location = new System.Drawing.Point(52, 6);
            this.nupdnMin.Name = "nupdnMin";
            this.nupdnMin.Size = new System.Drawing.Size(67, 21);
            this.nupdnMin.TabIndex = 9;
            this.nupdnMin.ValueChanged += new System.EventHandler(this.nupdnMin_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "Min :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LevelingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "LevelingControl";
            this.Size = new System.Drawing.Size(427, 348);
            this.Load += new System.EventHandler(this.HistogramControl_Load);
            this.SizeChanged += new System.EventHandler(this.HistogramControl_SizeChanged);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nupdnMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupdnMin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel DisplayPanel;
        private System.Windows.Forms.Panel SliderPanel;
        private System.Windows.Forms.Panel YaxisPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown nupdnMax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nupdnMin;
        private System.Windows.Forms.Label label1;
    }
}
