using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class BasicMonster : GameEntity
    {
        public Character character = new();
        public override void OnInnit()
        {

            AttackLogic attackLogic = new AttackLogic();
            attackLogic.spells.Add(new FireBall());
            attackLogic.maxMana = 100;
            attackLogic.currentMana = 100;

            AddComponent<AttackLogic>(attackLogic);

            AddComponent<HealthComponent>(new HealthComponent());

            AddComponent<Sprite>(new Sprite());


            transform.position.X = 5;
            transform.size = new Vector2(8, 8);
        }
    }
}