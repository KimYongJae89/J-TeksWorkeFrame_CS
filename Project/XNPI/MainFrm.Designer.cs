namespace XNPI
{
    partial class MainFrm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tblPnlMainFrame = new System.Windows.Forms.TableLayoutPanel();
            this.tblPnl = new System.Windows.Forms.TableLayoutPanel();
            this.tblPnlOP = new System.Windows.Forms.TableLayoutPanel();
            this.tblPnlEtcDevice = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSnapshot = new System.Windows.Forms.Panel();
            this.tblPnlMainFrame.SuspendLayout();
            this.tblPnl.SuspendLayout();
            this.tblPnlOP.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblPnlMainFrame
            // 
            this.tblPnlMainFrame.ColumnCount = 1;
            this.tblPnlMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPnlMainFrame.Controls.Add(this.tblPnl, 0, 0);
            this.tblPnlMainFrame.Controls.Add(this.pnlSnapshot, 0, 1);
            this.tblPnlMainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPnlMainFrame.Location = new System.Drawing.Point(0, 0);
            this.tblPnlMainFrame.Margin = new System.Windows.Forms.Padding(0);
            this.tblPnlMainFrame.Name = "tblPnlMainFrame";
            this.tblPnlMainFrame.RowCount = 2;
            this.tblPnlMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPnlMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 243F));
            this.tblPnlMainFrame.Size = new System.Drawing.Size(800, 450);
            this.tblPnlMainFrame.TabIndex = 0;
            // 
            // tblPnl
            // 
            this.tblPnl.ColumnCount = 3;
            this.tblPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 650F));
            this.tblPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.tblPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPnl.Controls.Add(this.tblPnlOP, 0, 0);
            this.tblPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPnl.Location = new System.Drawing.Point(0, 0);
            this.tblPnl.Margin = new System.Windows.Forms.Padding(0);
            this.tblPnl.Name = "tblPnl";
            this.tblPnl.RowCount = 1;
            this.tblPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPnl.Size = new System.Drawing.Size(800, 207);
            this.tblPnl.TabIndex = 1;
            // 
            // tblPnlOP
            // 
            this.tblPnlOP.ColumnCount = 1;
            this.tblPnlOP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPnlOP.Controls.Add(this.tblPnlEtcDevice, 0, 1);
            this.tblPnlOP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPnlOP.Location = new System.Drawing.Point(0, 3);
            this.tblPnlOP.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.tblPnlOP.Name = "tblPnlOP";
            this.tblPnlOP.RowCount = 5;
            this.tblPnlOP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblPnlOP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblPnlOP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblPnlOP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblPnlOP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblPnlOP.Size = new System.Drawing.Size(650, 201);
            this.tblPnlOP.TabIndex = 0;
            // 
            // tblPnlEtcDevice
            // 
            this.tblPnlEtcDevice.ColumnCount = 2;
            this.tblPnlEtcDevice.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tblPnlEtcDevice.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPnlEtcDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPnlEtcDevice.Location = new System.Drawing.Point(0, 60);
            this.tblPnlEtcDevice.Margin = new System.Windows.Forms.Padding(0);
            this.tblPnlEtcDevice.Name = "tblPnlEtcDevice";
            this.tblPnlEtcDevice.RowCount = 1;
            this.tblPnlEtcDevice.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPnlEtcDevice.Size = new System.Drawing.Size(650, 60);
            this.tblPnlEtcDevice.TabIndex = 0;
            // 
            // pnlSnapshot
            // 
            this.pnlSnapshot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSnapshot.Location = new System.Drawing.Point(0, 207);
            this.pnlSnapshot.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSnapshot.Name = "pnlSnapshot";
            this.pnlSnapshot.Size = new System.Drawing.Size(800, 243);
            this.pnlSnapshot.TabIndex = 0;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tblPnlMainFrame);
            this.Name = "MainFrm";
            this.Text = "XNPI";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrm_FormClosed);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.tblPnlMainFrame.ResumeLayout(false);
            this.tblPnl.ResumeLayout(false);
            this.tblPnlOP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tblPnlMainFrame;
        private System.Windows.Forms.Panel pnlSnapshot;
        private System.Windows.Forms.TableLayoutPanel tblPnl;
        private System.Windows.Forms.TableLayoutPanel tblPnlOP;
        private System.Windows.Forms.TableLayoutPanel tblPnlEtcDevice;
    }
}

