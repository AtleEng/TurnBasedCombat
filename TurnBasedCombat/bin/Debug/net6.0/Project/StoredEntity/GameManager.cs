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
            EntityManager.SpawnEntity(new UIManager(), new Vector2(), Vector2.One, this);

            BasicMonster basicMonster = new BasicMonster();

            EntityManager.SpawnEntity(player, new Vector2(-4, -1), new Vector2(2, 2));

            EntityManager.SpawnEntity(basicMonster, new Vector2(2, -1), new Vector2(2, 2));

            FightController fightController = new(player.character, basicMonster.character);
            AddComponent<FightController>(fightController);
        }
    }
}