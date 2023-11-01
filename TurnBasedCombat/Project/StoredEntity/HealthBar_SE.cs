using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class HealthBar : GameEntity
    {
        int flip = 1;
        List<HealthSprite> healthSprites = new();
        List<HealthSprite> shieldSprites = new();

        Texture2D healthbarTexture;

        public HealthBar()
        {
            healthbarTexture = Raylib.LoadTexture(@"Project\Sprites\HealthBar.png");
        }
        public override void OnInnit()
        {

        }
        public void UpdateHealthUI(int currentHealth, int maxHealth, int currentShield)
        {
            // Ensure currentHealth and currentShield are within bounds
            currentHealth = Math.Min(currentHealth, maxHealth);
            currentShield = Math.Min(currentShield, maxHealth);

            System.Console.WriteLine(currentHealth + " health / " + currentShield + " shield");

            // Clear the lists and destroy child entities
            healthSprites.Clear();
            shieldSprites.Clear();
            foreach (GameEntity child in children)
            {
                EntityManager.DestroyEntity(child);
            }

            float spriteYPosition = 1.6f;

            for (int i = 0; i < maxHealth; i++)
            {
                // Create and add health sprite
                HealthSprite healthSprite = new HealthSprite(healthbarTexture)
                {
                    name = "HealthSprite-" + i
                };
                healthSprites.Add(healthSprite);

                // Position health sprite
                Vector2 spritePos = new Vector2((-0.325f + 0.325f * i) * -flip, spriteYPosition);
                EntityManager.SpawnEntity(healthSprite, spritePos, new Vector2(0.1875f, 0.1875f), this);

                // Create and add shield sprite
                HealthSprite shieldSprite = new HealthSprite(healthbarTexture)
                {
                    name = "ShieldSprite-" + i
                };
                shieldSprite.sprite.FrameIndex = 1;
                shieldSprites.Add(shieldSprite);

                // Position shield sprite
                spritePos = new Vector2((-0.325f + 0.325f * i) * -flip, spriteYPosition - .325f);
                EntityManager.SpawnEntity(shieldSprite, spritePos, new Vector2(0.1875f, 0.1875f), this);

                // Set health and shield sprites based on correct health and shield values
                if (i < currentHealth)
                {
                    healthSprites[i].sprite.FrameIndex = 0;
                }
                else
                {
                    healthSprites[i].sprite.FrameIndex = 2;
                }
                if (i < currentShield)
                {
                    shieldSprites[i].isActive = true;
                }
                else
                {
                    shieldSprites[i].isActive = false;
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
}