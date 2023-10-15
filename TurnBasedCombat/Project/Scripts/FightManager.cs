using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class FightManager : Component, IScript
    {
        FightStates fightState = FightStates.beforeFight;

        Character player;
        Character enemy;

        Sprite sprite;
        public override void Start()
        {
            sprite = player.gameEntity.GetComponent<Sprite>();
        }
        public override void Update(float delta)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
            {
                sprite.FrameIndex++;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
            {
                sprite.FrameIndex--;
            }
            
            if (fightState == FightStates.beforeFight)
            {
                BeforeFightUppdate();
            }
            else if (fightState == FightStates.playerTurn)
            {
                PlayerTurnUppdate();
            }
            else if (fightState == FightStates.enemyTurn)
            {
                EnemyTurnUppdate();
            }
            else if (fightState == FightStates.endOfFight)
            {
                EndOfFightUppdate();
            }
        }
        void BeforeFightUppdate()
        {
            fightState = FightStates.playerTurn;
        }
        void PlayerTurnUppdate()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
            {
                player.Attack(0, player, enemy);

                fightState = FightStates.enemyTurn;
            }

        }
        void EnemyTurnUppdate()
        {
            System.Console.WriteLine("Enemy");
        }
        void EndOfFightUppdate()
        {

        }

        public void SetCharacters(Character player, Character enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }
        public enum FightStates
        {
            beforeFight, playerTurn, enemyTurn, endOfFight
        }
    }
}