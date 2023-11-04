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
            FightManager fightManager = new FightManager(cardManager, characterManager);
            AddComponent<FightManager>(fightManager);

            EntityManager.SpawnEntity(new QuitButton(), new Vector2(-6, -4.5f), new Vector2(2, 1));
            EntityManager.SpawnEntity(new DoneButton(fightManager), new Vector2(5, 3.5f), new Vector2(2, 1));
        }
    }
}