using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class StatsDisplay : GameEntity
    {
        public Character character;
        public Sprite sprite;
        public StatsDisplay(Character character, Sprite sprite)
        {
            this.character = character;
            this.sprite = sprite;
        }
        public override void OnInnit()
        {

        }
    }
    public class Stat
    {
        int currentValue;
        int maxValue;
    }
}