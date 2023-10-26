using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class HealthBar : GameEntity
    {
        public List<HealthSprite> healthSprites = new();
        Vector2[] spritesPositions = { new Vector2(-0.325f, 1.325f), new Vector2(0, 1.325f), new Vector2(0.325f, 1.325f), new Vector2(0.65f, 1.325f) };

        public override void OnInnit()
        {
            Texture2D healthTexture = Raylib.LoadTexture(@"Project\Sprites\HealthBar.png");
            for (int i = 0; i < spritesPositions.Length; i++)
            {
                HealthSprite healthSprite = new(healthTexture)
                {
                    name = "HealthSprite-" + i
                };

                healthSprites.Add(healthSprite);
                EntityManager.SpawnEntity(healthSprite, spritesPositions[i], new Vector2(0.1875f,0.1875f), this);
            }
        }
    }
    public class HealthSprite : GameEntity
    {
        public Sprite sprite;
        public HealthSprite(Texture2D texture2D)
        {
            sprite = new()
            {
                spriteSheet = texture2D,
                spriteGrid = new Vector2(3, 1),
                layer = 10
            };
            AddComponent<Sprite>(sprite);
        }
    }
}