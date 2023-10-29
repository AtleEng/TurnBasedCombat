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
        public Sprite sprite;
        public Animator animator;
        public Character()
        {
            HealthBar healthBar = new()
            {
                name = "HealthBar"
            };
            healthComponent = new(healthBar, 3, 1);
            AddComponent<HealthComponent>(healthComponent);
            EntityManager.SpawnEntity(healthBar, Vector2.Zero, Vector2.One, this);
            healthComponent.UpdateHealthUI();

            sprite = new Sprite
            {
                spriteSheet = Raylib.LoadTexture(@"Project\Sprites\units.png"),
                spriteGrid = new Vector2(6, 7)
            };
            AddComponent<Sprite>(sprite);

            animator = new(sprite);

            Animation attackAnimation = new(new int[] { 3, 4, 5 }, 0.2f, false);
            animator.AddAnimation("Attack", attackAnimation);

            AddComponent<Animator>(animator);
        }
    }
}