using System.Collections.Generic;
using BotTemplate.Interact;
using BotTemplate.Helper;
using BotTemplate.Constants;
using BotTemplate.Engines.Grindbot;

namespace BotTemplate.Engines.CustomClass
{
    public class CustomClass
    {
        public virtual byte DesignedForClass
        {
            get;
            set;
        }

        protected virtual int ComboPoints
        {
            get
            {
                return ObjectManager.GetComboPoints;
            }
        }

        protected virtual float PetManaPercent
        {
            get
            {
                return ObjectManager.PetObject.manaPercent;
            }
        }

        protected virtual float PetHealthPercent
        {
            get
            {
                return ObjectManager.PetObject.healthPercent;
            }
        }

        protected virtual bool GotTextureAsDebuff(string tex)
        {
            return Ingame.GotTextureAsDebuff(tex);
        }

        protected virtual int playerRage
        {
            get
            {
                return ObjectManager.PlayerObject.rage;
            }
        }

        protected virtual void ApplyMainhand(string name)
        {
            Ingame.EnchantMainHand(name);
        }

        protected virtual void ApplyOffhand(string name)
        {
            Ingame.EnchantOffHand(name);
        }

        protected virtual void Shoot()
        {
            Ingame.Shoot();
        }

        protected virtual void StopShoot()
        {
            Ingame.StopShoot();
        }

        protected virtual void Attack()
        {
            Ingame.Attack();
        }

        protected virtual void UseDrink()
        {
            Ingame.UseDrink();
        }

        protected virtual void UseFood()
        {
            Ingame.UseFood();
        }

        protected virtual bool IsDrinking
        {
            get
            {
                return Ingame.IsDrinking();
            }
        }

        protected virtual bool IsEating
        {
            get
            {
                return Ingame.IsEating();
            }
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual bool Initialize()
        {
            return false;
        }

        protected int TotalAdds
        {
            get
            {
                return ObjectManager.AggroMobs().Count;
            }
        }

        protected float TargetHealthPercent
        {
            get
            {
                return ObjectManager.TargetObject.healthPercent;
            }
        }

        protected float PlayerHealthPercent
        {
            get
            {
                return ObjectManager.PlayerHealthPercent;
            }
        }

        protected float PlayerManaPercent
        {
            get
            {
                return ObjectManager.PlayerObject.manaPercent;
            }
        }

        protected void StopRunning()
        {
            Calls.StopRunning();
        }

        protected float distanceToTarget()
        {
            return ObjectManager.TargetObject.Pos.differenceToPlayer();
        }

        protected bool IsPetTanking()
        {
            return (ObjectManager.TargetObject.targetGuid != 0 && ObjectManager.PetObject.guid == ObjectManager.TargetObject.targetGuid) ? true : false;
        }

        protected bool IsPetOnMyTarget()
        {
            return ObjectManager.PetObject.targetGuid == ObjectManager.PlayerObject.targetGuid ? true : false;
        }

        protected void Do(string code)
        {
            Calls.DoString(code);
        }

        protected bool TargetHasDebuff(string name)
        {
            return Ingame.GotDebuff(name, "target");
        }
        protected bool PlayerHasDebuff(string name)
        {
            return Ingame.GotDebuff(name, "player");
        }


        protected bool PlayerHasBuff(string name)
        {
            return Ingame.GotBuff(name);
        }

        protected int ItemCount(string name)
        {
            return Ingame.ItemCount(name);
        }

        protected void UseItem(string name)
        {
            Ingame.UseItem(name);
        }

        protected void Cast(string name, bool gotCastTime)
        {
            Ingame.Cast(name, gotCastTime);
        }

        protected void CastWait(string name, int ms, bool gotCastTime)
        {
            Ingame.CastWait(name, ms, gotCastTime);
        }

        protected bool WarriorCanOverpower()
        {
            if (ObjectManager.GetComboPoints > 0)
            {
                return true;
            }
            return false;
        }

        protected void SwitchTo(string name)
        {
            Ingame.SwitchToStance(name);
        }

        protected bool IsStance(string name)
        {
            return Ingame.IsInStance(name);
        }


        protected bool IsReady(string name)
        {
            return Ingame.IsSpellReady(name);
        }

        protected bool PetIsSpellReady(string name)
        {
            return Ingame.PetIsSpellReady(name);
        }

        protected void PetCast(string name)
        {
            Ingame.CastPetSpell(name);
        }

        protected void Stand()
        {
            Ingame.Stand();
        }

        protected bool PetGotTextureAsBuff(string name)
        {
            return Ingame.PetGotTextureAsBuff(name);
        }

        protected bool PetGotTextureAsDebuff(string name)
        {
            return Ingame.PetGotTextureAsDebuff(name);
        }

        protected bool GotTextureAsBuff(string name)
        {
            return Ingame.GotTextureAsBuff(name);
        }

        protected void PetAttack()
        {
            Ingame.PetAttack();
        }

        protected void DismissPet()
        {
            Ingame.DismissPet();
        }

        protected bool GotPet()
        {
            return Ingame.GotPet();
        }

        public virtual void Fight()
        {

        }

        protected bool HandleMovement
        {
            set
            {
                Grindbot.GrindbotFightMovement.HandleMovement = value;
            }
        }

        protected void MoveBack()
        {
            Ingame.moveBackwards();
        }

        

        public virtual bool BuffRoutine()
        {
            return false;
        }

        public virtual bool needMana
        {
            get
            {
                return Data.needMana;
            }
        }

        public virtual bool needHealth
        {
            get
            {
                return Data.needHealth;
            }
        }

        public virtual void RestMana(bool isWaitingForHealth)
        {

        }

        public virtual void RestHealth(bool isWaitingForMana)
        {

        }
    }
}
