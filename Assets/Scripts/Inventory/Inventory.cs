using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory present!");
            return;
        }
        Instance = this;
    }
    #endregion
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public int space = 20;
    public List<Item> items = new List<Item> ();

    public bool Add (Item item)
    {
        if(!item.isDefaultItem)
        {
            if(items.Count >= 20)
            {
                Debug.Log("Inventory Full, not enough space!");
                return false;
            }
            items.Add(item);
            if(onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            
        }
        return true;
    }

    public void Remove (Item item)
    {
        items.Remove (item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
