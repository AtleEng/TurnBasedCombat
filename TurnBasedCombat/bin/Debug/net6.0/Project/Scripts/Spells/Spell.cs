using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public abstract class Spell
    {
        int manaCost;
        public virtual void OnUse(Character user, Character target) { }
    }
}