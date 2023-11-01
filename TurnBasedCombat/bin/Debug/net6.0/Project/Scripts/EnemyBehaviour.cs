using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public abstract class EnemyBehaviour
    {
        public virtual void Action(Character[] characters, int ownIndex) { }
    }
    public class None : EnemyBehaviour
    {

    }
    public class Attack : EnemyBehaviour
    {
        public int dmg;
        public Attack(int dmg)
        {
            this.dmg = dmg;
        }
        public override void Action(Character[] characters, int ownIndex)
        {
            characters[0].healthComponent.TakeDMG(dmg, characters[ownIndex].healthComponent);
        }
    }
    public class Shielding : EnemyBehaviour
    {
        public int shieldApply;
        public Shielding(int shieldApply)
        {
            this.shieldApply = shieldApply;
        }
        public override void Action(Character[] characters, int ownIndex)
        {
            characters[ownIndex].healthComponent.AddShield(shieldApply);
        }
    }
    public class Heal : EnemyBehaviour
    {
        public int healAmount;
        public Heal(int shieldApply)
        {
            this.healAmount = shieldApply;
        }
        public override void Action(Character[] characters, int ownIndex)
        {
            for (int i = 1; i < characters.Length; i++)
            {
                characters[i].healthComponent.AddShield(healAmount);
            }
        }
    }
}