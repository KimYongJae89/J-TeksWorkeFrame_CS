namespace ImageManipulator
{
    partial class ImageManipulator
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageManipulator));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadRoiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRoiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.loadFilterListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFilterListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bitConverterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.to8bitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.to16bitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.to24bitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFilesFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.separateColorImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fitToScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.transformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotation90DegreesRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotation90DegreesLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.flipHorizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flipVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.roiListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profileListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.metaInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.cropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.measurementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lengthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.angleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processingTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.measureSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setLanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutImageManipulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.AboutPanel = new System.Windows.Forms.Panel();
            this.ProfilePanel = new System.Windows.Forms.Panel();
            this.MetaInfoPanel = new System.Windows.Forms.Panel();
            this.RoiListPanel = new System.Windows.Forms.Panel();
            this.DrawRulerPanel = new System.Windows.Forms.Panel();
            this.DrawNonePanel = new System.Windows.Forms.Panel();
            this.HistogramPanel = new System.Windows.Forms.Panel();
            this.FilterPanel = new System.Windows.Forms.Panel();
            this.FileOpenPanel = new System.Windows.Forms.Panel();
            this.FileSavePanel = new System.Windows.Forms.Panel();
            this.DrawRoiPanel = new System.Windows.Forms.Panel();
            this.WndLevelingPanel = new System.Windows.Forms.Panel();
            this.DrawProfilePanel = new System.Windows.Forms.Panel();
            this.DrawProtractorPanel = new System.Windows.Forms.Panel();
            this.MainFormToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.combine3ChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStripMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.processToolStripMenuItem,
            this.analyzeToolStripMenuItem,
            this.settingsToolStripMenuItem1});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(453, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.recentFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.loadRoiToolStripMenuItem,
            this.saveRoiToolStripMenuItem,
            this.toolStripSeparator13,
            this.loadFilterListToolStripMenuItem,
            this.saveFilterListToolStripMenuItem,
            this.toolStripSeparator3,
            this.bitConverterToolStripMenuItem1,
            this.toolStripSeparator8,
            this.saveToolStripMenuItem,
            this.toolStripSeparator5,
            this.separateColorImageToolStripMenuItem,
            this.combine3ChannelsToolStripMenuItem,
            this.toolStripSeparator12,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.openFileToolStripMenuItem.Text = "Open File...";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // recentFileToolStripMenuItem
            // 
            this.recentFileToolStripMenuItem.Name = "recentFileToolStripMenuItem";
            this.recentFileToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.recentFileToolStripMenuItem.Text = "Recent File..";
            this.recentFileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.recentFileToolStripMenuItem_DropDownOpening);
            this.recentFileToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.recentFileToolStripMenuItem_DropDownItemClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
            // 
            // loadRoiToolStripMenuItem
            // 
            this.loadRoiToolStripMenuItem.Name = "loadRoiToolStripMenuItem";
            this.loadRoiToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.loadRoiToolStripMenuItem.Text = "Load Roi...";
            this.loadRoiToolStripMenuItem.Click += new System.EventHandler(this.loadRoiToolStripMenuItem_Click);
            // 
            // saveRoiToolStripMenuItem
            // 
            this.saveRoiToolStripMenuItem.Name = "saveRoiToolStripMenuItem";
            this.saveRoiToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.saveRoiToolStripMenuItem.Text = "Save Roi...";
            this.saveRoiToolStripMenuItem.Click += new System.EventHandler(this.saveRoiToolStripMenuItem_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(187, 6);
            // 
            // loadFilterListToolStripMenuItem
            // 
            this.loadFilterListToolStripMenuItem.Name = "loadFilterListToolStripMenuItem";
            this.loadFilterListToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.loadFilterListToolStripMenuItem.Text = "Load FilterList";
            this.loadFilterListToolStripMenuItem.Click += new System.EventHandler(this.loadFilterListToolStripMenuItem_Click);
            // 
            // saveFilterListToolStripMenuItem
            // 
            this.saveFilterListToolStripMenuItem.Name = "saveFilterListToolStripMenuItem";
            this.saveFilterListToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.saveFilterListToolStripMenuItem.Text = "Save FilterList";
            this.saveFilterListToolStripMenuItem.Click += new System.EventHandler(this.saveFilterListToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(187, 6);
            // 
            // bitConverterToolStripMenuItem1
            // 
            this.bitConverterToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.to8bitToolStripMenuItem1,
            this.to16bitToolStripMenuItem1,
            this.to24bitToolStripMenuItem1});
            this.bitConverterToolStripMenuItem1.Name = "bitConverterToolStripMenuItem1";
            this.bitConverterToolStripMenuItem1.Size = new System.Drawing.Size(190, 22);
            this.bitConverterToolStripMenuItem1.Text = "Bit Converter";
            // 
            // to8bitToolStripMenuItem1
            // 
            this.to8bitToolStripMenuItem1.Name = "to8bitToolStripMenuItem1";
            this.to8bitToolStripMenuItem1.Size = new System.Drawing.Size(119, 22);
            this.to8bitToolStripMenuItem1.Text = "To 8bit";
            this.to8bitToolStripMenuItem1.Click += new System.EventHandler(this.to8bitToolStripMenuItem1_Click);
            // 
            // to16bitToolStripMenuItem1
            // 
            this.to16bitToolStripMenuItem1.Name = "to16bitToolStripMenuItem1";
            this.to16bitToolStripMenuItem1.Size = new System.Drawing.Size(119, 22);
            this.to16bitToolStripMenuItem1.Text = "To 16bit";
            this.to16bitToolStripMenuItem1.Click += new System.EventHandler(this.to16bitToolStripMenuItem1_Click);
            // 
            // to24bitToolStripMenuItem1
            // 
            this.to24bitToolStripMenuItem1.Name = "to24bitToolStripMenuItem1";
            this.to24bitToolStripMenuItem1.Size = new System.Drawing.Size(119, 22);
            this.to24bitToolStripMenuItem1.Text = "To 24bit";
            this.to24bitToolStripMenuItem1.Click += new System.EventHandler(this.to24bitToolStripMenuItem1_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(187, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageAsToolStripMenuItem,
            this.saveFilesFoldersToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveImageAsToolStripMenuItem
            // 
            this.saveImageAsToolStripMenuItem.Name = "saveImageAsToolStripMenuItem";
            this.saveImageAsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.saveImageAsToolStripMenuItem.Text = "Save Image As";
            this.saveImageAsToolStripMenuItem.Click += new System.EventHandler(this.saveImageAsToolStripMenuItem_Click);
            // 
            // saveFilesFoldersToolStripMenuItem
            // 
            this.saveFilesFoldersToolStripMenuItem.Name = "saveFilesFoldersToolStripMenuItem";
            this.saveFilesFoldersToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.saveFilesFoldersToolStripMenuItem.Text = "Save Files in Folders";
            this.saveFilesFoldersToolStripMenuItem.Click += new System.EventHandler(this.saveFilesFoldersToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(187, 6);
            // 
            // separateColorImageToolStripMenuItem
            // 
            this.separateColorImageToolStripMenuItem.Name = "separateColorImageToolStripMenuItem";
            this.separateColorImageToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.separateColorImageToolStripMenuItem.Text = "Separate Color Image";
            this.separateColorImageToolStripMenuItem.Click += new System.EventHandler(this.separateColorImageToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fitToScreenToolStripMenuItem,
            this.zoomToolStripMenuItem,
            this.panningToolStripMenuItem,
            this.toolStripSeparator4,
            this.gridToolStripMenuItem,
            this.toolStripSeparator6,
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.toolStripSeparator7,
            this.transformToolStripMenuItem,
            this.toolStripSeparator2,
            this.roiListToolStripMenuItem,
            this.profileListToolStripMenuItem,
            this.toolStripSeparator11,
            this.metaInfoToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.DropDownOpening += new System.EventHandler(this.viewToolStripMenuItem_DropDownOpening);
            // 
            // fitToScreenToolStripMenuItem
            // 
            this.fitToScreenToolStripMenuItem.Name = "fitToScreenToolStripMenuItem";
            this.fitToScreenToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.fitToScreenToolStripMenuItem.Text = "Fit To Screen";
            this.fitToScreenToolStripMenuItem.Click += new System.EventHandler(this.fitToScreenToolStripMenuItem_Click);
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.zoomToolStripMenuItem.Text = "1:1 Zoom";
            this.zoomToolStripMenuItem.Click += new System.EventHandler(this.zoomToolStripMenuItem_Click);
            // 
            // panningToolStripMenuItem
            // 
            this.panningToolStripMenuItem.Name = "panningToolStripMenuItem";
            this.panningToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.panningToolStripMenuItem.Text = "Panning";
            this.panningToolStripMenuItem.Click += new System.EventHandler(this.panningToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(141, 6);
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.gridToolStripMenuItem.Text = "Grid";
            this.gridToolStripMenuItem.Click += new System.EventHandler(this.gridToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(141, 6);
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.zoomInToolStripMenuItem.Text = "Zoom In";
            this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.zoomOutToolStripMenuItem.Text = "Zoom Out";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(141, 6);
            // 
            // transformToolStripMenuItem
            // 
            this.transformToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotation90DegreesRightToolStripMenuItem,
            this.rotation90DegreesLeftToolStripMenuItem,
            this.toolStripSeparator9,
            this.flipHorizontallyToolStripMenuItem,
            this.flipVerticallyToolStripMenuItem});
            this.transformToolStripMenuItem.Name = "transformToolStripMenuItem";
            this.transformToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.transformToolStripMenuItem.Text = "Transform";
            // 
            // rotation90DegreesRightToolStripMenuItem
            // 
            this.rotation90DegreesRightToolStripMenuItem.Name = "rotation90DegreesRightToolStripMenuItem";
            this.rotation90DegreesRightToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.rotation90DegreesRightToolStripMenuItem.Text = "Rotation 90 Degrees Right";
            this.rotation90DegreesRightToolStripMenuItem.Click += new System.EventHandler(this.rotation90DegreesRightToolStripMenuItem_Click);
            // 
            // rotation90DegreesLeftToolStripMenuItem
            // 
            this.rotation90DegreesLeftToolStripMenuItem.Name = "rotation90DegreesLeftToolStripMenuItem";
            this.rotation90DegreesLeftToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.rotation90DegreesLeftToolStripMenuItem.Text = "Rotation 90 Degrees Left";
            this.rotation90DegreesLeftToolStripMenuItem.Click += new System.EventHandler(this.rotation90DegreesLeftToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(213, 6);
            // 
            // flipHorizontallyToolStripMenuItem
            // 
            this.flipHorizontallyToolStripMenuItem.Name = "flipHorizontallyToolStripMenuItem";
            this.flipHorizontallyToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.flipHorizontallyToolStripMenuItem.Text = "Flip Horizontally";
            this.flipHorizontallyToolStripMenuItem.Click += new System.EventHandler(this.flipHorizontallyToolStripMenuItem_Click);
            // 
            // flipVerticallyToolStripMenuItem
            // 
            this.flipVerticallyToolStripMenuItem.Name = "flipVerticallyToolStripMenuItem";
            this.flipVerticallyToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.flipVerticallyToolStripMenuItem.Text = "Flip Vertically";
            this.flipVerticallyToolStripMenuItem.Click += new System.EventHandler(this.flipVerticallyToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(141, 6);
            // 
            // roiListToolStripMenuItem
            // 
            this.roiListToolStripMenuItem.Name = "roiListToolStripMenuItem";
            this.roiListToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.roiListToolStripMenuItem.Text = "Roi List";
            this.roiListToolStripMenuItem.Click += new System.EventHandler(this.roiListToolStripMenuItem_Click);
            // 
            // profileListToolStripMenuItem
            // 
            this.profileListToolStripMenuItem.Name = "profileListToolStripMenuItem";
            this.profileListToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.profileListToolStripMenuItem.Text = "Profile List";
            this.profileListToolStripMenuItem.Click += new System.EventHandler(this.profileListToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(141, 6);
            // 
            // metaInfoToolStripMenuItem
            // 
            this.metaInfoToolStripMenuItem.Name = "metaInfoToolStripMenuItem";
            this.metaInfoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.metaInfoToolStripMenuItem.Text = "Meta Info";
            this.metaInfoToolStripMenuItem.Click += new System.EventHandler(this.metaInfoToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filtersToolStripMenuItem,
            this.toolStripSeparator10,
            this.cropToolStripMenuItem,
            this.resizeToolStripMenuItem});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.processToolStripMenuItem.Text = "Process";
            this.processToolStripMenuItem.DropDownOpening += new System.EventHandler(this.processToolStripMenuItem_DropDownOpening);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.filtersToolStripMenuItem.Text = "Filters";
            this.filtersToolStripMenuItem.Click += new System.EventHandler(this.filtersToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(104, 6);
            // 
            // cropToolStripMenuItem
            // 
            this.cropToolStripMenuItem.Name = "cropToolStripMenuItem";
            this.cropToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.cropToolStripMenuItem.Text = "Crop";
            this.cropToolStripMenuItem.Click += new System.EventHandler(this.cropToolStripMenuItem_Click);
            // 
            // resizeToolStripMenuItem
            // 
            this.resizeToolStripMenuItem.Name = "resizeToolStripMenuItem";
            this.resizeToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.resizeToolStripMenuItem.Text = "Resize";
            this.resizeToolStripMenuItem.Click += new System.EventHandler(this.resizeToolStripMenuItem_Click);
            // 
            // analyzeToolStripMenuItem
            // 
            this.analyzeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.histogramToolStripMenuItem,
            this.profileToolStripMenuItem,
            this.roiToolStripMenuItem,
            this.measurementToolStripMenuItem,
            this.processingTimeToolStripMenuItem});
            this.analyzeToolStripMenuItem.Name = "analyzeToolStripMenuItem";
            this.analyzeToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.analyzeToolStripMenuItem.Text = "Analyze";
            this.analyzeToolStripMenuItem.DropDownOpening += new System.EventHandler(this.analyzeToolStripMenuItem_DropDownOpening);
            // 
            // histogramToolStripMenuItem
            // 
            this.histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            this.histogramToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.histogramToolStripMenuItem.Text = "Histogram";
            this.histogramToolStripMenuItem.Click += new System.EventHandler(this.histogramToolStripMenuItem_Click);
            // 
            // profileToolStripMenuItem
            // 
            this.profileToolStripMenuItem.Name = "profileToolStripMenuItem";
            this.profileToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.profileToolStripMenuItem.Text = "Add Profile";
            this.profileToolStripMenuItem.Click += new System.EventHandler(this.profileToolStripMenuItem_Click);
            // 
            // roiToolStripMenuItem
            // 
            this.roiToolStripMenuItem.Name = "roiToolStripMenuItem";
            this.roiToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.roiToolStripMenuItem.Text = "Add Roi";
            this.roiToolStripMenuItem.Click += new System.EventHandler(this.roiToolStripMenuItem_Click);
            // 
            // measurementToolStripMenuItem
            // 
            this.measurementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lengthToolStripMenuItem,
            this.angleToolStripMenuItem});
            this.measurementToolStripMenuItem.Name = "measurementToolStripMenuItem";
            this.measurementToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.measurementToolStripMenuItem.Text = "Measurement";
            // 
            // lengthToolStripMenuItem
            // 
            this.lengthToolStripMenuItem.Name = "lengthToolStripMenuItem";
            this.lengthToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.lengthToolStripMenuItem.Text = "Length";
            this.lengthToolStripMenuItem.Click += new System.EventHandler(this.lengthToolStripMenuItem_Click);
            // 
            // angleToolStripMenuItem
            // 
            this.angleToolStripMenuItem.Name = "angleToolStripMenuItem";
            this.angleToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.angleToolStripMenuItem.Text = "Angle";
            this.angleToolStripMenuItem.Click += new System.EventHandler(this.angleToolStripMenuItem_Click);
            // 
            // processingTimeToolStripMenuItem
            // 
            this.processingTimeToolStripMenuItem.Name = "processingTimeToolStripMenuItem";
            this.processingTimeToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.processingTimeToolStripMenuItem.Text = "Processing Time";
            this.processingTimeToolStripMenuItem.Click += new System.EventHandler(this.processingTimeToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.measureSettingToolStripMenuItem,
            this.setLanguageToolStripMenuItem,
            this.aboutImageManipulatorToolStripMenuItem});
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(62, 20);
            this.settingsToolStripMenuItem1.Text = "Settings";
            // 
            // measureSettingToolStripMenuItem
            // 
            this.measureSettingToolStripMenuItem.Name = "measureSettingToolStripMenuItem";
            this.measureSettingToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.measureSettingToolStripMenuItem.Text = "Set Measurement";
            this.measureSettingToolStripMenuItem.Click += new System.EventHandler(this.measureSettingToolStripMenuItem_Click);
            // 
            // setLanguageToolStripMenuItem
            // 
            this.setLanguageToolStripMenuItem.Name = "setLanguageToolStripMenuItem";
            this.setLanguageToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.setLanguageToolStripMenuItem.Text = "Set Language";
            this.setLanguageToolStripMenuItem.Click += new System.EventHandler(this.setLanguageToolStripMenuItem_Click);
            // 
            // aboutImageManipulatorToolStripMenuItem
            // 
            this.aboutImageManipulatorToolStripMenuItem.Name = "aboutImageManipulatorToolStripMenuItem";
            this.aboutImageManipulatorToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.aboutImageManipulatorToolStripMenuItem.Text = "About ImageManipulator";
            this.aboutImageManipulatorToolStripMenuItem.Click += new System.EventHandler(this.aboutImageManipulatorToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 14;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142857F));
            this.tableLayoutPanel1.Controls.Add(this.AboutPanel, 13, 0);
            this.tableLayoutPanel1.Controls.Add(this.ProfilePanel, 11, 0);
            this.tableLayoutPanel1.Controls.Add(this.MetaInfoPanel, 12, 0);
            this.tableLayoutPanel1.Controls.Add(this.RoiListPanel, 10, 0);
            this.tableLayoutPanel1.Controls.Add(this.DrawRulerPanel, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.DrawNonePanel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.HistogramPanel, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.FilterPanel, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.FileOpenPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.FileSavePanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.DrawRoiPanel, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.WndLevelingPanel, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.DrawProfilePanel, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.DrawProtractorPanel, 6, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(453, 28);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // AboutPanel
            // 
            this.AboutPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AboutPanel.BackgroundImage")));
            this.AboutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AboutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AboutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AboutPanel.Location = new System.Drawing.Point(416, 0);
            this.AboutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.AboutPanel.Name = "AboutPanel";
            this.AboutPanel.Size = new System.Drawing.Size(37, 28);
            this.AboutPanel.TabIndex = 13;
            this.AboutPanel.Click += new System.EventHandler(this.AboutPanel_Click);
            // 
            // ProfilePanel
            // 
            this.ProfilePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ProfilePanel.BackgroundImage")));
            this.ProfilePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ProfilePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProfilePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProfilePanel.Location = new System.Drawing.Point(352, 0);
            this.ProfilePanel.Margin = new System.Windows.Forms.Padding(0);
            this.ProfilePanel.Name = "ProfilePanel";
            this.ProfilePanel.Size = new System.Drawing.Size(32, 28);
            this.ProfilePanel.TabIndex = 11;
            this.ProfilePanel.Click += new System.EventHandler(this.ProfilePanel_Click);
            // 
            // MetaInfoPanel
            // 
            this.MetaInfoPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MetaInfoPanel.BackgroundImage")));
            this.MetaInfoPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MetaInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MetaInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MetaInfoPanel.Location = new System.Drawing.Point(384, 0);
            this.MetaInfoPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MetaInfoPanel.Name = "MetaInfoPanel";
            this.MetaInfoPanel.Size = new System.Drawing.Size(32, 28);
            this.MetaInfoPanel.TabIndex = 12;
            this.MetaInfoPanel.Click += new System.EventHandler(this.MetaInfoPanel_Click);
            // 
            // RoiListPanel
            // 
            this.RoiListPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RoiListPanel.BackgroundImage")));
            this.RoiListPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.RoiListPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RoiListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoiListPanel.Location = new System.Drawing.Point(320, 0);
            this.RoiListPanel.Margin = new System.Windows.Forms.Padding(0);
            this.RoiListPanel.Name = "RoiListPanel";
            this.RoiListPanel.Size = new System.Drawing.Size(32, 28);
            this.RoiListPanel.TabIndex = 16;
            this.RoiListPanel.Click += new System.EventHandler(this.RoiListPanel_Click);
            // 
            // DrawRulerPanel
            // 
            this.DrawRulerPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DrawRulerPanel.BackgroundImage")));
            this.DrawRulerPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DrawRulerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrawRulerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawRulerPanel.Location = new System.Drawing.Point(160, 0);
            this.DrawRulerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.DrawRulerPanel.Name = "DrawRulerPanel";
            this.DrawRulerPanel.Size = new System.Drawing.Size(32, 28);
            this.DrawRulerPanel.TabIndex = 12;
            this.DrawRulerPanel.Click += new System.EventHandler(this.DrawRulerPanel_Click);
            // 
            // DrawNonePanel
            // 
            this.DrawNonePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DrawNonePanel.BackgroundImage")));
            this.DrawNonePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DrawNonePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrawNonePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawNonePanel.Location = new System.Drawing.Point(64, 0);
            this.DrawNonePanel.Margin = new System.Windows.Forms.Padding(0);
            this.DrawNonePanel.Name = "DrawNonePanel";
            this.DrawNonePanel.Size = new System.Drawing.Size(32, 28);
            this.DrawNonePanel.TabIndex = 10;
            this.DrawNonePanel.Click += new System.EventHandler(this.DrawNonePanel_Click);
            // 
            // HistogramPanel
            // 
            this.HistogramPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("HistogramPanel.BackgroundImage")));
            this.HistogramPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.HistogramPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HistogramPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HistogramPanel.Location = new System.Drawing.Point(288, 0);
            this.HistogramPanel.Margin = new System.Windows.Forms.Padding(0);
            this.HistogramPanel.Name = "HistogramPanel";
            this.HistogramPanel.Size = new System.Drawing.Size(32, 28);
            this.HistogramPanel.TabIndex = 15;
            this.HistogramPanel.Click += new System.EventHandler(this.HistogramPanel_Click);
            // 
            // FilterPanel
            // 
            this.FilterPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("FilterPanel.BackgroundImage")));
            this.FilterPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FilterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FilterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterPanel.Location = new System.Drawing.Point(256, 0);
            this.FilterPanel.Margin = new System.Windows.Forms.Padding(0);
            this.FilterPanel.Name = "FilterPanel";
            this.FilterPanel.Size = new System.Drawing.Size(32, 28);
            this.FilterPanel.TabIndex = 14;
            this.FilterPanel.Click += new System.EventHandler(this.FilterPanel_Click);
            // 
            // FileOpenPanel
            // 
            this.FileOpenPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("FileOpenPanel.BackgroundImage")));
            this.FileOpenPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FileOpenPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FileOpenPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileOpenPanel.Location = new System.Drawing.Point(0, 0);
            this.FileOpenPanel.Margin = new System.Windows.Forms.Padding(0);
            this.FileOpenPanel.Name = "FileOpenPanel";
            this.FileOpenPanel.Size = new System.Drawing.Size(32, 28);
            this.FileOpenPanel.TabIndex = 8;
            this.FileOpenPanel.Click += new System.EventHandler(this.FileOpenPanel_Click);
            // 
            // FileSavePanel
            // 
            this.FileSavePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("FileSavePanel.BackgroundImage")));
            this.FileSavePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FileSavePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FileSavePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileSavePanel.Location = new System.Drawing.Point(32, 0);
            this.FileSavePanel.Margin = new System.Windows.Forms.Padding(0);
            this.FileSavePanel.Name = "FileSavePanel";
            this.FileSavePanel.Size = new System.Drawing.Size(32, 28);
            this.FileSavePanel.TabIndex = 9;
            this.FileSavePanel.Click += new System.EventHandler(this.FileSavePanel_Click);
            // 
            // DrawRoiPanel
            // 
            this.DrawRoiPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DrawRoiPanel.BackgroundImage")));
            this.DrawRoiPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DrawRoiPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrawRoiPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawRoiPanel.Location = new System.Drawing.Point(96, 0);
            this.DrawRoiPanel.Margin = new System.Windows.Forms.Padding(0);
            this.DrawRoiPanel.Name = "DrawRoiPanel";
            this.DrawRoiPanel.Size = new System.Drawing.Size(32, 28);
            this.DrawRoiPanel.TabIndex = 0;
            this.DrawRoiPanel.Click += new System.EventHandler(this.DrawRoiPanel_Click);
            // 
            // WndLevelingPanel
            // 
            this.WndLevelingPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("WndLevelingPanel.BackgroundImage")));
            this.WndLevelingPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.WndLevelingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WndLevelingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WndLevelingPanel.Location = new System.Drawing.Point(224, 0);
            this.WndLevelingPanel.Margin = new System.Windows.Forms.Padding(0);
            this.WndLevelingPanel.Name = "WndLevelingPanel";
            this.WndLevelingPanel.Size = new System.Drawing.Size(32, 28);
            this.WndLevelingPanel.TabIndex = 5;
            this.WndLevelingPanel.Click += new System.EventHandler(this.WndLevelingPanel_Click);
            // 
            // DrawProfilePanel
            // 
            this.DrawProfilePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DrawProfilePanel.BackgroundImage")));
            this.DrawProfilePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DrawProfilePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrawProfilePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawProfilePanel.Location = new System.Drawing.Point(128, 0);
            this.DrawProfilePanel.Margin = new System.Windows.Forms.Padding(0);
            this.DrawProfilePanel.Name = "DrawProfilePanel";
            this.DrawProfilePanel.Size = new System.Drawing.Size(32, 28);
            this.DrawProfilePanel.TabIndex = 4;
            this.DrawProfilePanel.Click += new System.EventHandler(this.DrawProfilePanel_Click);
            // 
            // DrawProtractorPanel
            // 
            this.DrawProtractorPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DrawProtractorPanel.BackgroundImage")));
            this.DrawProtractorPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DrawProtractorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrawProtractorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawProtractorPanel.Location = new System.Drawing.Point(192, 0);
            this.DrawProtractorPanel.Margin = new System.Windows.Forms.Padding(0);
            this.DrawProtractorPanel.Name = "DrawProtractorPanel";
            this.DrawProtractorPanel.Size = new System.Drawing.Size(32, 28);
            this.DrawProtractorPanel.TabIndex = 13;
            this.DrawProtractorPanel.Click += new System.EventHandler(this.DrawProtractorPanel_Click);
            // 
            // MainFormToolTip
            // 
            this.MainFormToolTip.AutomaticDelay = 250;
            this.MainFormToolTip.ShowAlways = true;
            // 
            // combine3ChannelsToolStripMenuItem
            // 
            this.combine3ChannelsToolStripMenuItem.Name = "combine3ChannelsToolStripMenuItem";
            this.combine3ChannelsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.combine3ChannelsToolStripMenuItem.Text = "Combine 3 Channels";
            this.combine3ChannelsToolStripMenuItem.Click += new System.EventHandler(this.combine3ChannelsToolStripMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(187, 6);
            // 
            // ImageManipulator
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 52);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStripMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "ImageManipulator";
            this.Text = "ImageManipulator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ImageManipulator_FormClosed);
            this.Load += new System.EventHandler(this.ImageManipulator_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ImageManipulator_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ImageManipulator_DragEnter);
            this.Resize += new System.EventHandler(this.ImageManipulator_Resize);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem loadRoiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveRoiToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFilesFoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fitToScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panningToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem transformToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotation90DegreesRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotation90DegreesLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem flipHorizontallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flipVerticallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roiListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metaInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cropToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem measurementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lengthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem angleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem profileListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem measureSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutImageManipulatorToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel DrawRoiPanel;
        private System.Windows.Forms.Panel DrawProfilePanel;
        private System.Windows.Forms.Panel FileOpenPanel;
        private System.Windows.Forms.Panel WndLevelingPanel;
        private System.Windows.Forms.Panel RoiListPanel;
        private System.Windows.Forms.Panel HistogramPanel;
        private System.Windows.Forms.Panel FilterPanel;
        private System.Windows.Forms.Panel DrawProtractorPanel;
        private System.Windows.Forms.Panel DrawRulerPanel;
        private System.Windows.Forms.Panel ProfilePanel;
        private System.Windows.Forms.Panel DrawNonePanel;
        private System.Windows.Forms.Panel FileSavePanel;
        private System.Windows.Forms.Panel AboutPanel;
        private System.Windows.Forms.Panel MetaInfoPanel;
        private System.Windows.Forms.ToolTip MainFormToolTip;
        private System.Windows.Forms.ToolStripMenuItem bitConverterToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem to8bitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem to16bitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem to24bitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem setLanguageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem saveFilterListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFilterListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem processingTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem separateColorImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem combine3ChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
    }
}

