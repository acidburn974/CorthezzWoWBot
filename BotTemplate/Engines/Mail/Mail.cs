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

namespace BotTemplate.Engines.Mails
{
    internal static class MailEngine
    {
        internal static Mail engine;
        internal static string name = "Mailer";
    }

    internal class Mail
    {
        private Thread thrWorker;
        internal bool isRunning
        {
            get
            {
                return Running;
            }
        }
        private bool Running;

        internal void StartEngine(string name)
        {
            Running = true;
            thrWorker = new Thread(Run) { IsBackground = true };
            thrWorker.Start();
            Exchange.CurrentEngine = name;
            Exchange.IsEngineRunning = true;
        }

        internal void StopEngine()
        {
            Running = false;
        }

        string getFirstNeck = "function topLel() CastSpellByName('disenchant'); for bagnumber = 0,4 do " +
            "for j = 1,16 do " +
            "link = GetContainerItemLink(bagnumber,j) " +
            "if (link) then link = gsub(link,'^.*%[(.*)%].*$','%1') " +
            "if ( string.lower('Nature\\'s Whisper') == string.lower(link)) then " +
            "PickupContainerItem(bagnumber,j) return; end end end end end topLel();";

        private void Run()
        {
            while (Running)
            {
                while (!ObjectManager.IsCasting && Running)
                {
                    Thread.Sleep(250);
                    Calls.DoString(getFirstNeck);
                }
                while (Running && Calls.IsLooting() == 0) Thread.Sleep(100);
                Calls.AutoLoot();
                while (Running && Calls.IsLooting() == 1) Thread.Sleep(100);
                Thread.Sleep(500);
            }
            //string money = Calls.GetText("money = GetMoney();", "money", 20);
            //double copperCount = 0;
            //bool one = Double.TryParse(money, out copperCount);
            //if (one)
            //{
            //    copperCount = (copperCount - (Data.MailerCharacters.Length * 30)) / Data.MailerCharacters.Length;
            //    foreach (string x in Data.MailerCharacters)
            //    {
            //        Calls.DoString("ClearSendMail() SetSendMailMoney(" + copperCount + ") SendMail('" + x + "', 'hello', 'do you attend the next raid?')");
            //        Thread.CurrentThread.Join(1000);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Cannot convert " + money + " to double");
            //}

            Exchange.IsEngineRunning = false;
            Running = false;
            Exchange.CurrentEngine = "None";
            MailEngine.engine = null;
        }
    }
}
