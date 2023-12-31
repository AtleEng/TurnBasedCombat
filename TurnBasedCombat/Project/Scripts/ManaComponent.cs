using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using CoreEngine;

namespace Engine
{
    public class ManaComponent : Component
    {
        public int currentMana = 4;
        public int maxMana = 4;

        public ManaBar manaBar;

        public ManaComponent(ManaBar manaBar)
        {
            this.manaBar = manaBar;
        }

        public override string PrintStats()
        {
            return $"Mana:{currentMana}/{maxMana}";
        }
        public bool UseMana(int manaAmount)
        {
            int mana = currentMana;
            mana -= manaAmount;
            if (mana < 0)
            {
                return false;
            }
            currentMana = mana;
            UppdateManaUI();
            return true;
        }
        public void AddMana(int manaAmount)
        {
            currentMana += manaAmount;
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
            UppdateManaUI();
        }

        void UppdateManaUI()
        {
            for (int i = 0; i < maxMana; i++)
            {
                manaBar.manaSprites[i].isActive = false;
            }
            for (int i = 0; i < currentMana; i++)
            {
                manaBar.manaSprites[i].isActive = true;
            }
        }
    }
}