namespace 自动备份
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lstMyListView = new System.Windows.Forms.ListView();
            this.name = new System.Windows.Forms.ColumnHeader();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btn_ip = new System.Windows.Forms.Button();
            this.btn_file_mod = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.lstView_file = new System.Windows.Forms.ListView();
            this.file = new System.Windows.Forms.ColumnHeader();
            this.prgBarBakeup = new System.Windows.Forms.ProgressBar();
            this.lbl_probar = new System.Windows.Forms.Label();
            this.btn_change = new System.Windows.Forms.Button();
            this.btn_rm = new System.Windows.Forms.Button();
            this.btn_file_rm = new System.Windows.Forms.Button();
            this.btn_file_add = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.lblPrgBar = new System.Windows.Forms.Label();
            this.btn_auto = new System.Windows.Forms.Button();
            this.cboAuto = new System.Windows.Forms.ComboBox();
            this.lblCtrl = new System.Windows.Forms.Label();
            this.timerAuto = new System.Windows.Forms.Timer(this.components);
            this.btnAutoStop = new System.Windows.Forms.Button();
            this.gboAutoXtimes = new System.Windows.Forms.GroupBox();
            this.btnManyTimes = new System.Windows.Forms.Button();
            this.btnBkPath = new System.Windows.Forms.Button();
            this.fbdPath = new System.Windows.Forms.FolderBrowserDialog();
            this.lbl_path = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.gboCtrl = new System.Windows.Forms.GroupBox();
            this.gboBK_Mode = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.gboNow = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboNowSec = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboNowMin = new System.Windows.Forms.ComboBox();
            this.cboNowHour = new System.Windows.Forms.ComboBox();
            this.dtp1time = new System.Windows.Forms.DateTimePicker();
            this.gboAuto1Time = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cboSec = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboMin = new System.Windows.Forms.ComboBox();
            this.cboHour = new System.Windows.Forms.ComboBox();
            this.btnNow = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxOtnm = new System.Windows.Forms.CheckBox();
            this.lblNE = new System.Windows.Forms.Label();
            this.btnInput = new System.Windows.Forms.Button();
            this.btnOutput = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblVersion = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblDev = new System.Windows.Forms.Label();
            this.gboAutoXtimes.SuspendLayout();
            this.gboCtrl.SuspendLayout();
            this.gboBK_Mode.SuspendLayout();
            this.gboNow.SuspendLayout();
            this.gboAuto1Time.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstMyListView
            // 
            this.lstMyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name});
            this.lstMyListView.Location = new System.Drawing.Point(9, 72);
            this.lstMyListView.Name = "lstMyListView";
            this.lstMyListView.Size = new System.Drawing.Size(240, 97);
            this.lstMyListView.TabIndex = 0;
            this.lstMyListView.UseCompatibleStateImageBehavior = false;
            this.lstMyListView.View = System.Windows.Forms.View.Details;
            this.lstMyListView.SelectedIndexChanged += new System.EventHandler(this.lstMyListView_SelectedIndexChanged);
            // 
            // name
            // 
            this.name.Text = "站点IP";
            this.name.Width = 236;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(9, 11);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(240, 21);
            this.txtIP.TabIndex = 1;
            // 
            // btn_ip
            // 
            this.btn_ip.Location = new System.Drawing.Point(17, 38);
            this.btn_ip.Name = "btn_ip";
            this.btn_ip.Size = new System.Drawing.Size(54, 23);
            this.btn_ip.TabIndex = 2;
            this.btn_ip.Text = "增加";
            this.toolTip1.SetToolTip(this.btn_ip, "增加备份站点项");
            this.btn_ip.UseVisualStyleBackColor = true;
            this.btn_ip.Click += new System.EventHandler(this.btn_ip_Click);
            // 
            // btn_file_mod
            // 
            this.btn_file_mod.Location = new System.Drawing.Point(99, 39);
            this.btn_file_mod.Name = "btn_file_mod";
            this.btn_file_mod.Size = new System.Drawing.Size(54, 23);
            this.btn_file_mod.TabIndex = 5;
            this.btn_file_mod.Text = "修改";
            this.toolTip1.SetToolTip(this.btn_file_mod, "修改备份文件项");
            this.btn_file_mod.UseVisualStyleBackColor = true;
            this.btn_file_mod.Click += new System.EventHandler(this.btn_file_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(9, 11);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(240, 21);
            this.txtFile.TabIndex = 4;
            this.txtFile.TextChanged += new System.EventHandler(this.txtFile_TextChanged);
            // 
            // lstView_file
            // 
            this.lstView_file.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.file});
            this.lstView_file.Location = new System.Drawing.Point(9, 72);
            this.lstView_file.Name = "lstView_file";
            this.lstView_file.Size = new System.Drawing.Size(240, 97);
            this.lstView_file.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstView_file.TabIndex = 3;
            this.lstView_file.UseCompatibleStateImageBehavior = false;
            this.lstView_file.View = System.Windows.Forms.View.Details;
            this.lstView_file.SelectedIndexChanged += new System.EventHandler(this.lstView_file_SelectedIndexChanged_1);
            // 
            // file
            // 
            this.file.Text = "文件名";
            this.file.Width = 236;
            // 
            // prgBarBakeup
            // 
            this.prgBarBakeup.Location = new System.Drawing.Point(84, 400);
            this.prgBarBakeup.Name = "prgBarBakeup";
            this.prgBarBakeup.Size = new System.Drawing.Size(637, 23);
            this.prgBarBakeup.TabIndex = 8;
            // 
            // lbl_probar
            // 
            this.lbl_probar.AutoSize = true;
            this.lbl_probar.Location = new System.Drawing.Point(26, 407);
            this.lbl_probar.Name = "lbl_probar";
            this.lbl_probar.Size = new System.Drawing.Size(53, 12);
            this.lbl_probar.TabIndex = 9;
            this.lbl_probar.Text = "备份进度";
            // 
            // btn_change
            // 
            this.btn_change.Location = new System.Drawing.Point(99, 39);
            this.btn_change.Name = "btn_change";
            this.btn_change.Size = new System.Drawing.Size(54, 23);
            this.btn_change.TabIndex = 12;
            this.btn_change.Text = "修改";
            this.toolTip1.SetToolTip(this.btn_change, "修改备份站点项");
            this.btn_change.UseVisualStyleBackColor = true;
            this.btn_change.Click += new System.EventHandler(this.btn_change_Click);
            // 
            // btn_rm
            // 
            this.btn_rm.Location = new System.Drawing.Point(180, 39);
            this.btn_rm.Name = "btn_rm";
            this.btn_rm.Size = new System.Drawing.Size(54, 23);
            this.btn_rm.TabIndex = 13;
            this.btn_rm.Text = "删除";
            this.toolTip1.SetToolTip(this.btn_rm, "删除备份站点项");
            this.btn_rm.UseVisualStyleBackColor = true;
            this.btn_rm.Click += new System.EventHandler(this.btn_rm_Click);
            // 
            // btn_file_rm
            // 
            this.btn_file_rm.Location = new System.Drawing.Point(180, 39);
            this.btn_file_rm.Name = "btn_file_rm";
            this.btn_file_rm.Size = new System.Drawing.Size(54, 23);
            this.btn_file_rm.TabIndex = 14;
            this.btn_file_rm.Text = "删除";
            this.toolTip1.SetToolTip(this.btn_file_rm, "删除备份文件项");
            this.btn_file_rm.UseVisualStyleBackColor = true;
            this.btn_file_rm.Click += new System.EventHandler(this.btn_file_rm_Click);
            // 
            // btn_file_add
            // 
            this.btn_file_add.Location = new System.Drawing.Point(17, 38);
            this.btn_file_add.Name = "btn_file_add";
            this.btn_file_add.Size = new System.Drawing.Size(54, 23);
            this.btn_file_add.TabIndex = 15;
            this.btn_file_add.Text = "增加";
            this.toolTip1.SetToolTip(this.btn_file_add, "增加备份文件项");
            this.btn_file_add.UseVisualStyleBackColor = true;
            this.btn_file_add.Click += new System.EventHandler(this.btn_file_add_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(605, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 25);
            this.button1.TabIndex = 20;
            this.button1.Text = "退出-测试用";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtLog.Location = new System.Drawing.Point(23, 321);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(756, 69);
            this.txtLog.TabIndex = 26;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(23, 296);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(53, 12);
            this.lblLog.TabIndex = 27;
            this.lblLog.Text = "日志信息";
            // 
            // lblPrgBar
            // 
            this.lblPrgBar.AutoSize = true;
            this.lblPrgBar.Font = new System.Drawing.Font("宋体", 8F);
            this.lblPrgBar.Location = new System.Drawing.Point(726, 407);
            this.lblPrgBar.Name = "lblPrgBar";
            this.lblPrgBar.Size = new System.Drawing.Size(23, 11);
            this.lblPrgBar.TabIndex = 28;
            this.lblPrgBar.Text = "0/0";
            // 
            // btn_auto
            // 
            this.btn_auto.Location = new System.Drawing.Point(23, 26);
            this.btn_auto.Name = "btn_auto";
            this.btn_auto.Size = new System.Drawing.Size(67, 23);
            this.btn_auto.TabIndex = 29;
            this.btn_auto.Text = "开始";
            this.toolTip1.SetToolTip(this.btn_auto, "启动备份");
            this.btn_auto.UseVisualStyleBackColor = true;
            this.btn_auto.Click += new System.EventHandler(this.btn_auto_Click);
            // 
            // cboAuto
            // 
            this.cboAuto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAuto.FormattingEnabled = true;
            this.cboAuto.Items.AddRange(new object[] {
            "倒计时",
            "定时一次",
            "定时多次"});
            this.cboAuto.Location = new System.Drawing.Point(51, 48);
            this.cboAuto.Name = "cboAuto";
            this.cboAuto.Size = new System.Drawing.Size(112, 20);
            this.cboAuto.TabIndex = 31;
            this.toolTip1.SetToolTip(this.cboAuto, "选择备份方式");
            this.cboAuto.SelectedIndexChanged += new System.EventHandler(this.cboAuto_SelectedIndexChanged_1);
            // 
            // lblCtrl
            // 
            this.lblCtrl.AutoSize = true;
            this.lblCtrl.Location = new System.Drawing.Point(26, 61);
            this.lblCtrl.Name = "lblCtrl";
            this.lblCtrl.Size = new System.Drawing.Size(101, 12);
            this.lblCtrl.TabIndex = 33;
            this.lblCtrl.Text = "当前状态: 未运行";
            // 
            // timerAuto
            // 
            this.timerAuto.Interval = 1000;
            this.timerAuto.Tick += new System.EventHandler(this.timerAuto_Tick);
            // 
            // btnAutoStop
            // 
            this.btnAutoStop.Location = new System.Drawing.Point(114, 26);
            this.btnAutoStop.Name = "btnAutoStop";
            this.btnAutoStop.Size = new System.Drawing.Size(72, 23);
            this.btnAutoStop.TabIndex = 34;
            this.btnAutoStop.Text = "取消";
            this.toolTip1.SetToolTip(this.btnAutoStop, "停止备份");
            this.btnAutoStop.UseVisualStyleBackColor = true;
            this.btnAutoStop.Click += new System.EventHandler(this.btnAutoStop_Click);
            // 
            // gboAutoXtimes
            // 
            this.gboAutoXtimes.Controls.Add(this.btnManyTimes);
            this.gboAutoXtimes.Location = new System.Drawing.Point(535, 20);
            this.gboAutoXtimes.Name = "gboAutoXtimes";
            this.gboAutoXtimes.Size = new System.Drawing.Size(244, 111);
            this.gboAutoXtimes.TabIndex = 36;
            this.gboAutoXtimes.TabStop = false;
            this.gboAutoXtimes.Text = "定时多次";
            // 
            // btnManyTimes
            // 
            this.btnManyTimes.Location = new System.Drawing.Point(66, 42);
            this.btnManyTimes.Name = "btnManyTimes";
            this.btnManyTimes.Size = new System.Drawing.Size(85, 23);
            this.btnManyTimes.TabIndex = 4;
            this.btnManyTimes.Text = "设置 ...";
            this.btnManyTimes.UseVisualStyleBackColor = true;
            this.btnManyTimes.Click += new System.EventHandler(this.btnManyTimes_Click);
            // 
            // btnBkPath
            // 
            this.btnBkPath.Location = new System.Drawing.Point(13, 20);
            this.btnBkPath.Name = "btnBkPath";
            this.btnBkPath.Size = new System.Drawing.Size(85, 23);
            this.btnBkPath.TabIndex = 37;
            this.btnBkPath.Text = "备份到 ...";
            this.toolTip1.SetToolTip(this.btnBkPath, "选择备份文件存放路径");
            this.btnBkPath.UseVisualStyleBackColor = true;
            this.btnBkPath.Click += new System.EventHandler(this.btnBkPath_Click);
            // 
            // lbl_path
            // 
            this.lbl_path.AutoSize = true;
            this.lbl_path.Location = new System.Drawing.Point(109, 25);
            this.lbl_path.Name = "lbl_path";
            this.lbl_path.Size = new System.Drawing.Size(77, 12);
            this.lbl_path.TabIndex = 10;
            this.lbl_path.Text = "D:\\fh_bakeup";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(7, 179);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(101, 12);
            this.lblFile.TabIndex = 42;
            this.lblFile.Text = "文件情况： 0文件";
            // 
            // gboCtrl
            // 
            this.gboCtrl.Controls.Add(this.btn_auto);
            this.gboCtrl.Controls.Add(this.btnAutoStop);
            this.gboCtrl.Controls.Add(this.lblCtrl);
            this.gboCtrl.Location = new System.Drawing.Point(535, 152);
            this.gboCtrl.Name = "gboCtrl";
            this.gboCtrl.Size = new System.Drawing.Size(244, 82);
            this.gboCtrl.TabIndex = 40;
            this.gboCtrl.TabStop = false;
            this.gboCtrl.Text = "备份控制台";
            // 
            // gboBK_Mode
            // 
            this.gboBK_Mode.Controls.Add(this.checkBox1);
            this.gboBK_Mode.Controls.Add(this.cboAuto);
            this.gboBK_Mode.Location = new System.Drawing.Point(301, 20);
            this.gboBK_Mode.Name = "gboBK_Mode";
            this.gboBK_Mode.Size = new System.Drawing.Size(214, 85);
            this.gboBK_Mode.TabIndex = 39;
            this.gboBK_Mode.TabStop = false;
            this.gboBK_Mode.Text = "备份方式";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(50, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 32;
            this.checkBox1.Text = "开机自动启动";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // gboNow
            // 
            this.gboNow.Controls.Add(this.label1);
            this.gboNow.Controls.Add(this.label2);
            this.gboNow.Controls.Add(this.cboNowSec);
            this.gboNow.Controls.Add(this.label3);
            this.gboNow.Controls.Add(this.cboNowMin);
            this.gboNow.Controls.Add(this.cboNowHour);
            this.gboNow.Location = new System.Drawing.Point(535, 20);
            this.gboNow.Name = "gboNow";
            this.gboNow.Size = new System.Drawing.Size(244, 111);
            this.gboNow.TabIndex = 39;
            this.gboNow.TabStop = false;
            this.gboNow.Text = "倒计时";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 38;
            this.label1.Text = "秒";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "分";
            // 
            // cboNowSec
            // 
            this.cboNowSec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNowSec.FormattingEnabled = true;
            this.cboNowSec.Location = new System.Drawing.Point(31, 65);
            this.cboNowSec.Name = "cboNowSec";
            this.cboNowSec.Size = new System.Drawing.Size(67, 20);
            this.cboNowSec.TabIndex = 37;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 38;
            this.label3.Text = "时";
            // 
            // cboNowMin
            // 
            this.cboNowMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNowMin.FormattingEnabled = true;
            this.cboNowMin.Location = new System.Drawing.Point(138, 40);
            this.cboNowMin.Name = "cboNowMin";
            this.cboNowMin.Size = new System.Drawing.Size(67, 20);
            this.cboNowMin.TabIndex = 37;
            // 
            // cboNowHour
            // 
            this.cboNowHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNowHour.FormattingEnabled = true;
            this.cboNowHour.Location = new System.Drawing.Point(32, 39);
            this.cboNowHour.Name = "cboNowHour";
            this.cboNowHour.Size = new System.Drawing.Size(67, 20);
            this.cboNowHour.TabIndex = 37;
            // 
            // dtp1time
            // 
            this.dtp1time.Location = new System.Drawing.Point(13, 27);
            this.dtp1time.Name = "dtp1time";
            this.dtp1time.Size = new System.Drawing.Size(122, 21);
            this.dtp1time.TabIndex = 0;
            // 
            // gboAuto1Time
            // 
            this.gboAuto1Time.Controls.Add(this.label11);
            this.gboAuto1Time.Controls.Add(this.dtp1time);
            this.gboAuto1Time.Controls.Add(this.label10);
            this.gboAuto1Time.Controls.Add(this.cboSec);
            this.gboAuto1Time.Controls.Add(this.label9);
            this.gboAuto1Time.Controls.Add(this.cboMin);
            this.gboAuto1Time.Controls.Add(this.cboHour);
            this.gboAuto1Time.Controls.Add(this.btnNow);
            this.gboAuto1Time.Location = new System.Drawing.Point(535, 20);
            this.gboAuto1Time.Name = "gboAuto1Time";
            this.gboAuto1Time.Size = new System.Drawing.Size(244, 111);
            this.gboAuto1Time.TabIndex = 35;
            this.gboAuto1Time.TabStop = false;
            this.gboAuto1Time.Text = "定时一次";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(218, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 38;
            this.label11.Text = "秒";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(83, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 38;
            this.label10.Text = "分";
            // 
            // cboSec
            // 
            this.cboSec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSec.FormattingEnabled = true;
            this.cboSec.Location = new System.Drawing.Point(147, 53);
            this.cboSec.Name = "cboSec";
            this.cboSec.Size = new System.Drawing.Size(67, 20);
            this.cboSec.TabIndex = 37;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(218, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 38;
            this.label9.Text = "时";
            // 
            // cboMin
            // 
            this.cboMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMin.FormattingEnabled = true;
            this.cboMin.Location = new System.Drawing.Point(12, 53);
            this.cboMin.Name = "cboMin";
            this.cboMin.Size = new System.Drawing.Size(67, 20);
            this.cboMin.TabIndex = 37;
            // 
            // cboHour
            // 
            this.cboHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHour.FormattingEnabled = true;
            this.cboHour.Location = new System.Drawing.Point(147, 27);
            this.cboHour.Name = "cboHour";
            this.cboHour.Size = new System.Drawing.Size(67, 20);
            this.cboHour.TabIndex = 37;
            // 
            // btnNow
            // 
            this.btnNow.Location = new System.Drawing.Point(54, 79);
            this.btnNow.Name = "btnNow";
            this.btnNow.Size = new System.Drawing.Size(121, 23);
            this.btnNow.TabIndex = 4;
            this.btnNow.Text = "当前日期和时间";
            this.toolTip1.SetToolTip(this.btnNow, "获取当前日期时间");
            this.btnNow.UseVisualStyleBackColor = true;
            this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxOtnm);
            this.groupBox1.Controls.Add(this.lblNE);
            this.groupBox1.Controls.Add(this.btnInput);
            this.groupBox1.Controls.Add(this.btnOutput);
            this.groupBox1.Location = new System.Drawing.Point(303, 123);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 111);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "站点修改";
            // 
            // checkBoxOtnm
            // 
            this.checkBoxOtnm.AutoSize = true;
            this.checkBoxOtnm.Location = new System.Drawing.Point(22, 29);
            this.checkBoxOtnm.Name = "checkBoxOtnm";
            this.checkBoxOtnm.Size = new System.Drawing.Size(132, 16);
            this.checkBoxOtnm.TabIndex = 33;
            this.checkBoxOtnm.Text = "从网管导入站点清单";
            this.toolTip1.SetToolTip(this.checkBoxOtnm, "站点清单来源选择. 不勾选, 则默认从IP.ini文件中导入.勾选则从网管导入");
            this.checkBoxOtnm.UseVisualStyleBackColor = true;
            // 
            // lblNE
            // 
            this.lblNE.AutoSize = true;
            this.lblNE.Location = new System.Drawing.Point(20, 88);
            this.lblNE.Name = "lblNE";
            this.lblNE.Size = new System.Drawing.Size(131, 12);
            this.lblNE.TabIndex = 34;
            this.lblNE.Text = "导入站点情况： 未导入";
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(22, 53);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(75, 23);
            this.btnInput.TabIndex = 2;
            this.btnInput.Text = "导入...";
            this.toolTip1.SetToolTip(this.btnInput, "从文件导入站点");
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(110, 54);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(75, 23);
            this.btnOutput.TabIndex = 12;
            this.btnOutput.Text = "导出...";
            this.toolTip1.SetToolTip(this.btnOutput, "保存已导入的站点");
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_path);
            this.groupBox2.Controls.Add(this.btnBkPath);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(23, 241);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(756, 53);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "路径选择";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(368, 429);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(419, 12);
            this.lblVersion.TabIndex = 44;
            this.lblVersion.Text = "烽火通信愿景: 做国内一流、国际知名的信息通信网络产品和解决方案提供商!";
            this.toolTip1.SetToolTip(this.lblVersion, "ver 1.0.2, SVN41, by cytao@fiberhome.com, 2016.4");
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "托盘";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMenuItem,
            this.hideMenuItem,
            this.exitMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
            this.contextMenuStrip1.Text = "托盘菜单";
            // 
            // showMenuItem
            // 
            this.showMenuItem.Name = "showMenuItem";
            this.showMenuItem.Size = new System.Drawing.Size(100, 22);
            this.showMenuItem.Text = "显示";
            this.showMenuItem.Click += new System.EventHandler(this.showMenuItem_Click);
            // 
            // hideMenuItem
            // 
            this.hideMenuItem.Name = "hideMenuItem";
            this.hideMenuItem.Size = new System.Drawing.Size(100, 22);
            this.hideMenuItem.Text = "隐藏";
            this.hideMenuItem.Click += new System.EventHandler(this.hideMenuItem_Click);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(100, 22);
            this.exitMenuItem.Text = "退出";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(17, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(274, 226);
            this.tabControl1.TabIndex = 43;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblFile);
            this.tabPage1.Controls.Add(this.btn_file_add);
            this.tabPage1.Controls.Add(this.lstView_file);
            this.tabPage1.Controls.Add(this.btn_file_rm);
            this.tabPage1.Controls.Add(this.txtFile);
            this.tabPage1.Controls.Add(this.btn_file_mod);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(266, 200);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "文件修改";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblDev);
            this.tabPage2.Controls.Add(this.lstMyListView);
            this.tabPage2.Controls.Add(this.btn_change);
            this.tabPage2.Controls.Add(this.txtIP);
            this.tabPage2.Controls.Add(this.btn_rm);
            this.tabPage2.Controls.Add(this.btn_ip);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(266, 200);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "站点修改";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblDev
            // 
            this.lblDev.AutoSize = true;
            this.lblDev.Location = new System.Drawing.Point(7, 179);
            this.lblDev.Name = "lblDev";
            this.lblDev.Size = new System.Drawing.Size(101, 12);
            this.lblDev.TabIndex = 43;
            this.lblDev.Text = "站点情况： 0站点";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 446);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblPrgBar);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.lbl_probar);
            this.Controls.Add(this.prgBarBakeup);
            this.Controls.Add(this.gboAuto1Time);
            this.Controls.Add(this.gboNow);
            this.Controls.Add(this.gboAutoXtimes);
            this.Controls.Add(this.gboBK_Mode);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gboCtrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(8, 8);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "烽火通信数通-自动备份助手";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing_1);
            this.gboAutoXtimes.ResumeLayout(false);
            this.gboCtrl.ResumeLayout(false);
            this.gboCtrl.PerformLayout();
            this.gboBK_Mode.ResumeLayout(false);
            this.gboBK_Mode.PerformLayout();
            this.gboNow.ResumeLayout(false);
            this.gboNow.PerformLayout();
            this.gboAuto1Time.ResumeLayout(false);
            this.gboAuto1Time.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstMyListView;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btn_ip;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.Button btn_file_mod;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.ListView lstView_file;
        private System.Windows.Forms.ColumnHeader file;
        private System.Windows.Forms.ProgressBar prgBarBakeup;
        private System.Windows.Forms.Label lbl_probar;
        private System.Windows.Forms.Button btn_change;
        private System.Windows.Forms.Button btn_rm;
        private System.Windows.Forms.Button btn_file_rm;
        private System.Windows.Forms.Button btn_file_add;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Label lblPrgBar;
        private System.Windows.Forms.Button btn_auto;
        private System.Windows.Forms.ComboBox cboAuto;
        private System.Windows.Forms.Label lblCtrl;
        private System.Windows.Forms.Button btnAutoStop;
        private System.Windows.Forms.GroupBox gboAutoXtimes;
        private System.Windows.Forms.Button btnManyTimes;
        private System.Windows.Forms.Button btnBkPath;
        private System.Windows.Forms.FolderBrowserDialog fbdPath;
        private System.Windows.Forms.Label lbl_path;
        private System.Windows.Forms.GroupBox gboCtrl;
        private System.Windows.Forms.GroupBox gboBK_Mode;
        private System.Windows.Forms.GroupBox gboNow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboNowSec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboNowMin;
        private System.Windows.Forms.ComboBox cboNowHour;
        private System.Windows.Forms.DateTimePicker dtp1time;
        private System.Windows.Forms.GroupBox gboAuto1Time;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboSec;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboMin;
        private System.Windows.Forms.ComboBox cboHour;
        private System.Windows.Forms.Button btnNow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label lblNE;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timerAuto;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblDev;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBoxOtnm;
    }
}

