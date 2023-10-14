using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class GameManager : GameEntity
    {
        Player player = new();
        public override void OnInnit()
        {
            FightManager fightManager = new();
            fightManager.SetCharacters(player.character, new BasicMonster().character);
            AddComponent<FightManager>(fightManager);

            EntityManager.SpawnEntity(new Player(), Vector2.Zero);
        }
    }
}