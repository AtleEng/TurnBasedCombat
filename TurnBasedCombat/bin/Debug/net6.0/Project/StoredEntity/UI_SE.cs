using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Engine
{
    public class UIManager : GameEntity
    {
        public UIManager()
        {
            name = "UI";

            GameEntity background = new()
            {
                name = "TheBackground"
            };
            Sprite backgroundSprite = new()
            {
                spriteSheet = Raylib.LoadTexture(@"Project\Sprites\UIFrame.png"),
                layer = 5
            };
            background.AddComponent<Sprite>(backgroundSprite);
            EntityManager.SpawnEntity(background, Vector2.Zero, new Vector2(14, 10), this);

            GameEntity uiFrame = new()
            {
                name = "UIFrame"
            };
            Sprite frameSprite = new()
            {
                spriteSheet = Raylib.LoadTexture(@"Project\Sprites\background.png"),
                layer = -1
            };
            uiFrame.AddComponent<Sprite>(frameSprite);
            EntityManager.SpawnEntity(uiFrame, Vector2.Zero, new Vector2(14, 10), this);
        }
    }
}