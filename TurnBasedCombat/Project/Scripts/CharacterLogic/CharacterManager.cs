using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;
using CoreAnimation;

namespace Engine
{
    public class CharacterManager : Component, IScript
    {
        public CardManager cardManager;
        public Dictionary<int, CharacterStats> allCharacters = new();
        public GameEntity characterHolder = new();

        public Character[] characters = new Character[4];
        Vector2[] characterPosition = { new Vector2(-4, -1f), new Vector2(0, -1f), new Vector2(2, -1f), new Vector2(4, -1f) };

        public CharacterManager(CardManager cardManager)
        {
            this.cardManager = cardManager;
        }
        public override void Start()
        {
            characterHolder.name = "Characters";
            EntityManager.SpawnEntity(characterHolder);
            SpawInCharacterObject();

            AddCharacter(0, "Player", 39, 4, 0, new());

            AddCharacter(1, "Skeleton", 36, 3, 0, new List<EnemyBehaviour> { new Attack(1) });
            AddCharacter(2, "The drunk man", 33, 4, 2, new List<EnemyBehaviour> { new Attack(1) });
            AddCharacter(3, "Archer", 30, 2, 0, new List<EnemyBehaviour> { new Attack(2), new NoBehaviour() });
            AddCharacter(4, "Ninja", 27, 2, 0, new List<EnemyBehaviour> { new Attack(1), new Attack(2) });
            AddCharacter(5, "Hammer man", 24, 3, 0, new List<EnemyBehaviour> { new Attack(2), new NoBehaviour() });
            AddCharacter(6, "Heavy Knight", 21, 4, 3, new List<EnemyBehaviour> { new Attack(2), new Attack(1) });
            AddCharacter(7, "Warrior", 18, 3, 1, new List<EnemyBehaviour> { new Attack(2), new NoBehaviour() });
            AddCharacter(8, "The ?", 15, 5, 5, new List<EnemyBehaviour> { new Shielding(3), new SummonCharacter(1) });
            AddCharacter(9, "Healer", 12, 4, 0, new List<EnemyBehaviour> { new Attack(1), new Heal(2) });
            AddCharacter(10, "Electro wizard", 9, 2, 0, new List<EnemyBehaviour> { new Attack(2), new NoBehaviour() });
            AddCharacter(11, "Nature wizard", 6, 2, 0, new List<EnemyBehaviour> { new Attack(2), new NoBehaviour() });
            AddCharacter(12, "Water wizard", 3, 2, 0, new List<EnemyBehaviour> { new Attack(2), new NoBehaviour() });
            AddCharacter(13, "Fire wizard", 0, 2, 0, new List<EnemyBehaviour> { new Attack(2), new NoBehaviour() });
        }

        public override void Update(float delta)
        {
            if (cardManager.selectedCard >= 0) { SelectTargetLogic(); }
        }
        List<Character> FindPotensialTargets(CardStats.TargetType targetType)
        {
            List<Character> potensialTargets = new();

            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].targetIcon.isActive = false;
                if (characters[i].isActive == true)
                {
                    if (targetType == CardStats.TargetType.Melee)
                    {
                        if (i > 0)
                        {
                            potensialTargets.Add(characters[i]);
                            return potensialTargets;
                        }
                    }
                    else if (targetType == CardStats.TargetType.Self)
                    {
                        if (i == 0)
                        {
                            potensialTargets.Add(characters[i]);
                            return potensialTargets;
                        }
                    }
                    else if (targetType == CardStats.TargetType.Range || targetType == CardStats.TargetType.All)
                    {
                        if (i > 0)
                        {
                            potensialTargets.Add(characters[i]);
                        }
                    }
                }
            }
            return potensialTargets;
        }
        void SelectTargetLogic()
        {
            CardStats.TargetType targetType = cardManager.cardsInHand[cardManager.selectedCard].cardComponent.cardStats.targetType;

            Vector2 mPos = WorldSpace.GetVirtualMousePos();
            List<Character> potensialTargets = FindPotensialTargets(targetType);
            bool isDone = false;

            for (int i = 0; i < potensialTargets.Count; i++)
            {
                if (Raylib.CheckCollisionPointRec
                (mPos, new Rectangle(potensialTargets[i].worldTransform.position.X - potensialTargets[i].worldTransform.size.X / 2,
                potensialTargets[i].worldTransform.position.Y - potensialTargets[i].worldTransform.size.Y / 2,
                potensialTargets[i].worldTransform.size.X, potensialTargets[i].worldTransform.size.Y)))
                {
                    if (targetType == CardStats.TargetType.All)
                    {
                        for (int j = 0; j < potensialTargets.Count; j++)
                        {
                            potensialTargets[j].targetIcon.isActive = true;
                        }
                    }
                    else
                    {
                        potensialTargets[i].targetIcon.isActive = true;
                    }
                    //klicked
                    if (Raylib.IsMouseButtonDown(0))
                    {
                        if (targetType == CardStats.TargetType.All && !isDone)
                        {
                            cardManager.UseCard(cardManager.selectedCard, characters[0], potensialTargets);
                            isDone = true;
                        }
                        else
                        {
                            List<Character> targets = new()
                            {
                                potensialTargets[i]
                            };
                            characters[0].animator.PlayAnimation("Attack");
                            cardManager.UseCard(cardManager.selectedCard, characters[0], targets);
                        }
                        for (int j = 0; j < potensialTargets.Count; j++)
                        {
                            potensialTargets[j].targetIcon.isActive = false;
                        }
                    }
                }
            }
        }
        public bool SetCharacter(int posIndex, int characterIndex)
        {
            if (posIndex >= characters.Length) { return false; }
            if (characters[posIndex].isActive)
            {
                System.Console.WriteLine($"Its allready a character at {posIndex}");
                return false;
            }

            if (characterIndex < 0) { characters[posIndex].isActive = false; }
            characters[posIndex].effectComponent.RemoveEffect(10);
            CharacterStats currentStats = allCharacters[characterIndex];
            characters[posIndex].characterStats = currentStats;

            //main
            characters[posIndex].name = currentStats.nameOfCharacter;
            characters[posIndex].isActive = true;

            //health and shield
            characters[posIndex].healthComponent.maxHealth = currentStats.startHealth;
            characters[posIndex].healthComponent.currentHealth = currentStats.startHealth;
            characters[posIndex].healthComponent.currentShield = currentStats.startShield;

            characters[posIndex].healthComponent.UpdateUI();

            //Sprite
            int spriteFrame = currentStats.characterSpriteIndex;
            characters[posIndex].sprite.FrameIndex = spriteFrame;
            if (posIndex != 0) { characters[posIndex].sprite.isFlipedX = true; }

            //animation
            Animation animation = new(new int[] { spriteFrame, spriteFrame + 1, spriteFrame + 2 }, 0.2f, false);
            characters[posIndex].animator.animations.Clear();
            characters[posIndex].animator.AddAnimation("Attack", animation);


            Console.WriteLine($"Creating character: {characters[posIndex].name} at: {posIndex}");
            return true;
        }
        public void SpawInCharacterObject()
        {
            for (int i = 0; i < characters.Length; i++)
            {
                Character character = new Character()
                {
                    isActive = false,
                };
                characters[i] = character;
                EntityManager.SpawnEntity(character, characterPosition[i], new Vector2(2, 2), characterHolder);
            }
        }
        void AddCharacter(int index, string name, int spriteIndex, int startHealth, int startShield, List<EnemyBehaviour> enemyBehaviour)
        {
            allCharacters.Add(index, new CharacterStats(name, spriteIndex, startHealth, startShield, enemyBehaviour));
        }
    }
}