using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;
using CoreAnimation;

namespace Engine
{
    public class CardComponent : Component, IScript
    {
        public int manaCost;
        public int healthCost;
        public int shieldCost;
        public Effect? effectCost;

        public int manaApply;
        public int dmgApply;
        public int shieldApply;
        public Effect? effectApply;
        public override string PrintStats()
        {
            return $"Card";
        }
        public override void Update(float delta)
        {
            Vector2 mPos = WorldSpace.GetVirtualMousePos();

            if (Raylib.CheckCollisionPointRec(mPos,
            new Rectangle(gameEntity.worldTransform.position.X - gameEntity.worldTransform.size.X / 2, gameEntity.worldTransform.position.Y - gameEntity.worldTransform.size.Y / 2,
            gameEntity.worldTransform.size.X, gameEntity.worldTransform.size.Y)))
            {
                System.Console.WriteLine("Hovering");
                if (Raylib.IsMouseButtonDown(0))
                {
                    System.Console.WriteLine("Use card");
                }
            }
        }

    }
}