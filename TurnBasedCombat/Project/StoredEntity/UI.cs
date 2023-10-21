using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Engine
{
    public class UIManager : GameEntity
    {
        Texture2D uiFrame = Raylib.LoadTexture(@"C:\Users\atle.engelbrektsson\Documents\C#\TurnBasedCombat\TurnBasedCombat\Project\Sprites\UIFrame.png");
        public override void OnInnit()
        {
            name = "UI";


            Sprite sprite = new()
            {
                spriteSheet = uiFrame,
            };
            AddComponent<Sprite>(sprite);
        }
    }
}