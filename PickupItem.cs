using UnityEngine;

public enum ItemType { Weapon, Helmet }

public class PickupItem : MonoBehaviour
{
    public ItemType type;

    // Stats
    public int damage;
    public float hitChance;
    public float critChance;

    public int bonusHealth;

    public string itemName = "Unknown Item";
}
