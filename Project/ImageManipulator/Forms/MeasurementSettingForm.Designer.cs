namespace ImageManipulator.Forms
{
    partial class MeasurementSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeasurementSettingForm));
            this.lblFovMessage = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.tbxFovWidth = new System.Windows.Forms.TextBox();
            this.tbxFovHeight = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnFovApply = new System.Windows.Forms.Button();
            this.cbxMeasurementType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.btnCalibratePixelSize = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUnit = new System.Windows.Forms.Label();
            this.cbxMeasurementUnit = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFovMessage
            // 
            this.lblFovMessage.AutoSize = true;
            this.lblFovMessage.Location = new System.Drawing.Point(8, 114);
            this.lblFovMessage.Name = "lblFovMessage";
            this.lblFovMessage.Size = new System.Drawing.Size(119, 12);
            this.lblFovMessage.TabIndex = 0;
            this.lblFovMessage.Text = "Please enter a FOV ";
            this.lblFovMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWidth
            // 
            this.lblWidth.Location = new System.Drawing.Point(10, 145);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(66, 21);
            this.lblWidth.TabIndex = 1;
            this.lblWidth.Text = "Width :";
            this.lblWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHeight
            // 
            this.lblHeight.Location = new System.Drawing.Point(10, 174);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(66, 21);
            this.lblHeight.TabIndex = 2;
            this.lblHeight.Text = "Height :";
            this.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxFovWidth
            // 
            this.tbxFovWidth.Location = new System.Drawing.Point(82, 145);
            this.tbxFovWidth.Name = "tbxFovWidth";
            this.tbxFovWidth.Size = new System.Drawing.Size(75, 21);
            this.tbxFovWidth.TabIndex = 3;
            this.tbxFovWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress_Event);
            // 
            // tbxFovHeight
            // 
            this.tbxFovHeight.Location = new System.Drawing.Point(82, 174);
            this.tbxFovHeight.Name = "tbxFovHeight";
            this.tbxFovHeight.Size = new System.Drawing.Size(75, 21);
            this.tbxFovHeight.TabIndex = 4;
            this.tbxFovHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress_Event);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(163, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "mm";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(163, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 21);
            this.label5.TabIndex = 6;
            this.label5.Text = "mm";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnFovApply
            // 
            this.btnFovApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFovApply.Location = new System.Drawing.Point(3, 3);
            this.btnFovApply.Name = "btnFovApply";
            this.btnFovApply.Size = new System.Drawing.Size(126, 33);
            this.btnFovApply.TabIndex = 7;
            this.btnFovApply.Text = "Apply";
            this.btnFovApply.UseVisualStyleBackColor = true;
            this.btnFovApply.Click += new System.EventHandler(this.btnFovApply_Click);
            // 
            // cbxMeasurementType
            // 
            this.cbxMeasurementType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMeasurementType.FormattingEnabled = true;
            this.cbxMeasurementType.Location = new System.Drawing.Point(68, 42);
            this.cbxMeasurementType.Name = "cbxMeasurementType";
            this.cbxMeasurementType.Size = new System.Drawing.Size(186, 20);
            this.cbxMeasurementType.TabIndex = 8;
            this.cbxMeasurementType.SelectedIndexChanged += new System.EventHandler(this.cbxMeasurementType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(9, 40);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(53, 23);
            this.lblType.TabIndex = 9;
            this.lblType.Text = "Type :";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCalibratePixelSize
            // 
            this.btnCalibratePixelSize.Location = new System.Drawing.Point(8, 79);
            this.btnCalibratePixelSize.Name = "btnCalibratePixelSize";
            this.btnCalibratePixelSize.Size = new System.Drawing.Size(247, 23);
            this.btnCalibratePixelSize.TabIndex = 10;
            this.btnCalibratePixelSize.Text = "Calibrate Pixel Size";
            this.btnCalibratePixelSize.UseVisualStyleBackColor = true;
            this.btnCalibratePixelSize.Click += new System.EventHandler(this.btnCalibratePixelSize_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(135, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(126, 33);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(270, 264);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnFovApply, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 222);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(264, 39);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxMeasurementUnit);
            this.panel1.Controls.Add(this.lblUnit);
            this.panel1.Controls.Add(this.lblType);
            this.panel1.Controls.Add(this.btnCalibratePixelSize);
            this.panel1.Controls.Add(this.lblFovMessage);
            this.panel1.Controls.Add(this.lblWidth);
            this.panel1.Controls.Add(this.cbxMeasurementType);
            this.panel1.Controls.Add(this.lblHeight);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tbxFovWidth);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbxFovHeight);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(264, 213);
            this.panel1.TabIndex = 1;
            // 
            // lblUnit
            // 
            this.lblUnit.Location = new System.Drawing.Point(9, 12);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(53, 23);
            this.lblUnit.TabIndex = 11;
            this.lblUnit.Text = "Unit :";
            this.lblUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxMeasurementUnit
            // 
            this.cbxMeasurementUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMeasurementUnit.FormattingEnabled = true;
            this.cbxMeasurementUnit.Location = new System.Drawing.Point(68, 12);
            this.cbxMeasurementUnit.Name = "cbxMeasurementUnit";
            this.cbxMeasurementUnit.Size = new System.Drawing.Size(186, 20);
            this.cbxMeasurementUnit.TabIndex = 12;
            this.cbxMeasurementUnit.SelectedIndexChanged += new System.EventHandler(this.cbxMeasurementUnit_SelectedIndexChanged);
            // 
            // MeasurementSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 264);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MeasurementSettingForm";
            this.Text = "Measurement";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MeasurementSettingForm_FormClosed);
            this.Load += new System.EventHandler(this.MeasurementSettingForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFovMessage;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.TextBox tbxFovWidth;
        private System.Windows.Forms.TextBox tbxFovHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFovApply;
        private System.Windows.Forms.ComboBox cbxMeasurementType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Button btnCalibratePixelSize;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxMeasurementUnit;
        private System.Windows.Forms.Label lblUnit;
    }
}