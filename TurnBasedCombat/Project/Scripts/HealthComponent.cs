using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class HealthComponent : Component, IScript
    {
        public int currentHealth;
        public int maxHealth;

        public void TakeDMG(int dmg, HealthComponent attacker)
        {
            currentHealth -= dmg;
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
        public void Die(HealthComponent killer)
        {
            Console.WriteLine($"{gameEntity.name} was killed by {killer.gameEntity.name}");
        }
    }
}