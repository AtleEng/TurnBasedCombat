using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class HealthBar : GameEntity
    {
        public List<GameEntity> manaSprites = new();
        Vector2[] spritesPositions = { new Vector2(-6.5f, 1.5f), new Vector2(-5.5f, 1.5f), new Vector2(-4.5f, 1.5f), new Vector2(-3.5f, 1.5f) };

        public override void OnInnit()
        {
            Texture2D healthTexture = Raylib.LoadTexture(@"Project\Sprites\HealthBar.png");
            for (int i = 0; i < spritesPositions.Length; i++)
            {
                GameEntity manaSprite = new();
                Sprite sprite = new()
                {
                    spriteSheet = healthTexture,
                    spriteGrid = new Vector2(1, 3),
                    layer = 10
                };
                manaSprite.name = "HealthSprite-" + i;
                manaSprite.AddComponent<Sprite>(sprite);
                manaSprites.Add(manaSprite);
                EntityManager.SpawnEntity(manaSprite, spritesPositions[i], Vector2.One, this);
            }
        }
    }
}