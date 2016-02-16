using System;
using BotTemplate.Interact;
using System.Windows.Forms;
using BotTemplate.Constants;
using BotTemplate.Engines;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BotTemplate.Helper.SpellSystem;

namespace BotTemplate.Helper
{
    internal static class Ingame
    {

        #region BG functions
        internal static bool IsInQ()
        {
            string LuaStatement =
                "test12 = GetBattlefieldTimeWaited(1)";

            return (Calls.GetText(LuaStatement, "test12", 10) != "0");
        }

        internal static bool IsBgEnd()
        {
            string LuaStatement =
                "test123 = GetBattlefieldWinner()";
            return (Calls.GetText(LuaStatement, "test123", 10) == "");
        }

        internal static bool CanJoinBg()
        {



            string LuaStatement =
                "test123 = GetBattlefieldPortExpiration(1)";
            return (Calls.GetText(LuaStatement, "test123", 10) != "0");

        }

        internal static string ZoneText()
        {


            string LuaStatement = "tesko1 = GetRealZoneText()";
            return Calls.GetText(LuaStatement, "tesko1", 30);

        }

        internal static void SignUp()
        {


            Calls.DoString("SelectGossipOption(1) JoinBattlefield(0)");

        }

        internal static bool IsBgFrameOpen()
        {



            string LuaStatement = "function IsOpen() if BattlefieldFrame:IsVisible() then return 'true' else return 'false' end end " +
                "tesko1 = IsOpen();";
            return (Calls.GetText(LuaStatement, "tesko1", 5).Trim() == "true");

        }

        internal static void CloseBgFrame()
        {


            Calls.DoString("BattlefieldFrame:Hide()");

        }

        internal static void AcceptPort()
        {


            Calls.DoString("AcceptBattlefieldPort(1, true)");

        }

        internal static void LeaveBg()
        {


            Calls.DoString("LeaveBattlefield()");

        }
        #endregion

        #region misc
        internal static void Follow(string name)
        {
            Calls.DoString("FollowByName('" + name + "');");
        }

        internal static void BackToLogin()
        {
            Calls.DoString("if GlueDialog:IsVisible() then GlueDialog:Hide(); end if RealmList:IsVisible() then RealmListCancelButton:Click(); end CharacterSelect_Exit();");
        }

        internal static bool IsDc()
        {
            try
            {
                if (ObjectManager.playerPtr == 0)
                {
                    string LuaStatement =
                        "test123 = 0 if AccountLogin ~= nil or CharacterSelect ~= nil then test123 = 1 end";

                    return (Calls.GetText(LuaStatement, "test123", 10) != "0");
                }
                return false;
            }
            catch { return false; }
        }

        internal static int GetLatency()
        {
            string LuaStatement =
                    "_,_,drei  = GetNetStats()";

            return Int32.Parse(Calls.GetText(LuaStatement, "drei", 2));
        }
        #endregion

        #region enchants
        internal static bool IsMainHandEnchantApplied
        {
            get
            {



                string LuaStatement = "mainhand1 = GetWeaponEnchantInfo()";
                return (Calls.GetText(LuaStatement, "mainhand1", 5) == "1");

            }
        }

        internal static bool IsOffHandEnchantApplied
        {
            get
            {


                string LuaStatement = "_, _, _, offhand1 = GetWeaponEnchantInfo()";
                return (Calls.GetText(LuaStatement, "offhand1", 5) == "1");

            }
        }

        internal static void EnchantMainHand(string name)
        {


            if (!Ingame.IsMainHandEnchantApplied)
            {
                Ingame.UseItem(name);
                Calls.DoString("PickupInventoryItem(16)");
            }

        }

        internal static void EnchantOffHand(string name)
        {


            if (!Ingame.IsOffHandEnchantApplied)
            {
                Ingame.UseItem(name);
                Calls.DoString("PickupInventoryItem(17)");
            }

        }
        #endregion

