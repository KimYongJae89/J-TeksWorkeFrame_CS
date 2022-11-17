namespace ImageManipulator.Forms
{
    partial class SeparateColorImageForm
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCovertBlueChannel = new System.Windows.Forms.Button();
            this.BluePanel = new System.Windows.Forms.Panel();
            this.btnCovertGreenChannel = new System.Windows.Forms.Button();
            this.GreenPanel = new System.Windows.Forms.Panel();
            this.btnCovertRedChannel = new System.Windows.Forms.Button();
            this.RedPanel = new System.Windows.Forms.Panel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.pbxMain = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.66667F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.MainPanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(748, 520);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnCovertBlueChannel, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.BluePanel, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnCovertGreenChannel, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.GreenPanel, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnCovertRedChannel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.RedPanel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(439, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(306, 514);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnCovertBlueChannel
            // 
            this.btnCovertBlueChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCovertBlueChannel.Location = new System.Drawing.Point(3, 486);
            this.btnCovertBlueChannel.Name = "btnCovertBlueChannel";
            this.btnCovertBlueChannel.Size = new System.Drawing.Size(300, 25);
            this.btnCovertBlueChannel.TabIndex = 5;
            this.btnCovertBlueChannel.Text = "Convert blue channel to 8-bit grey image";
            this.btnCovertBlueChannel.UseVisualStyleBackColor = true;
            this.btnCovertBlueChannel.Click += new System.EventHandler(this.btnCovertBlueChannel_Click);
            // 
            // BluePanel
            // 
            this.BluePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BluePanel.Location = new System.Drawing.Point(3, 345);
            this.BluePanel.Name = "BluePanel";
            this.BluePanel.Size = new System.Drawing.Size(300, 135);
            this.BluePanel.TabIndex = 4;
            // 
            // btnCovertGreenChannel
            // 
            this.btnCovertGreenChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCovertGreenChannel.Location = new System.Drawing.Point(3, 315);
            this.btnCovertGreenChannel.Name = "btnCovertGreenChannel";
            this.btnCovertGreenChannel.Size = new System.Drawing.Size(300, 24);
            this.btnCovertGreenChannel.TabIndex = 3;
            this.btnCovertGreenChannel.Text = "Convert green channel to 8-bit grey image";
            this.btnCovertGreenChannel.UseVisualStyleBackColor = true;
            this.btnCovertGreenChannel.Click += new System.EventHandler(this.btnCovertGreenChannel_Click);
            // 
            // GreenPanel
            // 
            this.GreenPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GreenPanel.Location = new System.Drawing.Point(3, 174);
            this.GreenPanel.Name = "GreenPanel";
            this.GreenPanel.Size = new System.Drawing.Size(300, 135);
            this.GreenPanel.TabIndex = 2;
            // 
            // btnCovertRedChannel
            // 
            this.btnCovertRedChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCovertRedChannel.Location = new System.Drawing.Point(3, 144);
            this.btnCovertRedChannel.Name = "btnCovertRedChannel";
            this.btnCovertRedChannel.Size = new System.Drawing.Size(300, 24);
            this.btnCovertRedChannel.TabIndex = 0;
            this.btnCovertRedChannel.Text = "Convert red channel to 8-bit grey image";
            this.btnCovertRedChannel.UseVisualStyleBackColor = true;
            this.btnCovertRedChannel.Click += new System.EventHandler(this.btnCovertRedChannel_Click);
            // 
            // RedPanel
            // 
            this.RedPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RedPanel.Location = new System.Drawing.Point(3, 3);
            this.RedPanel.Name = "RedPanel";
            this.RedPanel.Size = new System.Drawing.Size(300, 135);
            this.RedPanel.TabIndex = 1;
            // 
            // MainPanel
            // 
            this.MainPanel.AutoScroll = true;
            this.MainPanel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.MainPanel.Controls.Add(this.pbxMain);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(3, 3);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(430, 514);
            this.MainPanel.TabIndex = 1;
            // 
            // pbxMain
            // 
            this.pbxMain.Location = new System.Drawing.Point(35, 128);
            this.pbxMain.Name = "pbxMain";
            this.pbxMain.Size = new System.Drawing.Size(324, 242);
            this.pbxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxMain.TabIndex = 0;
            this.pbxMain.TabStop = false;
            // 
            // SeparateColorImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 520);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SeparateColorImageForm";
            this.Text = "SeparateColorImageForm";
            this.Load += new System.EventHandler(this.SeparateColorImageForm_Load);
            this.Resize += new System.EventHandler(this.SeparateColorImageForm_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.PictureBox pbxMain;
        private System.Windows.Forms.Button btnCovertRedChannel;
        private System.Windows.Forms.Panel GreenPanel;
        private System.Windows.Forms.Panel RedPanel;
        private System.Windows.Forms.Button btnCovertBlueChannel;
        private System.Windows.Forms.Panel BluePanel;
        private System.Windows.Forms.Button btnCovertGreenChannel;
    }
}