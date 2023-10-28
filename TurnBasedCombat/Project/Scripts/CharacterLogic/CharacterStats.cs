using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;
using Engine;

namespace Engine
{
    public struct CharacterStats
    {
        public string nameOfCharacter = "Card";
        public int characterSpriteIndex = 0;

        public int startHealth;
        public int startShield;

        public EnemyBehaviour enemyBehaviour;

        public CharacterStats(String nameOfCharacter, int characterSpriteIndex, int startHealth, int startShield, EnemyBehaviour enemyBehaviour)
        {
            this.nameOfCharacter = nameOfCharacter;
            this.characterSpriteIndex = characterSpriteIndex;
            this.startHealth = startHealth;
            this.startShield = startShield;
            this.enemyBehaviour = enemyBehaviour;
        }
    }
}
