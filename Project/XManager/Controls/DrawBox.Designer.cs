namespace XManager.Controls
{
    partial class DrawBox
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
            this.components = new System.ComponentModel.Container();
            this.pbx = new System.Windows.Forms.PictureBox();
            this.ctrxMenuPbx = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.originSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawCrossToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToISeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.originRawImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDrawing = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabelPoint = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelStringValue1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelValue1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelStringValue2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelValue2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelStringValue3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelValue3 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbx)).BeginInit();
            this.ctrxMenuPbx.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.pnlDrawing.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbx
            // 
            this.pbx.BackColor = System.Drawing.Color.Black;
            this.pbx.ContextMenuStrip = this.ctrxMenuPbx;
            this.pbx.Location = new System.Drawing.Point(192, 112);
            this.pbx.Margin = new System.Windows.Forms.Padding(0);
            this.pbx.Name = "pbx";
            this.pbx.Size = new System.Drawing.Size(141, 124);
            this.pbx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbx.TabIndex = 0;
            this.pbx.TabStop = false;
            this.pbx.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pbx.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbx_MouseDoubleClick);
            this.pbx.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbx_MouseDown);
            this.pbx.MouseEnter += new System.EventHandler(this.pbx_MouseEnter);
            this.pbx.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbx_MouseMove);
            this.pbx.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbx_MouseUp);
            this.pbx.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.OnMouseWheel);
            this.pbx.Resize += new System.EventHandler(this.pictureBox_Resize);
            // 
            // ctrxMenuPbx
            // 
            this.ctrxMenuPbx.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.originSizeToolStripMenuItem,
            this.drawCrossToolStripMenuItem,
            this.openToISeeToolStripMenuItem});
            this.ctrxMenuPbx.Name = "ctrxMenuPbx";
            this.ctrxMenuPbx.Size = new System.Drawing.Size(148, 92);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.testToolStripMenuItem.Text = "FitZoom";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // originSizeToolStripMenuItem
            // 
            this.originSizeToolStripMenuItem.Name = "originSizeToolStripMenuItem";
            this.originSizeToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.originSizeToolStripMenuItem.Text = "1:1 Zoom";
            this.originSizeToolStripMenuItem.Click += new System.EventHandler(this.originSizeToolStripMenuItem_Click);
            // 
            // drawCrossToolStripMenuItem
            // 
            this.drawCrossToolStripMenuItem.Name = "drawCrossToolStripMenuItem";
            this.drawCrossToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.drawCrossToolStripMenuItem.Text = "Draw Cross";
            this.drawCrossToolStripMenuItem.Click += new System.EventHandler(this.drawCrossToolStripMenuItem_Click);
            // 
            // openToISeeToolStripMenuItem
            // 
            this.openToISeeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.originRawImageToolStripMenuItem,
            this.histogramToolStripMenuItem});
            this.openToISeeToolStripMenuItem.Name = "openToISeeToolStripMenuItem";
            this.openToISeeToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.openToISeeToolStripMenuItem.Text = "Open to ISee!";
            // 
            // originRawImageToolStripMenuItem
            // 
            this.originRawImageToolStripMenuItem.Name = "originRawImageToolStripMenuItem";
            this.originRawImageToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.originRawImageToolStripMenuItem.Text = "Origin Raw Image";
            this.originRawImageToolStripMenuItem.Click += new System.EventHandler(this.originRawImageToolStripMenuItem_Click);
            // 
            // histogramToolStripMenuItem
            // 
            this.histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            this.histogramToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.histogramToolStripMenuItem.Text = "Histogram Image";
            this.histogramToolStripMenuItem.Click += new System.EventHandler(this.histogramToolStripMenuItem_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.pnlDrawing, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.statusStrip, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(584, 462);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // pnlDrawing
            // 
            this.pnlDrawing.Controls.Add(this.pbx);
            this.pnlDrawing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDrawing.Location = new System.Drawing.Point(0, 0);
            this.pnlDrawing.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDrawing.Name = "pnlDrawing";
            this.pnlDrawing.Size = new System.Drawing.Size(584, 442);
            this.pnlDrawing.TabIndex = 0;
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabelPoint,
            this.StatusLabelSize,
            this.StatusLabelStringValue1,
            this.StatusLabelValue1,
            this.StatusLabelStringValue2,
            this.StatusLabelValue2,
            this.StatusLabelStringValue3,
            this.StatusLabelValue3});
            this.statusStrip.Location = new System.Drawing.Point(0, 442);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(584, 20);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // StatusLabelPoint
            // 
            this.StatusLabelPoint.AutoSize = false;
            this.StatusLabelPoint.Name = "StatusLabelPoint";
            this.StatusLabelPoint.Size = new System.Drawing.Size(90, 15);
            this.StatusLabelPoint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabelSize
            // 
            this.StatusLabelSize.AutoSize = false;
            this.StatusLabelSize.Name = "StatusLabelSize";
            this.StatusLabelSize.Size = new System.Drawing.Size(179, 15);
            this.StatusLabelSize.Spring = true;
            this.StatusLabelSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusLabelStringValue1
            // 
            this.StatusLabelStringValue1.AutoSize = false;
            this.StatusLabelStringValue1.Name = "StatusLabelStringValue1";
            this.StatusLabelStringValue1.Size = new System.Drawing.Size(50, 15);
            this.StatusLabelStringValue1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusLabelValue1
            // 
            this.StatusLabelValue1.AutoSize = false;
            this.StatusLabelValue1.Name = "StatusLabelValue1";
            this.StatusLabelValue1.Size = new System.Drawing.Size(50, 15);
            this.StatusLabelValue1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabelStringValue2
            // 
            this.StatusLabelStringValue2.AutoSize = false;
            this.StatusLabelStringValue2.Name = "StatusLabelStringValue2";
            this.StatusLabelStringValue2.Size = new System.Drawing.Size(50, 15);
            this.StatusLabelStringValue2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusLabelValue2
            // 
            this.StatusLabelValue2.AutoSize = false;
            this.StatusLabelValue2.Name = "StatusLabelValue2";
            this.StatusLabelValue2.Size = new System.Drawing.Size(50, 15);
            this.StatusLabelValue2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabelStringValue3
            // 
            this.StatusLabelStringValue3.AutoSize = false;
            this.StatusLabelStringValue3.Name = "StatusLabelStringValue3";
            this.StatusLabelStringValue3.Size = new System.Drawing.Size(50, 15);
            this.StatusLabelStringValue3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusLabelValue3
            // 
            this.StatusLabelValue3.AutoSize = false;
            this.StatusLabelValue3.Name = "StatusLabelValue3";
            this.StatusLabelValue3.Size = new System.Drawing.Size(50, 15);
            this.StatusLabelValue3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DrawBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tableLayoutPanel2);
            this.DoubleBuffered = true;
            this.Name = "DrawBox";
            this.Size = new System.Drawing.Size(584, 462);
            this.Load += new System.EventHandler(this.CameraDawingPannel_Load);
            this.Move += new System.EventHandler(this.CameraDawingPannel_Move);
            this.Resize += new System.EventHandler(this.CameraDawingPannel_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbx)).EndInit();
            this.ctrxMenuPbx.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.pnlDrawing.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.PictureBox pbx;
        private System.Windows.Forms.ContextMenuStrip ctrxMenuPbx;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem originSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawCrossToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel pnlDrawing;
        private System.Windows.Forms.ToolStripMenuItem openToISeeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem originRawImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelPoint;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelSize;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelStringValue1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelValue1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelStringValue2;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelValue2;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelStringValue3;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelValue3;
    }
}
