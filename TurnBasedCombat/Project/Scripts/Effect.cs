using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Engine
{
    public abstract class Effect
    {
        public int level = 1;
        public int imageIndex = 1;

    }
    public class FireEffect : Effect
    {
        public FireEffect(int level)
        {
            this.level = level;
            imageIndex = 8;
        }
    }
    public class WaterEffect : Effect
    {
        public WaterEffect(int level)
        {
            this.level = level;
            imageIndex = 9;
        }
    }
    public class StunEffect : Effect
    {
        public StunEffect(int level)
        {
            this.level = level;
            imageIndex = 10;
        }
    }
    public class WeaknessEffect : Effect
    {
        public WeaknessEffect(int level)
        {
            this.level = level;
            imageIndex = 11;
        }
    }
    public class DrunkEffect : Effect
    {
        public DrunkEffect(int level)
        {
            this.level = level;
            imageIndex = 12;
        }
    }
    public class DodgeEffect : Effect
    {
        public DodgeEffect(int level)
        {
            this.level = level;
            imageIndex = 13;
        }
    }
    public class RageEffect : Effect
    {
        public RageEffect(int level)
        {
            this.level = level;
            imageIndex = 6;
        }
    }
    public class ManaEffect : Effect
    {
        public ManaEffect(int level)
        {
            this.level = level;
            imageIndex = 5;
        }
    }
}