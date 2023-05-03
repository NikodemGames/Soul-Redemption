using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Usable", menuName = "Inventory/Usable")]
public class Usables : Item
{
    public bool whichPotion;

    public override void Use()
    {
        base.Use();

        if(whichPotion)
        {
            Healthbar healthBar = FindObjectOfType<Healthbar>();
            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            if (healthBar != null)
            {
                healthBar.SetHealth(playerStats.maxHealth);
                RemoveFromInventory();
            }
        }

        else
        {
            Manabar manabar = FindObjectOfType<Manabar>();
            if (manabar != null)
            {
                manabar.SetMana(100);
                RemoveFromInventory();
            }
        }
    }
}
