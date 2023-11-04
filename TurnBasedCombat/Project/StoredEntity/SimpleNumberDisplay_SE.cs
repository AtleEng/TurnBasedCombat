using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class NumberDisplay : GameEntity
    {
        int theNumber;
        public void SetNumber(int i)
        {
            theNumber = Math.Max(Math.Min(i, 9), 0);
            numberSprite.FrameIndex = theNumber;
        }

        Sprite numberSprite = new();
        public NumberDisplay()
        {
            Texture2D numberTexture = Raylib.LoadTexture(@"Project\Sprites\textbase.png");
            numberSprite.spriteSheet = numberTexture;
            numberSprite.spriteGrid = new Vector2(10, 1);
            AddComponent<Sprite>(numberSprite);
        }
    }
}