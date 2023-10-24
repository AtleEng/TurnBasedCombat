using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public abstract class Effect
    {
        public Character effectedCharacter;
        public int level = 0; //remove one level every turn
        public virtual void OnStartOfTurn()
        {

        }
        public virtual void OnEndOfTurn()
        {

        }
    }
}