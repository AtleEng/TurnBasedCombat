using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class FightManager : Component, IScript
    {
        CardManager cardManager;
        CharacterManager characterManager;

        public FightManager(CardManager cardManager, CharacterManager characterManager)
        {
            this.cardManager = cardManager;
            this.characterManager = characterManager;
        }

        public override void Update(float delta)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                OnPlayerTurn();
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                OnEnemyTurn();
            }
        }
        void InnitBattle()
        {

        }
        void OnPlayerTurn()
        {
            cardManager.manaComponent.AddMana(1);
            cardManager.DiscardHand();
            cardManager.DrawFullHand();
        }
        void OnEnemyTurn()
        {

        }
    }
}