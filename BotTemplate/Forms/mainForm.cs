using System;
using System.Windows.Forms;
using BotTemplate.Helper;
using BotTemplate.Interact;
using BotTemplate.Engines;
using System.Threading;
using BotTemplate.Engines.Grindbot;
using BotTemplate.Engines.CustomClass;
using System.Runtime.InteropServices;
using System.ComponentModel;
using BotTemplate.Engines.Explorer;
using System.Linq;
using BotTemplate.Engines.Mails;
using System.IO;
using BotTemplate.Constants;
using BotTemplate.Engines.Assist;
using BotTemplate.Engines.Create;
using BotTemplate.Engines.Fishbot;
using BotTemplate.Engines.Master;
using BotTemplate.Engines.Networking;
using BotTemplate.Engines.Stockades;
using BotTemplate.Engines.Dupe;
using System.Diagnostics;
using System.Text;

namespace BotTemplate.Forms
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            CCManager.Initialisate();
            CCManager.GetCustomClasses();
            var timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 2000;
            timer.Start();
        }

        int count = 0;
        System.Media.SoundPlayer sound = new System.Media.SoundPlayer(@".\Beep.wav");
        void timer_Tick(object sender, EventArgs e)
        {
            if (ObjectManager.getObjThread.IsAlive == true)
            {
                if (ChatReader.Notify)
                {
                    sound.PlaySync();
                    count = count + 1;

                    if (count == 2)
                    {
                        ChatReader.Notify = false;
                        count = 0;
                    }
                }

                lPid.Text = "Attached to: " + ObjectManager.playerName;
                this.Text = "mainForm | " + ObjectManager.playerName;
                lObjTick.Text = "Manager tick: " + (int)ObjectManager.tickRate + " ms";
                if (Exchange.IsEngineRunning == true)
                {
                    lRunning.Text = "Running: " + Exchange.CurrentEngine;
                    lState.Text = "State: " + Exchange.CurrentState;
                    lFsmTick.Text = "FSM tick: " + (int)Exchange.tickRate + " ms";
                }
            }
            else
            {
                lPid.Text = "Attached to: -";
                this.Text = "mainForm";
            }
            if (Exchange.IsEngineRunning == false)
            {
                lRunning.Text = "Running: " + Exchange.CurrentEngine;
                lState.Text = "State: None";
            }
            lProfile.Text = "Profile: " + Data.profileName;
            tbLog.AppendText(Log.get);
        }

        private void bAttach_Click(object sender, EventArgs e)
        {
            if (ObjectManager.getObjThread.IsAlive == false)
            {
                attachForm att = new attachForm();
                att.ShowDialog();

                if (attachForm.isAttached)
                {
                    if (!Inject.isHookApplied)
                    {
                        if (Inject.Apply())
                        {
                            Inject.Init();
                            ObjectManager.ExecuteOnce = true;
                            ChatReader.ClearChat = true;
                            startObjectManager();
                        }
                        else
                        {
                            MessageBox.Show("Endscene is already modified, not attaching");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Bot is already attached");
            }
        }

        private void bDeattach_Click(object sender, EventArgs e)
        {
            if (ObjectManager.getObjThread.IsAlive == true)
            {
                ShowWindow(BmWrapper.memory.WindowHandle, SW_SHOW);
                if (!Exchange.IsEngineRunning)
                {
                    Inject.Restore();
                    ObjectManager.runThread = false;
                    ChatReader.runThread = false;
                    BmWrapper.memory.WriteUInt(0x00C7B2A4, 0x0F110B73);
                }
                else
                {
                    MessageBox.Show("Stop all bots first");
                }
            }
            else
            {
                MessageBox.Show("Bot is not attached");
            }
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            if (ObjectManager.getObjThread.IsAlive)
            {
                if (!Exchange.IsEngineRunning)
                {
                    if (rbCreate.Checked)
                    {
                        startCreate();
                    }
                }
                if (ObjectManager.playerPtr != 0)
                {
                    if (Data.LoadSettings())
                    {
                        if (!Exchange.IsEngineRunning)
                        {
                            if (rZgFarm.Checked)
                            {
                                startZgFarm();
                            }

                            if (rbZgAssist.Checked)
                            {
                                startZgAssist();
                            }

                            if (rbSpawn.Checked)
                            {
                                startSpawn();
                            }

                            if (rbDupe.Checked)
                            {
                                startDupe();
                            }

                            if (rbStock.Checked)
                            {
                                startStock();
                            }

                            if (rbSendMails.Checked)
                            {
                                startMail();
                            }

                            if (rGrindbot.Checked)
                            {
                                startGrind();
                            }

                            if (rbAssist.Checked)
                            {
                                UInt64 tmp = ObjectManager.leaderGuid;
                                if (tmp != 0)
                                {
                                    if (tmp == ObjectManager.playerGuid)
                                    {
                                        if (Data.Port != 0)
                                        {
                                            startMaster();
                                        }
                                    }
                                    else
                                    {
                                        if (Data.Port != 0)
                                        {
                                            startAssist();
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("You are not in a group");
                                }
                            }

                            if (rExplorer.Checked)
                            {
                                startExplorer();
                            }

                            if (rbFishbot.Checked)
                            {
                                startFishbot();
                            }

                        }
                        else
                        {
                            MessageBox.Show("A bot is already running");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Attach Objectmanager first");
            }
        }

        private static void startGrind()
        {
            if (Grindbot.engine == null)
            {
                bool success = Grindbot.Init();

                if (success == true)
                {
                    Grindbot.engine.StartEngine(Grindbot.name);
                }
            }
        }

        private static void startMaster()
        {
            if (Master.engine == null)
            {
                bool success = Master.Init();
                if (success == true)
                {
                    clientListen.start();
                    {
                        slaveStates.Reset();
                        Calls.DoString("SetLootMethod('freeforall');");
                        Master.engine.StartEngine(Master.name);
                    }
                }
            }
        }

        private static void startAssist()
        {
            if (Assist.engine == null)
            {
                bool success = Assist.Init();

                if (success == true)
                {
                    clientConnect.Start();
                    Assist.engine.StartEngine(Assist.name);
                }
            }
        }

        private static void startFishbot()
        {
            if (Fishbot.engine == null)
            {
                bool success = Fishbot.Init();

                if (success == true)
                {
                    Fishbot.engine.StartEngine(Fishbot.name);
                }
            }
        }

        private static void startStock()
        {
            if (Stockades.engine == null)
            {
                bool success = Stockades.Init();

                if (success == true)
                {
                    Stockades.engine.StartEngine(Stockades.name);
                }
            }
        }

        private static void startDupe()
        {
            if (DupeEngine.engine == null)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to vendor the items?", "Vendor?", MessageBoxButtons.YesNo);
                DupeEngine.engine = new Dupe();
                DupeEngine.engine.StartEngine("Duper", dialogResult == DialogResult.Yes);
            }
        }

        private static void startSpawn()
        {
            if (SpawnerEngine.engine == null)
            {
                SpawnerEngine.engine = new Spawner();
                SpawnerEngine.engine.StartEngine("Spawner");
            }
        }

        private static void startZgFarm()
        {
            if (ZgFarmEngine.engine == null)
            {
                ZgFarmEngine.engine = new ZgFarm();
                ZgFarmEngine.engine.StartEngine("ZG");
            }
        }

        private static void startZgAssist()
        {
            if (ZgAssistEngine.engine == null)
            {
                ZgAssistEngine.engine = new ZgAssist();
                ZgAssistEngine.engine.StartEngine("ZG Assist");
            }
        }

        private static void startMail()
        {
            if (MailEngine.engine == null)
            {
                //string chars = "";
                //foreach (string x in Data.MailerCharacters)
                //{
                //    chars += x + "\n";
                //}

                //DialogResult dialogResult = MessageBox.Show("Gold will be distributed over:\n" +
                //chars, "Sure?", MessageBoxButtons.YesNo);
                //if (dialogResult == DialogResult.Yes)
                //{
                //    MailEngine.engine = new Mail();
                //    MailEngine.engine.StartEngine(MailEngine.name);
                //}

                MailEngine.engine = new Mail();
                MailEngine.engine.StartEngine(MailEngine.name);
            }
        }

        private static void startCreate()
        {
            if (CreateEngine.engine == null)
            {
                if (File.Exists(@".\Create.txt"))
                {
                    string[] tmpChars = File.ReadAllLines(@".\Create.txt");
                    string tmpStringChars = "";
                    foreach (string x in tmpChars)
                    {
                        tmpStringChars += x + "\n";
                    }
                    DialogResult dialogResult = MessageBox.Show("Following Characters will be created:\n" +
                    tmpStringChars, "Sure?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        CreateEngine.engine = new Create(tmpChars);
                        CreateEngine.engine.StartEngine(CreateEngine.name);
                    }
                }
                else
                {
                    MessageBox.Show("Create.txt doesnt exist in " + Application.ExecutablePath.ToString());
                }
            }
        }

        private static void startExplorer()
        {
            if (ExplorerEngine.engine == null)
            {
                DialogResult dialogResult = MessageBox.Show("Do you really want to run the explorer?", "Sure?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (ObjectManager.MapId == 0 || ObjectManager.MapId == 1)
                    {
                        ExplorerEngine.engine = new Explorer();
                        ExplorerEngine.engine.StartEngine(ExplorerEngine.name);
                    }
                    else
                    {
                        MessageBox.Show("Wrong map id!");
                    }
                }
            }
        }

        private static void stopExplorer()
        {
            if (ExplorerEngine.engine != null)
            {
                ExplorerEngine.engine.StopRunning();
            }
        }

        private static void stopGrind()
        {
            if (Grindbot.engine != null)
            {
                Grindbot.engine.StopEngine();
                Grindbot.Dispose();
                GrindbotContainer.Reset();
            }
        }

        private static void stopMaster()
        {
            if (Master.engine != null)
            {
                Master.engine.StopEngine();
                Master.Dispose();
                MasterContainer.Reset();
                clientListen.stop();
            }
        }

        private static void stopAssist()
        {
            if (Assist.engine != null)
            {
                Assist.engine.StopEngine();
                Assist.Dispose();
                AssistContainer.reset();
                clientConnect.Stop();
            }
        }

        private static void stopFishbot()
        {
            if (Fishbot.engine != null)
            {
                Fishbot.engine.StopEngine();
                Fishbot.Dispose();
            }
        }

        private static void stopDupe()
        {
            if (DupeEngine.engine != null)
            {
                DupeEngine.engine.StopEngine();
            }
        }

        private static void stopSpawn()
        {
            if (SpawnerEngine.engine != null)
            {
                SpawnerEngine.engine.StopEngine();
            }
        }

        private static void stopZgFarm()
        {
            if (ZgFarmEngine.engine != null)
            {
                ZgFarmEngine.engine.StopEngine();
            }
        }

        private static void stopZgAssist()
        {
            if (ZgAssistEngine.engine != null)
            {
                ZgAssistEngine.engine.StopEngine();
            }
        }

        private static void stopMailer()
        {
            if (MailEngine.engine != null)
            {
                MailEngine.engine.StopEngine();
            }
        }

        private static void stopStock()
        {
            if (Stockades.engine != null)
            {
                Stockades.engine.StopEngine();
                Stockades.Dispose();
            }
        }

        private static void startObjectManager()
        {
            ObjectManager.getObjThread = new Thread(ObjectManager.getObj);
            ObjectManager.getObjThread.IsBackground = true;
            ObjectManager.runThread = true;
            ObjectManager.getObjThread.Start();
            startChatReader();
        }

        private static void startChatReader()
        {
            ChatReader.readChatThread = new Thread(ChatReader.readChat);
            ChatReader.readChatThread.IsBackground = true;
            ChatReader.runThread = true;
            ChatReader.readChatThread.Start();
        }

        private void bSettings_Click(object sender, EventArgs e)
        {
            if (Data.settingsExist)
            {
                if (Data.protectedExist)
                {
                    if (Data.mailerExist)
                    {
                        if (Data.LoadSettings())
                        {
                            settingsForm stForm = new settingsForm();
                            DialogResult test = stForm.ShowDialog();
                        }
                    }
                    else
                    {
                        Data.SaveMailer(new string[] { "" });
                    }
                }
                else
                {
                    Data.SaveProtected(new string[] { "" });
                }
            }
            else
            {
                Data.SaveSettings(0, 0, 0, 0, "", "", 0, "", "", 0, "", false, false, false, false, false, false, "0");
                MessageBox.Show("Settings recreated, hit Settings again", "Recreated");
            }

        }

        private void bStopBot_Click(object sender, EventArgs e)
        {
            if (Exchange.IsEngineRunning)
            {
                stopGrind();
                stopZgFarm();
                stopZgAssist();
                stopSpawn();
                stopMailer();
                stopMaster();
                stopAssist();
                stopFishbot();
                stopDupe();
                stopExplorer();
                stopStock();
                Calls.StopRunning();
            }
            else
            {
                MessageBox.Show("No bot running");
            }
        }

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hwnd, int command);
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        private void bHide_Click(object sender, EventArgs e)
        {

            if (BmWrapper.memory.IsProcessOpen) ShowWindow(BmWrapper.memory.WindowHandle, SW_HIDE);
        }

        private void bShow_Click(object sender, EventArgs e)
        {
            if (BmWrapper.memory.IsProcessOpen) ShowWindow(BmWrapper.memory.WindowHandle, SW_SHOW);
        }

        private void teleButton_Click(object sender, EventArgs e)
        {
            if (ObjectManager.getObjThread.IsAlive)
            {
                if (!Exchange.IsEngineRunning)
                {
                    this.Hide();
                    teleportForm f1 = new teleportForm();
                    f1.ShowDialog();
                    this.Show();
                }
            }
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            Data.LoadSettings();
        }

        private void bLog_Click(object sender, EventArgs e)
        {
            whisperForm whsp = new whisperForm();
            whsp.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ChatReader.doNotify = checkBox1.Checked;
        }

        private void bGetCurrentCoords_Click(object sender, EventArgs e)
        {
            if (ObjectManager.getObjThread.IsAlive)
            {
                if (!Exchange.IsEngineRunning)
                {
                    if (ObjectManager.playerPtr != 0)
                    {
                        Objects.Location tmp = ObjectManager.PlayerObject.Pos;
                        mtbX.Text = tmp.x.ToString();
                        mtbY.Text = tmp.y.ToString();
                        mtbZ.Text = tmp.z.ToString();
                    }
                }
            }
        }

        private void bPort_Click(object sender, EventArgs e)
        {
            if (ObjectManager.getObjThread.IsAlive)
            {
                if (!Exchange.IsEngineRunning)
                {
                    if (ObjectManager.playerPtr != 0)
                    {
                        try
                        {
                            Ingame.Tele(new Objects.Location(
                                Convert.ToSingle(mtbX.Text.Replace('.', ',')),
                                Convert.ToSingle(mtbY.Text.Replace('.', ',')),
                                Convert.ToSingle(mtbZ.Text.Replace('.', ','))),
                                60,
                                false);
                        }
                        catch { }
                    }
                }
            }

        }

        private void bstopFall_Click(object sender, EventArgs e)
        {
            if (ObjectManager.getObjThread.IsAlive)
            {
                if (!Exchange.IsEngineRunning)
                {
                    if (ObjectManager.playerPtr != 0)
                    {
                        Calls.SetMovementFlags(0);
                    }
                }
            }
        }

        private void bViewObjects_Click(object sender, EventArgs e)
        {
            if (ObjectManager.getObjThread.IsAlive)
            {
                if (!Exchange.IsEngineRunning)
                {
                    if (ObjectManager.playerPtr != 0)
                    {
                        this.Hide();
                        objectViewerForm form = new objectViewerForm();
                        form.ShowDialog();
                        this.Show();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Calls.GetText("lol = 'haha'", "lol", 4));
        }

        private void bToFile_Click(object sender, EventArgs e)
        {
            if (ObjectManager.getObjThread.IsAlive)
            {
                if (!Exchange.IsEngineRunning)
                {
                    if (ObjectManager.playerPtr != 0)
                    {
                        try
                        {
                            File.WriteAllText(@"C:\\tmpCoords.txt", mtbX.Text + "|" + mtbY.Text + "|" + mtbZ.Text);
                        }
                        catch { }
                    }
                }
            }
        }

        private void bFromFile_Click(object sender, EventArgs e)
        {
            if (ObjectManager.getObjThread.IsAlive)
            {
                if (!Exchange.IsEngineRunning)
                {
                    if (ObjectManager.playerPtr != 0)
                    {
                        try
                        {
                            string tmp = File.ReadAllText(@"C:\\tmpCoords.txt");
                            string[] tmpArr = tmp.Split('|');
                            mtbX.Text = tmpArr[0];
                            mtbY.Text = tmpArr[1];
                            mtbZ.Text = tmpArr[2];
                        }
                        catch { }
                    }
                }
            }
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            if (ObjectManager.getObjThread.IsAlive)
            {
                if (!Exchange.IsEngineRunning)
                {
                    if (ObjectManager.LoginState == "login")
                    {
                        if (Data.AccName != "" && Data.AccPw != "")
                        {
                            Calls.DoString("DefaultServerLogin('" + Data.AccName + "', '" + Data.AccPw + "');");
                        }
                    }
                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(ObjectManager.IsUnitOnGroup(ObjectManager.TargetObject.targetGuid, false, 100).ToString());
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            MessageBox.Show(Calls.GetText("money = GetMoney();", "money", 10));
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void bOrb_Click(object sender, EventArgs e)
        {
            Ingame.Tele(new Objects.Location(41.8915f, -285.414f, 105.958f), 60, false);
            Calls.OnRightClickObject(ObjectManager.GetGameObjectByName("Drakkisath's Brand").baseAdd, 1);
        }

        private void bMc_Click(object sender, EventArgs e)
        {
            Ingame.Tele(new Objects.Location(41.8915f, -285.414f, 105.958f), 60, false);
        }

        private void bAddUp_Click(object sender, EventArgs e)
        {
            float x = ObjectManager.PlayerObject.Pos.x +(float)1 * (float)Math.Cos(ObjectManager.Facing);
            float y = ObjectManager.PlayerObject.Pos.y + (float)1 * (float)Math.Sin(ObjectManager.Facing);
            float z = ObjectManager.PlayerObject.Pos.z;

            mtbX.Text = x.ToString();
            mtbY.Text = y.ToString();
            mtbZ.Text = z.ToString();
        }

        private void bGetCount_Click(object sender, EventArgs e)
        {
            string lua = "totalCount = 0 for bag = 0,4,1 do for slot = 1, GetContainerNumSlots(bag), 1 do name = GetContainerItemLink(bag,slot) if name then name = gsub(name,'^.*%[(.*)%].*$','%1') if string.find(name, '" + mtbItem.Text + "') ~= nil then _, stack = GetContainerItemInfo(bag, slot) totalCount = totalCount + stack; end end end end";
            MessageBox.Show(Calls.GetText(lua, "totalCount", 10));
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            Console.WriteLine(ObjectManager.TargetObject.Pos.differenceToPlayer());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Calls.DoString("for k = 0,4 do for j = 1,18 do link = GetContainerItemLink(k,j) if ( link ) then link = gsub(link,'^.*%[(.*)%].*$','%1') if ( string.lower(" + "\"Iron Grenade\"" + ") == string.lower(link)) then UseContainerItem(k,j) return end end end end");
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        internal static extern void MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        private void button3_Click(object sender, EventArgs e)
        {
            //Calls.DoString("TakeInboxItem(1, 1) Logout()");
            

            Calls.PacketZoneChange();
            Calls.PacketSendLogout();

            //MoveWindow(BmWrapper.memory.WindowHandle, 0, 0, 153, 119, true);

            //StringBuilder className = new StringBuilder(256);
            //int ret = GetClassName(Process.GetProcessById(BmWrapper.memory.ProcessId).MainWindowHandle, className, className.Capacity);

            //MessageBox.Show("Is crashed: " + ObjectManager.IsWowCrashed().ToString()
            //    + "\nHandles (on attach / current): " + BmWrapper.memory.WindowHandle + " " + Process.GetProcessById(BmWrapper.memory.ProcessId).MainWindowHandle
            //    + "\nWindow class: " + className.ToString());
        }
    }
}