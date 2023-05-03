using UnityEngine;
[RequireComponent(typeof(AudioSource))]

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
            gameObject.GetComponent<AudioSource>().Play();
            if (gameObject.CompareTag("FreeItem"))
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.GetComponent<ItemPickup>().enabled = false;
                gameObject.name = "Empty " + gameObject.name;
            }
            
        }
        
    }
}
