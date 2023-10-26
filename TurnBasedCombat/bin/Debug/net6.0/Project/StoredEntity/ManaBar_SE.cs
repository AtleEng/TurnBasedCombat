using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class ManaBar : GameEntity
    {
        public List<GameEntity> manaSprites = new();
        Vector2[] spritesPositions = { new Vector2(-6.5f, 1.5f), new Vector2(-5.5f, 1.5f), new Vector2(-4.5f, 1.5f), new Vector2(-3.5f, 1.5f) };

        public override void OnInnit()
        {
            Texture2D manaTexture = Raylib.LoadTexture(@"Project\Sprites\icons.png");
            for (int i = 0; i < spritesPositions.Length; i++)
            {
                GameEntity manaSprite = new();
                Sprite sprite = new()
                {
                    spriteSheet = manaTexture,
                    spriteGrid = new Vector2(7, 2),
                    layer = 10
                };
                manaSprite.name = "ManaSprite-" + i;
                manaSprite.AddComponent<Sprite>(sprite);
                manaSprites.Add(manaSprite);
                EntityManager.SpawnEntity(manaSprite, spritesPositions[i], Vector2.One, this);
            }
        }
    }
}