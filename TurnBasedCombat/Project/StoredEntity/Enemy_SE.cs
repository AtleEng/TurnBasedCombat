using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class Enemy : GameEntity
    {
        public override void OnInnit()
        {
            name = "Enemy";

            HealthComponent healthComponent = new();
            AddComponent<HealthComponent>(healthComponent);

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