using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class Card : GameEntity
    {
        public override void OnInnit()
        {
            name = "Card";

            CardComponent cardComponent = new();
            AddComponent<CardComponent>(cardComponent);

            Sprite sprite = new Sprite
            {
                spriteSheet = Raylib.LoadTexture(@"Project\Sprites\Cards1.png"),
                spriteGrid = new Vector2(3, 9),
                FrameIndex = 3
            };
            AddComponent<Sprite>(sprite);
        }
    }
}