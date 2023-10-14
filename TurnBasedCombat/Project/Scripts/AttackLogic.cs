using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class AttackLogic : Component, IScript
    {
        public int currentMana; //mana to use
        public int maxMana; //max amount of mana

        public List<Spell> spells = new(); //all the spells

        public void Attack(int spellIndex, Character victim, Character attacker)
        {
            if (!spells.Any()) { return; } //check if character has any spells

            if (spellIndex > spells.Count || spellIndex < 0) //check so the spell exist in list
            {
                spellIndex = 0;
                System.Console.WriteLine("Spell is out of the index in the list, using spell[0] instead");
            }

            spells[spellIndex].OnUse(attacker, victim); //use the spell
        }
    }
}