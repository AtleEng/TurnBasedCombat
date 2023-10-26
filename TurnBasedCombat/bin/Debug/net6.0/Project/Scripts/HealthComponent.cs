using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class HealthComponent : Component
    {
        public int currentHealth = 4;
        public int maxHealth = 4;
        public int currentShield = 0;
        public HealthBar healthBar;

        public HealthComponent(HealthBar healthBar)
        {
            this.healthBar = healthBar;
        }
        public override string PrintStats()
        {
            return $"Health: {currentHealth}/{maxHealth} Shield: {currentShield}";
        }
        public void TakeDMG(int dmg, HealthComponent attacker)
        {
            currentShield -= dmg;
            if (currentShield < 0)
            {
                currentHealth += currentShield;
            }
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die(attacker);
            }
            UppdateHealthUI();
        }
        public void Heal(int healAmount)
        {
            currentHealth += healAmount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            UppdateHealthUI();
        }
        public void AddShield(int shieldAmount)
        {
            currentShield += shieldAmount;
            if (currentShield > maxHealth)
            {
                currentShield = maxHealth;
            }
            UppdateHealthUI();
        }
        public void Die(HealthComponent killer)
        {
            Console.WriteLine($"{gameEntity.name} was killed by {killer.gameEntity.name}");
        }

        void UppdateHealthUI()
        {
            for (int i = 0; i < maxHealth; i++)
            {
                healthBar.healthSprites[i].sprite.FrameIndex = 2;
            }
            for (int i = 0; i < currentHealth; i++)
            {
                healthBar.healthSprites[i].sprite.FrameIndex = 0;
            }
        }
    }
}