        #region Item Management
        private static void SkipGossip()
        {
            string Statement = "arg = { GetGossipOptions() }; count = 1; typ = 2; while true do if arg[typ] ~= nil then if arg[typ] == 'vendor' then SelectGossipOption(count); break; else count = count + 1; typ = typ + 2; end else break end end";
            Calls.DoString(Statement);
        }

        internal static bool IsVendorFrameOpen()
        {
            string LuaStatement = "troll1 = IsVendorOpen();";

            return (Calls.GetText(LuaStatement, "troll1", 10).Trim() == "true");

        }

        internal static bool PopupVisible()
        {
            string LuaStatement = "troll1 = 'false' if StaticPopup1:IsVisible() then troll1 = 'true'; else troll1 = 'false' end";
            return (Calls.GetText(LuaStatement, "troll1", 10).Trim() == "true");
        }

        internal static void SellAll()
        {


            Calls.DoString("for bag = 0,4,1 do for slot = 1, GetContainerNumSlots(bag), 1 do local name = GetContainerItemLink(bag,slot); if name then UseContainerItem(bag,slot) end end end");
        }

        internal static void SellAllButGlobal()
        {
            string part1 = "if MerchantRepairAllButton:IsVisible() then MerchantRepairAllButton:Click() end " +
            "for bag = 0,4,1 do " +
            "for slot = 1, GetContainerNumSlots(bag), 1 do " +
            "link = GetContainerItemLink(bag,slot) " +
            "if link then " +
            "name = gsub(link,'^.*%[(.*)%].*$','%1') " +
            "if '1' == '1' ";
            
            string part2 = "";
            part2 += " and string.find(name, 'Dragon Finger') == nil";
            part2 += " and string.find(name, 'Glowstar') == nil";

            string part3 =
                " then " +
                "UseContainerItem(bag,slot) " +
                "end " +
                "end " +
                "end " +
                "end ";



            string execute = part1 + part2 + part3;

            Calls.DoString(execute);
        }

        internal static void SellAllBut(string[] items)
        {


            string part1 = "if MerchantRepairAllButton:IsVisible() then MerchantRepairAllButton:Click() end " +
            "for bag = 0,4,1 do " +
            "for slot = 1, GetContainerNumSlots(bag), 1 do " +
            "link = GetContainerItemLink(bag,slot) " +
            "if link then " +
            "name = gsub(link,'^.*%[(.*)%].*$','%1') " +
            "if '1' == '1' ";
            if (Data.keepGreen)
            {
                part1 = part1 + "and string.find(link, '1eff00') == nil ";
            }

            if (Data.keepBlue)
            {
                part1 = part1 + "and string.find(link, '0070dd') == nil ";
            }

            if (Data.keepPurple)
            {
                part1 = part1 + "and string.find(link, 'a335ee') == nil ";
            }

            
            string part2 = "";
            foreach (string x in items)
            {
                part2 += " and string.find(name, '" + x.Trim() + "') == nil";
            }

            part2 += " and string.find(name, 'Dragon Finger') == nil";
            part2 += " and string.find(name, 'Glowstar') == nil";
            
            string part3 =
                " then " +
                "UseContainerItem(bag,slot) " +
                "end " +
                "end " +
                "end " +
                "end ";



            string execute = part1 + part2 + part3;

            Calls.DoString(execute);

        }

        private static int QueryBag(int bagnumber, string item)
        {


            int totalSlots = 0;
            switch (bagnumber)
            {
                case 0:
                    totalSlots = 16;
                    break;

                case 1:
                    totalSlots = ObjectManager.Bag1.TotalSlots;
                    break;

                case 2:
                    totalSlots = ObjectManager.Bag2.TotalSlots;
                    break;

                case 3:
                    totalSlots = ObjectManager.Bag3.TotalSlots;
                    break;

                case 4:
                    totalSlots = ObjectManager.Bag4.TotalSlots;
                    break;

                default:
                    totalSlots = 0;
                    break;
            }

            string LuaStatement = "itemcount1 = getItemsInBag(" + totalSlots + "," + bagnumber + ",'" + item + "');";

            int tmpInt;
            Int32.TryParse(Calls.GetText(LuaStatement, "itemcount1", 10), out tmpInt);
            return tmpInt;

        }

