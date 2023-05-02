using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : Spell
{
    public GameObject fireballPrefab;
    public Transform castPoint;
    public float castSpeed = 10f;
    public int manaCost;
    public Manabar manaBar;

    public override void Cast()
    {
        
        if (manaBar.currentMana >= manaCost)
        {
            GameObject fireball = Instantiate(fireballPrefab, castPoint.position, castPoint.rotation);
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            rb.AddForce(castPoint.forward * castSpeed, ForceMode.VelocityChange);
            manaBar.currentMana -= manaCost;
        }
        else
        {
            Debug.Log("Not enough mana to cast spell!");
        }
    }

}
