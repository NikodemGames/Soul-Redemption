using UnityEngine;

public class ItemPickup : Interact
{
    // Start is called before the first frame update
    public Item item;
    public override void Interaction()
    {
        base.Interaction();

        PickUp();
        
    }

    void PickUp()
    {
        Debug.Log("Picked up " + item.name);
        bool wasPickedUp = Inventory.Instance.Add(item);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
        
    }
}
