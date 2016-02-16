using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotTemplate.Interact
{
    internal static class Register
    {
        internal static void LuaFunctions()
        {
            List<string> functions = new List<string>();

            functions.Add("function IsVendorOpen() if MerchantFrame:IsVisible() then return 'true' else return 'false' end end");
            
            functions.Add("function getItemsInBag(totalSlots,bagnumber,item) tmpItem = 0 for j = 1,totalSlots do " +
                "link = GetContainerItemLink(bagnumber,j) " +
                "if ( link ) then link = gsub(link,'^.*%[(.*)%].*$','%1') " +
                "if ( string.lower(item) == string.lower(link)) then " +
                "_, test12 = GetContainerItemInfo(bagnumber,j) tmpItem = tmpItem + test12 end end end return tmpItem end");

            functions.Add("function useItemInBag(totalSlots, bagnumber, item) for j = 1,totalSlots do " +
                "link = GetContainerItemLink(bagnumber,j) " +
                "if ( link ) then link = gsub(link,'^.*%[(.*)%].*$','%1') " +
                "if ( string.lower(item) == string.lower(link)) then " +
                "UseContainerItem(bagnumber,j) return 1 end end end return 0 end");

            functions.Add("function getSpellId(SpellName) " +
                 "local B = 'BOOKTYPE_SPELL' " +
                 "local SpellRank = 0 " +
                 "local SpellID = nil " +
                 "if SpellName then " +
                 "local SpellCount = 0 " +
                 "local ReturnName = nil " +
                 "local ReturnRank = nil " +
                 "while SpellName ~= ReturnName do " +
                 "SpellCount = SpellCount + 1 " +
                 "ReturnName, ReturnRank = GetSpellName(SpellCount, B) " +
                 "if not ReturnName then " +
                 "break " +
                 "end " +
                 "end " +
                 "while SpellName == ReturnName do " +
                 "if SpellRank then " +
                 "if SpellRank == 0 then " +
                 "return SpellCount " +
                 "elseif ReturnRank and ReturnRank ~= '' then " +
                 "local found, _, Rank = string.find(ReturnRank, '(%d+)') " +
                 "if found then " +
                 "ReturnRank = tonumber(Rank) " +
                 "else " +
                 "ReturnRank = 1 " +
                 "end " +
                 "else " +
                 "ReturnRank = 1 " +
                 "end " +
                 "if SpellRank == ReturnRank then " +
                 "return SpellCount " +
                 "end " +
                 "else " +
                 "SpellID = SpellCount " +
                 "end " +
                 "SpellCount = SpellCount + 1 " +
                 "ReturnName, ReturnRank = GetSpellName(SpellCount, B) " +
                 "end " +
                 "end " +
                 "return SpellID " +
                 "end ");

            functions.Add("function hasBuff(name) abc12 = getSpellId(name) if abc12 ~= nil then GetSpellForBot = GetSpellTexture(abc12, 'BOOKTYPE_SPELL') " +
             "i = 1 while UnitBuff('player',i) do " +
             "if GetSpellForBot == UnitBuff('player',i) then return 'true' end " +
             "i = i + 1 end return 'false' end end ");

            functions.Add("function hasParty2Buff(name) abc12 = getSpellId(name) if abc12 ~= nil then GetSpellForBot = GetSpellTexture(abc12, 'BOOKTYPE_SPELL') " +
             "i = 1 while UnitBuff('party2',i) do " +
             "if GetSpellForBot == UnitBuff('party2',i) then return 'true' end " +
             "i = i + 1 end return 'false' end end ");

            functions.Add("function hasParty3Buff(name) abc12 = getSpellId(name) if abc12 ~= nil then GetSpellForBot = GetSpellTexture(abc12, 'BOOKTYPE_SPELL') " +
             "i = 1 while UnitBuff('party3',i) do " +
             "if GetSpellForBot == UnitBuff('party3',i) then return 'true' end " +
             "i = i + 1 end return 'false' end end ");

            functions.Add("function hasParty4Buff(name) abc12 = getSpellId(name) if abc12 ~= nil then GetSpellForBot = GetSpellTexture(abc12, 'BOOKTYPE_SPELL') " +
             "i = 1 while UnitBuff('party4',i) do " +
             "if GetSpellForBot == UnitBuff('party4',i) then return 'true' end " +
             "i = i + 1 end return 'false' end end ");

            functions.Add("function hasParty1Buff(name) abc12 = getSpellId(name) if abc12 ~= nil then GetSpellForBot = GetSpellTexture(abc12, 'BOOKTYPE_SPELL') " +
             "i = 1 while UnitBuff('party1',i) do " +
             "if GetSpellForBot == UnitBuff('party1',i) then return 'true' end " +
             "i = i + 1 end return 'false' end end ");

            functions.Add("function hasDebuff(name,unit) abc12 = getSpellId(name) if abc12 ~= nil then GetSpellForBot = GetSpellTexture(abc12, 'BOOKTYPE_SPELL') " +
             "i = 1 while UnitDebuff(unit,i) do " +
             "if GetSpellForBot == UnitDebuff(unit,i) then return 'true' end " +
             "i = i + 1 end return 'false' end end ");

            functions.Add("function autoAttack() if IsCurrentAction('24') == nil then " +
                "CastSpellByName('Attack') end end");

            functions.Add("function shoot() if HasAction(23) == 1 then if IsAutoRepeatAction(23) == nil then " +
                "CastSpellByName('Shoot') end end end");

            functions.Add("function stopShoot() if HasAction(23) == 1 then if IsAutoRepeatAction(23) == 1 then " +
                "CastSpellByName('Shoot') end end end");

            functions.Add("function isSpellReady(name) " +
             "SpellID = getSpellId(name) " +
             "if SpellID then " +
             "startTime, Seconds, Enabled = GetSpellCooldown(SpellID, 'BOOKTYPE_SPELL') " +
             "return startTime, Seconds, Enabled " +
             "end " +
             "end");

            functions.Add("function hasBuffWithTexture(texture, unit) GetSpellForBot = texture " +
             "i = 1 while UnitBuff(unit,i) do " +
             "if GetSpellForBot == UnitBuff(unit,i) then return 'true' end " +
             "i = i + 1 end return 'false' end");


            //GetNumPartyMembers()
            functions.Add(@"function isGroupResting() GetSpellForBot = 'Interface\\Icons\\INV_Drink_07' GetSpellForBot2 = 'Interface\\Icons\\INV_Misc_Fork&Knife' " +

             "for index = 1,GetNumPartyMembers(),1 do " + 
             "i = 1 " +
             "while UnitBuff('party' .. index,i) do " +
             "if GetSpellForBot == UnitBuff('party' .. index,i) or GetSpellForBot2 == UnitBuff('party' .. index,i) then return 'true' end " +
             "i = i + 1 end " +
             "end " +
             "return 'false' end ");

            functions.Add("function hasDebuffWithTexture(texture, unit) GetSpellForBot = texture " +
             "i = 1 while UnitDebuff(unit,i) do " +
             "if GetSpellForBot == UnitDebuff(unit,i) then return 'true' end " +
             "i = i + 1 end return 'false' end ");

            functions.Add("function castPetSpell(name) " +
	        "for index = 1,11,1 do " + 
		    "curName = GetPetActionInfo(index); " +
		    "if curName == name then " +
			"CastPetAction(index); " +
			"break " +
		    "end end end");

            functions.Add("function gotPetSpellCd(name) " +
            "for index = 1,11,1 do " +
            "curName = GetPetActionInfo(index); " +
            "if curName == name then " +
            "startTime, duration, enable = GetPetActionCooldown(index); " +
            "return startTime, duration; " +
            "end end end");

            functions.Add("function returnStanceIndex(name) finalresult = 0 num = GetNumShapeshiftForms(); curNum = 1; while curNum <= num do _, name2, _, _ = GetShapeshiftFormInfo(curNum); if name2 == name then finalresult = curNum break else curNum = curNum + 1; end end return finalresult end");

            foreach (string x in functions)
            {
                Calls.DoString(x);
            }
        }
    }
}
