using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class Spell
    {
        public int manaCost;
        public int healthCost;
        public int shieldCost;
        public Effect? effectCost;

        public int manaApply;
        public int dmgApply;
        public int shieldApply;
        public Effect? effectApply;
        public virtual void OnUse(Character user, Character target)
        {
            target.healthComponent.TakeDMG(1, user.healthComponent);
        }
    }
}