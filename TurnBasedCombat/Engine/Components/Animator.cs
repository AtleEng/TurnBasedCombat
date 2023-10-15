using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;
using Engine;

namespace Engine
{
    public class Animator : Component
    {
        public Sprite sprite;
        public float timeBtwFrames = 0.1f;
        public bool isLooping;
        public bool isDoneWithAnimation;

        public Animator(Sprite sprite)
        {
            this.sprite = sprite;
        }
    }
}