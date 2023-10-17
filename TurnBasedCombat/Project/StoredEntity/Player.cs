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
                spriteSheet = Raylib.LoadTexture(@"C:\Users\atle.engelbrektsson\Documents\C#\TurnBasedCombat\TurnBasedCombat\Project\Sprites\Wizards.png"),
                spriteGrid = new Vector2(3, 4),
            };
            AddComponent<Sprite>(sprite);

            AnimatorController animator = new(sprite);

            Animation attackAnimation = new(new int[] { 0, 1, 2 }, 0.2f, false);
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