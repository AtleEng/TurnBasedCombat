using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;
using CoreAnimation;

namespace Engine
{
    public class Character : Component, IScript
    {
        public HealthComponent healthComponent;
        public AnimatorController animator;

        public bool hasAttacked;

        public Character(HealthComponent healthComponent, AnimatorController animator)
        {
            this.healthComponent = healthComponent;
            this.animator = animator;
        }
    }
}