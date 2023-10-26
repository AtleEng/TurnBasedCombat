using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class CardManager : Component, IScript
    {
        public Player player;
        public Dictionary<string, CardStats> allCards = new();
        List<CardStats> cardsInDrawpile = new();
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
            AddCard("ManaPotion", 25, 0, 0, 0, null, 2, 0, 0, null, CardStats.TargetType.Self);
            AddCard("HeavyArmor", 26, 3, 0, 0, null, 0, 0, 4, null, CardStats.TargetType.Self);

            cardsInDrawpile.Add(allCards["Dagger"]);
            cardsInDrawpile.Add(allCards["Dagger"]);
            cardsInDrawpile.Add(allCards["Mace"]);
            cardsInDrawpile.Add(allCards["ManaPotion"]);
            cardsInDrawpile.Add(allCards["ManaPotion"]);
            cardsInDrawpile.Add(allCards["Shield"]);
            cardsInDrawpile.Add(allCards["Shield"]);
            cardsInDrawpile.Add(allCards["Bow"]);
            cardsInDrawpile.Add(allCards["Fireball"]);
            cardsInDrawpile.Add(allCards["Fireball"]);

            SpawInCards();
            ShuffleDeck();
            DrawFullHand();
        }
        public override void Update(float delta)
        {
            Vector2 mPos = WorldSpace.GetVirtualMousePos();

            for (int i = 0; i < cardsInHand.Count; i++)
            {
                if (Raylib.CheckCollisionPointRec
                (mPos, new Rectangle(cardsInHand[i].worldTransform.position.X - cardsInHand[i].worldTransform.size.X / 2, cardsInHand[i].worldTransform.position.Y - cardsInHand[i].worldTransform.size.Y / 2,
                            cardsInHand[i].worldTransform.size.X, cardsInHand[i].worldTransform.size.Y))
                && cardsInHand[i].isActive)
                {
                    //hovering

                    //klicked
                    if (Raylib.IsMouseButtonDown(0))
                    {
                        selectedCard = i;

                        UseCard(i);
                    }
                }
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                DiscardHand();
                DrawFullHand();
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ONE))
            {
                DiscardCard(0);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_TWO))
            {
                DiscardCard(1);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_THREE))
            {
                DiscardCard(2);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_FOUR))
            {
                DiscardCard(3);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_D))
            {
                DrawACard(0);
            }
        }
        void SpawInCards()
        {
            for (int i = 0; i < cardPositions.Length; i++)
            {
                Card card = new Card(player)
                {
                    isActive = false,
                };
                cardsInHand.Add(card);
                EntityManager.SpawnEntity(card, cardPositions[i], new Vector2(2,2), cardHolder);
            }
        }

        //----------------------==CardLogic==----------------------
        public void UseCard(int i)
        {
            if (i >= cardsInHand.Count || !cardsInHand[i].isActive) { return; }//safety check
            if(!cardsInHand[i].cardComponent.CanUseCard()){return;}

            cardsInHand[i].cardComponent.UseCard();
            DiscardCard(i);
        }

        public void DrawACard(int i)
        {
            if (i >= cardsInHand.Count) { return; }

            if (cardsInHand[i].isActive)
            {
                DrawACard(i + 1);
                return;
            }

            if (cardsInDrawpile.Count <= 0)
            {
                ShuffleDeck();
            }

            CardStats drawnCardStats = cardsInDrawpile[0];
            cardsInDrawpile.RemoveAt(0);

            cardsInHand[i].cardComponent.cardStats = drawnCardStats;
            cardsInHand[i].name = "Card-" + drawnCardStats.nameOfCard;
            cardsInHand[i].isActive = true;
            cardsInHand[i].cardComponent.sprite.FrameIndex = cardsInHand[i].cardComponent.cardStats.cardSpriteIndex;

            Console.WriteLine($"Draw card: {cardsInHand[i].name} at: {i}");
        }

        public void DiscardCard(int i)
        {
            if (i >= cardsInHand.Count) { return; }
            if (!cardsInHand[i].isActive) { return; }

            cardsInDiscardPile.Add(cardsInHand[i].cardComponent.cardStats);
            cardsInHand[i].isActive = false;

            Console.WriteLine($"Discarding: {cardsInHand[i].name} at: {i}");
        }

        public void DrawFullHand()
        {
            Console.WriteLine("Drawing full hand");

            for (int i = 0; i < cardPositions.Length; i++)
            {
                if (!cardsInHand[i].isActive)
                {
                    DrawACard(i);
                }
            }
        }
        public void DiscardHand()
        {
            System.Console.WriteLine("Clear hand");
            for (int i = 0; i < cardsInHand.Count; i++)
            {
                if (cardsInHand[i].isActive) { DiscardCard(i); }
            }
        }
        public void ShuffleDeck()
        {
            // Combine cards from discardPile and drawPile
            List<CardStats> combinedDeck = new List<CardStats>(cardsInDrawpile);
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
            cardsInDrawpile = combinedDeck;
        }
        void AddCard(string nameOfCard,
        int cardSpriteIndex, int manaCost, int healthCost, int shieldCost, Effect? effectCost,
        int manaApply, int dmgApply, int shieldApply, Effect? effectApply, CardStats.TargetType targetType)
        {
            allCards.Add(nameOfCard, new CardStats(nameOfCard, cardSpriteIndex, manaCost, healthCost, shieldCost, effectCost, manaApply, dmgApply, shieldApply, effectApply, targetType));
        }

    }
}