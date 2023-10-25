using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class Player : GameEntity
    {
        public HealthComponent healthComponent;
        public ManaComponent manaComponent;
        public AnimatorController animator;
        public override void OnInnit()
        {
            name = "Player";

            GameEntity manaBar = new();
            manaBar.AddComponent<ManaBarLogic>(new ManaBarLogic());

            EntityManager.SpawnEntity(manaBar, Vector2.Zero, Vector2.One, null);

            ManaComponent manaComponent = new ManaComponent(manaBar.GetComponent<ManaBarLogic>());
            AddComponent<ManaComponent>(manaComponent);

            HealthComponent healthComponent = new();
            AddComponent<HealthComponent>(healthComponent);
            

            Sprite sprite = new Sprite
            {
                spriteSheet = Raylib.LoadTexture(@"Project\Sprites\units.png"),
                spriteGrid = new Vector2(6, 7),
                FrameIndex = 39
            };
            AddComponent<Sprite>(sprite);

            AnimatorController animator = new(sprite);

            Animation attackAnimation = new(new int[] { 39, 40, 41 }, 0.2f, false);
            animator.AddAnimation("Attack", attackAnimation);

            AddComponent<AnimatorController>(animator);
            
        }
    }
}