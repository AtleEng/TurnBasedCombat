using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Engine
{
    public class GameManager : GameEntity
    {
        UIManager uiManager = new();
        Player player = new();
        public override void OnInnit()
        {
            name = "GameManager";
            EntityManager.SpawnEntity(uiManager, new Vector2(), Vector2.One, this);

            Enemy enemy = new Enemy();

            EntityManager.SpawnEntity(player, new Vector2(-4, -1), new Vector2(2, 2));

            EntityManager.SpawnEntity(enemy, new Vector2(2, -1), new Vector2(2, 2));

            CardManager cardManager = new()
            {
                player = player
            };
            AddComponent<CardManager>(cardManager);
        }
    }
}