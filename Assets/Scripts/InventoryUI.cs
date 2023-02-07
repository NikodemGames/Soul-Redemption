using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform itemsParent;
    public GameObject inventoryUI;
    Inventory inventory;
    InventorySlot[] slots;
    void Start()
    {
        inventory = Inventory.Instance;
        inventory.onItemChangedCallback +=UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

    }

    void UpdateUI()
    {
        Debug.Log("Updating the ui");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
