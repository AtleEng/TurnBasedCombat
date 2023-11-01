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
            EntityManager.SpawnEntity(healthBar, Vector2.Zero, Vector2.One, this);
            healthComponent = new(healthBar);
            AddComponent<HealthComponent>(healthComponent);

            sprite = new Sprite
            {
                spriteSheet = Raylib.LoadTexture(@"Project\Sprites\units.png"),
                spriteGrid = new Vector2(6, 7)
            };
            AddComponent<Sprite>(sprite);

            animator = new(sprite);

            AddComponent<Animator>(animator);
        }
        public override void OnInnit()
        {

        }
    }
}