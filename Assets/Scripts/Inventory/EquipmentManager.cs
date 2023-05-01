using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    Equipment[] currentEquipment;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    Inventory inventory;
    public GameObject helmetObject,swordObject,shieldObject;

    public bool IsSlotOccupied(EquipmentSlot slot)
    {
        return currentEquipment[(int)slot] != null;
    }
    private void Start()
    {
        inventory = Inventory.Instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip (Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem= null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if(onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
        if (Instance.IsSlotOccupied(EquipmentSlot.Head))
        {
            // There is an item equipped in the Head slot, so enable the helmet object
            helmetObject.SetActive(true);
        }
        else
        {
            // No item equipped in the Head slot, so disable the helmet object
            helmetObject.SetActive(false);
        }
        if (Instance.IsSlotOccupied(EquipmentSlot.Weapon))
        {
            // There is an item equipped in the Head slot, so enable the helmet object
            swordObject.SetActive(true);
        }
        else
        {
            // No item equipped in the Head slot, so disable the helmet object
            swordObject.SetActive(false);
        }
        if (Instance.IsSlotOccupied(EquipmentSlot.Shield))
        {
            // There is an item equipped in the Head slot, so enable the helmet object
            shieldObject.SetActive(true);
        }
        else
        {
            // No item equipped in the Head slot, so disable the helmet object
            shieldObject.SetActive(false);
        }
    }
}
