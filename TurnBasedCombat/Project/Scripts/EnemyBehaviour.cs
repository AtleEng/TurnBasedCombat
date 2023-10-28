using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public abstract class EnemyBehaviour
    {
        public virtual void Action(Character character)
        {
            character.animator.PlayAnimation("Attack");
        }
    }
    public class None : EnemyBehaviour
    {

    }
}