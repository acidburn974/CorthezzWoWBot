using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using BotTemplate.Engines.CustomClass;

namespace CCs
{
    public class BalanceDruid : CustomClass
    {
        public override byte DesignedForClass
        {
            get
            {
                return 11;
            }
        }

        public override string Name
        {
            get
            {
                return "Druid level CC";
            }
        }

        public override void RestMana(bool isWaitingForHealth)
        {
            if (!isWaitingForHealth || PlayerManaPercent <= 15)
            {
                UseDrink();
            }
        }

        public override void RestHealth(bool isWaitingForMana)
        {
            if (!IsDrinking && !IsEating)
            {
                if (!PlayerHasBuff("Healing Touch"))
                {
                    this.CastWait("Healing Touch", 500, true);
                }
            }
        }

        public override void Fight()
        {
            Attack();

            if (distanceToTarget() > 15 && !TargetHasDebuff("Entangling Roots"))
            {
                this.CastWait("Entangling Roots", 500, true);
            }
            else
            {
                if (!TargetHasDebuff("Moonfire"))
                {
                    this.Cast("Moonfire", false);
                }

                if (PlayerHealthPercent <= 50 && !PlayerHasBuff("Rejuvenation"))
                {
                    this.Cast("Rejuvenation", false);
                }

                if (PlayerHealthPercent <= 30)
                {
                    this.CastWait("Healing Touch", 500, true);
                }

                this.Cast("Wrath", true);
            }
        }

        public override bool BuffRoutine()
        {
            if (!PlayerHasBuff("Mark of the Wild"))
            {
                this.Cast("Mark of the Wild", false);
                return false;
            }

            if (!PlayerHasBuff("Thorns"))
            {
                this.Cast("Thorns", false);
                return false;
            }

            return true;
        }
    }
}