        private static bool UseQueryBag(int bagnumber, string item)
        {


            int totalSlots = 0;
            switch (bagnumber)
            {
                case 0:
                    totalSlots = 16;
                    break;

                case 1:
                    totalSlots = ObjectManager.Bag1.TotalSlots;
                    break;

                case 2:
                    totalSlots = ObjectManager.Bag2.TotalSlots;
                    break;

                case 3:
                    totalSlots = ObjectManager.Bag3.TotalSlots;
                    break;

                case 4:
                    totalSlots = ObjectManager.Bag4.TotalSlots;
                    break;

                default:
                    totalSlots = 0;
                    break;
            }

            string LuaStatement = "tryhard = useItemInBag(" + totalSlots + "," + bagnumber + ",'" + item + "');";

            return (Calls.GetText(LuaStatement, "tryhard", 5) == "1");

        }

        internal static void UseDrink()
        {
            if (!IsDrinking()) UseItem(Data.drinkName);
        }

        internal static void UseFood()
        {
            if (!IsEating()) UseItem(Data.foodName);
        }

        internal static void UseItem(string name)
        {
            for (int i = 0; i <= ObjectManager.TotalBags; i = i + 1)
            {
                bool con = UseQueryBag(i, name);

                if (con == true)
                {
                    break;
                }
            }

        }

        internal static int ItemCount(string name)
        {
            int itemCount = 0;
            for (int i = 0; i <= ObjectManager.TotalBags; i = i + 1)
            {
                itemCount = itemCount + QueryBag(i, name);
            }
            return itemCount;

        }
        #endregion

        #region tele
        private static readonly object tele_Lock = new object();

        internal static void TeleOverTarget()
        {
            lock (tele_Lock)
            {
                float x = ObjectManager.TargetObject.Pos.x;
                float y = ObjectManager.TargetObject.Pos.y;
                float z = ObjectManager.TargetObject.Pos.z + 100;
                if (x != 0 && y != 0 && z != 0)
                {
                    Tele(new Objects.Location(x, y, z), 60, false);
                }
            }
        }

        internal static void KillPlayer()
        {
            lock (tele_Lock)
            {
                Ingame.Tele(new Objects.Location(ObjectManager.PlayerObject.Pos.x, ObjectManager.PlayerObject.Pos.y, -10000f), 60, false);
            }
        }

        internal static void hotKeyTele(Objects.Location TargetPos, float parSeconds)
        {
            lock (tele_Lock)
            {
                if (parSeconds > 0.25f) parSeconds = 0.25f;
                Calls.StepTelePreview(TargetPos.x, TargetPos.y, TargetPos.z, parSeconds);
            }
        }

        
        internal static void setCoords(Objects.Location TargetPos)
        {
            uint ptr = ObjectManager.playerPtr + 0x9B8;
            BmWrapper.memory.WriteFloat(ptr, TargetPos.x);
            BmWrapper.memory.WriteFloat(ptr + 4, TargetPos.y);
            BmWrapper.memory.WriteFloat(ptr + 8, TargetPos.z);
        }

