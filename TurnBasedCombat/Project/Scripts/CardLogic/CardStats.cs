using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public struct CardStats
    {
        public string nameOfCard = "Card";
        public int cardSpriteIndex = 0;

        public int manaCost;
        public int healthCost;
        public int shieldCost;
        public Effect? effectCost;

        public int manaApply;
        public int dmgApply;
        public int shieldApply;
        public Effect? effectApply;

        public TargetType targetType;

        public CardStats(string nameOfCard,
        int cardSpriteIndex, int manaCost, int healthCost, int shieldCost, Effect? effectCost,
        int manaApply, int dmgApply, int shieldApply, Effect? effectApply, TargetType targetType)
        {
            this.nameOfCard = nameOfCard;
            this.cardSpriteIndex = cardSpriteIndex;

            this.manaCost = manaCost;
            this.healthCost = healthCost;
            this.shieldCost = shieldCost;
            this.effectCost = effectCost;

            this.manaApply = manaApply;
            this.dmgApply = dmgApply;
            this.shieldApply = shieldApply;
            this.effectApply = effectApply;

            this.targetType = targetType;
        }

        public enum TargetType
        {
            Melee, Range, All, Self
        }
    }
}
