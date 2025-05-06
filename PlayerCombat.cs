using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    [Header("Weapon Stats")]
    // Base damage dealt per attack
    public int baseDamage = 10;
    // Chance to land a hit (0–1 range)
    public float hitChance = 0.85f;
    // Chance to land a critical hit
    public float critChance = 0.2f;
    // Multiplier applied to damage if a critical hit is triggered
    public float critMultiplier = 2f;

    // Visualize attack range in the editor (red sphere in front of player)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 1.5f, 1.5f);
    }



    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click (LMB) triggers attack
        {
            animator.SetTrigger("Attack");
            TryDealDamage();  // Immediately apply damage for MVP
        }

        if (Input.GetMouseButtonDown(1)) // Block
        {
            animator.SetTrigger("Block"); // Right-click (RMB) triggers block animation
        }
    }


    // This function will be called from animation event
    public void TryDealDamage()
    {
        float roll = Random.value;

        if (roll > hitChance)
        {
            Debug.Log("You missed!");
            return;
        }
        // Determine if the hit is critical
        bool isCrit = Random.value < critChance;
        int damage = isCrit ? Mathf.RoundToInt(baseDamage * critMultiplier) : baseDamage;

        Debug.Log(isCrit ? $"CRITICAL! You dealt {damage} damage." : $"Hit! You dealt {damage} damage.");

        // Find all colliders in attack range - temporary MVP system
        Collider[] hits = Physics.OverlapSphere(transform.position, 3f);

        // Apply damage to all enemies within range
        foreach (var hit in hits)
        {
            EnemyStats enemy = hit.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
