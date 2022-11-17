namespace XNPI.Controls
{
    partial class Snapshot
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
            this.tbPnl = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lbSectionNum = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnNewPipe = new System.Windows.Forms.Button();
            this.btnRetake = new System.Windows.Forms.Button();
            this.btnTriggerRepeat = new System.Windows.Forms.Button();
            this.btnTakeImg = new System.Windows.Forms.Button();
            this.tbPnl.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPnl
            // 
            this.tbPnl.ColumnCount = 2;
            this.tbPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tbPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbPnl.Controls.Add(this.pnlLeft, 0, 0);
            this.tbPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPnl.Location = new System.Drawing.Point(0, 0);
            this.tbPnl.Margin = new System.Windows.Forms.Padding(0);
            this.tbPnl.Name = "tbPnl";
            this.tbPnl.RowCount = 1;
            this.tbPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbPnl.Size = new System.Drawing.Size(500, 243);
            this.tbPnl.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.textBox1);
            this.pnlLeft.Controls.Add(this.lbSectionNum);
            this.pnlLeft.Controls.Add(this.btnReset);
            this.pnlLeft.Controls.Add(this.btnNewPipe);
            this.pnlLeft.Controls.Add(this.btnRetake);
            this.pnlLeft.Controls.Add(this.btnTriggerRepeat);
            this.pnlLeft.Controls.Add(this.btnTakeImg);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(120, 243);
            this.pnlLeft.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 63);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 6;
            // 
            // lbSectionNum
            // 
            this.lbSectionNum.AutoSize = true;
            this.lbSectionNum.Location = new System.Drawing.Point(10, 48);
            this.lbSectionNum.Name = "lbSectionNum";
            this.lbSectionNum.Size = new System.Drawing.Size(100, 12);
            this.lbSectionNum.TabIndex = 5;
            this.lbSectionNum.Text = "Section Number:";
            // 
            // btnReset
            // 
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Location = new System.Drawing.Point(10, 177);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 30);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnNewPipe
            // 
            this.btnNewPipe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewPipe.Location = new System.Drawing.Point(10, 100);
            this.btnNewPipe.Name = "btnNewPipe";
            this.btnNewPipe.Size = new System.Drawing.Size(100, 30);
            this.btnNewPipe.TabIndex = 3;
            this.btnNewPipe.Text = "New PIPE";
            this.btnNewPipe.UseVisualStyleBackColor = true;
            // 
            // btnRetake
            // 
            this.btnRetake.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetake.Location = new System.Drawing.Point(10, 131);
            this.btnRetake.Name = "btnRetake";
            this.btnRetake.Size = new System.Drawing.Size(100, 30);
            this.btnRetake.TabIndex = 2;
            this.btnRetake.Text = "Retake";
            this.btnRetake.UseVisualStyleBackColor = true;
            // 
            // btnTriggerRepeat
            // 
            this.btnTriggerRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTriggerRepeat.Location = new System.Drawing.Point(10, 208);
            this.btnTriggerRepeat.Name = "btnTriggerRepeat";
            this.btnTriggerRepeat.Size = new System.Drawing.Size(100, 30);
            this.btnTriggerRepeat.TabIndex = 1;
            this.btnTriggerRepeat.Text = "Trigger Repeat";
            this.btnTriggerRepeat.UseVisualStyleBackColor = true;
            // 
            // btnTakeImg
            // 
            this.btnTakeImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTakeImg.Location = new System.Drawing.Point(10, 5);
            this.btnTakeImg.Name = "btnTakeImg";
            this.btnTakeImg.Size = new System.Drawing.Size(100, 30);
            this.btnTakeImg.TabIndex = 0;
            this.btnTakeImg.Text = "Take Image";
            this.btnTakeImg.UseVisualStyleBackColor = true;
            // 
            // Snapshot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbPnl);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Snapshot";
            this.Size = new System.Drawing.Size(500, 243);
            this.Load += new System.EventHandler(this.Snapshot_Load);
            this.tbPnl.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbPnl;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbSectionNum;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnNewPipe;
        private System.Windows.Forms.Button btnRetake;
        private System.Windows.Forms.Button btnTriggerRepeat;
        private System.Windows.Forms.Button btnTakeImg;
    }
}
