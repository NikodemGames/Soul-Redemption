using TMPro;


public class PlayerStats : CharacterStats
{
    public TextMeshProUGUI playerStats;

    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;

    }

    // Update is called once per frame
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if(newItem !=null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }

    }
    private void Update()
    {
        playerStats.text = "armor: " + armor.GetValue();
    }
    public override void Die()
    {
        base.Die();
        //Masacre the player;
        PlayerManager.instance.KillPlayer();
    }
}
