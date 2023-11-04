using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class ImageDisplay : GameEntity
    {
        int theNumber;
        public void SetNumber(int i)
        {
            theNumber = Math.Max(Math.Min(i, 13), 0);
            imageSprite.FrameIndex = theNumber;
        }

        Sprite imageSprite = new();
        public ImageDisplay()
        {
            Texture2D numberTexture = Raylib.LoadTexture(@"Project\Sprites\icons.png");
            imageSprite.spriteSheet = numberTexture;
            imageSprite.spriteGrid = new Vector2(7, 2);
            AddComponent<Sprite>(imageSprite);
        }
    }
}