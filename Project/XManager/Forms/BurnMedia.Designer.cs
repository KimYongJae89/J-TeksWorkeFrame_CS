namespace XManager.Forms
{
    partial class BurnMedia
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxVolumeLabel = new System.Windows.Forms.TextBox();
            this.cbxdevices = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.statusProgressBar = new System.Windows.Forms.ProgressBar();
            this.labelStatusText = new System.Windows.Forms.Label();
            this.buttonBurn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelMediaType = new System.Windows.Forms.Label();
            this.buttonDetectMedia = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTotalSize = new System.Windows.Forms.Label();
            this.progressBarCapacity = new System.Windows.Forms.ProgressBar();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.comboBoxVerification = new System.Windows.Forms.ComboBox();
            this.labelVerification = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.supportedMediaLabel = new System.Windows.Forms.Label();
            this.backgroundBurnWorker = new System.ComponentModel.BackgroundWorker();
            this.checkBoxEject = new System.Windows.Forms.CheckBox();
            this.checkBoxCloseMedia = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.btnSaveUSB = new System.Windows.Forms.Button();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxAllselect = new System.Windows.Forms.CheckBox();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(13, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Volume Label :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxVolumeLabel
            // 
            this.tbxVolumeLabel.Location = new System.Drawing.Point(116, 76);
            this.tbxVolumeLabel.Name = "tbxVolumeLabel";
            this.tbxVolumeLabel.Size = new System.Drawing.Size(182, 21);
            this.tbxVolumeLabel.TabIndex = 1;
            // 
            // cbxdevices
            // 
            this.cbxdevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxdevices.FormattingEnabled = true;
            this.cbxdevices.Location = new System.Drawing.Point(12, 13);
            this.cbxdevices.Name = "cbxdevices";
            this.cbxdevices.Size = new System.Drawing.Size(286, 20);
            this.cbxdevices.TabIndex = 2;
            this.cbxdevices.SelectedIndexChanged += new System.EventHandler(this.cbxdevices_SelectedIndexChanged);
            this.cbxdevices.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.cbxdevices_Format);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.statusProgressBar);
            this.groupBox3.Controls.Add(this.labelStatusText);
            this.groupBox3.Controls.Add(this.buttonBurn);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox3.Location = new System.Drawing.Point(306, 333);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(286, 127);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Progress";
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Location = new System.Drawing.Point(14, 65);
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(261, 15);
            this.statusProgressBar.TabIndex = 8;
            // 
            // labelStatusText
            // 
            this.labelStatusText.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelStatusText.Location = new System.Drawing.Point(10, 17);
            this.labelStatusText.Name = "labelStatusText";
            this.labelStatusText.Size = new System.Drawing.Size(265, 42);
            this.labelStatusText.TabIndex = 7;
            this.labelStatusText.Text = "status";
            this.labelStatusText.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // buttonBurn
            // 
            this.buttonBurn.BackColor = System.Drawing.Color.DimGray;
            this.buttonBurn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonBurn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonBurn.Location = new System.Drawing.Point(98, 85);
            this.buttonBurn.Name = "buttonBurn";
            this.buttonBurn.Size = new System.Drawing.Size(103, 28);
            this.buttonBurn.TabIndex = 6;
            this.buttonBurn.Text = "&Burn";
            this.buttonBurn.UseVisualStyleBackColor = false;
            this.buttonBurn.Click += new System.EventHandler(this.buttonBurn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelMediaType);
            this.groupBox2.Controls.Add(this.buttonDetectMedia);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.labelTotalSize);
            this.groupBox2.Controls.Add(this.progressBarCapacity);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox2.Location = new System.Drawing.Point(306, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(286, 143);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected media type:";
            // 
            // labelMediaType
            // 
            this.labelMediaType.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelMediaType.Location = new System.Drawing.Point(136, 18);
            this.labelMediaType.Name = "labelMediaType";
            this.labelMediaType.Size = new System.Drawing.Size(139, 37);
            this.labelMediaType.TabIndex = 10;
            this.labelMediaType.Text = "Press \'Detect Media\' Button";
            // 
            // buttonDetectMedia
            // 
            this.buttonDetectMedia.BackColor = System.Drawing.Color.DimGray;
            this.buttonDetectMedia.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonDetectMedia.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonDetectMedia.Location = new System.Drawing.Point(14, 18);
            this.buttonDetectMedia.Name = "buttonDetectMedia";
            this.buttonDetectMedia.Size = new System.Drawing.Size(114, 26);
            this.buttonDetectMedia.TabIndex = 9;
            this.buttonDetectMedia.Text = "Detect Media";
            this.buttonDetectMedia.UseVisualStyleBackColor = false;
            this.buttonDetectMedia.Click += new System.EventHandler(this.buttonDetectMedia_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(7, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "0";
            // 
            // labelTotalSize
            // 
            this.labelTotalSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotalSize.AutoSize = true;
            this.labelTotalSize.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelTotalSize.Location = new System.Drawing.Point(225, 67);
            this.labelTotalSize.Name = "labelTotalSize";
            this.labelTotalSize.Size = new System.Drawing.Size(53, 12);
            this.labelTotalSize.TabIndex = 7;
            this.labelTotalSize.Text = "totalSize";
            // 
            // progressBarCapacity
            // 
            this.progressBarCapacity.Location = new System.Drawing.Point(7, 85);
            this.progressBarCapacity.Name = "progressBarCapacity";
            this.progressBarCapacity.Size = new System.Drawing.Size(268, 15);
            this.progressBarCapacity.Step = 1;
            this.progressBarCapacity.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarCapacity.TabIndex = 8;
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.ItemHeight = 24;
            this.listBoxFiles.Location = new System.Drawing.Point(598, 166);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.ScrollAlwaysVisible = true;
            this.listBoxFiles.Size = new System.Drawing.Size(280, 292);
            this.listBoxFiles.TabIndex = 16;
            this.listBoxFiles.Visible = false;
            // 
            // comboBoxVerification
            // 
            this.comboBoxVerification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVerification.FormattingEnabled = true;
            this.comboBoxVerification.Items.AddRange(new object[] {
            "None",
            "Quick",
            "Full"});
            this.comboBoxVerification.Location = new System.Drawing.Point(116, 50);
            this.comboBoxVerification.Name = "comboBoxVerification";
            this.comboBoxVerification.Size = new System.Drawing.Size(182, 20);
            this.comboBoxVerification.TabIndex = 15;
            this.comboBoxVerification.SelectedIndexChanged += new System.EventHandler(this.comboBoxVerification_SelectedIndexChanged);
            // 
            // labelVerification
            // 
            this.labelVerification.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelVerification.Location = new System.Drawing.Point(12, 53);
            this.labelVerification.Name = "labelVerification";
            this.labelVerification.Size = new System.Drawing.Size(89, 17);
            this.labelVerification.TabIndex = 14;
            this.labelVerification.Text = "Verification:";
            this.labelVerification.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(304, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "[ Supported Media ]";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // supportedMediaLabel
            // 
            this.supportedMediaLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.supportedMediaLabel.Location = new System.Drawing.Point(304, 43);
            this.supportedMediaLabel.Name = "supportedMediaLabel";
            this.supportedMediaLabel.Size = new System.Drawing.Size(286, 110);
            this.supportedMediaLabel.TabIndex = 18;
            // 
            // backgroundBurnWorker
            // 
            this.backgroundBurnWorker.WorkerReportsProgress = true;
            this.backgroundBurnWorker.WorkerSupportsCancellation = true;
            this.backgroundBurnWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundBurnWorker_DoWork);
            this.backgroundBurnWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundBurnWorker_ProgressChanged);
            this.backgroundBurnWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundBurnWorker_RunWorkerCompleted);
            // 
            // checkBoxEject
            // 
            this.checkBoxEject.AutoSize = true;
            this.checkBoxEject.Checked = true;
            this.checkBoxEject.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEject.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.checkBoxEject.Location = new System.Drawing.Point(163, 137);
            this.checkBoxEject.Name = "checkBoxEject";
            this.checkBoxEject.Size = new System.Drawing.Size(135, 16);
            this.checkBoxEject.TabIndex = 20;
            this.checkBoxEject.Text = "Eject when finished";
            this.checkBoxEject.UseVisualStyleBackColor = true;
            // 
            // checkBoxCloseMedia
            // 
            this.checkBoxCloseMedia.AutoSize = true;
            this.checkBoxCloseMedia.Checked = true;
            this.checkBoxCloseMedia.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCloseMedia.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.checkBoxCloseMedia.Location = new System.Drawing.Point(15, 137);
            this.checkBoxCloseMedia.Name = "checkBoxCloseMedia";
            this.checkBoxCloseMedia.Size = new System.Drawing.Size(96, 16);
            this.checkBoxCloseMedia.TabIndex = 19;
            this.checkBoxCloseMedia.Text = "Close media";
            this.checkBoxCloseMedia.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(12, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 23);
            this.label4.TabIndex = 21;
            this.label4.Text = "Type :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxType
            // 
            this.cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Location = new System.Drawing.Point(116, 107);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(107, 20);
            this.cbxType.TabIndex = 22;
            // 
            // btnSaveUSB
            // 
            this.btnSaveUSB.BackColor = System.Drawing.Color.DimGray;
            this.btnSaveUSB.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSaveUSB.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSaveUSB.Location = new System.Drawing.Point(229, 107);
            this.btnSaveUSB.Name = "btnSaveUSB";
            this.btnSaveUSB.Size = new System.Drawing.Size(69, 20);
            this.btnSaveUSB.TabIndex = 9;
            this.btnSaveUSB.Text = "USB Save";
            this.btnSaveUSB.UseVisualStyleBackColor = false;
            this.btnSaveUSB.Click += new System.EventHandler(this.btnSaveUSB_Click);
            // 
            // checkedListBox
            // 
            this.checkedListBox.BackColor = System.Drawing.Color.White;
            this.checkedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkedListBox.ForeColor = System.Drawing.Color.Black;
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(16, 21);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.ScrollAlwaysVisible = true;
            this.checkedListBox.Size = new System.Drawing.Size(251, 238);
            this.checkedListBox.TabIndex = 23;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkedListBox);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Location = new System.Drawing.Point(14, 184);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 276);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File List";
            // 
            // cbxAllselect
            // 
            this.cbxAllselect.AutoSize = true;
            this.cbxAllselect.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cbxAllselect.Location = new System.Drawing.Point(15, 162);
            this.cbxAllselect.Name = "cbxAllselect";
            this.cbxAllselect.Size = new System.Drawing.Size(77, 16);
            this.cbxAllselect.TabIndex = 25;
            this.cbxAllselect.Text = "All Select";
            this.cbxAllselect.UseVisualStyleBackColor = true;
            this.cbxAllselect.CheckedChanged += new System.EventHandler(this.cbxAllselect_CheckedChanged);
            // 
            // BurnMedia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(601, 467);
            this.Controls.Add(this.cbxAllselect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSaveUSB);
            this.Controls.Add(this.cbxType);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxEject);
            this.Controls.Add(this.checkBoxCloseMedia);
            this.Controls.Add(this.supportedMediaLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxVerification);
            this.Controls.Add(this.labelVerification);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cbxdevices);
            this.Controls.Add(this.tbxVolumeLabel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BurnMedia";
            this.Text = "CD/DVD & USB";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BurnMedia_FormClosing);
            this.Load += new System.EventHandler(this.BurnMedia_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxVolumeLabel;
        private System.Windows.Forms.ComboBox cbxdevices;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar statusProgressBar;
        private System.Windows.Forms.Label labelStatusText;
        private System.Windows.Forms.Button buttonBurn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelMediaType;
        private System.Windows.Forms.Button buttonDetectMedia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelTotalSize;
        private System.Windows.Forms.ProgressBar progressBarCapacity;
        private System.Windows.Forms.ComboBox comboBoxVerification;
        private System.Windows.Forms.Label labelVerification;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label supportedMediaLabel;
        private System.ComponentModel.BackgroundWorker backgroundBurnWorker;
        private System.Windows.Forms.CheckBox checkBoxEject;
        private System.Windows.Forms.CheckBox checkBoxCloseMedia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.Button btnSaveUSB;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbxAllselect;
    }
}