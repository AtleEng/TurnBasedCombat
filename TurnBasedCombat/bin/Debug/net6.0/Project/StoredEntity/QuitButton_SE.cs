using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;
using CoreEngine;

namespace Engine
{
    public class QuitButton : GameEntity
    {

        public QuitButton()
        {
            Sprite imageSprite = new();
            Texture2D texture = Raylib.LoadTexture(@"Project\Sprites\Buttons.png");
            imageSprite.spriteSheet = texture;
            imageSprite.spriteGrid = new Vector2(1, 2);
            imageSprite.layer = 20;
            AddComponent<Sprite>(imageSprite);

            Button button = new(LeaveGame);
            AddComponent<Button>(button);
        }
        static void LeaveGame()
        {
            Core.shouldClose = true;
        }
    }

}