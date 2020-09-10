using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] int maxMana = 100;
    [SerializeField] int currentMana = 100;

    public void SpendMana (int manaToSpend)
    {
        currentMana -= manaToSpend;
    }

    public void GainMana(int manaToGain)
    {
        currentMana += manaToGain;
    }

    public int GetMaxMana()
    {
        return maxMana;
    }

    public int GetCurrentMana()
    {
        return currentMana;
    }



}
