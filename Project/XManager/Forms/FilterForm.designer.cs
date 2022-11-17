namespace XManager.Forms
{
    partial class FilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterForm));
            this.lbxFilterList = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUserFilterListUp = new System.Windows.Forms.Button();
            this.btnUserFilterListDown = new System.Windows.Forms.Button();
            this.lbxUseFilterList = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUseFilter = new System.Windows.Forms.Button();
            this.btnDeleteFilter = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFilterSave = new System.Windows.Forms.Button();
            this.btnFilterApply = new System.Windows.Forms.Button();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterOptionPanel = new System.Windows.Forms.Panel();
            this.lblFilterDescription = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbxFilterList
            // 
            this.lbxFilterList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxFilterList.FormattingEnabled = true;
            this.lbxFilterList.ItemHeight = 12;
            this.lbxFilterList.Location = new System.Drawing.Point(3, 3);
            this.lbxFilterList.Name = "lbxFilterList";
            this.lbxFilterList.Size = new System.Drawing.Size(208, 452);
            this.lbxFilterList.TabIndex = 0;
            this.lbxFilterList.DoubleClick += new System.EventHandler(this.lbxFilterList_DoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbxUseFilterList, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbxFilterList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(927, 458);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnUserFilterListDown, 0, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(466, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(29, 452);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnUserFilterListUp);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 191);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(23, 29);
            this.panel2.TabIndex = 0;
            // 
            // btnUserFilterListUp
            // 
            this.btnUserFilterListUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUserFilterListUp.Location = new System.Drawing.Point(0, 0);
            this.btnUserFilterListUp.Name = "btnUserFilterListUp";
            this.btnUserFilterListUp.Size = new System.Drawing.Size(23, 29);
            this.btnUserFilterListUp.TabIndex = 0;
            this.btnUserFilterListUp.Text = "▲";
            this.btnUserFilterListUp.UseVisualStyleBackColor = true;
            this.btnUserFilterListUp.Click += new System.EventHandler(this.btnUserFilterListUp_Click);
            // 
            // btnUserFilterListDown
            // 
            this.btnUserFilterListDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUserFilterListDown.Location = new System.Drawing.Point(3, 231);
            this.btnUserFilterListDown.Name = "btnUserFilterListDown";
            this.btnUserFilterListDown.Size = new System.Drawing.Size(23, 29);
            this.btnUserFilterListDown.TabIndex = 1;
            this.btnUserFilterListDown.Text = "▼";
            this.btnUserFilterListDown.UseVisualStyleBackColor = true;
            this.btnUserFilterListDown.Click += new System.EventHandler(this.btnUserFilterListDown_Click);
            // 
            // lbxUseFilterList
            // 
            this.lbxUseFilterList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxUseFilterList.FormattingEnabled = true;
            this.lbxUseFilterList.ItemHeight = 12;
            this.lbxUseFilterList.Location = new System.Drawing.Point(252, 3);
            this.lbxUseFilterList.Name = "lbxUseFilterList";
            this.lbxUseFilterList.Size = new System.Drawing.Size(208, 452);
            this.lbxUseFilterList.TabIndex = 2;
            this.lbxUseFilterList.Click += new System.EventHandler(this.lbxUseFilterList_Click);
            this.lbxUseFilterList.DoubleClick += new System.EventHandler(this.lbxUseFilterList_DoubleClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnDeleteFilter, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(217, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(29, 452);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUseFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 191);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(23, 29);
            this.panel1.TabIndex = 0;
            // 
            // btnUseFilter
            // 
            this.btnUseFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUseFilter.Location = new System.Drawing.Point(0, 0);
            this.btnUseFilter.Name = "btnUseFilter";
            this.btnUseFilter.Size = new System.Drawing.Size(23, 29);
            this.btnUseFilter.TabIndex = 0;
            this.btnUseFilter.Text = "▶";
            this.btnUseFilter.UseVisualStyleBackColor = true;
            this.btnUseFilter.Click += new System.EventHandler(this.btnUseFilter_Click);
            // 
            // btnDeleteFilter
            // 
            this.btnDeleteFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteFilter.Location = new System.Drawing.Point(3, 231);
            this.btnDeleteFilter.Name = "btnDeleteFilter";
            this.btnDeleteFilter.Size = new System.Drawing.Size(23, 29);
            this.btnDeleteFilter.TabIndex = 1;
            this.btnDeleteFilter.Text = "◀";
            this.btnDeleteFilter.UseVisualStyleBackColor = true;
            this.btnDeleteFilter.Click += new System.EventHandler(this.btnDeleteFilter_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel11, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(501, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(423, 452);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.btnFilterSave, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnFilterApply, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 415);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(417, 34);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // btnFilterSave
            // 
            this.btnFilterSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFilterSave.Location = new System.Drawing.Point(211, 3);
            this.btnFilterSave.Name = "btnFilterSave";
            this.btnFilterSave.Size = new System.Drawing.Size(203, 28);
            this.btnFilterSave.TabIndex = 2;
            this.btnFilterSave.Text = "Save";
            this.btnFilterSave.UseVisualStyleBackColor = true;
            this.btnFilterSave.Click += new System.EventHandler(this.btnFilterSave_Click);
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFilterApply.Location = new System.Drawing.Point(3, 3);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.Size = new System.Drawing.Size(202, 28);
            this.btnFilterApply.TabIndex = 1;
            this.btnFilterApply.Text = "Apply";
            this.btnFilterApply.UseVisualStyleBackColor = true;
            this.btnFilterApply.Click += new System.EventHandler(this.btnFilterApply_Click);
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.FilterOptionPanel, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.lblFilterDescription, 0, 1);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 2;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(417, 406);
            this.tableLayoutPanel11.TabIndex = 1;
            // 
            // FilterOptionPanel
            // 
            this.FilterOptionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterOptionPanel.Location = new System.Drawing.Point(3, 3);
            this.FilterOptionPanel.Name = "FilterOptionPanel";
            this.FilterOptionPanel.Size = new System.Drawing.Size(411, 280);
            this.FilterOptionPanel.TabIndex = 1;
            // 
            // lblFilterDescription
            // 
            this.lblFilterDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFilterDescription.Location = new System.Drawing.Point(3, 286);
            this.lblFilterDescription.Name = "lblFilterDescription";
            this.lblFilterDescription.Size = new System.Drawing.Size(411, 120);
            this.lblFilterDescription.TabIndex = 2;
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 458);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FilterForm";
            this.Text = "Filter";
            this.Activated += new System.EventHandler(this.FilterForm_Activated);
            this.Deactivate += new System.EventHandler(this.FilterForm_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FilterForm_FormClosed);
            this.Load += new System.EventHandler(this.FilterDialog_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxFilterList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox lbxUseFilterList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUseFilter;
        private System.Windows.Forms.Button btnDeleteFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnUserFilterListUp;
        private System.Windows.Forms.Button btnUserFilterListDown;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnFilterApply;
        private System.Windows.Forms.Panel FilterOptionPanel;
        private System.Windows.Forms.Button btnFilterSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.Label lblFilterDescription;
    }
}