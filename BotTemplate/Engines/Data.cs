using System;
using System.Collections.Generic;
using BotTemplate.Objects;
using System.IO;
using System.Windows.Forms;
using BotTemplate.Interact;
using BotTemplate.Constants;
using System.Text;
using BotTemplate.Helper;
using System.Globalization;

namespace BotTemplate.Engines
{
    internal static class Data
    {
        #region Profile related
        internal static List<Location> Profile = new List<Location>();
        internal static List<int> Faction = new List<int>();
        internal static string VendorName = "";
        internal static Location VendorLocation = new Location(0,0,0);
        internal static bool gotVendor = false;
        internal static int wpCount
        {
            get
            {
                return Profile.Count;
            }
        }
        internal static string profileName = "None";
        internal static int curWp = 0;
        #endregion

        #region Settings
        internal static int regnerateHealthAt = 0;
        internal static int regnerateManaAt = 0;
        internal static int roamAway = 0;
        internal static int searchRange = 0;
        internal static string drinkName = "";
        internal static string foodName = "";
        internal static int fightRange = 0;
        internal static string AccName = "";
        internal static string AccPw = "";
        internal static int LeaveFreeSlots = 0;
        internal static string Lure = "";
        internal static bool UseCcRest = false;
        internal static bool VendorItems = false;
        internal static string[] ProtectedItems = new string[] { "" };
        internal static string[] MailerCharacters = new string[] { "" };
        internal static bool keepGreen = false;
        internal static bool keepBlue = false;
        internal static bool keepPurple = false;
        internal static bool StopOnVendorFail = false;
        internal static int Port = 0;
        #endregion

        #region are settings there?
        internal static string settingsPath = ".\\settings.ini";
        internal static string protectedPath = ".\\protected.ini";
        internal static string mailerPath = ".\\mailer.ini";
        internal static bool settingsExist
        {
            get
            {
                return File.Exists(settingsPath);
            }
        }

        internal static bool protectedExist
        {
            get
            {
                return File.Exists(protectedPath);
            }
        }

        internal static bool mailerExist
        {
            get
            {
                return File.Exists(mailerPath);
            }
        }
        #endregion

        #region load the settings
        internal static bool LoadSettings()
        {
            if (settingsExist && protectedExist && mailerExist)
            {
                string tmpSettings = Tools.DecodeFrom64(File.ReadAllText(settingsPath));
                string tmpSettings2 = Tools.DecodeFrom64(File.ReadAllText(protectedPath));
                string tmpSettings3 = Tools.DecodeFrom64(File.ReadAllText(mailerPath));
                string[] settings = tmpSettings.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                string[] settings2 = tmpSettings2.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                string[] settings3 = tmpSettings3.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                try
                {
                    {
                        regnerateHealthAt = Convert.ToInt32(settings[0]);
                        regnerateManaAt = Convert.ToInt32(settings[1]);
                        roamAway = Convert.ToInt32(settings[2]);
                        searchRange = Convert.ToInt32(settings[3]);
                        drinkName = settings[4].Trim();
                        foodName = settings[5].Trim();
                        fightRange = Convert.ToInt32(settings[6]);
                        AccName = settings[7].Trim();
                        AccPw = settings[8].Trim();
                        LeaveFreeSlots = Convert.ToInt32(settings[9].Trim());
                        Lure = settings[10].Trim();
                        UseCcRest = Convert.ToBoolean(settings[11].Trim());
                        VendorItems = Convert.ToBoolean(settings[12].Trim());
                        keepGreen = Convert.ToBoolean(settings[13].Trim());
                        keepBlue = Convert.ToBoolean(settings[14].Trim());
                        keepPurple = Convert.ToBoolean(settings[15].Trim());
                        StopOnVendorFail = Convert.ToBoolean(settings[16].Trim());
                        Port = Convert.ToInt32(settings[17].Trim());
                    }

                    {
                        List<string> tmpProtected = new List<string>();
                        foreach (string x in settings2)
                        {
                            string y = x.Trim();
                            if (y != "")
                            {
                                tmpProtected.Add(y.Trim());
                            }
                        }
                        ProtectedItems = tmpProtected.ToArray();
                    }

                    {
                        List<string> tmpMailer = new List<string>();
                        foreach (string x in settings3)
                        {
                            string y = x.Trim();
                            if (y != "")
                            {
                                tmpMailer.Add(y.Trim());
                            }
                        }
                        MailerCharacters = tmpMailer.ToArray();
                    }
                }
                catch
                {
                    MessageBox.Show("Settings are corrupted. Delete settings.ini and hit configuration");
                    return false;
                }
                return true;
            }
            else
            {
                MessageBox.Show("Hit settings button", "  Couldnt find some settings");
                return false;
            }
        }
        #endregion

