namespace XNPI
{
    partial class XRay
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
            this.nmrMA = new System.Windows.Forms.NumericUpDown();
            this.nmrKEV = new System.Windows.Forms.NumericUpDown();
            this.lbMA = new System.Windows.Forms.Label();
            this.lbKEV = new System.Windows.Forms.Label();
            this.btnOff = new System.Windows.Forms.Button();
            this.btnOn = new System.Windows.Forms.Button();
            this.grbx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrMA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrKEV)).BeginInit();
            this.SuspendLayout();
            // 
            // grbx
            // 
            this.grbx.Controls.Add(this.nmrMA);
            this.grbx.Controls.Add(this.nmrKEV);
            this.grbx.Controls.Add(this.lbMA);
            this.grbx.Controls.Add(this.lbKEV);
            this.grbx.Controls.Add(this.btnOff);
            this.grbx.Controls.Add(this.btnOn);
            this.grbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbx.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grbx.Location = new System.Drawing.Point(0, 0);
            this.grbx.Name = "grbx";
            this.grbx.Size = new System.Drawing.Size(360, 60);
            this.grbx.TabIndex = 0;
            this.grbx.TabStop = false;
            this.grbx.Text = "X-Ray Control";
            // 
            // nmrMA
            // 
            this.nmrMA.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.nmrMA.Location = new System.Drawing.Point(270, 23);
            this.nmrMA.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmrMA.Name = "nmrMA";
            this.nmrMA.Size = new System.Drawing.Size(50, 21);
            this.nmrMA.TabIndex = 8;
            this.nmrMA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nmrKEV
            // 
            this.nmrKEV.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.nmrKEV.Location = new System.Drawing.Point(184, 23);
            this.nmrKEV.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmrKEV.Name = "nmrKEV";
            this.nmrKEV.Size = new System.Drawing.Size(50, 21);
            this.nmrKEV.TabIndex = 7;
            this.nmrKEV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbMA
            // 
            this.lbMA.AutoSize = true;
            this.lbMA.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbMA.Location = new System.Drawing.Point(320, 27);
            this.lbMA.Name = "lbMA";
            this.lbMA.Size = new System.Drawing.Size(24, 12);
            this.lbMA.TabIndex = 4;
            this.lbMA.Text = "mA";
            // 
            // lbKEV
            // 
            this.lbKEV.AutoSize = true;
            this.lbKEV.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbKEV.Location = new System.Drawing.Point(234, 27);
            this.lbKEV.Name = "lbKEV";
            this.lbKEV.Size = new System.Drawing.Size(28, 12);
            this.lbKEV.TabIndex = 3;
            this.lbKEV.Text = "KeV";
            // 
            // btnOff
            // 
            this.btnOff.Enabled = false;
            this.btnOff.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOff.Location = new System.Drawing.Point(87, 21);
            this.btnOff.Name = "btnOff";
            this.btnOff.Size = new System.Drawing.Size(75, 25);
            this.btnOff.TabIndex = 2;
            this.btnOff.Text = "OFF";
            this.btnOff.UseVisualStyleBackColor = true;
            // 
            // btnOn
            // 
            this.btnOn.Enabled = false;
            this.btnOn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOn.Location = new System.Drawing.Point(6, 21);
            this.btnOn.Name = "btnOn";
            this.btnOn.Size = new System.Drawing.Size(75, 25);
            this.btnOn.TabIndex = 1;
            this.btnOn.Text = "ON";
            this.btnOn.UseVisualStyleBackColor = true;
            // 
            // XRay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbx);
            this.Name = "XRay";
            this.Size = new System.Drawing.Size(360, 60);
            this.Load += new System.EventHandler(this.XRay_Load);
            this.grbx.ResumeLayout(false);
            this.grbx.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrMA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrKEV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbx;
        private System.Windows.Forms.Button btnOff;
        private System.Windows.Forms.Button btnOn;
        private System.Windows.Forms.Label lbMA;
        private System.Windows.Forms.Label lbKEV;
        private System.Windows.Forms.NumericUpDown nmrMA;
        private System.Windows.Forms.NumericUpDown nmrKEV;
    }
}
