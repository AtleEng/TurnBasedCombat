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
        public Dictionary<string, CharacterStats> allCharacters = new();
        GameEntity characterHolder = new();

        Character[] characters = new Character[4];
        Vector2[] characterPosition = { new Vector2(-4, -1), new Vector2(0, -1), new Vector2(2, -1), new Vector2(4, -1) };

        int selectedCharacter = -1;

        public CharacterManager(CardManager cardManager)
        {
            this.cardManager = cardManager;
        }
        public override void Start()
        {
            characterHolder.name = "Characters";
            EntityManager.SpawnEntity(characterHolder);

            AddCharacter("Player", 39, 4, 0, new None());

            AddCharacter("Skeleton", 0, 3, 0, new None());

            SpawInCharacterObjects();
        }
        public override void Update(float delta)
        {
            if (cardManager.selectedCard >= 0) { SelectTargetLogic(); }
        }
        void SelectTargetLogic()
        {
            List<Character> potensialTargets = new();
            Vector2 mPos = WorldSpace.GetVirtualMousePos();

            CardStats.TargetType targetType = cardManager.cardsInHand[cardManager.selectedCard].cardComponent.cardStats.targetType;

            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i].isActive == true)
                {
                    if (targetType == CardStats.TargetType.Melee)
                    {
                        if (i > 0 && potensialTargets.Count == 0)
                        {
                            potensialTargets.Add(characters[i]);
                        }
                    }
                    else if (targetType == CardStats.TargetType.Self)
                    {
                        if (i == 0)
                        {
                            potensialTargets.Add(characters[i]);
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
            for (int i = 0; i < potensialTargets.Count; i++)
            {
                if (Raylib.CheckCollisionPointRec
                (mPos, new Rectangle(potensialTargets[i].worldTransform.position.X - potensialTargets[i].worldTransform.size.X / 2,
                potensialTargets[i].worldTransform.position.Y - potensialTargets[i].worldTransform.size.Y / 2,
                potensialTargets[i].worldTransform.size.X, potensialTargets[i].worldTransform.size.Y)))
                {
                    System.Console.WriteLine("Valid target");
                    //klicked
                    if (Raylib.IsMouseButtonDown(0))
                    {
                        System.Console.WriteLine("Attack");
                        if (targetType == CardStats.TargetType.All)
                        {
                            cardManager.UseCard(cardManager.selectedCard, characters[0], potensialTargets);
                        }
                        else
                        {
                            List<Character> targets = new()
                            {
                                potensialTargets[i]
                            };
                            cardManager.UseCard(cardManager.selectedCard, characters[0], targets);
                        }

                    }
                }


            }
        }
        void UpdateCharacterStats(int i)
        {
            //sprite
            int spriteFrame = characters[i].characterStats.characterSpriteIndex;
            characters[i].sprite.FrameIndex = spriteFrame;
            //animation
            Animation animation = new(new int[] { spriteFrame, spriteFrame + 1, spriteFrame + 2 }, 0.2f, false);
            characters[i].animator.AddAnimation("Attack", animation);

        }
        void SpawInCharacterObjects()
        {
            for (int i = 0; i < characterPosition.Length; i++)
            {
                Character character = new Character();
                if (i == 0)
                {
                    character.name = "Player";
                    character.characterStats = allCharacters["Player"];
                }
                else
                {
                    character.name = $"Enemy{i}";
                    character.sprite.isFlipedX = true;
                    //character.animator.gameEntity.isActive = false;
                }
                characters[i] = character;
                EntityManager.SpawnEntity(character, characterPosition[i], new Vector2(2, 2), characterHolder);
                UpdateCharacterStats(i);
            }
        }
        void AddCharacter(String name, int spriteIndex, int startHealth, int startShield, EnemyBehaviour enemyBehaviour)
        {
            allCharacters.Add(name, new CharacterStats(name, spriteIndex, startHealth, startShield, enemyBehaviour));
        }
    }
}