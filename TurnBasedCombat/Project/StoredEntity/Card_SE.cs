using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class Card : GameEntity
    {
        public CardComponent cardComponent;
        public Sprite sprite;

        public Card()
        {
            sprite = new Sprite
            {
                spriteSheet = Raylib.LoadTexture(@"Project\Sprites\Cards1.png"),
                spriteGrid = new Vector2(3, 9),
                FrameIndex = 0,
                layer = 10
            };
            AddComponent<Sprite>(sprite);

            cardComponent = new();
            AddComponent<CardComponent>(cardComponent);
        }
    }
}