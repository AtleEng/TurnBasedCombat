using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;
using CoreAnimation;

namespace Engine
{
    public class Character : Component, IScript
    {
        public HealthComponent healthComponent;
        public AnimatorController animator;

        public bool hasAttacked;

        public int currentMana; //mana to use
        public int maxMana; //max amount of mana

        public List<Spell> spells = new(); //all the spells

        public Character(HealthComponent healthComponent, AnimatorController animator)
        {
            this.healthComponent = healthComponent;
            this.animator = animator;
        }
        public void Attack(int spellIndex, Character victim, Character attacker)
        {
            if (!spells.Any()) { return; } //check if character has any spells

            if (spellIndex > spells.Count || spellIndex < 0) //check so the spell exist in list
            {
                spellIndex = 0;
                Console.WriteLine("Spell is out of the index in the list, using spell[0] instead");
            }

            spells[spellIndex].OnUse(attacker, victim); //use the spell
        }
    }
}