        internal static void TeleHb(Objects.Location TargetPos, float seconds, bool Preview)
        {
            lock (tele_Lock)
            {
                if (seconds > 0.25f) seconds = 0.25f;

                uint ptr = ObjectManager.playerPtr;
                if (ptr != 0)
                {
                    float diff = ObjectManager.PlayerObject.Pos.differenceTo(TargetPos);
                    bool success = true;
                    if (diff > 2)
                    {
                        // 7 meter alle 1000 ms
                        float startX = ObjectManager.PlayerObject.Pos.x;
                        float startY = ObjectManager.PlayerObject.Pos.y;
                        float steps = (float)diff / (7f * seconds);
                        float stepRangeX = (TargetPos.x - startX) / steps;
                        float StepRangeY = (TargetPos.y - startY) / steps;

                        Calls.SetTeleStart(startX,
                            startY,
                            ObjectManager.PlayerObject.Pos.z);
                        Calls.StopRunning();
                        Calls.SetMovementFlags(0);
                        Thread.CurrentThread.Join(50);

                        for (int i = 0; i < steps - 1; i++)
                        {
                            startX = startX + stepRangeX;
                            startY = startY + StepRangeY;

                            if (Preview)
                            {
                                success = Calls.StepTelePreview(startX, startY, 10000, seconds);
                            }
                            else
                            {
                                success = Calls.StepTele(startX, startY, 10000, seconds, 0);
                            }

                            if (!success)
                            {
                                break;
                            }
                        }
                    }
                    if (success)
                    {
                        if (Preview)
                        {
                            success = Calls.StepTelePreview(TargetPos.x, TargetPos.y, TargetPos.z, seconds);
                        }
                        else
                        {
                            success = Calls.StepTele(TargetPos.x, TargetPos.y, TargetPos.z, seconds, 1);
                        }
                    }
                }
            }
        }

        internal static void Tele(Objects.Location TargetPos, float seconds, bool Preview)
        {
            lock (tele_Lock)
            {
                //uint xAddr = ObjectManager.playerPtr + 0x9B8;
                //uint yAddr = ObjectManager.playerPtr + 0x9B8 + 0x4;
                //uint zAddr = ObjectManager.playerPtr + 0x9B8 + 0x8;

                //BmWrapper.memory.WriteFloat(xAddr, TargetPos.x);
                //BmWrapper.memory.WriteFloat(yAddr, TargetPos.y);
                //BmWrapper.memory.WriteFloat(zAddr, TargetPos.z);
                if (seconds > 0.35f) seconds = 0.35f;

                uint ptr = ObjectManager.playerPtr;
                if (ptr != 0)
                {
                    float diff = ObjectManager.PlayerObject.Pos.differenceTo(TargetPos);
                    bool success = true;
                    if (diff > 2)
                    {
                        // 7 meter alle 1000 ms
                        float startX = ObjectManager.PlayerObject.Pos.x;
                        float startY = ObjectManager.PlayerObject.Pos.y;
                        float steps = (float)diff / (7f * seconds);
                        float stepRangeX = (TargetPos.x - startX) / steps;
                        float StepRangeY = (TargetPos.y - startY) / steps;

                        Calls.SetTeleStart(startX,
                            startY,
                            ObjectManager.PlayerObject.Pos.z);
                        Calls.StopRunning();
                        Calls.SetMovementFlags(0);
                        Thread.CurrentThread.Join(50);

                        for (int i = 0; i < steps - 1; i++)
                        {
                            startX = startX + stepRangeX;
                            startY = startY + StepRangeY;

                            if (Preview)
                            {
                                success = Calls.StepTelePreview(startX, startY, 10000, seconds);
                            }
                            else
                            {
                                success = Calls.StepTele(startX, startY, 10000, seconds, 0);
                            }

                            if (!success)
                            {
                                break;
                            }
                        }
                    }
                    if (success)
                    {
                        if (Preview)
                        {
                            success = Calls.StepTelePreview(TargetPos.x, TargetPos.y, TargetPos.z, seconds);
                        }
                        else
                        {
                            success = Calls.StepTele(TargetPos.x, TargetPos.y, TargetPos.z, seconds, 1);
                        }
                    }
                }
            }
        }

