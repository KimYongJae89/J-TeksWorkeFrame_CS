namespace KiyControls.Forms
{
    partial class DicomOpenForm
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
            this.pnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlThumb = new System.Windows.Forms.Panel();
            this.thnailView = new KiyControls.Controls.ThumbnailView();
            this.pnlEtc = new System.Windows.Forms.Panel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbxNumberOfThumbnails = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nuUpDnIndex = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlThumb.SuspendLayout();
            this.pnlEtc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuUpDnIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.pnlMain.ColumnCount = 1;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Controls.Add(this.pnlThumb, 0, 0);
            this.pnlMain.Controls.Add(this.pnlEtc, 0, 1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 2;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.pnlMain.Size = new System.Drawing.Size(984, 312);
            this.pnlMain.TabIndex = 2;
            // 
            // pnlThumb
            // 
            this.pnlThumb.Controls.Add(this.thnailView);
            this.pnlThumb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlThumb.Location = new System.Drawing.Point(1, 1);
            this.pnlThumb.Margin = new System.Windows.Forms.Padding(0);
            this.pnlThumb.Name = "pnlThumb";
            this.pnlThumb.Size = new System.Drawing.Size(982, 279);
            this.pnlThumb.TabIndex = 2;
            // 
            // thnailView
            // 
            this.thnailView.CellHeight = 262;
            this.thnailView.CellWidth = 262;
            this.thnailView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thnailView.Location = new System.Drawing.Point(0, 0);
            this.thnailView.Margin = new System.Windows.Forms.Padding(0);
            this.thnailView.Name = "thnailView";
            this.thnailView.PaddingOfEachCell = 20;
            this.thnailView.Size = new System.Drawing.Size(982, 279);
            this.thnailView.TabIndex = 0;
            // 
            // pnlEtc
            // 
            this.pnlEtc.Controls.Add(this.btnSelect);
            this.pnlEtc.Controls.Add(this.tbxNumberOfThumbnails);
            this.pnlEtc.Controls.Add(this.label2);
            this.pnlEtc.Controls.Add(this.nuUpDnIndex);
            this.pnlEtc.Controls.Add(this.label1);
            this.pnlEtc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEtc.Location = new System.Drawing.Point(1, 281);
            this.pnlEtc.Margin = new System.Windows.Forms.Padding(0);
            this.pnlEtc.Name = "pnlEtc";
            this.pnlEtc.Size = new System.Drawing.Size(982, 30);
            this.pnlEtc.TabIndex = 3;
            // 
            // btnSelect
            // 
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelect.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSelect.Location = new System.Drawing.Point(882, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(100, 30);
            this.btnSelect.TabIndex = 8;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tbxNumberOfThumbnails
            // 
            this.tbxNumberOfThumbnails.Location = new System.Drawing.Point(49, 5);
            this.tbxNumberOfThumbnails.Name = "tbxNumberOfThumbnails";
            this.tbxNumberOfThumbnails.ReadOnly = true;
            this.tbxNumberOfThumbnails.Size = new System.Drawing.Size(50, 21);
            this.tbxNumberOfThumbnails.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(5, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Count:";
            // 
            // nuUpDnIndex
            // 
            this.nuUpDnIndex.Location = new System.Drawing.Point(156, 5);
            this.nuUpDnIndex.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nuUpDnIndex.Name = "nuUpDnIndex";
            this.nuUpDnIndex.Size = new System.Drawing.Size(50, 21);
            this.nuUpDnIndex.TabIndex = 5;
            this.nuUpDnIndex.ValueChanged += new System.EventHandler(this.nuUpDnIndex_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(114, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Index:";
            // 
            // DicomOpenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 312);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DicomOpenForm";
            this.Text = "Dicom Frames";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DicomOpenForm_FormClosed);
            this.Load += new System.EventHandler(this.DicomOpenForm_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlThumb.ResumeLayout(false);
            this.pnlEtc.ResumeLayout(false);
            this.pnlEtc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuUpDnIndex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.Panel pnlThumb;
        private Controls.ThumbnailView thnailView;
        private System.Windows.Forms.Panel pnlEtc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nuUpDnIndex;
        private System.Windows.Forms.TextBox tbxNumberOfThumbnails;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelect;
    }
}