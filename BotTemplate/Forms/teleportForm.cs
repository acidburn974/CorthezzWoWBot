using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BotTemplate.Objects;
using BotTemplate.Engines;
using BotTemplate.Helper;
using BotTemplate.Interact;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;

namespace BotTemplate.Forms
{
    public partial class teleportForm : Form
    {
        [DllImport("USER32.dll")]
        static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public teleportForm()
        {
            InitializeComponent();
            if (File.Exists(@"teleports.ini"))
            {
                strTeleports = Tools.DecodeFrom64(File.ReadAllText(@"teleports.ini")).Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList<String>();
            }
            UpdateGrid();
            BmWrapper.memory.WriteUInt(Inject.click2PortToggle, 0);
            Calls.cancelTele = false;
            thrTele = new Thread(Run) { IsBackground = true };
            runThread = true;
            thrTele.Start();
        }
        private List<String> strTeleports = new List<String>();

        private Thread thrTele;
        private Objects.Location curLoc;
        private bool gotJob = false;
        Objects.Location loc = new Objects.Location();
        int lastPort = 0;
        bool runThread = false;

        bool shouldPort = true;
        int timestamp = 0;

        private void Run()
        {
            while (runThread)
            {
                if (gotJob)
                {
                    if (Environment.TickCount - lastPort >= 500)
                    {
                        Ingame.Tele(curLoc, 60, true);
                        gotJob = false;
                        lastPort = Environment.TickCount;
                    }
                }
                else
                {
                    if (cbHotKey.Checked)
                    {
                        if (GetForegroundWindow() == BmWrapper.memory.WindowHandle) // has the attached wow focus?
                        {
                            if (BmWrapper.memory.ReadUInt(Inject.click2PortToggle) == 1)
                            {
                                loc.x = BmWrapper.memory.ReadFloat(Inject.click2PortCoords);
                                loc.y = BmWrapper.memory.ReadFloat(Inject.click2PortCoords + 4);
                                loc.z = BmWrapper.memory.ReadFloat(Inject.click2PortCoords + 8);
                                BmWrapper.memory.WriteUInt(Inject.click2PortToggle, 0);
                                Ingame.Tele(loc, 60, false);

                            }
                            else
                            {
                                float seconds = (float)nudTeleSpeed.Value;
                                float speed = 7f * seconds;
                                if ((GetKeyState(0x68) & 0x0000FF80) == 0x0000FF80) // Numpad 8 pressed?
                                {
                                    loc.x = ObjectManager.PlayerObject.Pos.x + (float)speed * (float)Math.Cos(ObjectManager.Facing);
                                    loc.y = ObjectManager.PlayerObject.Pos.y + (float)speed * (float)Math.Sin(ObjectManager.Facing);
                                    loc.z = ObjectManager.PlayerObject.Pos.z;
                                    Console.WriteLine("Difference: " + loc.differenceTo(ObjectManager.PlayerObject.Pos));
                                    Ingame.hotKeyTele(loc, (float)seconds);
                                }
                                else if ((GetKeyState(0x67) & 0x0000FF80) == 0x0000FF80) // Numpad 7 pressed?
                                {
                                    loc.x = ObjectManager.PlayerObject.Pos.x;
                                    loc.y = ObjectManager.PlayerObject.Pos.y;
                                    loc.z = ObjectManager.PlayerObject.Pos.z + (float)speed;
                                    Ingame.hotKeyTele(loc, (float)seconds);
                                }
                                else if ((GetKeyState(0x69) & 0x0000FF80) == 0x0000FF80) // Numpad 9 pressed?
                                {
                                    loc.x = ObjectManager.PlayerObject.Pos.x;
                                    loc.y = ObjectManager.PlayerObject.Pos.y;
                                    loc.z = ObjectManager.PlayerObject.Pos.z - (float)speed;
                                    Ingame.hotKeyTele(loc, (float)seconds);
                                }
                                else if ((GetKeyState(0x65) & 0x0000FF80) == 0x0000FF80) // Numpad 5 pressed?
                                {
                                    loc.x = ObjectManager.PlayerObject.Pos.x + (float)speed * (float)-Math.Cos(ObjectManager.Facing);
                                    loc.y = ObjectManager.PlayerObject.Pos.y + (float)speed * (float)-Math.Sin(ObjectManager.Facing);
                                    loc.z = ObjectManager.PlayerObject.Pos.z;
                                    Ingame.hotKeyTele(loc, (float)seconds);
                                }
                                else if ((GetKeyState(0x64) & 0x0000FF80) == 0x0000FF80) // Numpad 4 pressed?
                                {
                                    loc.x = ObjectManager.PlayerObject.Pos.x + (float)speed * (float)Math.Cos(ObjectManager.Facing + (Math.PI / 2));
                                    loc.y = ObjectManager.PlayerObject.Pos.y + (float)speed * (float)Math.Sin(ObjectManager.Facing + (Math.PI / 2));
                                    loc.z = ObjectManager.PlayerObject.Pos.z;
                                    Ingame.hotKeyTele(loc, (float)seconds);
                                }
                                else if ((GetKeyState(0x66) & 0x0000FF80) == 0x0000FF80) // Numpad 6 pressed?
                                {
                                    loc.x = ObjectManager.PlayerObject.Pos.x + (float)speed * (float)Math.Cos(ObjectManager.Facing - (Math.PI / 2));
                                    loc.y = ObjectManager.PlayerObject.Pos.y + (float)speed * (float)Math.Sin(ObjectManager.Facing - (Math.PI / 2));
                                    loc.z = ObjectManager.PlayerObject.Pos.z;
                                    Ingame.hotKeyTele(loc, (float)seconds);
                                }
                            }
                        }
                        else
                        {
                            //BmWrapper.memory.WriteUInt(Inject.timeStampMdofier, 0);
                        }
                    }
                }
            }
        }

