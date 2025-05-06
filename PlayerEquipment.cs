using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    // References to weapon and helmet attachment points on the player (e.g. hand and head)
    public Transform weaponHolder;
    public Transform helmetHolder;

    // Currently equipped weapon and helmet GameObjects
    private GameObject currentWeapon;
    private GameObject currentHelmet;
    // References to the player's combat and stats scripts
    private PlayerCombat combat;
    private PlayerStats stats;

    void Start()
    {
        // Cache references to combat and stat components on the player
        combat = GetComponent<PlayerCombat>();
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        // Press 'E' to pick up a weapon or helmet
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check for nearby PickupItem objects within a small radius

            Collider[] pickups = Physics.OverlapSphere(transform.position, 2f);
            foreach (var item in pickups)
            {
                PickupItem p = item.GetComponent<PickupItem>();
                if (p != null)
                {
                    Equip(p); // Equip the item
                    Destroy(item.gameObject); // Remove pickup from the world
                    break; // Only equip one item at a time

                }
            }
        }

        // Drop Weapon - Press 'F' to drop the currently equipped weapon
        if (Input.GetKeyDown(KeyCode.F) && currentWeapon)
        {
            Destroy(currentWeapon); // Remove the weapon from the hand
           // Reset player combat stats to default
            combat.baseDamage = 10;
            combat.hitChance = 0.85f;
            combat.critChance = 0.15f;
        }

        // Drop Helmet - Press 'G' to drop the currently equipped helmet
        if (Input.GetKeyDown(KeyCode.G) && currentHelmet)
        {
            Destroy(currentHelmet); // Remove helmet from head
            stats.maxHealth -= 30; // Reduce player's max health
            // Clamps current health to new max
            if (stats.currentHealth > stats.maxHealth)
                stats.currentHealth = stats.maxHealth;
        }
    }

    void Equip(PickupItem item)
    {
        if (item.type == ItemType.Weapon)
        {
            // Replaces existing weapon if one is equipped
            if (currentWeapon) Destroy(currentWeapon);
            // Instantiate the new weapon as a child of the weapon holder
            currentWeapon = Instantiate(item.gameObject, weaponHolder);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
            // Updates combat stats based on weapon
            combat.baseDamage = item.damage;
            combat.hitChance = item.hitChance;
            combat.critChance = item.critChance;
        }
        else if (item.type == ItemType.Helmet)
        {
            // Replaces existing helmet if one is equipped
            if (currentHelmet) Destroy(currentHelmet);
            // Instantiates the new helmet as a child of the helmet holder
            currentHelmet = Instantiate(item.gameObject, helmetHolder);
            currentHelmet.transform.localPosition = Vector3.zero;
            currentHelmet.transform.localRotation = Quaternion.identity;

            // Update health stats based on helmet
            stats.maxHealth += item.bonusHealth;
            stats.currentHealth += item.bonusHealth;
        }
    }
}
