namespace XNPI.Controls
{
    partial class Device
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
            this.cbxActiveCAM = new System.Windows.Forms.CheckBox();
            this.grbx = new System.Windows.Forms.GroupBox();
            this.lbFPS = new System.Windows.Forms.Label();
            this.lbSelectDevice = new System.Windows.Forms.Label();
            this.cbxSelectDevice = new System.Windows.Forms.ComboBox();
            this.grbx.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxActiveCAM
            // 
            this.cbxActiveCAM.AutoSize = true;
            this.cbxActiveCAM.Location = new System.Drawing.Point(13, 25);
            this.cbxActiveCAM.Name = "cbxActiveCAM";
            this.cbxActiveCAM.Size = new System.Drawing.Size(90, 16);
            this.cbxActiveCAM.TabIndex = 0;
            this.cbxActiveCAM.Text = "Active CAM";
            this.cbxActiveCAM.UseVisualStyleBackColor = true;
            // 
            // grbx
            // 
            this.grbx.Controls.Add(this.lbFPS);
            this.grbx.Controls.Add(this.lbSelectDevice);
            this.grbx.Controls.Add(this.cbxSelectDevice);
            this.grbx.Controls.Add(this.cbxActiveCAM);
            this.grbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbx.Location = new System.Drawing.Point(0, 0);
            this.grbx.Name = "grbx";
            this.grbx.Size = new System.Drawing.Size(500, 60);
            this.grbx.TabIndex = 1;
            this.grbx.TabStop = false;
            this.grbx.Text = "Device";
            // 
            // lbFPS
            // 
            this.lbFPS.AutoSize = true;
            this.lbFPS.Location = new System.Drawing.Point(380, 27);
            this.lbFPS.Name = "lbFPS";
            this.lbFPS.Size = new System.Drawing.Size(68, 12);
            this.lbFPS.TabIndex = 3;
            this.lbFPS.Text = "FPS : -1.00";
            // 
            // lbSelectDevice
            // 
            this.lbSelectDevice.AutoSize = true;
            this.lbSelectDevice.Location = new System.Drawing.Point(126, 27);
            this.lbSelectDevice.Name = "lbSelectDevice";
            this.lbSelectDevice.Size = new System.Drawing.Size(86, 12);
            this.lbSelectDevice.TabIndex = 2;
            this.lbSelectDevice.Text = "Select Device:";
            // 
            // cbxSelectDevice
            // 
            this.cbxSelectDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelectDevice.FormattingEnabled = true;
            this.cbxSelectDevice.Location = new System.Drawing.Point(212, 23);
            this.cbxSelectDevice.Name = "cbxSelectDevice";
            this.cbxSelectDevice.Size = new System.Drawing.Size(140, 20);
            this.cbxSelectDevice.TabIndex = 1;
            // 
            // Device
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbx);
            this.Name = "Device";
            this.Size = new System.Drawing.Size(500, 60);
            this.Load += new System.EventHandler(this.Device_Load);
            this.grbx.ResumeLayout(false);
            this.grbx.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cbxActiveCAM;
        private System.Windows.Forms.GroupBox grbx;
        private System.Windows.Forms.Label lbSelectDevice;
        private System.Windows.Forms.ComboBox cbxSelectDevice;
        private System.Windows.Forms.Label lbFPS;
    }
}
