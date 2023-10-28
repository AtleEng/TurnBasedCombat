using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class CharacterManager : Component, IScript
    {
        public Dictionary<string, CharacterStats> allCharacters = new();
        GameEntity characterHolder = new();

        List<Character> characters = new();
        Vector2[] characterPosition = { new Vector2(-4, -1), new Vector2(0, -1), new Vector2(2, -1), new Vector2(4, -1) };

        int selectedCard = -1; //if negative no card is selected
        int selectedCharacter = -1;

        public override void Start()
        {
            characterHolder.name = "Characters";
            EntityManager.SpawnEntity(characterHolder);

            AddCharacter("Player", 39, 4, 0, new None());

            AddCharacter("Skeleton", 0, 3, 0, new None());

            SpawInCharacter();
        }
        public override void Update(float delta)
        {

        }
        void SpawInCharacter()
        {
            for (int i = 0; i < characterPosition.Length; i++)
            {
                Character character = new Character();
                if (i == 0)
                {
                    character.name = "Player";
                    character.characterStats = allCharacters["Player"];
                }
                else
                {
                    character.name = $"Enemy{i}";
                    character.sprite.isFlipedX = true;
                    character.characterStats = allCharacters["Player"];
                }
                characters.Add(character);
                EntityManager.SpawnEntity(character, characterPosition[i], new Vector2(2, 2), characterHolder);
            }
        }
        void AddCharacter(String name, int spriteIndex, int startHealth, int startShield, EnemyBehaviour enemyBehaviour)
        {
            allCharacters.Add(name, new CharacterStats(name, spriteIndex, startHealth, startShield, enemyBehaviour));
        }
    }
}