        private void SaveChanges()
        {
            string tmp = "";
            foreach (string x in strTeleports)
            {
                tmp += x + Environment.NewLine;
            }
            File.WriteAllText(@"teleports.ini", Tools.EncodeTo64(tmp));
        }

        private void UpdateGrid()
        {
            telePointGrid.Rows.Clear();

            foreach (string x in strTeleports)
            {
                string[] tmp = x.Split('|');
                if (tmp.Length == 5)
                {
                    if (tmp[0].ToLower().Contains(tbSearch.Text.ToLower()))
                    {
                        float tmpX;
                        if (float.TryParse(tmp[1].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out tmpX))
                        {
                            float tmpY;
                            if (float.TryParse(tmp[2].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out tmpY))
                            {
                                float tmpZ;
                                if (float.TryParse(tmp[3].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out tmpZ))
                                {
                                    int tmpMapId;
                                    if (int.TryParse(tmp[4].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out tmpMapId))
                                    {
                                        telePoint p = new telePoint();
                                        p.name = tmp[0];
                                        p.mapId = tmpMapId;
                                        p.x = tmpX;
                                        p.y = tmpY;
                                        p.z = tmpZ;
                                        telePointGrid.Rows.Add(p);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateGrid();  
        }

        private void bSaveCurrent_Click(object sender, EventArgs e)
        {
            if (ObjectManager.playerPtr != 0)
            {
                string tmp2 = "";
                Enhancements.InputDialog.Show("Sure?", "Name for Teleportpoint", ref tmp2);
                if (tmp2 != "")
                {
                    bool canCreate = true;
                    foreach (string x in strTeleports)
                    {
                        if (x.Split('|')[0].ToLower() == tmp2.ToLower()) canCreate = false;
                    }
                    if (canCreate)
                    {
                        UnitObject tmp = ObjectManager.PlayerObject;
                        strTeleports.Add(tmp2 + "|" + tmp.Pos.x + "|"
                            + tmp.Pos.y + "|"
                            + tmp.Pos.z + "|"
                            + ObjectManager.MapId);
                        
                        SaveChanges();
                        UpdateGrid();
                    }
                    else
                    {
                        MessageBox.Show("Point with name already exists");
                    }
                }
            }
        }

        private void telePointGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
                string tmp = telePointGrid.CurrentRow.Cells[0].Value.ToString();
                for (int i = 0; i < strTeleports.Count; i++)
                {
                    if (strTeleports[i].Split('|')[0] == tmp)
                    {
                        strTeleports.RemoveAt(i);
                        SaveChanges();
                        UpdateGrid();
                        break;
                    }
                }
            }
        }

        private void telePointGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!gotJob)
            {
                if (e.RowIndex != -1)
                {
                    telePoint t = (telePoint)telePointGrid.CurrentRow.Cells[0].Value;
                    if (t.mapId == ObjectManager.MapId)
                    {
                        curLoc = new Objects.Location(t.x, t.y, t.z);
                        gotJob = true;
                    }
                }
            }
        }

        private void cbHotKey_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHotKey.Checked)
            {
                BmWrapper.memory.WriteUInt(Inject.click2PortToggle, 0);
            }
        }

        private void teleportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            runThread = false;
        }
    }

    internal class telePoint
    {
        internal float x;
        internal float y;
        internal float z;
        internal int mapId;
        internal string name;

        public override string ToString()
        {
            return name;
        }
    }
}
