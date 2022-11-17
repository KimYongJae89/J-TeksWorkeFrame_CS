namespace XManager.Forms
{
    partial class ProcessingTimeForm
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
            this.lbxProcessingTime = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbxProcessingTime
            // 
            this.lbxProcessingTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxProcessingTime.FormattingEnabled = true;
            this.lbxProcessingTime.ItemHeight = 12;
            this.lbxProcessingTime.Location = new System.Drawing.Point(0, 0);
            this.lbxProcessingTime.Name = "lbxProcessingTime";
            this.lbxProcessingTime.Size = new System.Drawing.Size(284, 262);
            this.lbxProcessingTime.TabIndex = 0;
            // 
            // ProcessingTimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lbxProcessingTime);
            this.Name = "ProcessingTimeForm";
            this.Text = "ProcessingTime";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProcessingTimeForm_FormClosed);
            this.Load += new System.EventHandler(this.ProcessingTimeForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxProcessingTime;
    }
}