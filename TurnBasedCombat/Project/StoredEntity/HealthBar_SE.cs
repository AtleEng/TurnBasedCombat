using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class HealthBar : GameEntity
    {
        public int maxHealth = 3;
        public int flip = 1;
        public List<HealthSprite> healthSprites = new();
        public List<HealthSprite> shieldSprites = new();

        public override void OnInnit()
        {
            Texture2D healthbarTexture = Raylib.LoadTexture(@"Project\Sprites\HealthBar.png");
            for (int i = 0; i < maxHealth; i++)
            {
                HealthSprite healthSprite = new(healthbarTexture)
                {
                    name = "HealthSprite-" + i
                };
                healthSprites.Add(healthSprite);

                Vector2 spritePos = new Vector2((-0.325f + 0.325f * i) * -flip, 1.65f);
                EntityManager.SpawnEntity(healthSprite, spritePos, new Vector2(0.1875f, 0.1875f), this);
            }
            for (int i = 0; i < maxHealth; i++)
            {
                HealthSprite shieldSprite = new(healthbarTexture)
                {
                    name = "ShieldSprite-" + i
                };
                shieldSprite.sprite.FrameIndex = 1;
                shieldSprites.Add(shieldSprite);

                Vector2 spritePos = new Vector2((-0.325f + 0.325f * i) * -flip, 1.325f);
                EntityManager.SpawnEntity(shieldSprite, spritePos, new Vector2(0.1875f, 0.1875f), this);
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