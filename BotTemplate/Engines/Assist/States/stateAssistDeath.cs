using BotTemplate.Helper;
using BotTemplate.Interact;
using System;
using System.Threading;
using BotTemplate.Engines.Networking;

namespace BotTemplate.Engines.Assist.States
{
    public class stateAssistDeath : State
    {
        // needs the state to get executed?
        public override bool NeedToRun
        {
            get
            {
                return (ObjectManager.isDeath || ObjectManager.isGhost);
            }
        }

        public override string Name
        {
            get
            {
                return "im death";
            }
        }

        // higher number = higher priority
        public override int Priority
        {
            get
            {
                return 80;
            }
        }

        cTimer RezzTimer = new cTimer(120000);
        cTimer WaitAfterRelease = new cTimer(5000);
        cTimer RezzTimer2 = new cTimer(500);
        public override void Run()
        {
            if (!ObjectManager.isGhost)
            {
                Calls.DoString("RepopMe();");
            }
            else
            {
                if (ObjectManager.CorpseLocation.differenceToPlayer() > 3)
                {
                    WaitAfterRelease.Reset();
                    while (!WaitAfterRelease.IsReady()) Thread.CurrentThread.Join(100);
                    Ingame.Tele(ObjectManager.CorpseLocation, 60, false);
                }
                else
                {
                    RezzTimer.Reset();
                    while (!RezzTimer.IsReady()) Thread.CurrentThread.Join(100);
                    
                    while (ObjectManager.isGhost && ObjectManager.playerPtr != 0)
                    {
                        Calls.DoString("RetrieveCorpse();");
                        RezzTimer2.Reset();
                        while (!RezzTimer2.IsReady()) Thread.CurrentThread.Join(100);
                    }
                }
            }
        }
    }
}