        internal static void TeleNoZFake(Objects.Location TargetPos, float seconds, bool Preview)
        {
            lock (tele_Lock)
            {
                if (seconds > 0.25f) seconds = 0.25f;

                uint ptr = ObjectManager.playerPtr;
                if (ptr != 0)
                {
                    float diff = ObjectManager.PlayerObject.Pos.differenceTo(TargetPos);
                    bool success = true;
                    if (diff > 2)
                    {
                        // 7 meter alle 1000 ms
                        float startX = ObjectManager.PlayerObject.Pos.x;
                        float startY = ObjectManager.PlayerObject.Pos.y;
                        float z = ObjectManager.PlayerObject.Pos.z;
                        float steps = (float)diff / (7f * seconds);
                        float stepRangeX = (TargetPos.x - startX) / steps;
                        float StepRangeY = (TargetPos.y - startY) / steps;
                        
                        Calls.SetTeleStart(startX,
                            startY,
                            ObjectManager.PlayerObject.Pos.z);
                        Calls.StopRunning();
                        Calls.SetMovementFlags(0);
                        Thread.CurrentThread.Join(50);

                        for (int i = 0; i < steps - 1; i++)
                        {
                            startX = startX + stepRangeX;
                            startY = startY + StepRangeY;

                            if (Preview)
                            {
                                success = Calls.StepTelePreview(startX, startY, z, seconds);
                            }
                            else
                            {
                                success = Calls.StepTele(startX, startY, 10000, z, 0);
                            }

                            if (!success)
                            {
                                break;
                            }
                        }
                    }
                    if (success)
                    {
                        if (Preview)
                        {
                            success = Calls.StepTelePreview(TargetPos.x, TargetPos.y, TargetPos.z, seconds);
                        }
                        else
                        {
                            success = Calls.StepTele(TargetPos.x, TargetPos.y, TargetPos.z, seconds, 1);
                        }
                    }
                }
            }
        }
        #endregion

        #region Movement
        internal static void moveForward()
        {
            if (!Calls.MovementContainsFlag((uint)Offsets.movementFlags.Forward))
            {
                Calls.SetControlBit((uint)Offsets.controlBits.Back, 0);
                Calls.SetControlBit((uint)Offsets.controlBits.Front, 1);
            }
        }

        internal static void moveBackwards()
        {
            if (!Calls.MovementContainsFlag((uint)Offsets.movementFlags.Back))
            {
                Calls.SetControlBit((uint)Offsets.controlBits.Front, 0);
                Calls.SetControlBit((uint)Offsets.controlBits.Back, 1);
            }
        }

        internal static void Jump()
        {
            Calls.DoString("Jump();");
        }
        #endregion

        #region Spells
        internal static bool druidIsBear()
        {
            return GotBuff("Dire Bear Form") || GotBuff("Bear Form");
        }

        internal static bool druidIsCat()
        {
            return GotBuff("Cat Form");
        }

        private static StringBuilder writeToChain = new StringBuilder(castChain);
        private static string castChain = "";

        private static StringBuilder writeToPetChain = new StringBuilder(petCastChain);
        private static string petCastChain = "";
        
        internal static void Cast(string name, bool gotCastTime)
        {
            if (gotCastTime)
            {
                if (Calls.MovementIsOnly(0) || Calls.MovementIsOnly((uint)Offsets.movementFlags.Swimming))
                {
                    if (IsSpellReady(name))
                    {
                        //Calls.DoString("CastSpellByName('" + name + "');");
                        writeToChain.Append("CastSpellByName('" + name + "');");
                    }
                }
            }
            else
            {
                if (IsSpellReady(name))
                {
                    //Calls.DoString("CastSpellByName('" + name + "');");
                    writeToChain.Append("CastSpellByName('" + name + "');");
                }
            }
        }

        internal static void CastWait(string name, int ms, bool gotCastTime)
        {
            if (!SpellManager.SpellContains(name))
            {
                if (gotCastTime)
                {
                    if (Calls.MovementIsOnly(0) || Calls.MovementIsOnly((uint)Offsets.movementFlags.Swimming))
                    {
                        if (IsSpellReady(name))
                        {
                            Cast(name, gotCastTime);
                            SpellManager.Add(new Spell(name, ms));
                        }
                    }
                }
                else
                {
                    if (IsSpellReady(name))
                    {
                        Cast(name, gotCastTime);
                        SpellManager.Add(new Spell(name, ms));
                    }
                }
            }
        }

