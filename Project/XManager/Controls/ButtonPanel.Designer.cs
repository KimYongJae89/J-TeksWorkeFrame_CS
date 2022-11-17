namespace XManager.Controls
{
    partial class ButtonPanel
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ButtonPanel));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.lblAdministrator = new System.Windows.Forms.Label();
            this.lblAcqusitionState = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tblPnl = new System.Windows.Forms.TableLayoutPanel();
            this.lbLoad = new System.Windows.Forms.Label();
            this.AquisitionimageList = new System.Windows.Forms.ImageList(this.components);
            this.lbl_Aquisition = new System.Windows.Forms.Label();
            this.lbHistogram = new System.Windows.Forms.Label();
            this.lbSave = new System.Windows.Forms.Label();
            this.lbl_Rotate_CCW = new System.Windows.Forms.Label();
            this.lbl_Rotate_CW = new System.Windows.Forms.Label();
            this.lbl_flip_horizontal = new System.Windows.Forms.Label();
            this.lbl_flip_vertical = new System.Windows.Forms.Label();
            this.lbRuler = new System.Windows.Forms.Label();
            this.lbProtractor = new System.Windows.Forms.Label();
            this.lbSelectRegion = new System.Windows.Forms.Label();
            this.lbFilter = new System.Windows.Forms.Label();
            this.lbFovSetting = new System.Windows.Forms.Label();
            this.lblProfile = new System.Windows.Forms.Label();
            this.lbReset = new System.Windows.Forms.Label();
            this.lbRegionHisto = new System.Windows.Forms.Label();
            this.lbLineProfile = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lbl_Settings = new System.Windows.Forms.Label();
            this.lbl_Capture = new System.Windows.Forms.Label();
            this.lbl_DSA_View = new System.Windows.Forms.Label();
            this.lbl_CameraSettings = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTestInputImage = new System.Windows.Forms.Button();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.lbl_Exit = new System.Windows.Forms.Label();
            this.timerAcq = new System.Windows.Forms.Timer(this.components);
            this.fileDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFiledlg = new System.Windows.Forms.SaveFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lblProcessingTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tblPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(150, 975);
            this.splitContainer1.SplitterDistance = 296;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.lblAdministrator);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.lblAcqusitionState);
            this.splitContainer4.Size = new System.Drawing.Size(150, 296);
            this.splitContainer4.SplitterDistance = 249;
            this.splitContainer4.TabIndex = 0;
            this.splitContainer4.TabStop = false;
            // 
            // lblAdministrator
            // 
            this.lblAdministrator.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAdministrator.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblAdministrator.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblAdministrator.Location = new System.Drawing.Point(0, 0);
            this.lblAdministrator.Name = "lblAdministrator";
            this.lblAdministrator.Size = new System.Drawing.Size(150, 59);
            this.lblAdministrator.TabIndex = 0;
            this.lblAdministrator.Text = "Administrator";
            this.lblAdministrator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAcqusitionState
            // 
            this.lblAcqusitionState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAcqusitionState.Font = new System.Drawing.Font("HY중고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblAcqusitionState.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblAcqusitionState.Location = new System.Drawing.Point(0, 0);
            this.lblAcqusitionState.Name = "lblAcqusitionState";
            this.lblAcqusitionState.Size = new System.Drawing.Size(150, 43);
            this.lblAcqusitionState.TabIndex = 0;
            this.lblAcqusitionState.Text = "Cam OFF";
            this.lblAcqusitionState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer2
            // 
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tblPnl);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(150, 639);
            this.splitContainer2.SplitterDistance = 478;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // tblPnl
            // 
            this.tblPnl.ColumnCount = 2;
            this.tblPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblPnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblPnl.Controls.Add(this.lblProcessingTime, 0, 8);
            this.tblPnl.Controls.Add(this.lbLoad, 1, 3);
            this.tblPnl.Controls.Add(this.lbl_Aquisition, 0, 0);
            this.tblPnl.Controls.Add(this.lbHistogram, 1, 0);
            this.tblPnl.Controls.Add(this.lbSave, 0, 3);
            this.tblPnl.Controls.Add(this.lbl_Rotate_CCW, 0, 1);
            this.tblPnl.Controls.Add(this.lbl_Rotate_CW, 1, 1);
            this.tblPnl.Controls.Add(this.lbl_flip_horizontal, 0, 2);
            this.tblPnl.Controls.Add(this.lbl_flip_vertical, 1, 2);
            this.tblPnl.Controls.Add(this.lbRuler, 0, 4);
            this.tblPnl.Controls.Add(this.lbProtractor, 1, 4);
            this.tblPnl.Controls.Add(this.lbSelectRegion, 0, 5);
            this.tblPnl.Controls.Add(this.lbFilter, 0, 7);
            this.tblPnl.Controls.Add(this.lbFovSetting, 0, 8);
            this.tblPnl.Controls.Add(this.lblProfile, 1, 5);
            this.tblPnl.Controls.Add(this.lbReset, 1, 7);
            this.tblPnl.Controls.Add(this.lbRegionHisto, 0, 6);
            this.tblPnl.Controls.Add(this.lbLineProfile, 1, 6);
            this.tblPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPnl.Location = new System.Drawing.Point(0, 0);
            this.tblPnl.Name = "tblPnl";
            this.tblPnl.RowCount = 9;
            this.tblPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tblPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tblPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tblPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tblPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tblPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tblPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tblPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tblPnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tblPnl.Size = new System.Drawing.Size(150, 478);
            this.tblPnl.TabIndex = 0;
            // 
            // lbLoad
            // 
            this.lbLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLoad.ImageKey = "Load32x32.png";
            this.lbLoad.ImageList = this.AquisitionimageList;
            this.lbLoad.Location = new System.Drawing.Point(78, 159);
            this.lbLoad.Name = "lbLoad";
            this.lbLoad.Size = new System.Drawing.Size(69, 53);
            this.lbLoad.TabIndex = 25;
            this.lbLoad.Text = "label1";
            this.lbLoad.Click += new System.EventHandler(this.lbLoad_Click);
            // 
            // AquisitionimageList
            // 
            this.AquisitionimageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("AquisitionimageList.ImageStream")));
            this.AquisitionimageList.TransparentColor = System.Drawing.Color.Transparent;
            this.AquisitionimageList.Images.SetKeyName(0, "Aquisition_Start(32x32).png");
            this.AquisitionimageList.Images.SetKeyName(1, "Aquisition_Stop(32x32).png");
            this.AquisitionimageList.Images.SetKeyName(2, "rotate_CCW.png");
            this.AquisitionimageList.Images.SetKeyName(3, "rotate_CW.png");
            this.AquisitionimageList.Images.SetKeyName(4, "flip_horizontal.png");
            this.AquisitionimageList.Images.SetKeyName(5, "flip_vertical.png");
            this.AquisitionimageList.Images.SetKeyName(6, "Settings.png");
            this.AquisitionimageList.Images.SetKeyName(7, "Capture.png");
            this.AquisitionimageList.Images.SetKeyName(8, "Camera.png");
            this.AquisitionimageList.Images.SetKeyName(9, "Exit.png");
            this.AquisitionimageList.Images.SetKeyName(10, "CameraChange.png");
            this.AquisitionimageList.Images.SetKeyName(11, "Histogram.png");
            this.AquisitionimageList.Images.SetKeyName(12, "Ruler.png");
            this.AquisitionimageList.Images.SetKeyName(13, "Protractor.png");
            this.AquisitionimageList.Images.SetKeyName(14, "Reset.png");
            this.AquisitionimageList.Images.SetKeyName(15, "FilterIcon.png");
            this.AquisitionimageList.Images.SetKeyName(16, "Save32x32.png");
            this.AquisitionimageList.Images.SetKeyName(17, "Load32x32.png");
            this.AquisitionimageList.Images.SetKeyName(18, "lineProfile32x32.png");
            this.AquisitionimageList.Images.SetKeyName(19, "selectRegion32x32.png");
            this.AquisitionimageList.Images.SetKeyName(20, "RegionProfile32x32.png");
            this.AquisitionimageList.Images.SetKeyName(21, "ProcessingList.png");
            this.AquisitionimageList.Images.SetKeyName(22, "Profile.png");
            // 
            // lbl_Aquisition
            // 
            this.lbl_Aquisition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Aquisition.ImageIndex = 0;
            this.lbl_Aquisition.ImageList = this.AquisitionimageList;
            this.lbl_Aquisition.Location = new System.Drawing.Point(3, 0);
            this.lbl_Aquisition.Name = "lbl_Aquisition";
            this.lbl_Aquisition.Size = new System.Drawing.Size(69, 53);
            this.lbl_Aquisition.TabIndex = 7;
            this.lbl_Aquisition.Click += new System.EventHandler(this.lbl_Aquisition_Click);
            // 
            // lbHistogram
            // 
            this.lbHistogram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHistogram.ImageKey = "Histogram.png";
            this.lbHistogram.ImageList = this.AquisitionimageList;
            this.lbHistogram.Location = new System.Drawing.Point(78, 0);
            this.lbHistogram.Name = "lbHistogram";
            this.lbHistogram.Size = new System.Drawing.Size(69, 53);
            this.lbHistogram.TabIndex = 17;
            this.lbHistogram.Click += new System.EventHandler(this.lbHistogram_Click);
            // 
            // lbSave
            // 
            this.lbSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSave.ImageKey = "Save32x32.png";
            this.lbSave.ImageList = this.AquisitionimageList;
            this.lbSave.Location = new System.Drawing.Point(3, 159);
            this.lbSave.Name = "lbSave";
            this.lbSave.Size = new System.Drawing.Size(69, 53);
            this.lbSave.TabIndex = 24;
            this.lbSave.Text = "label1";
            this.lbSave.Click += new System.EventHandler(this.lbSave_Click);
            // 
            // lbl_Rotate_CCW
            // 
            this.lbl_Rotate_CCW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Rotate_CCW.ImageKey = "rotate_CW.png";
            this.lbl_Rotate_CCW.ImageList = this.AquisitionimageList;
            this.lbl_Rotate_CCW.Location = new System.Drawing.Point(3, 53);
            this.lbl_Rotate_CCW.Name = "lbl_Rotate_CCW";
            this.lbl_Rotate_CCW.Size = new System.Drawing.Size(69, 53);
            this.lbl_Rotate_CCW.TabIndex = 8;
            this.lbl_Rotate_CCW.Text = "label1";
            this.lbl_Rotate_CCW.Click += new System.EventHandler(this.lbl_Rotate_CCW_Click);
            // 
            // lbl_Rotate_CW
            // 
            this.lbl_Rotate_CW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Rotate_CW.ImageKey = "rotate_CCW.png";
            this.lbl_Rotate_CW.ImageList = this.AquisitionimageList;
            this.lbl_Rotate_CW.Location = new System.Drawing.Point(78, 53);
            this.lbl_Rotate_CW.Name = "lbl_Rotate_CW";
            this.lbl_Rotate_CW.Size = new System.Drawing.Size(69, 53);
            this.lbl_Rotate_CW.TabIndex = 9;
            this.lbl_Rotate_CW.Text = "label1";
            this.lbl_Rotate_CW.Click += new System.EventHandler(this.lbl_Rotate_CW_Click);
            // 
            // lbl_flip_horizontal
            // 
            this.lbl_flip_horizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_flip_horizontal.ImageKey = "flip_horizontal.png";
            this.lbl_flip_horizontal.ImageList = this.AquisitionimageList;
            this.lbl_flip_horizontal.Location = new System.Drawing.Point(3, 106);
            this.lbl_flip_horizontal.Name = "lbl_flip_horizontal";
            this.lbl_flip_horizontal.Size = new System.Drawing.Size(69, 53);
            this.lbl_flip_horizontal.TabIndex = 10;
            this.lbl_flip_horizontal.Text = "label1";
            this.lbl_flip_horizontal.Click += new System.EventHandler(this.lbl_flip_horizontal_Click);
            // 
            // lbl_flip_vertical
            // 
            this.lbl_flip_vertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_flip_vertical.ImageKey = "flip_vertical.png";
            this.lbl_flip_vertical.ImageList = this.AquisitionimageList;
            this.lbl_flip_vertical.Location = new System.Drawing.Point(78, 106);
            this.lbl_flip_vertical.Name = "lbl_flip_vertical";
            this.lbl_flip_vertical.Size = new System.Drawing.Size(69, 53);
            this.lbl_flip_vertical.TabIndex = 11;
            this.lbl_flip_vertical.Text = "label1";
            this.lbl_flip_vertical.Click += new System.EventHandler(this.lbl_flip_vertical_Click);
            // 
            // lbRuler
            // 
            this.lbRuler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRuler.ImageKey = "Ruler.png";
            this.lbRuler.ImageList = this.AquisitionimageList;
            this.lbRuler.Location = new System.Drawing.Point(3, 212);
            this.lbRuler.Name = "lbRuler";
            this.lbRuler.Size = new System.Drawing.Size(69, 53);
            this.lbRuler.TabIndex = 20;
            this.lbRuler.Click += new System.EventHandler(this.lbRuler_Click);
            // 
            // lbProtractor
            // 
            this.lbProtractor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbProtractor.ImageKey = "Protractor.png";
            this.lbProtractor.ImageList = this.AquisitionimageList;
            this.lbProtractor.Location = new System.Drawing.Point(78, 212);
            this.lbProtractor.Name = "lbProtractor";
            this.lbProtractor.Size = new System.Drawing.Size(69, 53);
            this.lbProtractor.TabIndex = 19;
            this.lbProtractor.Click += new System.EventHandler(this.lbProtractor_Click);
            // 
            // lbSelectRegion
            // 
            this.lbSelectRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSelectRegion.ImageKey = "selectRegion32x32.png";
            this.lbSelectRegion.ImageList = this.AquisitionimageList;
            this.lbSelectRegion.Location = new System.Drawing.Point(3, 265);
            this.lbSelectRegion.Name = "lbSelectRegion";
            this.lbSelectRegion.Size = new System.Drawing.Size(69, 53);
            this.lbSelectRegion.TabIndex = 27;
            this.lbSelectRegion.Text = "label1";
            this.lbSelectRegion.Click += new System.EventHandler(this.lbSelectRegion_Click);
            // 
            // lbFilter
            // 
            this.lbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFilter.ImageKey = "FilterIcon.png";
            this.lbFilter.ImageList = this.AquisitionimageList;
            this.lbFilter.Location = new System.Drawing.Point(3, 371);
            this.lbFilter.Name = "lbFilter";
            this.lbFilter.Size = new System.Drawing.Size(69, 53);
            this.lbFilter.TabIndex = 23;
            this.lbFilter.Text = "label1";
            this.lbFilter.Click += new System.EventHandler(this.lbFilter_Click);
            // 
            // lbFovSetting
            // 
            this.lbFovSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFovSetting.ImageKey = "Settings.png";
            this.lbFovSetting.ImageList = this.AquisitionimageList;
            this.lbFovSetting.Location = new System.Drawing.Point(78, 424);
            this.lbFovSetting.Name = "lbFovSetting";
            this.lbFovSetting.Size = new System.Drawing.Size(69, 54);
            this.lbFovSetting.TabIndex = 18;
            this.lbFovSetting.Text = "label1";
            this.lbFovSetting.Click += new System.EventHandler(this.lbFovSetting_Click);
            // 
            // lblProfile
            // 
            this.lblProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProfile.ImageKey = "Profile.png";
            this.lblProfile.ImageList = this.AquisitionimageList;
            this.lblProfile.Location = new System.Drawing.Point(78, 265);
            this.lblProfile.Name = "lblProfile";
            this.lblProfile.Size = new System.Drawing.Size(69, 53);
            this.lblProfile.TabIndex = 29;
            this.lblProfile.Click += new System.EventHandler(this.lblProfile_Click);
            // 
            // lbReset
            // 
            this.lbReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbReset.ImageKey = "Reset.png";
            this.lbReset.ImageList = this.AquisitionimageList;
            this.lbReset.Location = new System.Drawing.Point(78, 371);
            this.lbReset.Name = "lbReset";
            this.lbReset.Size = new System.Drawing.Size(69, 53);
            this.lbReset.TabIndex = 21;
            this.lbReset.Text = "label1";
            this.lbReset.Click += new System.EventHandler(this.lbReset_Click);
            // 
            // lbRegionHisto
            // 
            this.lbRegionHisto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRegionHisto.ImageKey = "RegionProfile32x32.png";
            this.lbRegionHisto.ImageList = this.AquisitionimageList;
            this.lbRegionHisto.Location = new System.Drawing.Point(3, 318);
            this.lbRegionHisto.Name = "lbRegionHisto";
            this.lbRegionHisto.Size = new System.Drawing.Size(69, 53);
            this.lbRegionHisto.TabIndex = 28;
            this.lbRegionHisto.Text = "label1";
            this.lbRegionHisto.Click += new System.EventHandler(this.lbRegionHisto_Click);
            // 
            // lbLineProfile
            // 
            this.lbLineProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLineProfile.ImageKey = "lineProfile32x32.png";
            this.lbLineProfile.ImageList = this.AquisitionimageList;
            this.lbLineProfile.Location = new System.Drawing.Point(78, 318);
            this.lbLineProfile.Name = "lbLineProfile";
            this.lbLineProfile.Size = new System.Drawing.Size(69, 53);
            this.lbLineProfile.TabIndex = 26;
            this.lbLineProfile.Text = "label1";
            this.lbLineProfile.Click += new System.EventHandler(this.lbLineProfile_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lbl_Settings);
            this.splitContainer3.Panel1.Controls.Add(this.lbl_Capture);
            this.splitContainer3.Panel1.Controls.Add(this.lbl_DSA_View);
            this.splitContainer3.Panel1.Controls.Add(this.lbl_CameraSettings);
            this.splitContainer3.Panel1.Controls.Add(this.button1);
            this.splitContainer3.Panel1.Controls.Add(this.btnTestInputImage);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer3.Size = new System.Drawing.Size(150, 157);
            this.splitContainer3.SplitterDistance = 88;
            this.splitContainer3.TabIndex = 0;
            this.splitContainer3.TabStop = false;
            // 
            // lbl_Settings
            // 
            this.lbl_Settings.ImageKey = "Settings.png";
            this.lbl_Settings.ImageList = this.AquisitionimageList;
            this.lbl_Settings.Location = new System.Drawing.Point(45, 18);
            this.lbl_Settings.Name = "lbl_Settings";
            this.lbl_Settings.Size = new System.Drawing.Size(69, 65);
            this.lbl_Settings.TabIndex = 12;
            this.lbl_Settings.Text = "label1";
            this.lbl_Settings.Visible = false;
            this.lbl_Settings.Click += new System.EventHandler(this.lbl_Settings_Click);
            // 
            // lbl_Capture
            // 
            this.lbl_Capture.ImageIndex = 7;
            this.lbl_Capture.ImageList = this.AquisitionimageList;
            this.lbl_Capture.Location = new System.Drawing.Point(45, 18);
            this.lbl_Capture.Name = "lbl_Capture";
            this.lbl_Capture.Size = new System.Drawing.Size(69, 62);
            this.lbl_Capture.TabIndex = 15;
            this.lbl_Capture.Visible = false;
            this.lbl_Capture.Click += new System.EventHandler(this.lbl_Capture_Click);
            // 
            // lbl_DSA_View
            // 
            this.lbl_DSA_View.ImageKey = "CameraChange.png";
            this.lbl_DSA_View.ImageList = this.AquisitionimageList;
            this.lbl_DSA_View.Location = new System.Drawing.Point(45, 18);
            this.lbl_DSA_View.Name = "lbl_DSA_View";
            this.lbl_DSA_View.Size = new System.Drawing.Size(69, 62);
            this.lbl_DSA_View.TabIndex = 16;
            this.lbl_DSA_View.Text = "label1";
            this.lbl_DSA_View.Visible = false;
            // 
            // lbl_CameraSettings
            // 
            this.lbl_CameraSettings.ImageKey = "Camera.png";
            this.lbl_CameraSettings.ImageList = this.AquisitionimageList;
            this.lbl_CameraSettings.Location = new System.Drawing.Point(45, 18);
            this.lbl_CameraSettings.Name = "lbl_CameraSettings";
            this.lbl_CameraSettings.Size = new System.Drawing.Size(69, 65);
            this.lbl_CameraSettings.TabIndex = 13;
            this.lbl_CameraSettings.Text = "label1";
            this.lbl_CameraSettings.Visible = false;
            this.lbl_CameraSettings.Click += new System.EventHandler(this.lbl_CameraSettings_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTestInputImage
            // 
            this.btnTestInputImage.Enabled = false;
            this.btnTestInputImage.Location = new System.Drawing.Point(26, 20);
            this.btnTestInputImage.Name = "btnTestInputImage";
            this.btnTestInputImage.Size = new System.Drawing.Size(69, 62);
            this.btnTestInputImage.TabIndex = 17;
            this.btnTestInputImage.Text = "Input Image";
            this.btnTestInputImage.UseVisualStyleBackColor = true;
            this.btnTestInputImage.Visible = false;
            this.btnTestInputImage.Click += new System.EventHandler(this.btnTestInputImage_Click);
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer5.IsSplitterFixed = true;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.lbl_Exit);
            this.splitContainer5.Size = new System.Drawing.Size(150, 65);
            this.splitContainer5.SplitterDistance = 69;
            this.splitContainer5.TabIndex = 0;
            this.splitContainer5.TabStop = false;
            // 
            // lbl_Exit
            // 
            this.lbl_Exit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Exit.ImageKey = "Exit.png";
            this.lbl_Exit.ImageList = this.AquisitionimageList;
            this.lbl_Exit.Location = new System.Drawing.Point(0, 0);
            this.lbl_Exit.Name = "lbl_Exit";
            this.lbl_Exit.Size = new System.Drawing.Size(69, 65);
            this.lbl_Exit.TabIndex = 14;
            this.lbl_Exit.Click += new System.EventHandler(this.lbl_Exit_Click);
            // 
            // lblProcessingTime
            // 
            this.lblProcessingTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProcessingTime.ImageKey = "ProcessingList.png";
            this.lblProcessingTime.ImageList = this.AquisitionimageList;
            this.lblProcessingTime.Location = new System.Drawing.Point(3, 424);
            this.lblProcessingTime.Name = "lblProcessingTime";
            this.lblProcessingTime.Size = new System.Drawing.Size(69, 54);
            this.lblProcessingTime.TabIndex = 30;
            this.lblProcessingTime.Text = "label1";
            this.lblProcessingTime.Click += new System.EventHandler(this.lblProcessingTime_Click);
            // 
            // ButtonPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ButtonPanel";
            this.Size = new System.Drawing.Size(150, 975);
            this.Load += new System.EventHandler(this.ButtonPanel_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tblPnl.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Timer timerAcq;
        private System.Windows.Forms.ImageList AquisitionimageList;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label lblAcqusitionState;
        private System.Windows.Forms.Label lblAdministrator;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tblPnl;
        private System.Windows.Forms.Label lbl_flip_vertical;
        private System.Windows.Forms.Label lbl_flip_horizontal;
        private System.Windows.Forms.Label lbl_Rotate_CW;
        private System.Windows.Forms.Label lbl_Aquisition;
        private System.Windows.Forms.Label lbl_Rotate_CCW;
        private System.Windows.Forms.Label lbHistogram;
        private System.Windows.Forms.Label lbProtractor;
        private System.Windows.Forms.Label lbRuler;
        private System.Windows.Forms.Label lbFovSetting;
        private System.Windows.Forms.Label lbReset;
        private System.Windows.Forms.Label lbFilter;
        private System.Windows.Forms.Label lbSave;
        private System.Windows.Forms.Label lbLoad;
        private System.Windows.Forms.OpenFileDialog fileDlg;
        private System.Windows.Forms.SaveFileDialog saveFiledlg;
        private System.Windows.Forms.Label lbLineProfile;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label lbl_Settings;
        private System.Windows.Forms.Label lbl_Capture;
        private System.Windows.Forms.Label lbl_DSA_View;
        private System.Windows.Forms.Label lbl_CameraSettings;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnTestInputImage;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.Label lbl_Exit;
        private System.Windows.Forms.Label lbSelectRegion;
        private System.Windows.Forms.Label lbRegionHisto;
        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.Label lblProcessingTime;
    }
}
