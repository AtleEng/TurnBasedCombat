using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class FireBall : Spell
    {
        int dmg = 2;
        public override void OnUse(Character user, Character target)
        {
            target.healthComponent.TakeDMG(dmg, user.healthComponent);
        }

    }
}