using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class Enemy : GameEntity
    {
        public HealthComponent healthComponent;
        public AnimatorController animator;
        public override void OnInnit()
        {
            name = "Enemy";

            HealthBar healthBar = new()
            {
                name = "HealthBar"
            };
            healthComponent = new(healthBar, 3, 1);
            AddComponent<HealthComponent>(healthComponent);
            EntityManager.SpawnEntity(healthBar, Vector2.Zero, Vector2.One, this);
            healthComponent.UppdateHealthUI();

            Sprite sprite = new Sprite
            {
                spriteSheet = Raylib.LoadTexture(@"Project\Sprites\units.png"),
                spriteGrid = new Vector2(6, 7),
                isFlipedX = true,
                FrameIndex = 3
            };
            AddComponent<Sprite>(sprite);

            AnimatorController animator = new(sprite);

            Animation attackAnimation = new(new int[] { 3, 4, 5 }, 0.2f, false);
            animator.AddAnimation("Attack", attackAnimation);

            AddComponent<AnimatorController>(animator);
        }
    }
}