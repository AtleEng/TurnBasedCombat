using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;
using Engine;

namespace Engine
{
    public class Sprite : Component
    {
        public Texture2D spriteSheet;
        public Vector2 spriteGrid = Vector2.One;
        int frameIndex;
        public int FrameIndex
        {
            get
            {
                return frameIndex;
            }
            set
            {
                if (frameIndex >= spriteGrid.X * spriteGrid.Y || frameIndex < 0)
                {
                    frameIndex = 0;
                }
                System.Console.WriteLine(frameIndex);
            }
        }
        public Color colorTint = Color.WHITE;
    }
}