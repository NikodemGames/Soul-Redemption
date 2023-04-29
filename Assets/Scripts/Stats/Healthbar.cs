using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthbar;


    public void SetHealth(int hp)
    {
        healthbar.value = hp;
    }


    public void SetMaxHealth(int hp)
    {
        healthbar.maxValue = hp;
        healthbar.value = hp;
    }

}
