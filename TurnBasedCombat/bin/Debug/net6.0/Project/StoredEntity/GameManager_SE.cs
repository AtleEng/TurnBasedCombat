using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Engine
{
    public class GameManager : GameEntity
    {
        public GameManager()
        {
            name = "GameManager";

            UIManager uIManager = new UIManager();
            EntityManager.SpawnEntity(uIManager, new Vector2(), Vector2.One, this);

            ManaBar manaBar = new()
            {
                name = "ManaBar"
            };
            EntityManager.SpawnEntity(manaBar, Vector2.Zero, Vector2.One, uIManager);

            ManaComponent manaComponent = new(manaBar);

            AddComponent<ManaComponent>(manaComponent);


            CardManager cardManager = new()
            {
                manaComponent = manaComponent
            };
            CharacterManager characterManager = new CharacterManager(cardManager);

            AddComponent<CharacterManager>(characterManager);
            AddComponent<CardManager>(cardManager);

            AddComponent<FightManager>(new FightManager(cardManager, characterManager));
        }
    }
}