        internal static void Stand()
        {
            Calls.DoString("DoEmote('stand')");
        }

        internal static void SwitchToStance(string name)
        {
            Calls.DoString("CastShapeshiftForm(returnStanceIndex('" + name + "'));");
        }

        internal static bool IsInStance(string name)
        {


            string LuaStatement = "_, _, active, _ = GetShapeshiftFormInfo(returnStanceIndex('" + name + "'))";
            return (Calls.GetText(LuaStatement, "active", 1) == "1");

        }

        internal static string getSpellId(string name)
        {
            string LuaStatement = "finalresult = getSpellId('" + name + "');";

            return Calls.GetText(LuaStatement, "finalresult", 6);

        }

        internal static void CastFinal()
        {
            if (!IsGCD())
            {
                if (ObjectManager.PlayerObject.isChanneling == 0 && !ObjectManager.IsCasting)
                {
                    if (writeToChain.Length != 0)
                    {
                        Calls.DoString(writeToChain.ToString());
                    }
                }
            }

            if (ObjectManager.playerClass == (uint)Offsets.classIds.Hunter || ObjectManager.playerClass == (uint)Offsets.classIds.Warlock)
            {
                if (!IsPetGCD())
                {
                    if (writeToPetChain.Length != 0)
                    {
                        Calls.DoString(writeToPetChain.ToString());
                    }
                }
            }

            writeToChain.Length = 0;
            writeToPetChain.Length = 0;
        }

        internal static bool IsGCD()
        {
            return CooldownManager.Contains("GCD");
        }

        internal static bool IsPetGCD()
        {
            return PetCooldownManager.Contains("GCD");
        }

