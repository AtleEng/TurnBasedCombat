using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreAnimation;

namespace Engine
{
    public class ManaBar : GameEntity
    {
        public List<ManaSprite> manaSprites = new();
        Vector2[] spritesPositions = { new Vector2(-6.5f, 1.5f), new Vector2(-5.5f, 1.5f), new Vector2(-4.5f, 1.5f), new Vector2(-3.5f, 1.5f) };

        public ManaBar()
        {
            Texture2D manaTexture = Raylib.LoadTexture(@"Project\Sprites\icons.png");
            for (int i = 0; i < spritesPositions.Length; i++)
            {
                ManaSprite manaSprite = new(manaTexture)
                {
                    name = "ManaSprite-" + i
                };
                manaSprites.Add(manaSprite);
                EntityManager.SpawnEntity(manaSprite, spritesPositions[i], Vector2.One, this);
            }
        }
    }
    public class ManaSprite : GameEntity
    {
        public Sprite sprite;
        public ManaSprite(Texture2D texture2D)
        {
            sprite = new()
            {
                spriteSheet = texture2D,
                spriteGrid = new Vector2(7, 2),
                layer = 10
            };
            AddComponent<Sprite>(sprite);
        }
    }
}