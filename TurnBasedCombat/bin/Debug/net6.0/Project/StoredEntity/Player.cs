using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class Player : GameEntity
    {
        public Character character;
        public override void OnInnit()
        {
            name = "Player";

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

            character = new(healthComponent, animator);
            character.spells.Add(new FireBall());
            character.maxMana = 100;
            character.currentMana = 100;

            AddComponent<Character>(character);
        }
    }
}