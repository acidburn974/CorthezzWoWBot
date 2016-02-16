using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BotTemplate.Interact;
using BotTemplate.Helper;
using BotTemplate.Constants;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Diagnostics;

namespace BotTemplate.Engines.Dupe
{
    internal static class DupeEngine
    {
        internal static Dupe engine;
        internal static string name = "Dupe";
    }

    internal class Dupe
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        uint WM_KEYUP = 0x0101;
        uint WM_KEYDOWN = 0x0100;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        internal static extern void MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        private Thread thrWorker;
        internal bool isRunning
        {
            get
            {
                return Running;
            }
        }
        private bool Running;
        private bool doVendor;

        internal void StartEngine(string name, bool parDoVendor)
        {
            Running = true;
            doVendor = parDoVendor;
            thrWorker = new Thread(Run) { IsBackground = true };

            string[] lines = File.ReadAllLines("./dupe.txt");
            vendorName = lines[0].Trim();

            float x;
            float y;
            float z;
            string[] tmpCoords;

            tmpCoords = lines[1].Replace(',', '.').Split('|');
            float.TryParse(tmpCoords[0], NumberStyles.Any, CultureInfo.InvariantCulture, out x);
            float.TryParse(tmpCoords[1], NumberStyles.Any, CultureInfo.InvariantCulture, out y);
            float.TryParse(tmpCoords[2], NumberStyles.Any, CultureInfo.InvariantCulture, out z);
            Vendor = new Objects.Location(x, y, z);

            mbId = Convert.ToInt32(lines[2]);

            tmpCoords = lines[3].Replace(',', '.').Split('|');
            float.TryParse(tmpCoords[0], NumberStyles.Any, CultureInfo.InvariantCulture, out x);
            float.TryParse(tmpCoords[1], NumberStyles.Any, CultureInfo.InvariantCulture, out y);
            float.TryParse(tmpCoords[2], NumberStyles.Any, CultureInfo.InvariantCulture, out z);
            Mailbox = new Objects.Location(x, y, z);

            thrWorker.Start();
            Exchange.CurrentEngine = name;
            Exchange.IsEngineRunning = true;
        }

        internal void StopEngine()
        {
            Running = false;
        }

        Objects.Location Vendor;
        Objects.Location Mailbox;
        string vendorName;
        int mbId = 0;
        string mailLua = "troll1 = 'false' if MailFrame:IsVisible() then troll1 = 'true' end;";
        string vendorLua = "troll1 = 'false' if MerchantFrame:IsVisible() then troll1 = 'true' end;";

        bool afterDc = false;

