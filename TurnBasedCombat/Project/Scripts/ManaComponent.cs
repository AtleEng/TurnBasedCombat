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

        public ManaBarLogic manaBarLogic;

        public ManaComponent(ManaBarLogic manaBarLogic)
        {
            this.manaBarLogic = manaBarLogic;
        }

        public override string PrintStats()
        {
            return $"Mana:{currentMana}/{maxMana}";
        }
        public bool UseMana(int manaAmount)
        {
            int mana = currentMana;
            mana -= manaAmount;
            if (mana <= 0)
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
                manaBarLogic.manaSprites[i].isActive = false;
            }
            for (int i = 0; i < currentMana; i++)
            {
                manaBarLogic.manaSprites[i].isActive = true;
            }
        }
    }
}