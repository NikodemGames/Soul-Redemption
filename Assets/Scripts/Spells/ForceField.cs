using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : Spell
{
    public GameObject barrierPrefab;
    public float countdownTime = 3f;
    public int manaCost;
    public Manabar manaBar;
    public PlayerStats playerStats;
    public override void Cast()
    {
        if(manaBar.currentMana >= manaCost&&countdownTime>=3)
        {
            barrierPrefab.SetActive(true);
            manaBar.currentMana -= manaCost;
            playerStats.armor.AddModifier(5);
        }
        else
        {
            Debug.Log("Not enough mana to cast spell!");
        }
    }
    void Update()
    {
        if (barrierPrefab.activeSelf)
        {
            countdownTime -= Time.deltaTime;
            

            if (countdownTime <= 0f)
            {
                barrierPrefab.SetActive(false);
                countdownTime = 3f;
                playerStats.armor.RemoveModifier(5);
            }
        }
        
    }

}
