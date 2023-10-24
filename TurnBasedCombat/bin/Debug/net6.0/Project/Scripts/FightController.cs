using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class FightController : Component, IScript
    {
        FightStates fightState = FightStates.beforeFight;

        Character player;
        Character enemy;

        public FightController(Character player, Character enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public override void Start()
        {

        }
        public override void Update(float delta)
        {
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
            else if (fightState == FightStates.wonFight)
            {
                WinFightUppdate();
            }
            else if (fightState == FightStates.loseFight)
            {
                LoseFightUppdate();
            }
        }
        void BeforeFightUppdate()
        {
            fightState = FightStates.playerTurn;
        }
        void PlayerTurnUppdate()
        {
            if (player.healthComponent.currentHealth <= 0)
            {
                fightState = FightStates.loseFight;
                return;
            }
            if (!player.hasAttacked)
            {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
                {
                    //player.Attack(0, enemy, player);
                    player.animator.PlayAnimation("Attack");
                    player.hasAttacked = true;
                }
            }
            else
            {
                if (!player.animator.isPlaying)
                {
                    fightState = FightStates.enemyTurn;
                    enemy.hasAttacked = false;
                }
            }
        }
        void EnemyTurnUppdate()
        {
            if (enemy.healthComponent.currentHealth <= 0)
            {
                fightState = FightStates.wonFight;
                return;
            }
            if (!enemy.hasAttacked)
            {
                System.Console.WriteLine("EnemyTurn:");

                //enemy.Attack(0, player, enemy);
                enemy.animator.PlayAnimation("Attack");
                enemy.hasAttacked = true;

            }
            else
            {
                if (!enemy.animator.isPlaying)
                {
                    fightState = FightStates.playerTurn;
                    player.hasAttacked = false;
                }
            }
        }
        void WinFightUppdate()
        {
            System.Console.WriteLine("Won");
        }
        void LoseFightUppdate()
        {
            System.Console.WriteLine("Lost");
        }
        public enum FightStates
        {
            beforeFight, playerTurn, enemyTurn, wonFight, loseFight
        }
    }
}