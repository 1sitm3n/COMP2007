using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    // Maximum health of the enemy
    public int maxHealth = 50;
    // Current health value (updated during combat)
    public int currentHealth;
    // Flag to track if the enemy is dead
    public bool isDead = false;
    // Delegate event for notifying external systems (like respawn) when the enemy dies
    public System.Action onDeath;

    private Animator animator;

    void Start()
    {
        // Set current health to maximum on start
        currentHealth = maxHealth;
        // Get Animator from child object (e.g., the Gladiator model)
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int amount)
    {
        Debug.Log($"{gameObject.name} took {amount} damage!");

        if (isDead) return;
        // Reduce current health
        currentHealth -= amount;
        // Check for death
        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        isDead = true;
        Debug.Log("Enemy died!");
        // Play death animation
        if (animator != null)
            animator.SetTrigger("Die");

        // Disable AI logic to stop movement and actions
        GetComponent<EnemyAI>().enabled = false;
        // Notify other systems (e.g., spawner) that this enemy has died
        if (onDeath != null)
            onDeath.Invoke();
        // Destroy enemy object after delay (for animation to finish)
        Destroy(gameObject, 3f);
    }
}
