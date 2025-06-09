using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class PlayerStats 
{
    public int maxHP = 10;
    public int attackPower = 3;
    public float moveSpeed = 1.5f;
    public int level = 1;
    public int currentExp = 0;
    public int expToNextLevel = 20;


    public void GainExp(int amount)
    {
        currentExp += amount;

        while (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            level++;
            expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.2f);
            maxHP += 2;
            attackPower += 1;
            moveSpeed += 0.1f;
           
        }
    }


}
