namespace ImageManipulator.Forms
{
    partial class ThresholdForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MaintabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.nupdnWeight = new System.Windows.Forms.NumericUpDown();
            this.nupdnBlockSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.cbxOtsuEnable = new System.Windows.Forms.CheckBox();
            this.nupdnValue = new System.Windows.Forms.NumericUpDown();
            this.lblValue = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.MaintabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupdnWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupdnBlockSize)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupdnValue)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(208, 238);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MaintabControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 187);
            this.panel1.TabIndex = 0;
            // 
            // MaintabControl
            // 
            this.MaintabControl.Controls.Add(this.tabPage1);
            this.MaintabControl.Controls.Add(this.tabPage2);
            this.MaintabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MaintabControl.Location = new System.Drawing.Point(0, 0);
            this.MaintabControl.Name = "MaintabControl";
            this.MaintabControl.SelectedIndex = 0;
            this.MaintabControl.Size = new System.Drawing.Size(202, 187);
            this.MaintabControl.TabIndex = 0;
            this.MaintabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.lblValue);
            this.tabPage1.Controls.Add(this.cbxOtsuEnable);
            this.tabPage1.Controls.Add(this.nupdnValue);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(194, 161);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Threshold";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.nupdnWeight);
            this.tabPage2.Controls.Add(this.nupdnBlockSize);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cbxType);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(194, 161);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Adaptive";
            // 
            // nupdnWeight
            // 
            this.nupdnWeight.Location = new System.Drawing.Point(99, 69);
            this.nupdnWeight.Name = "nupdnWeight";
            this.nupdnWeight.Size = new System.Drawing.Size(79, 21);
            this.nupdnWeight.TabIndex = 7;
            this.nupdnWeight.ValueChanged += new System.EventHandler(this.nupdnWeight_ValueChanged);
            // 
            // nupdnBlockSize
            // 
            this.nupdnBlockSize.Location = new System.Drawing.Point(99, 42);
            this.nupdnBlockSize.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nupdnBlockSize.Name = "nupdnBlockSize";
            this.nupdnBlockSize.Size = new System.Drawing.Size(79, 21);
            this.nupdnBlockSize.TabIndex = 6;
            this.nupdnBlockSize.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nupdnBlockSize.ValueChanged += new System.EventHandler(this.nupdnBlockSize_ValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(19, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Weight :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Block Size :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxType
            // 
            this.cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Location = new System.Drawing.Point(99, 16);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(79, 20);
            this.cbxType.TabIndex = 3;
            this.cbxType.SelectedIndexChanged += new System.EventHandler(this.cbxType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Type :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnApply, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 196);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 39);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(100, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 39);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnApply.Location = new System.Drawing.Point(0, 0);
            this.btnApply.Margin = new System.Windows.Forms.Padding(0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(100, 39);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // cbxOtsuEnable
            // 
            this.cbxOtsuEnable.AutoSize = true;
            this.cbxOtsuEnable.Location = new System.Drawing.Point(19, 51);
            this.cbxOtsuEnable.Name = "cbxOtsuEnable";
            this.cbxOtsuEnable.Size = new System.Drawing.Size(49, 16);
            this.cbxOtsuEnable.TabIndex = 3;
            this.cbxOtsuEnable.Text = "Auto";
            this.cbxOtsuEnable.UseVisualStyleBackColor = true;
            this.cbxOtsuEnable.CheckedChanged += new System.EventHandler(this.cbxOtsuEnable_CheckedChanged);
            // 
            // nupdnValue
            // 
            this.nupdnValue.Location = new System.Drawing.Point(78, 16);
            this.nupdnValue.Name = "nupdnValue";
            this.nupdnValue.Size = new System.Drawing.Size(91, 21);
            this.nupdnValue.TabIndex = 2;
            this.nupdnValue.ValueChanged += new System.EventHandler(this.nupdnValue_ValueChanged);
            // 
            // lblValue
            // 
            this.lblValue.Location = new System.Drawing.Point(19, 16);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(53, 23);
            this.lblValue.TabIndex = 1;
            this.lblValue.Text = "Value :";
            this.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ThresholdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 238);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ThresholdForm";
            this.Text = "ThresholdForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ThresholdForm_FormClosed);
            this.Load += new System.EventHandler(this.ThresholdForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.MaintabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nupdnWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupdnBlockSize)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nupdnValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl MaintabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nupdnWeight;
        private System.Windows.Forms.NumericUpDown nupdnBlockSize;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.CheckBox cbxOtsuEnable;
        private System.Windows.Forms.NumericUpDown nupdnValue;
    }
}