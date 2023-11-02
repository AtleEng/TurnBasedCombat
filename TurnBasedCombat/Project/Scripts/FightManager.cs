using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class FightManager : Component, IScript
    {
        public States gameState = States.startPlayerTurn;

        CardManager cardManager;
        CharacterManager characterManager;

        public FightManager(CardManager cardManager, CharacterManager characterManager)
        {
            this.cardManager = cardManager;
            this.characterManager = characterManager;
        }

        public override void Update(float delta)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                InnitBattle(new int[] { 0, 1, 3, 6 });
            }

            if (gameState == States.startPlayerTurn)
            {
                OnPlayerTurn();
                gameState = States.playerTurn;
            }
            else if (gameState == States.playerTurn)
            {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                {
                    gameState = States.startEnemyTurn;
                }
            }
            else if (gameState == States.startEnemyTurn)
            {
                cardManager.DiscardHand();
                OnEnemyTurn();
                gameState = States.enemyTurn;
            }
            else if (gameState == States.enemyTurn)
            {
                Character[] characters = characterManager.characters;
                bool isntDone = false;
                for (int i = 1; i < characters.Length; i++)
                {
                    if (characters[i].animator.gameEntity.isActive == true &&
                    characters[i].animator.isPlaying == true)
                    {
                        isntDone = true;
                    }
                }
                if (!isntDone)
                {
                    gameState = States.startPlayerTurn;
                }
            }
        }
        void InnitBattle(int[] indexes)
        {
            cardManager.DiscardHand();
            cardManager.DrawFullHand();

            for (int i = 0; i < characterManager.characters.Length; i++)
            {
                if (indexes[i] >= 0)
                {
                    characterManager.SetCharacter(i, indexes[i]);
                }
            }
        }
        void OnPlayerTurn()
        {
            cardManager.manaComponent.AddMana(1);
            cardManager.DrawFullHand();
        }
        void OnEnemyTurn()
        {
            Random random = new Random();
            for (int i = 1; i < characterManager.characters.Length; i++)
            {
                if (characterManager.characters[i].isActive == true && characterManager.characters[i].characterStats.enemyBehaviour.Count > 0)
                {
                    int randomIndex = random.Next(characterManager.characters[i].characterStats.enemyBehaviour.Count);
                    characterManager.characters[i].characterStats.enemyBehaviour[randomIndex].Action(characterManager.characters, i);
                    characterManager.characters[i].animator.PlayAnimation("Attack");
                }
            }
        }

        public enum States
        {
            startPlayerTurn, playerTurn, startEnemyTurn, enemyTurn
        }
    }
}