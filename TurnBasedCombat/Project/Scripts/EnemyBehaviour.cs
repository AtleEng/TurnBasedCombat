using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public abstract class EnemyBehaviour
    {
        public int applyModifier = 0;
        public int imageIndex = 3;
        public virtual void Action(Character[] characters, int ownIndex, CharacterManager characterManager) { }
    }
    public class NoBehaviour : EnemyBehaviour
    {

    }
    public class Attack : EnemyBehaviour
    {
        public Attack(int dmg)
        {
            applyModifier = dmg;
            imageIndex = 3;
        }
        public override void Action(Character[] characters, int ownIndex, CharacterManager characterManager)
        {
            characters[0].healthComponent.TakeDMG((int)(applyModifier * characters[ownIndex].effectComponent.CalculateDMGModifier(characters[0], characters[ownIndex])), characters[ownIndex].healthComponent);
        }
    }
    public class Shielding : EnemyBehaviour
    {
        public Shielding(int shieldApply)
        {
            applyModifier = shieldApply;
            imageIndex = 1;
        }
        public override void Action(Character[] characters, int ownIndex, CharacterManager characterManager)
        {
            characters[ownIndex].healthComponent.AddShield(applyModifier);
        }
    }
    public class Heal : EnemyBehaviour
    {
        public Heal(int shieldApply)
        {
            applyModifier = shieldApply;
            imageIndex = 4;
        }
        public override void Action(Character[] characters, int ownIndex, CharacterManager characterManager)
        {
            for (int i = 1; i < characters.Length; i++)
            {
                characters[i].healthComponent.Heal(applyModifier);
            }
        }
    }
    public class SummonCharacter : EnemyBehaviour
    {
        public int characterIndex;
        public SummonCharacter(int characterIndex)
        {
            this.characterIndex = characterIndex;
            imageIndex = 2;
        }
        public override void Action(Character[] characters, int ownIndex, CharacterManager characterManager)
        {
            for (int i = 1; i < characters.Length; i++)
            {
                if (characterManager.SetCharacter(i, characterIndex)) { return; }
            }
        }
    }
}