        private void Run()
        {
            int runCounter = 0;

            while (Running)
            {
                try
                {
                    if (ObjectManager.isProcessOpen)
                    {
                        if (ObjectManager.IsWowCrashed())
                        {
                            Process.GetProcessById(BmWrapper.memory.ProcessId).Kill();
                            continue;
                        }

                        if (ObjectManager.LoginState == "login")
                        {
                            Calls.DoString("DefaultServerLogin('" + Data.AccName + "', '" + Data.AccPw + "');");
                            int wait = 0;
                            while (!ObjectManager.IsWowCrashed() && ObjectManager.characterCount() == 0)
                            {
                                Thread.Sleep(500);
                                wait++;
                                if (wait >= 120)
                                {
                                    Process.GetProcessById(BmWrapper.memory.ProcessId).Kill();
                                    break;
                                }
                            }
                            afterDc = true;
                        }
                        else if (ObjectManager.LoginState == "charselect" && afterDc)
                        {
                            if (!ObjectManager.IsWowCrashed())
                            {
                                // Enter -> Login
                                SendMessage(BmWrapper.memory.WindowHandle, WM_KEYDOWN, 0x0D, 0);
                                SendMessage(BmWrapper.memory.WindowHandle, WM_KEYUP, 0x0D, 0);
                            }

                            while (!ObjectManager.IsWowCrashed() && ObjectManager.playerPtr == 0 && Running)
                            {
                                Thread.Sleep(100);
                            }
                            while (!ObjectManager.IsWowCrashed() && ObjectManager.PlayerObject.Pos.x == 0 && Running)
                            {
                                Thread.Sleep(100);
                            }
                            if (!ObjectManager.IsWowCrashed())
                            {
                                Ingame.Tele(Mailbox, 60, true);
                                Calls.SendMovementUpdate(0xEE, (uint)Environment.TickCount);
                            }
                        }


                        if (runCounter < 60)
                        {
                            if (ObjectManager.playerPtr != 0)
                            {
                                while (!ObjectManager.IsWowCrashed() && ObjectManager.PlayerObject.Pos.differenceTo(Mailbox) > 2)
                                {
                                    Ingame.Tele(Mailbox, 60, true);
                                }
                                afterDc = false;
                                Calls.AntiAfk();
                                Objects.GameObject mailbox = new Objects.GameObject(0, 0, 0);
                                while (!ObjectManager.IsWowCrashed() && mailbox.baseAdd == 0 && Running && !(ObjectManager.LoginState == "login"))
                                    mailbox = ObjectManager.GetGameObjectById(mbId);

                                if (!ObjectManager.IsWowCrashed())
                                    Calls.OnRightClickObject(mailbox.baseAdd, 0);


                                while (!ObjectManager.IsWowCrashed() && !(Calls.GetText(mailLua, "troll1", 10).Trim() == "true") && Running && !(ObjectManager.LoginState == "login"))
                                {
                                    Thread.Sleep(100);
                                }
                                Thread.Sleep(1500);

                                if (!ObjectManager.IsWowCrashed())
                                {
                                    //Calls.DoString("TakeInboxItem(1, 1) Logout()");
                                    SendMessage(BmWrapper.memory.WindowHandle, WM_KEYDOWN, 0x31, 0);
                                    SendMessage(BmWrapper.memory.WindowHandle, WM_KEYUP, 0x31, 0);

                                    Calls.PacketZoneChange();
                                    Calls.PacketSendLogout();
                                }
                                    
                                while (!ObjectManager.IsWowCrashed() && ObjectManager.characterCount() == 0 && Running && !(ObjectManager.LoginState == "login"))
                                {
                                    Thread.Sleep(100);
                                }
                                Thread.Sleep(250);

                                if (!ObjectManager.IsWowCrashed())
                                {
                                    // Enter -> Login
                                    SendMessage(BmWrapper.memory.WindowHandle, WM_KEYDOWN, 0x0D, 0);
                                    SendMessage(BmWrapper.memory.WindowHandle, WM_KEYUP, 0x0D, 0);
                                }

                                while (!ObjectManager.IsWowCrashed() && ObjectManager.playerPtr == 0 && Running && !(ObjectManager.LoginState == "login"))
                                {
                                    Thread.Sleep(100);
                                }
                                runCounter++;
                            }
                        }
                        else
                        {
                            if (doVendor)
                            {
                                while (!ObjectManager.IsWowCrashed() && ObjectManager.PlayerObject.Pos.differenceTo(Vendor) > 2)
                                {
                                    Ingame.Tele(Vendor, 60, true);
                                }
                                if (!ObjectManager.IsWowCrashed())
                                    Calls.SendMovementUpdate(0xEE, (uint)Environment.TickCount);

                                Objects.UnitObject vendorObject = new Objects.UnitObject(0, 0, 0);
                                while (!ObjectManager.IsWowCrashed() && vendorObject.baseAdd == 0 && Running)
                                    vendorObject = ObjectManager.GetUnitByName(vendorName);

                                if (!ObjectManager.IsWowCrashed())
                                    Calls.OnRightClickUnit(vendorObject.baseAdd, 0);

                                while (!ObjectManager.IsWowCrashed() && !(Calls.GetText(vendorLua, "troll1", 10).Trim() == "true") && Running)
                                {
                                    Thread.Sleep(100);
                                }

                                if (!ObjectManager.IsWowCrashed())
                                    Ingame.SellAll();
                                while (!ObjectManager.IsWowCrashed() && ObjectManager.PlayerObject.Pos.differenceTo(Mailbox) > 2)
                                {
                                    Ingame.Tele(Mailbox, 60, true);
                                }
                                if (!ObjectManager.IsWowCrashed())
                                    Calls.SendMovementUpdate(0xEE, (uint)Environment.TickCount);
                                runCounter = 0;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (!ObjectManager.isProcessOpen)
                        {
                            ObjectManager.runThread = false;
                            ChatReader.runThread = false;
                            Inject.Restore();
                            BmWrapper.memory.Close();
                            Process WoW = Process.Start(@"C:\Users\Cody\Desktop\WoW 1.12.1\Feenix 1.12 client\WoW.exe");
                            Thread.Sleep(30000);
                            BmWrapper.memory.OpenProcessAndThread(WoW.Id);
                            Inject.Apply();
                            Inject.Init();

                            MoveWindow(BmWrapper.memory.WindowHandle, 0, 0, 153, 119, true);

                            ObjectManager.getObjThread = new Thread(ObjectManager.getObj);
                            ObjectManager.getObjThread.IsBackground = true;
                            ObjectManager.runThread = true;
                            ObjectManager.getObjThread.Start();

                            ChatReader.readChatThread = new Thread(ChatReader.readChat);
                            ChatReader.readChatThread.IsBackground = true;
                            ChatReader.runThread = true;
                            ChatReader.readChatThread.Start();
                        }
                    }
                }
                catch { }
            }
            Exchange.IsEngineRunning = false;
            Exchange.CurrentEngine = "None";
            DupeEngine.engine = null;
        }
    }


}
