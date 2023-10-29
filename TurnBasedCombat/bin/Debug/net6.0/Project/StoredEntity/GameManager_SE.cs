using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Engine
{
    public class GameManager : GameEntity
    {
        public override void OnInnit()
        {
            name = "GameManager";
            EntityManager.SpawnEntity(new UIManager(), new Vector2(), Vector2.One, this);

            ManaBar manaBar = new()
            {
                name = "ManaBar"
            };
            EntityManager.SpawnEntity(manaBar, Vector2.Zero, Vector2.One, this);

            ManaComponent manaComponent = new(manaBar);

            AddComponent<ManaComponent>(manaComponent);

            CardManager cardManager = new()
            {
                manaComponent = manaComponent
            };
            AddComponent<CharacterManager>(new CharacterManager(cardManager));
            AddComponent<CardManager>(cardManager);



        }
    }
}