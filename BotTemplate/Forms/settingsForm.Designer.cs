namespace BotTemplate.Forms
{
    partial class settingsForm
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
            this.nudHealth = new System.Windows.Forms.NumericUpDown();
            this.lHealth = new System.Windows.Forms.Label();
            this.lMana = new System.Windows.Forms.Label();
            this.nudMana = new System.Windows.Forms.NumericUpDown();
            this.bSave = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.nudRoam = new System.Windows.Forms.NumericUpDown();
            this.lRoam = new System.Windows.Forms.Label();
            this.lSearch = new System.Windows.Forms.Label();
            this.nudSearch = new System.Windows.Forms.NumericUpDown();
            this.mtbDrink = new System.Windows.Forms.MaskedTextBox();
            this.mtbFood = new System.Windows.Forms.MaskedTextBox();
            this.lDrink = new System.Windows.Forms.Label();
            this.lFood = new System.Windows.Forms.Label();
            this.lPw = new System.Windows.Forms.Label();
            this.lAcc = new System.Windows.Forms.Label();
            this.mtbPw = new System.Windows.Forms.MaskedTextBox();
            this.mtbAcc = new System.Windows.Forms.MaskedTextBox();
            this.lRange = new System.Windows.Forms.Label();
            this.nudRange = new System.Windows.Forms.NumericUpDown();
            this.lLeaveSlots = new System.Windows.Forms.Label();
            this.nudLeaveSlots = new System.Windows.Forms.NumericUpDown();
            this.lLure = new System.Windows.Forms.Label();
            this.mtbLure = new System.Windows.Forms.MaskedTextBox();
            this.cbResting = new System.Windows.Forms.CheckBox();
            this.tbProtected = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbVendor = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbGreen = new System.Windows.Forms.CheckBox();
            this.cbBlue = new System.Windows.Forms.CheckBox();
            this.cbPurple = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cbStopOnVendorFail = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.lMailReciever = new System.Windows.Forms.Label();
            this.tbMailer = new System.Windows.Forms.TextBox();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.mtbPort = new System.Windows.Forms.MaskedTextBox();
            this.lConnectPort = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRoam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeaveSlots)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.SuspendLayout();
            // 
            // nudHealth
            // 
            this.nudHealth.Location = new System.Drawing.Point(141, 18);
            this.nudHealth.Name = "nudHealth";
            this.nudHealth.Size = new System.Drawing.Size(58, 20);
            this.nudHealth.TabIndex = 0;
            // 
            // lHealth
            // 
            this.lHealth.AutoSize = true;
            this.lHealth.Location = new System.Drawing.Point(20, 20);
            this.lHealth.Name = "lHealth";
            this.lHealth.Size = new System.Drawing.Size(115, 13);
            this.lHealth.TabIndex = 1;
            this.lHealth.Text = "Regnerate at % health:";
            // 
            // lMana
            // 
            this.lMana.AutoSize = true;
            this.lMana.Location = new System.Drawing.Point(20, 45);
            this.lMana.Name = "lMana";
            this.lMana.Size = new System.Drawing.Size(112, 13);
            this.lMana.TabIndex = 2;
            this.lMana.Text = "Regnerate at % mana:";
            // 
            // nudMana
            // 
            this.nudMana.Location = new System.Drawing.Point(141, 43);
            this.nudMana.Name = "nudMana";
            this.nudMana.Size = new System.Drawing.Size(58, 20);
            this.nudMana.TabIndex = 3;
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(87, 367);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(71, 30);
            this.bSave.TabIndex = 4;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(176, 367);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(71, 30);
            this.bCancel.TabIndex = 5;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // nudRoam
            // 
            this.nudRoam.Location = new System.Drawing.Point(119, 18);
            this.nudRoam.Name = "nudRoam";
            this.nudRoam.Size = new System.Drawing.Size(58, 20);
            this.nudRoam.TabIndex = 6;
            // 
            // lRoam
            // 
            this.lRoam.AutoSize = true;
            this.lRoam.Location = new System.Drawing.Point(20, 20);
            this.lRoam.Name = "lRoam";
            this.lRoam.Size = new System.Drawing.Size(82, 13);
            this.lRoam.TabIndex = 7;
            this.lRoam.Text = "Roam from WP:";
            // 
            // lSearch
            // 
            this.lSearch.AutoSize = true;
            this.lSearch.Location = new System.Drawing.Point(20, 45);
            this.lSearch.Name = "lSearch";
            this.lSearch.Size = new System.Drawing.Size(93, 13);
            this.lSearch.TabIndex = 9;
            this.lSearch.Text = "Mob searchrange:";
            // 
            // nudSearch
            // 
            this.nudSearch.Location = new System.Drawing.Point(119, 43);
            this.nudSearch.Name = "nudSearch";
            this.nudSearch.Size = new System.Drawing.Size(58, 20);
            this.nudSearch.TabIndex = 8;
            // 
            // mtbDrink
            // 
            this.mtbDrink.Location = new System.Drawing.Point(90, 17);
            this.mtbDrink.Name = "mtbDrink";
            this.mtbDrink.Size = new System.Drawing.Size(99, 20);
            this.mtbDrink.TabIndex = 10;
            this.mtbDrink.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventNoBackslash);
            // 
            // mtbFood
            // 
            this.mtbFood.Location = new System.Drawing.Point(90, 42);
            this.mtbFood.Name = "mtbFood";
            this.mtbFood.Size = new System.Drawing.Size(99, 20);
            this.mtbFood.TabIndex = 11;
            this.mtbFood.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventNoBackslash);
            // 
            // lDrink
            // 
            this.lDrink.AutoSize = true;
            this.lDrink.Location = new System.Drawing.Point(20, 20);
            this.lDrink.Name = "lDrink";
            this.lDrink.Size = new System.Drawing.Size(64, 13);
            this.lDrink.TabIndex = 12;
            this.lDrink.Text = "Drink name:";
            // 
            // lFood
            // 
            this.lFood.AutoSize = true;
            this.lFood.Location = new System.Drawing.Point(20, 45);
            this.lFood.Name = "lFood";
            this.lFood.Size = new System.Drawing.Size(63, 13);
            this.lFood.TabIndex = 13;
            this.lFood.Text = "Food name:";
            // 
            // lPw
            // 
            this.lPw.AutoSize = true;
            this.lPw.Location = new System.Drawing.Point(20, 45);
            this.lPw.Name = "lPw";
            this.lPw.Size = new System.Drawing.Size(56, 13);
            this.lPw.TabIndex = 18;
            this.lPw.Text = "Password:";
            // 
            // lAcc
            // 
            this.lAcc.AutoSize = true;
            this.lAcc.Location = new System.Drawing.Point(20, 20);
            this.lAcc.Name = "lAcc";
            this.lAcc.Size = new System.Drawing.Size(29, 13);
            this.lAcc.TabIndex = 17;
            this.lAcc.Text = "Acc:";
            // 
            // mtbPw
            // 
            this.mtbPw.Location = new System.Drawing.Point(82, 42);
            this.mtbPw.Name = "mtbPw";
            this.mtbPw.Size = new System.Drawing.Size(99, 20);
            this.mtbPw.TabIndex = 16;
            this.mtbPw.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventOnlyLetterDigit);
            // 
            // mtbAcc
            // 
            this.mtbAcc.Location = new System.Drawing.Point(82, 17);
            this.mtbAcc.Name = "mtbAcc";
            this.mtbAcc.Size = new System.Drawing.Size(99, 20);
            this.mtbAcc.TabIndex = 15;
            this.mtbAcc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventOnlyLetterDigit);
            // 
            // lRange
            // 
            this.lRange.AutoSize = true;
            this.lRange.Location = new System.Drawing.Point(20, 20);
            this.lRange.Name = "lRange";
            this.lRange.Size = new System.Drawing.Size(60, 13);
            this.lRange.TabIndex = 19;
            this.lRange.Text = "Fightrange:";
            // 
            // nudRange
            // 
            this.nudRange.Location = new System.Drawing.Point(86, 18);
            this.nudRange.Name = "nudRange";
            this.nudRange.Size = new System.Drawing.Size(91, 20);
            this.nudRange.TabIndex = 20;
            // 
            // lLeaveSlots
            // 
            this.lLeaveSlots.AutoSize = true;
            this.lLeaveSlots.Location = new System.Drawing.Point(20, 20);
            this.lLeaveSlots.Name = "lLeaveSlots";
            this.lLeaveSlots.Size = new System.Drawing.Size(95, 13);
            this.lLeaveSlots.TabIndex = 22;
            this.lLeaveSlots.Text = "Leave X slots free:";
            // 
            // nudLeaveSlots
            // 
            this.nudLeaveSlots.Location = new System.Drawing.Point(162, 21);
            this.nudLeaveSlots.Name = "nudLeaveSlots";
            this.nudLeaveSlots.Size = new System.Drawing.Size(58, 20);
            this.nudLeaveSlots.TabIndex = 21;
            // 
            // lLure
            // 
            this.lLure.AutoSize = true;
            this.lLure.Location = new System.Drawing.Point(20, 20);
            this.lLure.Name = "lLure";
            this.lLure.Size = new System.Drawing.Size(53, 13);
            this.lLure.TabIndex = 24;
            this.lLure.Text = "Use Lure:";
            // 
            // mtbLure
            // 
            this.mtbLure.Location = new System.Drawing.Point(79, 17);
            this.mtbLure.Name = "mtbLure";
            this.mtbLure.Size = new System.Drawing.Size(99, 20);
            this.mtbLure.TabIndex = 23;
            this.mtbLure.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventNoBackslash);
            // 
            // cbResting
            // 
            this.cbResting.AutoSize = true;
            this.cbResting.Location = new System.Drawing.Point(20, 80);
            this.cbResting.Name = "cbResting";
            this.cbResting.Size = new System.Drawing.Size(137, 17);
            this.cbResting.TabIndex = 25;
            this.cbResting.Text = "Use own resting routine";
            this.cbResting.UseVisualStyleBackColor = true;
            // 
            // tbProtected
            // 
            this.tbProtected.Location = new System.Drawing.Point(20, 70);
            this.tbProtected.Multiline = true;
            this.tbProtected.Name = "tbProtected";
            this.tbProtected.Size = new System.Drawing.Size(193, 77);
            this.tbProtected.TabIndex = 26;
            this.tbProtected.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventNoBackslash);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Protected Items:";
            // 
            // cbVendor
            // 
            this.cbVendor.AutoSize = true;
            this.cbVendor.Location = new System.Drawing.Point(20, 250);
            this.cbVendor.Name = "cbVendor";
            this.cbVendor.Size = new System.Drawing.Size(88, 17);
            this.cbVendor.TabIndex = 28;
            this.cbVendor.Text = "Vendor Items";
            this.cbVendor.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Keep:";
            // 
            // cbGreen
            // 
            this.cbGreen.AutoSize = true;
            this.cbGreen.Location = new System.Drawing.Point(20, 185);
            this.cbGreen.Name = "cbGreen";
            this.cbGreen.Size = new System.Drawing.Size(67, 17);
            this.cbGreen.TabIndex = 30;
            this.cbGreen.Text = "Common";
            this.cbGreen.UseVisualStyleBackColor = true;
            // 
            // cbBlue
            // 
            this.cbBlue.AutoSize = true;
            this.cbBlue.Location = new System.Drawing.Point(93, 185);
            this.cbBlue.Name = "cbBlue";
            this.cbBlue.Size = new System.Drawing.Size(49, 17);
            this.cbBlue.TabIndex = 31;
            this.cbBlue.Text = "Rare";
            this.cbBlue.UseVisualStyleBackColor = true;
            // 
            // cbPurple
            // 
            this.cbPurple.AutoSize = true;
            this.cbPurple.Location = new System.Drawing.Point(148, 185);
            this.cbPurple.Name = "cbPurple";
            this.cbPurple.Size = new System.Drawing.Size(47, 17);
            this.cbPurple.TabIndex = 32;
            this.cbPurple.Text = "Epic";
            this.cbPurple.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(327, 349);
            this.tabControl1.TabIndex = 33;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lRoam);
            this.tabPage1.Controls.Add(this.nudRoam);
            this.tabPage1.Controls.Add(this.nudSearch);
            this.tabPage1.Controls.Add(this.lSearch);
            this.tabPage1.Location = new System.Drawing.Point(4, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(319, 305);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Pathing";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lHealth);
            this.tabPage2.Controls.Add(this.nudHealth);
            this.tabPage2.Controls.Add(this.lMana);
            this.tabPage2.Controls.Add(this.nudMana);
            this.tabPage2.Location = new System.Drawing.Point(4, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(319, 305);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Regnerate";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cbStopOnVendorFail);
            this.tabPage3.Controls.Add(this.lLeaveSlots);
            this.tabPage3.Controls.Add(this.cbPurple);
            this.tabPage3.Controls.Add(this.nudLeaveSlots);
            this.tabPage3.Controls.Add(this.cbBlue);
            this.tabPage3.Controls.Add(this.tbProtected);
            this.tabPage3.Controls.Add(this.cbGreen);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.cbVendor);
            this.tabPage3.Location = new System.Drawing.Point(4, 40);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(319, 305);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Vendoring";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cbStopOnVendorFail
            // 
            this.cbStopOnVendorFail.AutoSize = true;
            this.cbStopOnVendorFail.Location = new System.Drawing.Point(20, 215);
            this.cbStopOnVendorFail.Name = "cbStopOnVendorFail";
            this.cbStopOnVendorFail.Size = new System.Drawing.Size(115, 17);
            this.cbStopOnVendorFail.TabIndex = 33;
            this.cbStopOnVendorFail.Text = "Stop on vendor fail";
            this.cbStopOnVendorFail.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.cbResting);
            this.tabPage4.Controls.Add(this.lDrink);
            this.tabPage4.Controls.Add(this.mtbDrink);
            this.tabPage4.Controls.Add(this.mtbFood);
            this.tabPage4.Controls.Add(this.lFood);
            this.tabPage4.Location = new System.Drawing.Point(4, 40);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(319, 305);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Rest";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.lLure);
            this.tabPage5.Controls.Add(this.mtbLure);
            this.tabPage5.Location = new System.Drawing.Point(4, 40);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(319, 305);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Fishing";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.lRange);
            this.tabPage6.Controls.Add(this.nudRange);
            this.tabPage6.Location = new System.Drawing.Point(4, 40);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(319, 305);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Fight";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.lAcc);
            this.tabPage7.Controls.Add(this.lPw);
            this.tabPage7.Controls.Add(this.mtbAcc);
            this.tabPage7.Controls.Add(this.mtbPw);
            this.tabPage7.Location = new System.Drawing.Point(4, 40);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(319, 305);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Relog";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.lMailReciever);
            this.tabPage8.Controls.Add(this.tbMailer);
            this.tabPage8.Location = new System.Drawing.Point(4, 40);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(319, 305);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Mailer";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // lMailReciever
            // 
            this.lMailReciever.AutoSize = true;
            this.lMailReciever.Location = new System.Drawing.Point(20, 20);
            this.lMailReciever.Name = "lMailReciever";
            this.lMailReciever.Size = new System.Drawing.Size(74, 13);
            this.lMailReciever.TabIndex = 28;
            this.lMailReciever.Text = "Mail recipients";
            // 
            // tbMailer
            // 
            this.tbMailer.Location = new System.Drawing.Point(20, 40);
            this.tbMailer.Multiline = true;
            this.tbMailer.Name = "tbMailer";
            this.tbMailer.Size = new System.Drawing.Size(193, 77);
            this.tbMailer.TabIndex = 27;
            this.tbMailer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventOnlyLetter);
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.mtbPort);
            this.tabPage9.Controls.Add(this.lConnectPort);
            this.tabPage9.Location = new System.Drawing.Point(4, 40);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(319, 305);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "Group Mode";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // mtbPort
            // 
            this.mtbPort.Location = new System.Drawing.Point(88, 17);
            this.mtbPort.Name = "mtbPort";
            this.mtbPort.Size = new System.Drawing.Size(100, 20);
            this.mtbPort.TabIndex = 3;
            this.mtbPort.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.mtbConnectPort_MaskInputRejected);
            this.mtbPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventOnlyDigits);
            // 
            // lConnectPort
            // 
            this.lConnectPort.AutoSize = true;
            this.lConnectPort.Location = new System.Drawing.Point(20, 20);
            this.lConnectPort.Name = "lConnectPort";
            this.lConnectPort.Size = new System.Drawing.Size(51, 13);
            this.lConnectPort.TabIndex = 1;
            this.lConnectPort.Text = "Use Port:";
            this.lConnectPort.Click += new System.EventHandler(this.lConnectPort_Click);
            // 
            // settingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 413);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "settingsForm";
            this.Text = "settingsForm";
            ((System.ComponentModel.ISupportInitialize)(this.nudHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRoam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeaveSlots)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            this.tabPage9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudHealth;
        private System.Windows.Forms.Label lHealth;
        private System.Windows.Forms.Label lMana;
        private System.Windows.Forms.NumericUpDown nudMana;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.NumericUpDown nudRoam;
        private System.Windows.Forms.Label lRoam;
        private System.Windows.Forms.Label lSearch;
        private System.Windows.Forms.NumericUpDown nudSearch;
        private System.Windows.Forms.MaskedTextBox mtbDrink;
        private System.Windows.Forms.MaskedTextBox mtbFood;
        private System.Windows.Forms.Label lDrink;
        private System.Windows.Forms.Label lFood;
        private System.Windows.Forms.Label lPw;
        private System.Windows.Forms.Label lAcc;
        private System.Windows.Forms.MaskedTextBox mtbPw;
        private System.Windows.Forms.MaskedTextBox mtbAcc;
        private System.Windows.Forms.Label lRange;
        private System.Windows.Forms.NumericUpDown nudRange;
        private System.Windows.Forms.Label lLeaveSlots;
        private System.Windows.Forms.NumericUpDown nudLeaveSlots;
        private System.Windows.Forms.Label lLure;
        private System.Windows.Forms.MaskedTextBox mtbLure;
        private System.Windows.Forms.CheckBox cbResting;
        private System.Windows.Forms.TextBox tbProtected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbVendor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbGreen;
        private System.Windows.Forms.CheckBox cbBlue;
        private System.Windows.Forms.CheckBox cbPurple;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.CheckBox cbStopOnVendorFail;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Label lMailReciever;
        private System.Windows.Forms.TextBox tbMailer;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.MaskedTextBox mtbPort;
        private System.Windows.Forms.Label lConnectPort;
    }
}