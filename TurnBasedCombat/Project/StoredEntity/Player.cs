using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Engine
{
    public class Player : GameEntity
    {
        public Character character;
        public override void OnInnit()
        {
            HealthComponent healthComponent = new();
            AddComponent<HealthComponent>(healthComponent);

            Sprite sprite = new Sprite
            {
                spriteSheet = Raylib.LoadTexture(@"C:\Users\atle.engelbrektsson\Documents\C#\TurnBasedCombat\TurnBasedCombat\Project\Sprites\FireWizardSpriteSheet.png"),
                spriteGrid = new Vector2(4, 1),
            };
            AddComponent<Sprite>(sprite);

            Animator animator = new(sprite);

            character = new(healthComponent, animator);
            character.spells.Add(new FireBall());
            character.maxMana = 100;
            character.currentMana = 100;

            AddComponent<Character>(character);
        }
    }
}