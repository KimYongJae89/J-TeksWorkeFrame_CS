namespace ImageManipulator.Forms
{
    partial class ImageTagViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageTagViewForm));
            this.gridTaginfo = new System.Windows.Forms.DataGridView();
            this.colTagCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTagVal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridTaginfo)).BeginInit();
            this.SuspendLayout();
            // 
            // gridTaginfo
            // 
            this.gridTaginfo.AllowUserToAddRows = false;
            this.gridTaginfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTaginfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTaginfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTagCode,
            this.colTagVal});
            this.gridTaginfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTaginfo.Location = new System.Drawing.Point(0, 0);
            this.gridTaginfo.Name = "gridTaginfo";
            this.gridTaginfo.RowHeadersVisible = false;
            this.gridTaginfo.RowTemplate.Height = 23;
            this.gridTaginfo.Size = new System.Drawing.Size(311, 444);
            this.gridTaginfo.TabIndex = 0;
            // 
            // colTagCode
            // 
            this.colTagCode.HeaderText = "Tag Code";
            this.colTagCode.Name = "colTagCode";
            this.colTagCode.ReadOnly = true;
            this.colTagCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colTagVal
            // 
            this.colTagVal.HeaderText = "Value";
            this.colTagVal.Name = "colTagVal";
            this.colTagVal.ReadOnly = true;
            // 
            // ImageTagViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 444);
            this.Controls.Add(this.gridTaginfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImageTagViewForm";
            this.Text = "Tag Info";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ImageTagViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridTaginfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridTaginfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagVal;
    }
}