namespace XManager.Forms
{
    partial class SetUpForm
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
            this.cbxAvgCnt = new System.Windows.Forms.ComboBox();
            this.lblAvgCount = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbxLangugae = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbxDevice = new System.Windows.Forms.GroupBox();
            this.lblCameraType = new System.Windows.Forms.Label();
            this.cbxCameraType = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.gbxMeasurement = new System.Windows.Forms.GroupBox();
            this.lblMeasurementType = new System.Windows.Forms.Label();
            this.btnCalibratePixelSize = new System.Windows.Forms.Button();
            this.tbxFovHeight = new System.Windows.Forms.TextBox();
            this.lblFovMessage = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.tbxFovWidth = new System.Windows.Forms.TextBox();
            this.cbxMeasurementType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbxDevice.SuspendLayout();
            this.panel4.SuspendLayout();
            this.gbxMeasurement.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxAvgCnt
            // 
            this.cbxAvgCnt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAvgCnt.FormattingEnabled = true;
            this.cbxAvgCnt.Location = new System.Drawing.Point(112, 45);
            this.cbxAvgCnt.Name = "cbxAvgCnt";
            this.cbxAvgCnt.Size = new System.Drawing.Size(71, 20);
            this.cbxAvgCnt.TabIndex = 11;
            // 
            // lblAvgCount
            // 
            this.lblAvgCount.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblAvgCount.Location = new System.Drawing.Point(6, 42);
            this.lblAvgCount.Name = "lblAvgCount";
            this.lblAvgCount.Size = new System.Drawing.Size(71, 23);
            this.lblAvgCount.TabIndex = 10;
            this.lblAvgCount.Text = "Avg Count :";
            this.lblAvgCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(594, 275);
            this.tableLayoutPanel3.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbxLangugae);
            this.panel2.Controls.Add(this.lblLanguage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(588, 39);
            this.panel2.TabIndex = 1;
            // 
            // cbxLangugae
            // 
            this.cbxLangugae.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLangugae.FormattingEnabled = true;
            this.cbxLangugae.Location = new System.Drawing.Point(115, 9);
            this.cbxLangugae.Name = "cbxLangugae";
            this.cbxLangugae.Size = new System.Drawing.Size(125, 20);
            this.cbxLangugae.TabIndex = 15;
            // 
            // lblLanguage
            // 
            this.lblLanguage.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblLanguage.Location = new System.Drawing.Point(5, 9);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(104, 23);
            this.lblLanguage.TabIndex = 14;
            this.lblLanguage.Text = "Language  Type:";
            this.lblLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel4.Controls.Add(this.btnApply, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnCancel, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 233);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(588, 39);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnApply.Location = new System.Drawing.Point(351, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(114, 33);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(471, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(114, 33);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 48);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(588, 179);
            this.panel3.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(588, 179);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbxDevice);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 173);
            this.panel1.TabIndex = 0;
            // 
            // gbxDevice
            // 
            this.gbxDevice.Controls.Add(this.lblCameraType);
            this.gbxDevice.Controls.Add(this.cbxCameraType);
            this.gbxDevice.Controls.Add(this.cbxAvgCnt);
            this.gbxDevice.Controls.Add(this.lblAvgCount);
            this.gbxDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxDevice.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.gbxDevice.Location = new System.Drawing.Point(0, 0);
            this.gbxDevice.Name = "gbxDevice";
            this.gbxDevice.Size = new System.Drawing.Size(294, 173);
            this.gbxDevice.TabIndex = 13;
            this.gbxDevice.TabStop = false;
            this.gbxDevice.Text = "Device";
            // 
            // lblCameraType
            // 
            this.lblCameraType.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblCameraType.Location = new System.Drawing.Point(6, 19);
            this.lblCameraType.Name = "lblCameraType";
            this.lblCameraType.Size = new System.Drawing.Size(100, 23);
            this.lblCameraType.TabIndex = 11;
            this.lblCameraType.Text = "Camera Type :";
            this.lblCameraType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxCameraType
            // 
            this.cbxCameraType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCameraType.DropDownWidth = 200;
            this.cbxCameraType.FormattingEnabled = true;
            this.cbxCameraType.Location = new System.Drawing.Point(112, 19);
            this.cbxCameraType.Name = "cbxCameraType";
            this.cbxCameraType.Size = new System.Drawing.Size(169, 20);
            this.cbxCameraType.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.gbxMeasurement);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(303, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(282, 173);
            this.panel4.TabIndex = 1;
            // 
            // gbxMeasurement
            // 
            this.gbxMeasurement.Controls.Add(this.lblMeasurementType);
            this.gbxMeasurement.Controls.Add(this.btnCalibratePixelSize);
            this.gbxMeasurement.Controls.Add(this.tbxFovHeight);
            this.gbxMeasurement.Controls.Add(this.lblFovMessage);
            this.gbxMeasurement.Controls.Add(this.label12);
            this.gbxMeasurement.Controls.Add(this.lblWidth);
            this.gbxMeasurement.Controls.Add(this.tbxFovWidth);
            this.gbxMeasurement.Controls.Add(this.cbxMeasurementType);
            this.gbxMeasurement.Controls.Add(this.label11);
            this.gbxMeasurement.Controls.Add(this.lblHeight);
            this.gbxMeasurement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxMeasurement.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.gbxMeasurement.Location = new System.Drawing.Point(0, 0);
            this.gbxMeasurement.Name = "gbxMeasurement";
            this.gbxMeasurement.Size = new System.Drawing.Size(282, 173);
            this.gbxMeasurement.TabIndex = 16;
            this.gbxMeasurement.TabStop = false;
            this.gbxMeasurement.Text = "Measurement";
            // 
            // lblMeasurementType
            // 
            this.lblMeasurementType.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblMeasurementType.Location = new System.Drawing.Point(16, 17);
            this.lblMeasurementType.Name = "lblMeasurementType";
            this.lblMeasurementType.Size = new System.Drawing.Size(53, 23);
            this.lblMeasurementType.TabIndex = 9;
            this.lblMeasurementType.Text = "Type :";
            this.lblMeasurementType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCalibratePixelSize
            // 
            this.btnCalibratePixelSize.ForeColor = System.Drawing.Color.Black;
            this.btnCalibratePixelSize.Location = new System.Drawing.Point(15, 45);
            this.btnCalibratePixelSize.Name = "btnCalibratePixelSize";
            this.btnCalibratePixelSize.Size = new System.Drawing.Size(261, 23);
            this.btnCalibratePixelSize.TabIndex = 10;
            this.btnCalibratePixelSize.Text = "Calibrate Pixel Size";
            this.btnCalibratePixelSize.UseVisualStyleBackColor = true;
            this.btnCalibratePixelSize.Click += new System.EventHandler(this.btnCalibratePixelSize_Click);
            // 
            // tbxFovHeight
            // 
            this.tbxFovHeight.Location = new System.Drawing.Point(89, 139);
            this.tbxFovHeight.Name = "tbxFovHeight";
            this.tbxFovHeight.Size = new System.Drawing.Size(75, 21);
            this.tbxFovHeight.TabIndex = 4;
            // 
            // lblFovMessage
            // 
            this.lblFovMessage.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFovMessage.Location = new System.Drawing.Point(13, 82);
            this.lblFovMessage.Name = "lblFovMessage";
            this.lblFovMessage.Size = new System.Drawing.Size(261, 12);
            this.lblFovMessage.TabIndex = 0;
            this.lblFovMessage.Text = "Please enter a FOV ";
            this.lblFovMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label12.Location = new System.Drawing.Point(170, 110);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 21);
            this.label12.TabIndex = 5;
            this.label12.Text = "mm";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWidth
            // 
            this.lblWidth.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblWidth.Location = new System.Drawing.Point(17, 110);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(66, 21);
            this.lblWidth.TabIndex = 1;
            this.lblWidth.Text = "Width :";
            this.lblWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxFovWidth
            // 
            this.tbxFovWidth.Location = new System.Drawing.Point(89, 110);
            this.tbxFovWidth.Name = "tbxFovWidth";
            this.tbxFovWidth.Size = new System.Drawing.Size(75, 21);
            this.tbxFovWidth.TabIndex = 3;
            // 
            // cbxMeasurementType
            // 
            this.cbxMeasurementType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMeasurementType.FormattingEnabled = true;
            this.cbxMeasurementType.Location = new System.Drawing.Point(75, 19);
            this.cbxMeasurementType.Name = "cbxMeasurementType";
            this.cbxMeasurementType.Size = new System.Drawing.Size(201, 20);
            this.cbxMeasurementType.TabIndex = 8;
            this.cbxMeasurementType.SelectedIndexChanged += new System.EventHandler(this.cbxMeasurementType_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label11.Location = new System.Drawing.Point(170, 139);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 21);
            this.label11.TabIndex = 6;
            this.label11.Text = "mm";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHeight
            // 
            this.lblHeight.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblHeight.Location = new System.Drawing.Point(17, 139);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(66, 21);
            this.lblHeight.TabIndex = 2;
            this.lblHeight.Text = "Height :";
            this.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SetUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(594, 275);
            this.Controls.Add(this.tableLayoutPanel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SetUpForm";
            this.Text = "SetUp";
            this.Load += new System.EventHandler(this.FovSettingForm_Load);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.gbxDevice.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.gbxMeasurement.ResumeLayout(false);
            this.gbxMeasurement.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblAvgCount;
        private System.Windows.Forms.ComboBox cbxAvgCnt;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblMeasurementType;
        private System.Windows.Forms.Button btnCalibratePixelSize;
        private System.Windows.Forms.Label lblFovMessage;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.ComboBox cbxMeasurementType;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxFovWidth;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxFovHeight;
        private System.Windows.Forms.Label lblCameraType;
        private System.Windows.Forms.ComboBox cbxCameraType;
        private System.Windows.Forms.ComboBox cbxLangugae;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox gbxDevice;
        private System.Windows.Forms.GroupBox gbxMeasurement;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
    }
}