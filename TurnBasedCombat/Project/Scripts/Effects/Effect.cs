using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public abstract class Effect
    {
        public int duration = 0;
        public virtual void OnStartOfTurn()
        {

        }
        public virtual void OnEndOfTurn()
        {

        }
    }
}