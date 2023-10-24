using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class CardManager : Component, IScript
    {
        public Dictionary<String, CardStats> allCards = new();
        List<CardStats> cardsInDrawPile = new();
        List<CardStats> cardsInDiscardPile = new();
        List<Card> cardsInHand = new();
        Vector2[] cardPositions = { new Vector2(-4, 3.25f), new Vector2(-2, 3.25f), new Vector2(0, 3.25f), new Vector2(2, 3.25f) };

        GameEntity cardHolder = new();

        int selectedCard = 0;
        public override void Start()
        {
            cardHolder.name = "Cards";
            EntityManager.SpawnEntity(cardHolder);

            AddCard("Dagger", 0, 1, 0, 0, null, 0, 2, 0, null, CardStats.TargetType.Melee);
            AddCard("PosionDagger", 1, 2, 0, 0, null, 0, 2, 0, null, CardStats.TargetType.Melee);
            AddCard("Waterbolt", 2, 1, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.Range);
            AddCard("Mace", 3, 0, 0, 4, null, 0, 2, 0, null, CardStats.TargetType.Melee);
            AddCard("BallOfPoison", 4, 2, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.Range);
            AddCard("Rain", 5, 1, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.All);
            AddCard("HeavyAxe", 6, 2, 0, 0, null, 0, 4, 0, null, CardStats.TargetType.Melee);
            AddCard("ChainLighting", 7, 2, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.All);
            AddCard("Tsunami", 8, 4, 0, 0, null, 0, 2, 0, null, CardStats.TargetType.All);
            AddCard("BloodDagger", 9, 0, 1, 0, null, 0, 4, 0, null, CardStats.TargetType.Melee);
            AddCard("LightingStrike", 10, 1, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.Range);
            AddCard("Fireball", 11, 3, 0, 0, null, 0, 2, 0, null, CardStats.TargetType.Range);
            AddCard("Bow", 12, 1, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.Range);
            AddCard("Bersek", 13, 0, 1, 0, null, 0, 0, 0, null, CardStats.TargetType.Self);
            AddCard("BurnTheEarth", 14, 1, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.All);
            AddCard("Bomb", 15, 2, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.All);
            AddCard("Boots", 16, 1, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.Self);
            AddCard("Torch", 17, 1, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.Melee);
            AddCard("ArrowRain", 18, 2, 0, 0, null, 0, 2, 0, null, CardStats.TargetType.Range);
            AddCard("MagicRing", 19, 1, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.Self);
            AddCard("Shield", 20, 1, 0, 0, null, 0, 0, 1, null, CardStats.TargetType.Self);
            AddCard("Drinking", 21, 0, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.Self);
            AddCard("HolyProtection", 22, 0, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.Self);
            AddCard("LightArmor", 23, 2, 0, 0, null, 0, 0, 2, null, CardStats.TargetType.Self);
            AddCard("PartyTime", 24, 0, 0, 0, null, 0, 0, 0, null, CardStats.TargetType.All);
            AddCard("ManaPotion", 25, 0, 0, 0, null, 1, 0, 0, null, CardStats.TargetType.Self);
            AddCard("HeavyArmor", 26, 3, 0, 0, null, 0, 0, 4, null, CardStats.TargetType.Self);

            cardsInDrawPile.Add(allCards["Dagger"]);
            cardsInDrawPile.Add(allCards["Dagger"]);
            cardsInDrawPile.Add(allCards["Mace"]);
            cardsInDrawPile.Add(allCards["ManaPotion"]);
            cardsInDrawPile.Add(allCards["ManaPotion"]);
            cardsInDrawPile.Add(allCards["Shield"]);
            cardsInDrawPile.Add(allCards["Shield"]);
            cardsInDrawPile.Add(allCards["Bow"]);
            cardsInDrawPile.Add(allCards["Fireball"]);
            cardsInDrawPile.Add(allCards["Fireball"]);

            ShuffleDeck();
            OnDraw();
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

                EntityManager.SpawnEntity(card, cardPositions[i], Vector2.One, cardHolder);
            }
        }
        public void ShuffleDeck()
        {
            // Combine cards from discardPile and drawPile
            List<CardStats> combinedDeck = new List<CardStats>(cardsInDrawPile);
            combinedDeck.AddRange(cardsInDiscardPile);

            Random rand = new Random();
            int n = combinedDeck.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);
                CardStats temp = combinedDeck[i];
                combinedDeck[i] = combinedDeck[j];
                combinedDeck[j] = temp;
            }

            // Clear discardPile
            cardsInDiscardPile.Clear();

            // Assign the shuffled combined deck back to drawPile
            cardsInDrawPile = combinedDeck;
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

        void AddCard(string nameOfCard,
        int cardSpriteIndex, int manaCost, int healthCost, int shieldCost, Effect? effectCost,
        int manaApply, int dmgApply, int shieldApply, Effect? effectApply, CardStats.TargetType targetType)
        {
            allCards.Add(nameOfCard, new CardStats(nameOfCard, cardSpriteIndex, manaCost, healthCost, shieldCost, effectCost, manaApply, dmgApply, shieldApply, effectApply, targetType));
        }

    }
}