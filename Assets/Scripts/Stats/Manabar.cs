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