        internal static bool GotBuff(string name)
        {
            if (!BuffManager.Contains(name))
            {
                string LuaStatement = "finalresult = hasBuff('" + name + "');";
                if (Calls.GetText(LuaStatement, "finalresult", 5).Trim() == "true")
                {
                    BuffManager.Add(name);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        internal static bool GotDebuff(string name, string unit)
        {
            string LuaStatement =  "finalresult = hasDebuff('" + name + "','" + unit + "');";
            return (Calls.GetText(LuaStatement, "finalresult", 5).Trim() == "true");
        }

        internal static void PlaceAutoAttackAndShoot()
        {
            string LuaStatement = "abc12 = getSpellId('Shoot') " +
             "if abc12 ~= nil then " +
             "PickupSpell(abc12, 'BOOKTYPE_SPELL') PlaceAction(23) ClearCursor() end";

            Calls.DoString(LuaStatement);


        }

        internal static void Shoot()
        {
            string LuaStatement = "shoot()";
            Calls.DoString(LuaStatement);
        }

        internal static void StopShoot()
        {
            string LuaStatement = "stopShoot()";
            Calls.DoString(LuaStatement);
        }

        internal static int GetComboPoints()
        {
            return Convert.ToInt32(Calls.GetText("points = GetComboPoints('target')", "points", 1));
        }

        internal static void Attack()
        {
            if (!IsGCD())
            {
                Calls.OnRightClickUnit(ObjectManager.TargetObject.baseAdd, 0);
            }
        }

        internal static bool IsTargetInCombat()
        {
            return (Calls.GetText("abc = UnitAffectingCombat('target')", "abc", 1) == "1");
        }

        internal static bool IsSpellReady(string name)
        {
            name = name.Replace("()", "");
            if (!CooldownManager.Contains("GCD") && !CooldownManager.Contains(name))
            {
                string LuaStatement = "eins, zwei, drei = isSpellReady('" + name + "')";
                string[] result = Calls.GetText(LuaStatement, new string[] { "eins", "zwei", "drei" }, 20);
                if (result[0].Trim() == "0")
                {
                    return true;
                }
                else
                {
                    if (result[0] != "" && result[1] != "")
                    {
                        double timeStamp = Convert.ToDouble(result[0].Replace(".", ""));
                        double secCd = TimeSpan.FromSeconds(Convert.ToDouble(result[1].Replace(".", ","))).TotalMilliseconds;

                        if (result[2] == "1")
                        {
                            if (secCd == 1500)
                            {
                                CooldownManager.Add("GCD", timeStamp + secCd);
                            }
                            CooldownManager.Add(name, timeStamp + secCd);
                        }
                    }
                }
            }
            return false;
        }

        internal static bool GotTextureAsBuff(string tex)
        {
            string LuaStatement = "finalresult = hasBuffWithTexture('" + tex + "', 'player');";
            return (Calls.GetText(LuaStatement, "finalresult", 5).Trim() == "true");
        }

        internal static bool GotTextureAsDebuff(string tex)
        {
            string LuaStatement = "finalresult = hasDebuffWithTexture('" + tex + "', 'player');";
            return (Calls.GetText(LuaStatement, "finalresult", 5).Trim() == "true");
        }

        internal static bool IsEating()
        {
            string eat = @"Interface\\Icons\\INV_Misc_Fork&Knife";
            return GotTextureAsBuff(eat);
        }

        internal static bool IsPartyEatingOrDrinking()
        {
            try
            {
                return Convert.ToBoolean(Calls.GetText("maxo = isGroupResting()", "maxo", 5));
            }
            catch
            {
                return false;
            }
        }

        internal static bool IsDrinking()
        {
            string drink = @"Interface\\Icons\\INV_Drink_07";
            return GotTextureAsBuff(drink);
        }
        #endregion

        #region Pet stuff
        internal static void PetFollow()
        {
            Calls.DoString("PetFollow()");
        }

        internal static bool PetGotTextureAsDebuff(string tex)
        {
            string LuaStatement = "finalresult = hasDebuffWithTexture('" + tex + "', 'pet');";
            return (Calls.GetText(LuaStatement, "finalresult", 5).Trim() == "true");
        }

        internal static bool PetGotTextureAsBuff(string tex)
        {
            string LuaStatement = "finalresult = hasBuffWithTexture('" + tex + "', 'pet');";

            return (Calls.GetText(LuaStatement, "finalresult", 5).Trim() == "true");
        }

        internal static void CastPetSpell(string name)
        {
            if (PetIsSpellReady(name))
            {
                writeToPetChain.Append("castPetSpell('" + name + "');");
            }
        }

        internal static void DismissPet()
        {
            Calls.DoString("PetDismiss()");
        }

        internal static bool GotPet()
        {
            return (ObjectManager.PetObject.guid != 0);
        }

        internal static void PetAttack()
        {
            writeToChain.Append("PetAttack()");
        }

        internal static bool PetIsSpellReady(string name)
        {
            name = name.Replace("()", "");
            if (!PetCooldownManager.Contains("GCD") && !PetCooldownManager.Contains(name))
            {
                string LuaStatement = "eins, zwei = gotPetSpellCd('" + name + "')";
                string[] result = Calls.GetText(LuaStatement, new string[] { "eins", "zwei" }, 20);
                if (result[0].Trim() == "0")
                {
                    return true;
                }
                else
                {
                    if (result[0] != "" && result[1] != "")
                    {
                        double timeStamp = Convert.ToDouble(result[0].Replace(".", ""));
                        double secCd = TimeSpan.FromSeconds(Convert.ToDouble(result[1].Replace(".", ","))).TotalMilliseconds;
                        if (secCd == 1500)
                        {
                            PetCooldownManager.Add("GCD", timeStamp + secCd);
                        }
                        PetCooldownManager.Add(name, timeStamp + secCd);
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
