using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manabar : MonoBehaviour
{
    public Slider manabar;
    public int maxMana;
    public int currentMana;

    private void Start()
    {
        SetMaxMana(maxMana);
    }

    private void Update()
    {
        // Here you could add any logic to modify the mana value over time,
        // such as natural regeneration or mana drain from other effects.
        // For this example, we'll just update the slider to reflect the
        // current mana value.

        if (manabar.value != currentMana)
        {
            SetMana(currentMana);
        }
    }

    public void SetMana(int mana)
    {
        currentMana = mana;
        manabar.value = mana;
    }

    public void SetMaxMana(int mana)
    {
        manabar.maxValue = mana;
        manabar.value = mana;
    }
}
