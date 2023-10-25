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
        public void OnUseCard()
        {
            if (player.manaComponent.UseMana(cardStats.manaCost))
            {
                System.Console.WriteLine("Used card");
            }
        }
    }
}