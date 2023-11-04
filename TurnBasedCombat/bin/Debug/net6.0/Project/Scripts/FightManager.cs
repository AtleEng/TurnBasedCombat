using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class FightManager : Component, IScript
    {
        public States gameState = States.beforeBattle;

        CardManager cardManager;
        CharacterManager characterManager;

        bool done;

        public FightManager(CardManager cardManager, CharacterManager characterManager)
        {
            this.cardManager = cardManager;
            this.characterManager = characterManager;
        }

        public void Done()
        {
            done = true;
        }
        public override void Update(float delta)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                InnitBattle(new int[] { 0, 5, 2, 8 });
            }

            if (gameState == States.startPlayerTurn)
            {
                OnPlayerTurn();
                gameState = States.playerTurn;
                done = false;
            }
            else if (gameState == States.playerTurn)
            {
                if (done)
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

            for (int i = 0; i < characterManager.characters.Length; i++)
            {
                if (indexes[i] >= 0)
                {
                    characterManager.SetCharacter(i, indexes[i]);
                }
            }
            gameState = States.startPlayerTurn;
        }
        void OnPlayerTurn()
        {
            cardManager.DrawFullHand();
            if (characterManager.characters[0].effectComponent.currentEffect != null)
            {
                if (characterManager.characters[0].effectComponent.currentEffect.GetType() == typeof(ManaEffect))
                {
                    cardManager.manaComponent.AddMana(1);
                }
                if (characterManager.characters[0].effectComponent.currentEffect.GetType() == typeof(StunEffect))
                {
                    cardManager.DiscardHand();
                }
            }
            cardManager.manaComponent.AddMana(1);

            foreach (Character character in characterManager.characters)
            {
                if (character.effectComponent.currentEffect != null)
                {
                    if (character.effectComponent.currentEffect.GetType() == typeof(FireEffect))
                    {
                        character.healthComponent.TakeDMG(1, character.healthComponent);
                    }
                    character.effectComponent.RemoveEffect(1);
                }
            }



            Random random = new Random();
            Character[] characters = characterManager.characters; // Cache the characters array for better performance

            for (int i = 1; i < characters.Length; i++)
            {
                Character character = characters[i];

                if (character.isActive && character.characterStats.enemyBehaviour.Count > 0)
                {
                    int randomIndex = random.Next(character.characterStats.enemyBehaviour.Count);
                    character.randomIndex = randomIndex;
                    EnemyBehaviour enemyBehavior = character.characterStats.enemyBehaviour[randomIndex];

                    character.numberDisplay.SetNumber(enemyBehavior.applyModifier);
                    if (enemyBehavior.applyModifier > 0)
                    {
                        character.numberDisplay.isActive = true;
                    }
                    else
                    {
                        character.numberDisplay.isActive = false;
                    }

                    character.imageDisplay.SetNumber(enemyBehavior.imageIndex);
                    if (enemyBehavior.GetType() != typeof(NoBehaviour))
                    {
                        character.imageDisplay.isActive = true;
                    }
                    else
                    {
                        character.imageDisplay.isActive = false;
                    }
                }
                else
                {
                    character.numberDisplay.isActive = false;
                    character.imageDisplay.isActive = false;
                }
            }
        }
        void OnEnemyTurn()
        {
            for (int i = 1; i < characterManager.characters.Length; i++)
            {
                if (characterManager.characters[i].isActive == true
                && characterManager.characters[i].characterStats.enemyBehaviour.Count > 0
                && characterManager.characters[i].characterStats.enemyBehaviour[characterManager.characters[i].randomIndex].GetType() != typeof(NoBehaviour)
                )
                {
                    if (characterManager.characters[i].effectComponent.currentEffect == null)
                    {
                        characterManager.characters[i].characterStats.enemyBehaviour[characterManager.characters[i].randomIndex].Action(characterManager.characters, i, characterManager);
                        characterManager.characters[i].animator.PlayAnimation("Attack");
                    }
                    else if (characterManager.characters[i].effectComponent.currentEffect.GetType() != typeof(StunEffect))
                    {
                        characterManager.characters[i].characterStats.enemyBehaviour[characterManager.characters[i].randomIndex].Action(characterManager.characters, i, characterManager);
                        characterManager.characters[i].animator.PlayAnimation("Attack");
                    }

                }
            }
        }

        public enum States
        {
            startPlayerTurn, playerTurn, startEnemyTurn, enemyTurn, beforeBattle, winning, losing
        }
    }
}