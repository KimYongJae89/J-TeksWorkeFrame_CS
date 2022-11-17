namespace ImageManipulator.Forms
{
    partial class Combine3ChannelForm
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
            this.btnOpenBlueChannel = new System.Windows.Forms.Button();
            this.BluePanel = new System.Windows.Forms.Panel();
            this.btnOpenGreenChannel = new System.Windows.Forms.Button();
            this.GreenPanel = new System.Windows.Forms.Panel();
            this.btnOpenRedChannel = new System.Windows.Forms.Button();
            this.RedPanel = new System.Windows.Forms.Panel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.pbxMain = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnConvertToImage = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMain)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
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
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnOpenBlueChannel, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.BluePanel, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnOpenGreenChannel, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.GreenPanel, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnOpenRedChannel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.RedPanel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 6);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(439, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(306, 514);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnOpenBlueChannel
            // 
            this.btnOpenBlueChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenBlueChannel.Location = new System.Drawing.Point(3, 456);
            this.btnOpenBlueChannel.Name = "btnOpenBlueChannel";
            this.btnOpenBlueChannel.Size = new System.Drawing.Size(300, 24);
            this.btnOpenBlueChannel.TabIndex = 5;
            this.btnOpenBlueChannel.Text = "Open Blue Channel";
            this.btnOpenBlueChannel.UseVisualStyleBackColor = true;
            this.btnOpenBlueChannel.Click += new System.EventHandler(this.btnOpenBlueChannel_Click);
            // 
            // BluePanel
            // 
            this.BluePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BluePanel.Location = new System.Drawing.Point(3, 325);
            this.BluePanel.Name = "BluePanel";
            this.BluePanel.Size = new System.Drawing.Size(300, 125);
            this.BluePanel.TabIndex = 4;
            // 
            // btnOpenGreenChannel
            // 
            this.btnOpenGreenChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenGreenChannel.Location = new System.Drawing.Point(3, 295);
            this.btnOpenGreenChannel.Name = "btnOpenGreenChannel";
            this.btnOpenGreenChannel.Size = new System.Drawing.Size(300, 24);
            this.btnOpenGreenChannel.TabIndex = 3;
            this.btnOpenGreenChannel.Text = "Open Green Channel";
            this.btnOpenGreenChannel.UseVisualStyleBackColor = true;
            this.btnOpenGreenChannel.Click += new System.EventHandler(this.btnOpenGreenChannel_Click);
            // 
            // GreenPanel
            // 
            this.GreenPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GreenPanel.Location = new System.Drawing.Point(3, 164);
            this.GreenPanel.Name = "GreenPanel";
            this.GreenPanel.Size = new System.Drawing.Size(300, 125);
            this.GreenPanel.TabIndex = 2;
            // 
            // btnOpenRedChannel
            // 
            this.btnOpenRedChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenRedChannel.Location = new System.Drawing.Point(3, 134);
            this.btnOpenRedChannel.Name = "btnOpenRedChannel";
            this.btnOpenRedChannel.Size = new System.Drawing.Size(300, 24);
            this.btnOpenRedChannel.TabIndex = 0;
            this.btnOpenRedChannel.Text = "Open Red Channel";
            this.btnOpenRedChannel.UseVisualStyleBackColor = true;
            this.btnOpenRedChannel.Click += new System.EventHandler(this.btnOpenRedChannel_Click);
            // 
            // RedPanel
            // 
            this.RedPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RedPanel.Location = new System.Drawing.Point(3, 3);
            this.RedPanel.Name = "RedPanel";
            this.RedPanel.Size = new System.Drawing.Size(300, 125);
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
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btnConvertToImage, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnPreview, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 483);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(306, 31);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // btnPreview
            // 
            this.btnPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPreview.Location = new System.Drawing.Point(3, 3);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(147, 25);
            this.btnPreview.TabIndex = 0;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnConvertToImage
            // 
            this.btnConvertToImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConvertToImage.Location = new System.Drawing.Point(156, 3);
            this.btnConvertToImage.Name = "btnConvertToImage";
            this.btnConvertToImage.Size = new System.Drawing.Size(147, 25);
            this.btnConvertToImage.TabIndex = 1;
            this.btnConvertToImage.Text = "convert to image";
            this.btnConvertToImage.UseVisualStyleBackColor = true;
            this.btnConvertToImage.Click += new System.EventHandler(this.btnConvertToImage_Click);
            // 
            // Combine3ChannelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 520);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Combine3ChannelForm";
            this.Text = "Combine3ChannelForm";
            this.Load += new System.EventHandler(this.Combine3ChannelForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxMain)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnOpenBlueChannel;
        private System.Windows.Forms.Panel BluePanel;
        private System.Windows.Forms.Button btnOpenGreenChannel;
        private System.Windows.Forms.Panel GreenPanel;
        private System.Windows.Forms.Button btnOpenRedChannel;
        private System.Windows.Forms.Panel RedPanel;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.PictureBox pbxMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnConvertToImage;
        private System.Windows.Forms.Button btnPreview;
    }
}