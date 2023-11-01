using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using Engine;

namespace Engine
{
    public class CardComponent : Component
    {
        public CardStats cardStats = new("?", 0, 0, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.All);
        public bool CanUseCard(ManaComponent manaComponent)
        {
            if (manaComponent.UseMana(cardStats.manaCost))
            {
                return true;
            }
            return false;
        }
        public void UseCard(Character user, List<Character> targets, ManaComponent manaComponent)
        {
            System.Console.WriteLine($"Used card: {cardStats.nameOfCard}");

            user.healthComponent.currentShield -= cardStats.shieldCost;
            if (user.healthComponent.currentShield < 0) { user.healthComponent.currentShield = 0; }

            user.healthComponent.TakeDMG(cardStats.healthCost, user.healthComponent);

            //add effect

            foreach (Character target in targets)
            {
                manaComponent.AddMana(cardStats.manaApply);

                target.healthComponent.AddShield(cardStats.shieldApply);

                target.healthComponent.TakeDMG(cardStats.dmgApply, user.healthComponent);

                //add effect
            }
        }
    }
}