using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Engine
{
    public class GameManager : GameEntity
    {
        Player player = new();
        public override void OnInnit()
        {
            name = "GameManager";

            BasicMonster basicMonster = new BasicMonster();

            EntityManager.SpawnEntity(player, new Vector2(-2, 1));

            EntityManager.SpawnEntity(basicMonster, new Vector2(2, 1));

            FightManager fightManager = new(player.character, basicMonster.character);
            AddComponent<FightManager>(fightManager);
        }
    }
}