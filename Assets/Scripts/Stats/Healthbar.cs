using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthbar;
    public CharacterStats characterStats;


    private void Start()
    {
        SetMaxHealth(characterStats.maxHealth);
    }

    private void Update()
    {


        if (healthbar.value != characterStats.currentHealth)
        {
            SetHealth(characterStats.currentHealth);
        }
    }
    public void SetHealth(int hp)
    {
        healthbar.value = hp;
        characterStats.currentHealth = hp;
    }


    public void SetMaxHealth(int hp)
    {
        healthbar.maxValue = hp;
        healthbar.value = hp;
    }

}
