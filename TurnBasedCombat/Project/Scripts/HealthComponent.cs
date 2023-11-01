using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class HealthComponent : Component
    {
        public int currentHealth;
        public int maxHealth;
        public int currentShield;
        public HealthBar healthBar;

        public HealthComponent(HealthBar healthBar)
        {
            this.healthBar = healthBar;
        }
        public override string PrintStats()
        {
            return $"Health: {currentHealth}/{maxHealth} Shield: {currentShield}";
        }
        public void TakeDMG(int damage, HealthComponent attacker)
        {
            if (currentShield > 0)
            {
                if (damage < currentShield)
                {
                    currentShield -= damage;
                }
                else
                {
                    currentHealth += currentShield - damage; // Add the remainder of the damage to health
                    currentShield = 0; // Shield is depleted
                }
            }
            else
            {
                currentHealth -= damage;
            }
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die(attacker);
            }
            else
            {
                Console.WriteLine($"{attacker.gameEntity.name} dealt {damage} dmg to {gameEntity.name}");
            }
            UpdateUI();
        }
        public void Heal(int healAmount)
        {
            currentHealth += healAmount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            UpdateUI();
        }
        public void AddShield(int shieldAmount)
        {
            currentShield += shieldAmount;
            if (currentShield > maxHealth)
            {
                currentShield = maxHealth;
            }
            UpdateUI();
        }
        public void Die(HealthComponent killer)
        {
            Console.WriteLine($"{gameEntity.name} was killed by {killer.gameEntity.name}");
            gameEntity.isActive = false;
        }

        public void UpdateUI()
        {
            healthBar.UpdateHealthUI(currentHealth, maxHealth, currentShield);
        }
    }
}