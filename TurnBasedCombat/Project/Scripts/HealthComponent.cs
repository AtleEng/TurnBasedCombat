using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class HealthComponent : Component
    {
        public int currentHealth = 5;
        public int maxHealth = 5;
        public int currentShield = 0;
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
        }
        public void Heal(int healAmount)
        {
            currentHealth += healAmount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
        public void AddShield(int shieldAmount)
        {
            currentShield += shieldAmount;
            if (currentShield > maxHealth)
            {
                currentShield = maxHealth;
            }
        }
        public void Die(HealthComponent killer)
        {
            Console.WriteLine($"{gameEntity.name} was killed by {killer.gameEntity.name}");
        }
    }
}