using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class CardManager : Component, IScript
    {
        public Dictionary<String, CardStats> allCards;
        List<CardStats> cardsInDrawPile = new();
        List<CardStats> cardsInDiscardPile = new();
        List<Card> cardsInHand = new();
        Vector2[] cardPositions = { new Vector2(-2, 0), new Vector2(-1, 0), new Vector2(0, 0), new Vector2(1, 0) };

        int selectedCard = 0;
        public override void Start()
        {
            allCards.Add("Dagger", new CardStats("Dagger", 0, 1, 0, 0, null, 0, 2, 0, null, CardStats.TargetType.Melee));
            cardsInDrawPile.Add(new CardStats("PosionDagger", 1, 2, 0, 0, null, 0, 2, 0, null, CardStats.TargetType.Melee));
            cardsInDrawPile.Add(new CardStats("Waterbolt", 2, 1, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.Range));

            cardsInDrawPile.Add(new CardStats("Mace", 3, 0, 0, 4, null, 0, 2, 0, null, CardStats.TargetType.Melee));
            cardsInDrawPile.Add(new CardStats("BallOfPoison", 4, 2, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.Range));
            cardsInDrawPile.Add(new CardStats("Rain", 5, 1, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.All));

            cardsInDrawPile.Add(new CardStats("HeavyAxe", 6, 2, 0, 0, null, 0, 4, 0, null, CardStats.TargetType.Melee));
            cardsInDrawPile.Add(new CardStats("ChainLighting", 7, 2, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.All));
            cardsInDrawPile.Add(new CardStats("Tsunami", 8, 4, 0, 0, null, 0, 2, 0, null, CardStats.TargetType.All));

            cardsInDrawPile.Add(new CardStats("BloodDagger", 9, 0, 1, 0, null, 0, 4, 0, null, CardStats.TargetType.Melee));
            cardsInDrawPile.Add(new CardStats("LightingStrike", 10, 1, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.Range));
            cardsInDrawPile.Add(new CardStats("Fireball", 11, 3, 0, 0, null, 0, 2, 0, null, CardStats.TargetType.Range));

            cardsInDrawPile.Add(new CardStats("Bow", 12, 1, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.Range));
            cardsInDrawPile.Add(new CardStats("Bersek", 13, 0, 1, 0, null, 0, 0, 0, null, CardStats.TargetType.Self));
            cardsInDrawPile.Add(new CardStats("BurnTheEarth", 14, 1, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.All));

            cardsInDrawPile.Add(new CardStats("Bomb", 14, 2, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.All));
            cardsInDrawPile.Add(new CardStats("Boots", 15, 1, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.Self));
            cardsInDrawPile.Add(new CardStats("Torch", 16, 1, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.Melee));

            cardsInDrawPile.Add(new CardStats("ArrowRain", 17, 2, 0, 0, null, 0, 2, 0, null, CardStats.TargetType.Range));
            cardsInDrawPile.Add(new CardStats("MagicRing", 18, 1, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.Self));
            cardsInDrawPile.Add(new CardStats("Shield", 19, 1, 0, 0, null, 0, 0, 1, null, CardStats.TargetType.Self));

            cardsInDrawPile.Add(new CardStats("Drinking", 20, 0, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.Self));
            cardsInDrawPile.Add(new CardStats("HolyProtection", 21, 0, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.Self));
            cardsInDrawPile.Add(new CardStats("LightArmor", 22, 2, 0, 0, null, 0, 0, 2, null, CardStats.TargetType.Self));

            cardsInDrawPile.Add(new CardStats("PartyTime", 23, 0, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.All));
            cardsInDrawPile.Add(new CardStats("ManaPotion", 24, 0, 0, 0, null, 1, 0, 0, null, CardStats.TargetType.Self));
            cardsInDrawPile.Add(new CardStats("HeavyArmor", 25, 3, 0, 0, null, 0, 0, 4, null, CardStats.TargetType.Self));
        }

        public override void Update(float delta)
        {
            Vector2 mPos = WorldSpace.GetVirtualMousePos();

            for (int i = 0; i < cardsInHand.Count; i++)
            {
                if (Raylib.CheckCollisionPointRec
                (mPos, new Rectangle(cardsInHand[i].worldTransform.position.X - cardsInHand[i].worldTransform.size.X / 2, cardsInHand[i].worldTransform.position.Y - cardsInHand[i].worldTransform.size.Y / 2,
                            cardsInHand[i].worldTransform.size.X, cardsInHand[i].worldTransform.size.Y))
                )
                {
                    //hovering

                    //klicked
                    if (Raylib.IsMouseButtonDown(0))
                    {
                        selectedCard = i;
                    }
                }
            }
        }
        public void OnDraw()
        {
            for (int i = 0; i < cardPositions.Length; i++)
            {
                if (cardsInDrawPile.Count < 0) { ShuffleDeck(); }

                Card card = new(cardsInDrawPile[0]);
                cardsInDrawPile.Remove(cardsInDrawPile[0]);

                cardsInHand.Add(card);

                EntityManager.SpawnEntity(card, cardPositions[i], Vector2.One, gameEntity);
            }
        }
        public void ShuffleDeck()
        {
            Random rand = new Random();
            int n = cardsInDiscardPile.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);
                CardStats temp = cardsInDiscardPile[i];
                cardsInDiscardPile[i] = cardsInDiscardPile[j];
                cardsInDiscardPile[j] = temp;
            }
            cardsInDrawPile = cardsInDiscardPile;
            cardsInDiscardPile.Clear();
        }
        public void OnDone()
        {
            for (int i = 0; i < cardsInHand.Count; i++)
            {
                cardsInDiscardPile.Add(cardsInHand[i].cardStats);

                EntityManager.DestroyEntity(cardsInHand[i]);
            }
            cardsInHand.Clear();
        }

    }
}