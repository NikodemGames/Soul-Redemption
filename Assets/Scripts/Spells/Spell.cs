using System.Collections;
using TMPro;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public abstract void Cast();
    public GameObject uiObject;
    public TextMeshProUGUI logText;
    private bool isDisplayed = false;

    public void EnableLog(string text)
    {
        if (!isDisplayed)
        {
            StartCoroutine(EnableDisableCoroutine());
            logText.text = text;
        }
        
    }

    private IEnumerator EnableDisableCoroutine()
    {
        isDisplayed = true;
        uiObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        uiObject.SetActive(false);
        isDisplayed= false;
    }
}

