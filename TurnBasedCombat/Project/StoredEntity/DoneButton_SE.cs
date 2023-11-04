using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;
using CoreEngine;

namespace Engine
{
    public class DoneButton : GameEntity
    {

        public DoneButton(FightManager fightManager)
        {
            Sprite imageSprite = new();
            Texture2D texture = Raylib.LoadTexture(@"Project\Sprites\Buttons.png");
            imageSprite.spriteSheet = texture;
            imageSprite.spriteGrid = new Vector2(1, 2);
            imageSprite.FrameIndex = 1;
            imageSprite.layer = 20;
            AddComponent<Sprite>(imageSprite);

            Action myAction = () => fightManager.Done();
            Button button = new(myAction);

            AddComponent<Button>(button);
        }
        static void Done()
        {

        }
    }

}