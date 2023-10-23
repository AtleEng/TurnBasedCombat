using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class BasicMonster : GameEntity
    {
        public Character character;
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

            character = new(healthComponent, animator);
            character.spells.Add(new FireBall());
            character.maxMana = 100;
            character.currentMana = 100;

            AddComponent<Character>(character);
        }
    }
}