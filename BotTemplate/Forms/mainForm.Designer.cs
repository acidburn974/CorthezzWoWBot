namespace BotTemplate.Forms
{
    partial class mainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.bAttach = new System.Windows.Forms.Button();
            this.bDeattach = new System.Windows.Forms.Button();
            this.bStart = new System.Windows.Forms.Button();
            this.rGrindbot = new System.Windows.Forms.RadioButton();
            this.bSettings = new System.Windows.Forms.Button();
            this.bStopBot = new System.Windows.Forms.Button();
            this.bHide = new System.Windows.Forms.Button();
            this.bShow = new System.Windows.Forms.Button();
            this.lPid = new System.Windows.Forms.Label();
            this.lRunning = new System.Windows.Forms.Label();
            this.lState = new System.Windows.Forms.Label();
            this.lProfile = new System.Windows.Forms.Label();
            this.lFsmTick = new System.Windows.Forms.Label();
            this.lObjTick = new System.Windows.Forms.Label();
            this.bLog = new System.Windows.Forms.Button();
            this.rExplorer = new System.Windows.Forms.RadioButton();
            this.teleButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.rbSendMails = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rbZgAssist = new System.Windows.Forms.RadioButton();
            this.rZgFarm = new System.Windows.Forms.RadioButton();
            this.rbSpawn = new System.Windows.Forms.RadioButton();
            this.rbDupe = new System.Windows.Forms.RadioButton();
            this.rbStock = new System.Windows.Forms.RadioButton();
            this.lVersion = new System.Windows.Forms.Label();
            this.rbFishbot = new System.Windows.Forms.RadioButton();
            this.rbCreate = new System.Windows.Forms.RadioButton();
            this.rbAssist = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.mtbItem = new System.Windows.Forms.MaskedTextBox();
            this.bGetCount = new System.Windows.Forms.Button();
            this.bAddUp = new System.Windows.Forms.Button();
            this.bLogin = new System.Windows.Forms.Button();
            this.bFromFile = new System.Windows.Forms.Button();
            this.bToFile = new System.Windows.Forms.Button();
            this.bViewObjects = new System.Windows.Forms.Button();
            this.bstopFall = new System.Windows.Forms.Button();
            this.bPort = new System.Windows.Forms.Button();
            this.bGetCurrentCoords = new System.Windows.Forms.Button();
            this.mtbZ = new System.Windows.Forms.MaskedTextBox();
            this.mtbY = new System.Windows.Forms.MaskedTextBox();
            this.mtbX = new System.Windows.Forms.MaskedTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // bAttach
            // 
            this.bAttach.Location = new System.Drawing.Point(18, 97);
            this.bAttach.Name = "bAttach";
            this.bAttach.Size = new System.Drawing.Size(79, 33);
            this.bAttach.TabIndex = 0;
            this.bAttach.Text = "Attach";
            this.bAttach.UseVisualStyleBackColor = false;
            this.bAttach.Click += new System.EventHandler(this.bAttach_Click);
            // 
            // bDeattach
            // 
            this.bDeattach.Location = new System.Drawing.Point(103, 97);
            this.bDeattach.Name = "bDeattach";
            this.bDeattach.Size = new System.Drawing.Size(79, 33);
            this.bDeattach.TabIndex = 1;
            this.bDeattach.Text = "Deattach";
            this.bDeattach.UseVisualStyleBackColor = false;
            this.bDeattach.Click += new System.EventHandler(this.bDeattach_Click);
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(18, 136);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(79, 33);
            this.bStart.TabIndex = 5;
            this.bStart.Text = "Start Bot";
            this.bStart.UseVisualStyleBackColor = false;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // rGrindbot
            // 
            this.rGrindbot.AutoSize = true;
            this.rGrindbot.Location = new System.Drawing.Point(190, 139);
            this.rGrindbot.Name = "rGrindbot";
            this.rGrindbot.Size = new System.Drawing.Size(65, 17);
            this.rGrindbot.TabIndex = 7;
            this.rGrindbot.TabStop = true;
            this.rGrindbot.Text = "Grindbot";
            this.rGrindbot.UseVisualStyleBackColor = true;
            // 
            // bSettings
            // 
            this.bSettings.Location = new System.Drawing.Point(188, 97);
            this.bSettings.Name = "bSettings";
            this.bSettings.Size = new System.Drawing.Size(79, 33);
            this.bSettings.TabIndex = 9;
            this.bSettings.Text = "Settings";
            this.bSettings.UseVisualStyleBackColor = false;
            this.bSettings.Click += new System.EventHandler(this.bSettings_Click);
            // 
            // bStopBot
            // 
            this.bStopBot.Location = new System.Drawing.Point(103, 135);
            this.bStopBot.Name = "bStopBot";
            this.bStopBot.Size = new System.Drawing.Size(79, 33);
            this.bStopBot.TabIndex = 12;
            this.bStopBot.Text = "Stop Bot";
            this.bStopBot.UseVisualStyleBackColor = false;
            this.bStopBot.Click += new System.EventHandler(this.bStopBot_Click);
            // 
            // bHide
            // 
            this.bHide.Location = new System.Drawing.Point(18, 175);
            this.bHide.Name = "bHide";
            this.bHide.Size = new System.Drawing.Size(79, 33);
            this.bHide.TabIndex = 13;
            this.bHide.Text = "Hide";
            this.bHide.UseVisualStyleBackColor = false;
            this.bHide.Click += new System.EventHandler(this.bHide_Click);
            // 
            // bShow
            // 
            this.bShow.Location = new System.Drawing.Point(103, 174);
            this.bShow.Name = "bShow";
            this.bShow.Size = new System.Drawing.Size(79, 33);
            this.bShow.TabIndex = 14;
            this.bShow.Text = "Show";
            this.bShow.UseVisualStyleBackColor = false;
            this.bShow.Click += new System.EventHandler(this.bShow_Click);
            // 
            // lPid
            // 
            this.lPid.AutoSize = true;
            this.lPid.Location = new System.Drawing.Point(15, 17);
            this.lPid.Name = "lPid";
            this.lPid.Size = new System.Drawing.Size(65, 13);
            this.lPid.TabIndex = 16;
            this.lPid.Text = "Attached to:";
            // 
            // lRunning
            // 
            this.lRunning.AutoSize = true;
            this.lRunning.Location = new System.Drawing.Point(132, 17);
            this.lRunning.Name = "lRunning";
            this.lRunning.Size = new System.Drawing.Size(50, 13);
            this.lRunning.TabIndex = 17;
            this.lRunning.Text = "Running:";
            // 
            // lState
            // 
            this.lState.AutoSize = true;
            this.lState.Location = new System.Drawing.Point(132, 35);
            this.lState.Name = "lState";
            this.lState.Size = new System.Drawing.Size(35, 13);
            this.lState.TabIndex = 18;
            this.lState.Text = "State:";
            // 
            // lProfile
            // 
            this.lProfile.AutoSize = true;
            this.lProfile.Location = new System.Drawing.Point(15, 71);
            this.lProfile.Name = "lProfile";
            this.lProfile.Size = new System.Drawing.Size(39, 13);
            this.lProfile.TabIndex = 19;
            this.lProfile.Text = "Profile:";
            // 
            // lFsmTick
            // 
            this.lFsmTick.AutoSize = true;
            this.lFsmTick.Location = new System.Drawing.Point(15, 53);
            this.lFsmTick.Name = "lFsmTick";
            this.lFsmTick.Size = new System.Drawing.Size(52, 13);
            this.lFsmTick.TabIndex = 21;
            this.lFsmTick.Text = "FSM tick:";
            // 
            // lObjTick
            // 
            this.lObjTick.AutoSize = true;
            this.lObjTick.Location = new System.Drawing.Point(15, 35);
            this.lObjTick.Name = "lObjTick";
            this.lObjTick.Size = new System.Drawing.Size(72, 13);
            this.lObjTick.TabIndex = 20;
            this.lObjTick.Text = "Manager tick:";
            // 
            // bLog
            // 
            this.bLog.Location = new System.Drawing.Point(18, 216);
            this.bLog.Name = "bLog";
            this.bLog.Size = new System.Drawing.Size(79, 33);
            this.bLog.TabIndex = 27;
            this.bLog.Text = "Log";
            this.bLog.UseVisualStyleBackColor = false;
            this.bLog.Click += new System.EventHandler(this.bLog_Click);
            // 
            // rExplorer
            // 
            this.rExplorer.AutoSize = true;
            this.rExplorer.Location = new System.Drawing.Point(190, 159);
            this.rExplorer.Name = "rExplorer";
            this.rExplorer.Size = new System.Drawing.Size(63, 17);
            this.rExplorer.TabIndex = 28;
            this.rExplorer.TabStop = true;
            this.rExplorer.Text = "Explorer";
            this.rExplorer.UseVisualStyleBackColor = true;
            // 
            // teleButton
            // 
            this.teleButton.Location = new System.Drawing.Point(103, 216);
            this.teleButton.Name = "teleButton";
            this.teleButton.Size = new System.Drawing.Size(79, 33);
            this.teleButton.TabIndex = 29;
            this.teleButton.Text = "Teleport";
            this.teleButton.UseVisualStyleBackColor = false;
            this.teleButton.Click += new System.EventHandler(this.teleButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(17, 255);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(126, 17);
            this.checkBox1.TabIndex = 31;
            this.checkBox1.Text = "Beep on chat activity";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // rbSendMails
            // 
            this.rbSendMails.AutoSize = true;
            this.rbSendMails.Location = new System.Drawing.Point(190, 179);
            this.rbSendMails.Name = "rbSendMails";
            this.rbSendMails.Size = new System.Drawing.Size(107, 17);
            this.rbSendMails.TabIndex = 32;
            this.rbSendMails.TabStop = true;
            this.rbSendMails.Text = "Neck Deenchant";
            this.rbSendMails.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(310, 382);
            this.tabControl1.TabIndex = 33;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rbZgAssist);
            this.tabPage1.Controls.Add(this.rZgFarm);
            this.tabPage1.Controls.Add(this.rbSpawn);
            this.tabPage1.Controls.Add(this.rbDupe);
            this.tabPage1.Controls.Add(this.rbStock);
            this.tabPage1.Controls.Add(this.lVersion);
            this.tabPage1.Controls.Add(this.rbFishbot);
            this.tabPage1.Controls.Add(this.rbCreate);
            this.tabPage1.Controls.Add(this.rbAssist);
            this.tabPage1.Controls.Add(this.lPid);
            this.tabPage1.Controls.Add(this.rbSendMails);
            this.tabPage1.Controls.Add(this.bAttach);
            this.tabPage1.Controls.Add(this.checkBox1);
            this.tabPage1.Controls.Add(this.bDeattach);
            this.tabPage1.Controls.Add(this.teleButton);
            this.tabPage1.Controls.Add(this.bStart);
            this.tabPage1.Controls.Add(this.rExplorer);
            this.tabPage1.Controls.Add(this.bLog);
            this.tabPage1.Controls.Add(this.rGrindbot);
            this.tabPage1.Controls.Add(this.bSettings);
            this.tabPage1.Controls.Add(this.bStopBot);
            this.tabPage1.Controls.Add(this.lRunning);
            this.tabPage1.Controls.Add(this.bHide);
            this.tabPage1.Controls.Add(this.lState);
            this.tabPage1.Controls.Add(this.bShow);
            this.tabPage1.Controls.Add(this.lProfile);
            this.tabPage1.Controls.Add(this.lObjTick);
            this.tabPage1.Controls.Add(this.lFsmTick);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(302, 356);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rbZgAssist
            // 
            this.rbZgAssist.AutoSize = true;
            this.rbZgAssist.Location = new System.Drawing.Point(60, 299);
            this.rbZgAssist.Name = "rbZgAssist";
            this.rbZgAssist.Size = new System.Drawing.Size(70, 17);
            this.rbZgAssist.TabIndex = 41;
            this.rbZgAssist.TabStop = true;
            this.rbZgAssist.Text = "ZG Assist";
            this.rbZgAssist.UseVisualStyleBackColor = true;
            // 
            // rZgFarm
            // 
            this.rZgFarm.AutoSize = true;
            this.rZgFarm.Location = new System.Drawing.Point(138, 299);
            this.rZgFarm.Name = "rZgFarm";
            this.rZgFarm.Size = new System.Drawing.Size(40, 17);
            this.rZgFarm.TabIndex = 40;
            this.rZgFarm.TabStop = true;
            this.rZgFarm.Text = "ZG";
            this.rZgFarm.UseVisualStyleBackColor = true;
            // 
            // rbSpawn
            // 
            this.rbSpawn.AutoSize = true;
            this.rbSpawn.Location = new System.Drawing.Point(190, 299);
            this.rbSpawn.Name = "rbSpawn";
            this.rbSpawn.Size = new System.Drawing.Size(67, 17);
            this.rbSpawn.TabIndex = 39;
            this.rbSpawn.TabStop = true;
            this.rbSpawn.Text = "Spawner";
            this.rbSpawn.UseVisualStyleBackColor = true;
            // 
            // rbDupe
            // 
            this.rbDupe.AutoSize = true;
            this.rbDupe.Location = new System.Drawing.Point(190, 279);
            this.rbDupe.Name = "rbDupe";
            this.rbDupe.Size = new System.Drawing.Size(51, 17);
            this.rbDupe.TabIndex = 38;
            this.rbDupe.TabStop = true;
            this.rbDupe.Text = "Dupe";
            this.rbDupe.UseVisualStyleBackColor = true;
            // 
            // rbStock
            // 
            this.rbStock.AutoSize = true;
            this.rbStock.Location = new System.Drawing.Point(190, 259);
            this.rbStock.Name = "rbStock";
            this.rbStock.Size = new System.Drawing.Size(76, 17);
            this.rbStock.TabIndex = 37;
            this.rbStock.TabStop = true;
            this.rbStock.Text = "Stockades";
            this.rbStock.UseVisualStyleBackColor = true;
            // 
            // lVersion
            // 
            this.lVersion.AutoSize = true;
            this.lVersion.Location = new System.Drawing.Point(245, 334);
            this.lVersion.Name = "lVersion";
            this.lVersion.Size = new System.Drawing.Size(34, 13);
            this.lVersion.TabIndex = 36;
            this.lVersion.Text = "v1.09";
            // 
            // rbFishbot
            // 
            this.rbFishbot.AutoSize = true;
            this.rbFishbot.Location = new System.Drawing.Point(190, 239);
            this.rbFishbot.Name = "rbFishbot";
            this.rbFishbot.Size = new System.Drawing.Size(59, 17);
            this.rbFishbot.TabIndex = 35;
            this.rbFishbot.TabStop = true;
            this.rbFishbot.Text = "Fishbot";
            this.rbFishbot.UseVisualStyleBackColor = true;
            // 
            // rbCreate
            // 
            this.rbCreate.AutoSize = true;
            this.rbCreate.Location = new System.Drawing.Point(190, 219);
            this.rbCreate.Name = "rbCreate";
            this.rbCreate.Size = new System.Drawing.Size(56, 17);
            this.rbCreate.TabIndex = 34;
            this.rbCreate.TabStop = true;
            this.rbCreate.Text = "Create";
            this.rbCreate.UseVisualStyleBackColor = true;
            // 
            // rbAssist
            // 
            this.rbAssist.AutoSize = true;
            this.rbAssist.Location = new System.Drawing.Point(190, 199);
            this.rbAssist.Name = "rbAssist";
            this.rbAssist.Size = new System.Drawing.Size(84, 17);
            this.rbAssist.TabIndex = 33;
            this.rbAssist.TabStop = true;
            this.rbAssist.Text = "Group Mode";
            this.rbAssist.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.mtbItem);
            this.tabPage2.Controls.Add(this.bGetCount);
            this.tabPage2.Controls.Add(this.bAddUp);
            this.tabPage2.Controls.Add(this.bLogin);
            this.tabPage2.Controls.Add(this.bFromFile);
            this.tabPage2.Controls.Add(this.bToFile);
            this.tabPage2.Controls.Add(this.bViewObjects);
            this.tabPage2.Controls.Add(this.bstopFall);
            this.tabPage2.Controls.Add(this.bPort);
            this.tabPage2.Controls.Add(this.bGetCurrentCoords);
            this.tabPage2.Controls.Add(this.mtbZ);
            this.tabPage2.Controls.Add(this.mtbY);
            this.tabPage2.Controls.Add(this.mtbX);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(302, 356);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Dev";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(187, 66);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 48);
            this.button2.TabIndex = 16;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 37);
            this.button1.TabIndex = 15;
            this.button1.Text = "Get Distance";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_3);
            // 
            // mtbItem
            // 
            this.mtbItem.Location = new System.Drawing.Point(59, 149);
            this.mtbItem.Name = "mtbItem";
            this.mtbItem.Size = new System.Drawing.Size(131, 20);
            this.mtbItem.TabIndex = 14;
            // 
            // bGetCount
            // 
            this.bGetCount.Location = new System.Drawing.Point(196, 142);
            this.bGetCount.Name = "bGetCount";
            this.bGetCount.Size = new System.Drawing.Size(86, 33);
            this.bGetCount.TabIndex = 13;
            this.bGetCount.Text = "Get Count";
            this.bGetCount.UseVisualStyleBackColor = true;
            this.bGetCount.Click += new System.EventHandler(this.bGetCount_Click);
            // 
            // bAddUp
            // 
            this.bAddUp.Location = new System.Drawing.Point(6, 277);
            this.bAddUp.Name = "bAddUp";
            this.bAddUp.Size = new System.Drawing.Size(77, 20);
            this.bAddUp.TabIndex = 12;
            this.bAddUp.Text = "+1";
            this.bAddUp.UseVisualStyleBackColor = true;
            this.bAddUp.Click += new System.EventHandler(this.bAddUp_Click);
            // 
            // bLogin
            // 
            this.bLogin.Location = new System.Drawing.Point(101, 18);
            this.bLogin.Name = "bLogin";
            this.bLogin.Size = new System.Drawing.Size(77, 30);
            this.bLogin.TabIndex = 9;
            this.bLogin.Text = "Login";
            this.bLogin.UseVisualStyleBackColor = true;
            this.bLogin.Click += new System.EventHandler(this.bLogin_Click);
            // 
            // bFromFile
            // 
            this.bFromFile.Location = new System.Drawing.Point(89, 195);
            this.bFromFile.Name = "bFromFile";
            this.bFromFile.Size = new System.Drawing.Size(77, 22);
            this.bFromFile.TabIndex = 8;
            this.bFromFile.Text = "From File";
            this.bFromFile.UseVisualStyleBackColor = true;
            this.bFromFile.Click += new System.EventHandler(this.bFromFile_Click);
            // 
            // bToFile
            // 
            this.bToFile.Location = new System.Drawing.Point(6, 195);
            this.bToFile.Name = "bToFile";
            this.bToFile.Size = new System.Drawing.Size(77, 22);
            this.bToFile.TabIndex = 7;
            this.bToFile.Text = "To File";
            this.bToFile.UseVisualStyleBackColor = true;
            this.bToFile.Click += new System.EventHandler(this.bToFile_Click);
            // 
            // bViewObjects
            // 
            this.bViewObjects.Location = new System.Drawing.Point(18, 18);
            this.bViewObjects.Name = "bViewObjects";
            this.bViewObjects.Size = new System.Drawing.Size(77, 30);
            this.bViewObjects.TabIndex = 6;
            this.bViewObjects.Text = "View Objects";
            this.bViewObjects.UseVisualStyleBackColor = true;
            this.bViewObjects.Click += new System.EventHandler(this.bViewObjects_Click);
            // 
            // bstopFall
            // 
            this.bstopFall.Location = new System.Drawing.Point(172, 249);
            this.bstopFall.Name = "bstopFall";
            this.bstopFall.Size = new System.Drawing.Size(77, 22);
            this.bstopFall.TabIndex = 5;
            this.bstopFall.Text = "Stop fall";
            this.bstopFall.UseVisualStyleBackColor = true;
            this.bstopFall.Click += new System.EventHandler(this.bstopFall_Click);
            // 
            // bPort
            // 
            this.bPort.Location = new System.Drawing.Point(89, 249);
            this.bPort.Name = "bPort";
            this.bPort.Size = new System.Drawing.Size(77, 22);
            this.bPort.TabIndex = 4;
            this.bPort.Text = "Port";
            this.bPort.UseVisualStyleBackColor = true;
            this.bPort.Click += new System.EventHandler(this.bPort_Click);
            // 
            // bGetCurrentCoords
            // 
            this.bGetCurrentCoords.Location = new System.Drawing.Point(6, 249);
            this.bGetCurrentCoords.Name = "bGetCurrentCoords";
            this.bGetCurrentCoords.Size = new System.Drawing.Size(77, 22);
            this.bGetCurrentCoords.TabIndex = 3;
            this.bGetCurrentCoords.Text = "Get Current";
            this.bGetCurrentCoords.UseVisualStyleBackColor = true;
            this.bGetCurrentCoords.Click += new System.EventHandler(this.bGetCurrentCoords_Click);
            // 
            // mtbZ
            // 
            this.mtbZ.Location = new System.Drawing.Point(172, 223);
            this.mtbZ.Name = "mtbZ";
            this.mtbZ.Size = new System.Drawing.Size(77, 20);
            this.mtbZ.TabIndex = 2;
            // 
            // mtbY
            // 
            this.mtbY.Location = new System.Drawing.Point(89, 223);
            this.mtbY.Name = "mtbY";
            this.mtbY.Size = new System.Drawing.Size(77, 20);
            this.mtbY.TabIndex = 1;
            // 
            // mtbX
            // 
            this.mtbX.Location = new System.Drawing.Point(6, 223);
            this.mtbX.Name = "mtbX";
            this.mtbX.Size = new System.Drawing.Size(77, 20);
            this.mtbX.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(302, 356);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Misc";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tbLog);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(302, 356);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Log";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tbLog
            // 
            this.tbLog.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLog.Location = new System.Drawing.Point(18, 18);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(245, 267);
            this.tbLog.TabIndex = 0;
            this.tbLog.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(302, 356);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "About";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 221);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(101, 287);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 41);
            this.button3.TabIndex = 17;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 415);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.Text = "mainForm";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bAttach;
        private System.Windows.Forms.Button bDeattach;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.RadioButton rGrindbot;
        private System.Windows.Forms.Button bSettings;
        private System.Windows.Forms.Button bStopBot;
        private System.Windows.Forms.Button bHide;
        private System.Windows.Forms.Button bShow;
        private System.Windows.Forms.Label lPid;
        private System.Windows.Forms.Label lRunning;
        private System.Windows.Forms.Label lState;
        private System.Windows.Forms.Label lProfile;
        private System.Windows.Forms.Label lFsmTick;
        private System.Windows.Forms.Label lObjTick;
        private System.Windows.Forms.Button bLog;
        private System.Windows.Forms.RadioButton rExplorer;
        private System.Windows.Forms.Button teleButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.RadioButton rbSendMails;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button bGetCurrentCoords;
        private System.Windows.Forms.MaskedTextBox mtbZ;
        private System.Windows.Forms.MaskedTextBox mtbY;
        private System.Windows.Forms.MaskedTextBox mtbX;
        private System.Windows.Forms.Button bPort;
        private System.Windows.Forms.Button bstopFall;
        private System.Windows.Forms.Button bViewObjects;
        private System.Windows.Forms.RadioButton rbAssist;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RadioButton rbCreate;
        private System.Windows.Forms.RadioButton rbFishbot;
        private System.Windows.Forms.Button bFromFile;
        private System.Windows.Forms.Button bToFile;
        private System.Windows.Forms.Button bLogin;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Label lVersion;
        private System.Windows.Forms.RadioButton rbStock;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbDupe;
        private System.Windows.Forms.RadioButton rbSpawn;
        private System.Windows.Forms.RadioButton rZgFarm;
        private System.Windows.Forms.Button bAddUp;
        private System.Windows.Forms.RadioButton rbZgAssist;
        private System.Windows.Forms.MaskedTextBox mtbItem;
        private System.Windows.Forms.Button bGetCount;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

