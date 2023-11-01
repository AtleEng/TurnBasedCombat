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
                InnitBattle(new int[] { 0, 1, 3, 5 });
            }

            if (gameState == States.startPlayerTurn)
            {
                OnPlayerTurn();
                gameState = States.endOfPlayerTurn;
            }
            else if (gameState == States.endOfPlayerTurn)
            {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                {
                    gameState = States.startEnemyTurn;
                }
            }
            else if (gameState == States.startEnemyTurn)
            {
                OnEnemyTurn();
                gameState = States.endOfEnemyTurn;
            }
            else if (gameState == States.endOfEnemyTurn)
            {
                Character[] characters = characterManager.characters;
                for (int i = 1; i < characters.Length; i++)
                {
                    if (characters[i].animator.gameEntity.isActive == true &&
                    characters[i].animator.isPlaying == false)
                    {
                        gameState = States.startPlayerTurn;
                    }
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
            cardManager.DiscardHand();
            cardManager.DrawFullHand();
        }
        void OnEnemyTurn()
        {
            Character[] characters = characterManager.characters;
            Random random = new Random();
            for (int i = 1; i < characters.Length; i++)
            {
                if (characters[i].animator.gameEntity.isActive == true &&
                characters[i].characterStats.enemyBehaviour != null)
                {

                    int randomIndex = random.Next(characters[i].characterStats.enemyBehaviour.Count);

                    characters[i].animator.PlayAnimation("Attack");
                    characters[i].characterStats.enemyBehaviour[randomIndex].Action(characters, i);
                }
            }
        }

        public enum States
        {
            startPlayerTurn, endOfPlayerTurn, startEnemyTurn, endOfEnemyTurn
        }
    }
}