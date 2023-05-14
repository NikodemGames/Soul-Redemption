using Unity.VisualScripting;
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
        else if(manaBar.currentMana< manaCost)
        {
            EnableLog("Not enough mana!");
        }
        else if (countdownTime<3)
        {
            EnableLog("Spell is not ready yet!");
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
