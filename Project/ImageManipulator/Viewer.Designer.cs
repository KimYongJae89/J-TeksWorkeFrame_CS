namespace ImageManipulator
{
    partial class Viewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Viewer));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabelPoint = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelStringValue1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelValue1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelStringValue2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelValue2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelStringValue3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelValue3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.DisplayPanel = new System.Windows.Forms.Panel();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabelPoint,
            this.StatusLabelSize,
            this.StatusLabelStringValue1,
            this.StatusLabelValue1,
            this.StatusLabelStringValue2,
            this.StatusLabelValue2,
            this.StatusLabelStringValue3,
            this.StatusLabelValue3});
            this.statusStrip.Location = new System.Drawing.Point(0, 425);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(578, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // StatusLabelPoint
            // 
            this.StatusLabelPoint.AutoSize = false;
            this.StatusLabelPoint.Name = "StatusLabelPoint";
            this.StatusLabelPoint.Size = new System.Drawing.Size(90, 17);
            this.StatusLabelPoint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabelSize
            // 
            this.StatusLabelSize.AutoSize = false;
            this.StatusLabelSize.Name = "StatusLabelSize";
            this.StatusLabelSize.Size = new System.Drawing.Size(173, 17);
            this.StatusLabelSize.Spring = true;
            this.StatusLabelSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusLabelStringValue1
            // 
            this.StatusLabelStringValue1.AutoSize = false;
            this.StatusLabelStringValue1.Name = "StatusLabelStringValue1";
            this.StatusLabelStringValue1.Size = new System.Drawing.Size(50, 17);
            this.StatusLabelStringValue1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusLabelValue1
            // 
            this.StatusLabelValue1.AutoSize = false;
            this.StatusLabelValue1.Name = "StatusLabelValue1";
            this.StatusLabelValue1.Size = new System.Drawing.Size(50, 17);
            this.StatusLabelValue1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabelStringValue2
            // 
            this.StatusLabelStringValue2.AutoSize = false;
            this.StatusLabelStringValue2.Name = "StatusLabelStringValue2";
            this.StatusLabelStringValue2.Size = new System.Drawing.Size(50, 17);
            this.StatusLabelStringValue2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusLabelValue2
            // 
            this.StatusLabelValue2.AutoSize = false;
            this.StatusLabelValue2.Name = "StatusLabelValue2";
            this.StatusLabelValue2.Size = new System.Drawing.Size(50, 17);
            this.StatusLabelValue2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabelStringValue3
            // 
            this.StatusLabelStringValue3.AutoSize = false;
            this.StatusLabelStringValue3.Name = "StatusLabelStringValue3";
            this.StatusLabelStringValue3.Size = new System.Drawing.Size(50, 17);
            this.StatusLabelStringValue3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusLabelValue3
            // 
            this.StatusLabelValue3.AutoSize = false;
            this.StatusLabelValue3.Name = "StatusLabelValue3";
            this.StatusLabelValue3.Size = new System.Drawing.Size(50, 17);
            this.StatusLabelValue3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DisplayPanel
            // 
            this.DisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayPanel.Location = new System.Drawing.Point(0, 0);
            this.DisplayPanel.Margin = new System.Windows.Forms.Padding(0);
            this.DisplayPanel.Name = "DisplayPanel";
            this.DisplayPanel.Size = new System.Drawing.Size(578, 425);
            this.DisplayPanel.TabIndex = 1;
            this.DisplayPanel.SizeChanged += new System.EventHandler(this.DisplayPanel_SizeChanged);
            // 
            // Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(578, 447);
            this.Controls.Add(this.DisplayPanel);
            this.Controls.Add(this.statusStrip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Viewer";
            this.Tag = "Viewer";
            this.Text = "Viewer";
            this.Activated += new System.EventHandler(this.Viewer_Activated);
            this.Deactivate += new System.EventHandler(this.Viewer_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Viewer_FormClosed);
            this.Load += new System.EventHandler(this.Viewer_Load);
            this.SizeChanged += new System.EventHandler(this.Viewer_SizeChanged);
            this.Click += new System.EventHandler(this.Viewer_Click);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelPoint;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelSize;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelStringValue1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelValue1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelStringValue2;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelValue2;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelStringValue3;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelValue3;
        private System.Windows.Forms.Panel DisplayPanel;
    }
}