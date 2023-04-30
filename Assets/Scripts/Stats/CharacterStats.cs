using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStats : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth { get; set; }
    public Stat damage;
    public Stat armor;
    private Healthbar healthbar;
    private CinemachineImpulseSource impulseSource;

    void Awake()
    {
        currentHealth = maxHealth;
        healthbar = gameObject.GetComponent<Healthbar>();
    }
    private void Start()
    {
        if (healthbar != null)
        {
            healthbar.SetMaxHealth(maxHealth);
        }
    }
 
    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue(); 
        damage =Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        if (healthbar != null)
        {
            healthbar.SetHealth(currentHealth);
        }
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //Die
        //Should be overwritten
        Debug.Log(transform.name + " has died.");
    }
}
