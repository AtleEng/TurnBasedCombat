using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class Card : GameEntity
    {
        public CardStats cardStats;
        public Card(CardStats cardStats)
        {
            this.cardStats = cardStats;
        }
        public override void OnInnit()
        {
            name = "Card-" + cardStats.nameOfCard;
            localTransform.size = new Vector2(2,2);

            Sprite sprite = new Sprite
            {
                spriteSheet = Raylib.LoadTexture(@"Project\Sprites\Cards1.png"),
                spriteGrid = new Vector2(3, 9),
                FrameIndex = cardStats.cardSpriteIndex,
                layer = 10
            };
            AddComponent<Sprite>(sprite);
        }
    }
}