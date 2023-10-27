using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using Engine;

namespace Engine
{
    public class CardComponent : Component
    {
        public CardStats cardStats = new("?", 0, 0, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.All);
        public Sprite sprite;
        public Player player;
        public CardComponent(Sprite sprite, Player player)
        {
            this.sprite = sprite;
            this.player = player;
        }
        public bool CanUseCard()
        {
            if (player.manaComponent.UseMana(cardStats.manaCost))
            {
                System.Console.WriteLine($"Used card: {cardStats.nameOfCard}");
                player.healthComponent.currentShield -= cardStats.shieldCost;
                if (player.healthComponent.currentShield < 0) { player.healthComponent.currentShield = 0; }

                player.healthComponent.TakeDMG(cardStats.healthCost, player.healthComponent);

                return true;
            }
            return false;
        }

        public void UseCard()
        {
            player.manaComponent.AddMana(cardStats.manaApply);
            player.healthComponent.AddShield(cardStats.shieldApply);
        }
    }
}