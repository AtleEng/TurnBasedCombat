using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class EffectComponent : Component
    {
        public Effect currentEffect;
        ImageDisplay imageDisplay = new();
        public override void Start()
        {
            EntityManager.SpawnEntity(imageDisplay, new Vector2(-0.75f, -1f), new Vector2(0.5f, 0.5f), gameEntity);
            imageDisplay.isActive = false;
        }
        public void ApplyEffect(Effect effect)
        {
            currentEffect = effect;
            UpdateUI();
        }
        public void RemoveEffect(int amount)
        {
            if (currentEffect == null) { return; }
            currentEffect.level -= amount;
            if (currentEffect.level <= 0) { currentEffect = null; }
            UpdateUI();
        }

        public float CalculateDMGModifier(Character target, Character user)
        {
            Random random = new Random();
            int hitChance = 100;
            float extraAttack = 1;
            if (target.effectComponent.currentEffect != null)
            {
                if (target.effectComponent.currentEffect.GetType() == typeof(DodgeEffect))
                {
                    hitChance /= 2;
                }
            }
            if (user.effectComponent.currentEffect != null)
            {
                if (user.effectComponent.currentEffect.GetType() == typeof(DrunkEffect))
                {
                    hitChance /= 2;
                    extraAttack *= 2;
                }
                if (user.effectComponent.currentEffect.GetType() == typeof(RageEffect))
                {
                    extraAttack *= 1.5f;
                }
                if (user.effectComponent.currentEffect.GetType() == typeof(WeaknessEffect))
                {
                    extraAttack *= 0.5f;
                }
            }
            if (random.Next(100) > hitChance) { extraAttack = 0f; }
            return extraAttack;
        }

        public void UpdateUI()
        {
            if (currentEffect == null)
            {
                imageDisplay.isActive = false;
            }
            else
            {
                imageDisplay.isActive = true;
                imageDisplay.SetNumber(currentEffect.imageIndex);
            }
        }
    }
}