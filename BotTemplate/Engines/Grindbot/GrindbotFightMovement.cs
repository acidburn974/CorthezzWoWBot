using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotTemplate.Interact;
using BotTemplate.Helper;
using BotTemplate.Constants;

namespace BotTemplate.Engines.Grindbot
{
    internal static class GrindbotFightMovement
    {
        internal static bool HandleMovement = true;
        internal static cTimer waitTurn = new cTimer(250);

        internal static void Handle()
        {
            if (!Calls.IsFacing(ObjectManager.TargetObject.Pos))
            {
                Calls.TurnCharacter(ObjectManager.TargetObject.Pos);
            }
            if (HandleMovement)
            {
                if (ObjectManager.PlayerObject.isChanneling == 0 && !ObjectManager.IsCasting)
                {
                    float disToTarget = ObjectManager.TargetObject.Pos.differenceToPlayer();
                    if (disToTarget > BotTemplate.Engines.Data.fightRange)
                    {
                        Ingame.moveForward();
                    }
                    else
                    {
                        if (disToTarget > 0.8)
                        {
                            Calls.StopRunning();
                        }
                        else
                        {
                            if (!Calls.MovementIsOnly((uint)Offsets.movementFlags.Back))
                            {
                                Ingame.moveBackwards();
                            }
                        }
                    }
                }
            }
        }
    }
}
