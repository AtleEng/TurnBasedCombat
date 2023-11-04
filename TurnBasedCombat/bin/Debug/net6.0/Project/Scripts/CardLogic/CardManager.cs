using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class CardManager : Component, IScript
    {
        public ManaComponent manaComponent;
        public Dictionary<string, CardStats> allCards = new();
        List<CardStats> cardsInDrawpile = new();
        List<CardStats> cardsInDiscardPile = new();
        public Card[] cardsInHand = new Card[4];

        Vector2[] cardPositions = { new Vector2(-4, 3.25f), new Vector2(-2, 3.25f), new Vector2(0, 3.25f), new Vector2(2, 3.25f) };
        GameEntity cardHolder = new();
        public int selectedCard = -1; //if negative no card is selected

        public override void Start()
        {
            cardHolder.name = "Cards";

            EntityManager.SpawnEntity(cardHolder);

            AddCard("Dagger", 0, 1, 0, 0, null, 0, 2, 0, null, CardStats.TargetType.Melee);
            AddCard("PosionDagger", 1, 2, 0, 0, null, 0, 2, 0, new WeaknessEffect(2), CardStats.TargetType.Melee);
            AddCard("Waterbolt", 2, 1, 0, 0, null, 0, 1, 0, new WaterEffect(1), CardStats.TargetType.Range);
            AddCard("Mace", 3, 0, 0, 4, null, 0, 2, 0, null, CardStats.TargetType.Melee);
            AddCard("BallOfPoison", 4, 2, 0, 0, null, 0, 1, 0, new WeaknessEffect(1), CardStats.TargetType.Range);
            AddCard("Rain", 5, 0, 0, 0, new WaterEffect(1), 0, 0, 0, new WaterEffect(1), CardStats.TargetType.All);
            AddCard("HeavyAxe", 6, 2, 0, 0, null, 0, 4, 0, null, CardStats.TargetType.Melee);
            AddCard("ChainLighting", 7, 2, 0, 0, null, 0, 1, 0, new StunEffect(1), CardStats.TargetType.All);
            AddCard("Tsunami", 8, 4, 0, 0, null, 0, 3, 0, new WaterEffect(1), CardStats.TargetType.All);
            AddCard("BloodDagger", 9, 0, 1, 0, null, 0, 4, 0, null, CardStats.TargetType.Melee);
            AddCard("LightingStrike", 10, 1, 0, 0, null, 0, 1, 0, new StunEffect(1), CardStats.TargetType.Range);
            AddCard("Fireball", 11, 3, 0, 0, null, 0, 2, 0, new FireEffect(2), CardStats.TargetType.Range);
            AddCard("Bow", 12, 1, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.Range);
            AddCard("Bersek", 13, 0, 1, 0, null, 0, 0, 0, new RageEffect(3), CardStats.TargetType.Self);
            AddCard("BurnTheEarth", 14, 1, 0, 0, null, 0, 0, 0, new FireEffect(1), CardStats.TargetType.All);
            AddCard("Bomb", 15, 2, 0, 0, null, 0, 1, 0, null, CardStats.TargetType.All);
            AddCard("Boots", 16, 1, 0, 0, null, 0, 0, 0, new DodgeEffect(2), CardStats.TargetType.Self);
            AddCard("Torch", 17, 1, 0, 0, null, 0, 1, 0, new FireEffect(2), CardStats.TargetType.Melee);
            AddCard("ArrowRain", 18, 2, 0, 0, null, 0, 2, 0, null, CardStats.TargetType.Range);
            AddCard("MagicRing", 19, 1, 0, 0, null, 0, 0, 0, new ManaEffect(2), CardStats.TargetType.Self);
            AddCard("Shield", 20, 1, 0, 0, null, 0, 0, 1, null, CardStats.TargetType.Self);
            AddCard("Drinking", 21, 0, 0, 0, null, 0, 0, 0, new DrunkEffect(2), CardStats.TargetType.Self);
            AddCard("HolyProtection", 22, 2, 0, 0, null, 0, 0, 2, new DodgeEffect(1), CardStats.TargetType.Self);
            AddCard("LightArmor", 23, 2, 0, 0, null, 0, 0, 2, null, CardStats.TargetType.Self);
            AddCard("PartyTime", 24, 0, 0, 0, new DrunkEffect(2), 0, 0, 0, new DrunkEffect(2), CardStats.TargetType.All);
            AddCard("ManaPotion", 25, 0, 0, 0, null, 2, 0, 0, null, CardStats.TargetType.Self);
            AddCard("HeavyArmor", 26, 3, 0, 0, null, 0, 0, 4, null, CardStats.TargetType.Self);

            foreach (CardStats card in allCards.Values)
            {
                cardsInDiscardPile.Add(card);
            }

            SpawInCardObjects();
        }
        public override void Update(float delta)
        {
            SelectCardLogic();
        }
        void SelectCardLogic()
        {
            Vector2 mPos = WorldSpace.GetVirtualMousePos();

            if (Raylib.IsMouseButtonDown(0))
            {
                selectedCard = -1;
            }
            for (int i = 0; i < cardsInHand.Length; i++)
            {
                cardsInHand[i].localTransform.position = cardPositions[i];//reset hovering

                if (Raylib.CheckCollisionPointRec
                (mPos, new Rectangle(cardsInHand[i].worldTransform.position.X - cardsInHand[i].worldTransform.size.X / 2, cardsInHand[i].worldTransform.position.Y - cardsInHand[i].worldTransform.size.Y / 2,
                            cardsInHand[i].worldTransform.size.X, cardsInHand[i].worldTransform.size.Y))
                && cardsInHand[i].isActive)
                {
                    if (selectedCard < 0)
                    {
                        //hovering
                        cardsInHand[i].localTransform.position.Y = cardPositions[i].Y - 0.125f;
                    }
                    //klicked
                    if (Raylib.IsMouseButtonDown(0))
                    {
                        selectedCard = i;
                    }
                }
                if (selectedCard >= 0)
                {
                    cardsInHand[selectedCard].localTransform.position.Y = cardPositions[selectedCard].Y - 0.125f;
                }
            }
        }

        void SpawInCardObjects()
        {
            for (int i = 0; i < cardPositions.Length; i++)
            {
                Card card = new Card()
                {
                    isActive = false,
                };
                cardsInHand[i] = card;
                EntityManager.SpawnEntity(card, cardPositions[i], new Vector2(2, 2), cardHolder);
            }
        }

        //----------------------==CardLogic==----------------------
        public void UseCard(int i, Character user, List<Character> target)
        {
            if (i >= cardsInHand.Length || !cardsInHand[i].isActive) { return; }//safety check
            if (!cardsInHand[i].cardComponent.CanUseCard(manaComponent)) { return; }

            cardsInHand[i].cardComponent.UseCard(user, target, manaComponent);
            DiscardCard(i);
        }

        public void DrawACard(int i)
        {
            if (i >= cardsInHand.Length) { return; }

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
            cardsInHand[i].sprite.FrameIndex = cardsInHand[i].cardComponent.cardStats.cardSpriteIndex;

            Console.WriteLine($"Draw card: {cardsInHand[i].name} at: {i}");
        }

        public void DiscardCard(int i)
        {
            if (i >= cardsInHand.Length) { return; }
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
            for (int i = 0; i < cardsInHand.Length; i++)
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