        #region save settings
        internal static void SaveSettings(int parRegHealth, int parRegMana, int parRoamAway, int parSearchRange,
            string parDrink, string parFood, int parRange, string parAccName, string parAccPw, int parLeaveFreeSlots, string parLure, bool parUseCcRest, bool parVendorItems, bool parKeepGreen, bool parKeepBlue, bool parKeepPurple, 
            bool parStopOnVendorFail,
            string parPort)
        {
            string set = "";
            set += parRegHealth.ToString() + Environment.NewLine;
            set += parRegMana.ToString() + Environment.NewLine;
            set += parRoamAway.ToString() + Environment.NewLine;
            set += parSearchRange.ToString() + Environment.NewLine;
            parDrink = parDrink.Replace("\'", "\\'").Replace("\"", "\\\"");
            set += parDrink.ToString() + Environment.NewLine;
            parFood = parFood.Replace("\'", "\\'").Replace("\"", "\\\"");
            set += parFood.ToString() + Environment.NewLine;
            set += parRange.ToString() + Environment.NewLine;
            set += parAccName + Environment.NewLine;
            set += parAccPw + Environment.NewLine;
            set += parLeaveFreeSlots.ToString() + Environment.NewLine;
            parLure = parLure.Replace("\'", "\\'").Replace("\"", "\\\"");
            set += parLure + Environment.NewLine;
            set += parUseCcRest.ToString() + Environment.NewLine;
            set += parVendorItems.ToString() + Environment.NewLine;
            set += parKeepGreen.ToString() + Environment.NewLine;
            set += parKeepBlue.ToString() + Environment.NewLine;
            set += parKeepPurple.ToString() + Environment.NewLine;
            set += parStopOnVendorFail.ToString() + Environment.NewLine;
            set += parPort.ToString() + Environment.NewLine;
            
            File.WriteAllText(settingsPath, Tools.EncodeTo64(set));
            LoadSettings();
        }

        internal static void SaveProtected(string[] parProtectedItems)
        {
            string set2 = "";
            foreach (string x in parProtectedItems)
            {
                string tmpString = x.Trim();
                if (tmpString != "")
                {
                    set2 += tmpString + Environment.NewLine;
                }
            }
            File.WriteAllText(protectedPath, Tools.EncodeTo64(set2));
            LoadSettings();

        }

        internal static void SaveMailer(string[] parCharacters)
        {
            string set2 = "";
            foreach (string x in parCharacters)
            {
                string tmpString = x.Trim();
                if (tmpString != "")
                {
                    set2 += tmpString + Environment.NewLine;
                }
            }
            File.WriteAllText(mailerPath, Tools.EncodeTo64(set2));
            LoadSettings();
        }
        #endregion

        #region need health / mana
        internal static bool needHealth
        {
            get
            {
                return ObjectManager.PlayerObject.healthPercent < regnerateHealthAt ? true : false;
            }
        }

        internal static bool needMana
        {
            get
            {
                if (ObjectManager.playerClass == (uint)Offsets.classIds.Warrior
                        && ObjectManager.playerClass == (uint)Offsets.classIds.Rogue)
                {
                    return false;
                }

                return ObjectManager.PlayerObject.manaPercent < regnerateManaAt ? true : false;
            }
        }
        #endregion

        #region Loading Profile
        internal static bool getProfile()
        {
            bool loadNew = true;
            if (Profile.Count != 0)
            {
                string txt = "Already got a profile" +
                    "\nWant to load a new one?";
                DialogResult dialogResult = MessageBox.Show(txt, "Load", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    loadNew = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    loadNew = false;
                }
            }

            if (loadNew)
            {
                OpenFileDialog LoadProfile = new OpenFileDialog();
                LoadProfile.Filter = "Bot Profiles (*.ini) | *.ini";

                if (LoadProfile.ShowDialog() == DialogResult.OK)
                {
                    Profile.Clear();
                    Faction.Clear();
                    profileName = LoadProfile.SafeFileName;
                    string profilePath = LoadProfile.FileName;
                    StreamReader ReadWaypoints = new StreamReader(profilePath);
                    string[] tmpProfile = ReadWaypoints.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    for (int i = 0; i < tmpProfile.Length; i = i + 1)
                    {
                        tmpProfile[i] = tmpProfile[i].Trim();
                    }
                    ReadWaypoints.Close();
                    ReadWaypoints.Dispose();
                    return extractProfile(tmpProfile);
                }
                else
                {
                    return false;
                }
            }
            return true;

        }

        private static float ToFloat(string str)
        {
            return float.Parse(str.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
        }

        private static bool extractProfile(string[] wp)
        {
            bool error = false;
            gotVendor = false;
            
            for (int i = 0; i < wp.Length && error == false; i = i + 1)
            {
                if (i == 0)
                {
                    foreach (string p in wp[i].Split(','))
                    {
                        try
                        {
                            Faction.Add(Convert.ToInt32(p));
                        }
                        catch
                        {
                            error = true;
                            break;
                        }
                    }
                }
                else
                {
                    try
                    {
                        string[] tmp = wp[i].Split('|');

                        if (tmp[0] == "NWP" && tmp.Length == 4)
                        {
                            float x = ToFloat(tmp[1]);
                            float y = ToFloat(tmp[2]);
                            float z = ToFloat(tmp[3]);

                            Location tmpLoc = new Location();
                            tmpLoc.x = x;
                            tmpLoc.y = y;
                            tmpLoc.z = z;
                            Profile.Add(tmpLoc);
                        }
                        else if (tmp[0].Contains("Vendor"))
                        {
                            Data.VendorName = tmp[0].Split(':')[1];


                            float x = ToFloat(tmp[1]);
                            float y = ToFloat(tmp[2]);
                            float z = ToFloat(tmp[3]);

                            Location tmpLoc = new Location();
                            tmpLoc.x = x;
                            tmpLoc.y = y;
                            tmpLoc.z = z;
                            Data.VendorLocation = tmpLoc;
                            gotVendor = true;
                        }
                    }
                    catch
                    {
                        error = true;
                    }
                }
            }

            if (error == true)
            {
                Profile.Clear();
                Faction.Clear();
                profileName = "None";
                MessageBox.Show("There was an error loading your profile");
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
