using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class Character : GameEntity
    {
        public CharacterStats characterStats;
        public HealthComponent healthComponent;
        public EffectComponent effectComponent = new();
        public Sprite sprite;
        public Animator animator;

        public GameEntity targetIcon;

        public NumberDisplay numberDisplay;
        public ImageDisplay imageDisplay;

        public int randomIndex = 0;
        public Character()
        {
            HealthBar healthBar = new()
            {
                name = "HealthBar"
            };
            EntityManager.SpawnEntity(healthBar, Vector2.Zero, Vector2.One, this);
            healthComponent = new(healthBar);
            AddComponent<HealthComponent>(healthComponent);

            AddComponent<EffectComponent>(effectComponent);

            sprite = new Sprite
            {
                spriteSheet = Raylib.LoadTexture(@"Project\Sprites\units.png"),
                spriteGrid = new Vector2(6, 7)
            };
            AddComponent<Sprite>(sprite);

            animator = new(sprite);

            AddComponent<Animator>(animator);

            targetIcon = new()
            {
                isActive = false
            };
            Sprite targetIconSprite = new Sprite
            {
                spriteSheet = Raylib.LoadTexture(@"Project\Sprites\Target.png"),
                spriteGrid = new Vector2(1, 1)
            };
            targetIcon.AddComponent<Sprite>(targetIconSprite);
            EntityManager.SpawnEntity(targetIcon, new Vector2(0, -1), new Vector2(0.5f, 0.5f), this);

            numberDisplay = new()
            {
                isActive = false
            };
            EntityManager.SpawnEntity(numberDisplay, new Vector2(0.5f, -2f), new Vector2(0.5f, 0.5f), this);
            imageDisplay = new()
            {
                isActive = false
            };
            EntityManager.SpawnEntity(imageDisplay, new Vector2(-0.5f, -2f), new Vector2(0.5f, 0.5f), this);
        }
    }
}