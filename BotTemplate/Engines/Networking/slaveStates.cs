using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotTemplate.Interact;
using System.IO;
using BotTemplate.Helper;

namespace BotTemplate.Engines.Networking
{
    internal static class slaveStates
    {
        private static object locker = new object();
        private static int num1 = -1;
        private static int num2 = -1;
        private static int num3 = -1;
        private static int num4 = -1;
        private static UInt64 guid;
        internal static bool returnCoords = true;
        internal static string process(string content)
        {
            lock (locker)
            {
                try
                {
                    if (content == "coords")
                    {
                        if (ObjectManager.isDeath || ObjectManager.isGhost || !returnCoords || Ingame.IsDc())
                        {
                            return "0|0|0";
                        }
                        else
                        {
                            Objects.UnitObject tmp = ObjectManager.PlayerObject;
                            return tmp.Pos.x + "|" + tmp.Pos.y + "|" + tmp.Pos.z;
                        }
                    }
                    else
                    {
                        string[] cont = content.Split('|');
                        int tmpNum = Convert.ToInt32(cont[0]);
                        guid = Convert.ToUInt64(cont[1]);
                        if (guid == ObjectManager.party1Guid)
                        {
                            if (num1 < tmpNum)
                            {
                                num1 = tmpNum;
                                if (cont[2] == "wait")
                                {
                                    party1Ready = false;
                                }
                                else if (cont[2] == "resume")
                                {
                                    party1Ready = true;
                                }
                                File.AppendAllText(@".\\socketLog.txt", DateTime.Now.ToString("HH:mm:ss") + " | Listener: Setting party 1 to " + cont[2] + Environment.NewLine);
                                return cont[2];
                            }
                        }
                        else if (guid == ObjectManager.party2Guid)
                        {
                            if (num2 < tmpNum)
                            {
                                num2 = tmpNum;
                                if (cont[2] == "wait")
                                {
                                    party2Ready = false;
                                }
                                else if (cont[2] == "resume")
                                {
                                    party2Ready = true;
                                }
                                File.AppendAllText(@".\\socketLog.txt", DateTime.Now.ToString("HH:mm:ss") + " | Listener: Setting party 2 to " + cont[2] + Environment.NewLine);
                                return cont[2];
                            }
                        }
                        else if (guid == ObjectManager.party3Guid)
                        {
                            if (num3 < tmpNum)
                            {
                                num3 = tmpNum;
                                if (cont[2] == "wait")
                                {
                                    party3Ready = false;
                                }
                                else if (cont[2] == "resume")
                                {
                                    party3Ready = true;
                                }
                                File.AppendAllText(@".\\socketLog.txt", DateTime.Now.ToString("HH:mm:ss") + " | Listener: Setting party 3 to " + cont[2] + Environment.NewLine);
                                return cont[2];
                            }
                        }
                        else if (guid == ObjectManager.party4Guid)
                        {
                            if (num4 < tmpNum)
                            {
                                num4 = tmpNum;
                                if (cont[2] == "wait")
                                {
                                    party4Ready = false;
                                }
                                else if (cont[2] == "resume")
                                {
                                    party4Ready = true;
                                }
                                File.AppendAllText(@".\\socketLog.txt", DateTime.Now.ToString("HH:mm:ss") + " | Listener: Setting party 4 to " + cont[2] + Environment.NewLine);
                                return cont[2];
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    File.AppendAllText(@".\\socketLog.txt", DateTime.Now.ToString("HH:mm:ss") + " | Listener Exception: " + e.ToString() + Environment.NewLine);
                }
                return "invalid";
            }
        }
        private static bool party1Ready = true;
        private static bool party2Ready = true;
        private static bool party3Ready = true;
        private static bool party4Ready = true;

        internal static void Reset()
        {
            party1Ready = true;
            party2Ready = true;
            party3Ready = true;
            party4Ready = true;
        }

        internal static bool slavesReady
        {
            get
            {
                lock (locker)
                {
                    bool tmp = false;
                    switch (clientListen.groupCount)
                    {
                        case 1:
                            tmp = party1Ready;
                            break;

                        case 2:
                            tmp = party1Ready && party2Ready;
                            break;

                        case 3:
                            tmp = party1Ready && party2Ready && party3Ready;
                            break;

                        case 4:
                            tmp = party1Ready && party2Ready && party3Ready && party4Ready;
                            break;
                    }
                    return tmp;
                }
            }
        }
    }
}
