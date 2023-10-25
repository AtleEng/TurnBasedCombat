using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using Engine;

namespace Engine
{
    public class CardComponent : Component
    {
        public CardStats cardStats = new("?", 0, 0, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.All);
        public Sprite sprite;
        public CardComponent(Sprite sprite)
        {
            this.sprite = sprite;
        }
        public void OnUseCard()
        {

        }
    }
}