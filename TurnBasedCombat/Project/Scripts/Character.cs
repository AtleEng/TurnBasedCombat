using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class Character : Component, IScript
    {
        public HealthComponent healthComponent;
        public AttackLogic attackLogic;
        /*
                public Character(HealthComponent healthComponent, AttackLogic attackLogic)
                {
                    this.attackLogic = attackLogic;
                    this.healthComponent = healthComponent;
                }
            }
            */
    }
}