using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100; // Player's max health.
    public int currentHealth; // Player's current health.

    private Animator animator; // Reference to the Animator to trigger animations (e.g. death)

    private bool isDead = false; // Tracks whether the player is already dead to prevent repeated logic

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health at the start of the game

        animator = GetComponentInChildren<Animator>(); // Finds the Animator component on child object.
    }

    public void TakeDamage(int amount)
    {
        // Prevents damage if already dead
        if (isDead) return;
        // Subtracts damage and clamps to 0
        currentHealth = Mathf.Max(0, currentHealth - amount);

        if (currentHealth <= 0)
        {
            Die(); // Trigger death animation if health reaches zero.
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player died!");

        // Trigger animation
        if (animator != null)
            animator.SetTrigger("Die");

        // Disable gameplay scripts
        if (TryGetComponent<PlayerMovement>(out var movement))
            movement.enabled = false;

        if (TryGetComponent<PlayerCombat>(out var combat))
            combat.enabled = false;

        if (TryGetComponent<PlayerEquipment>(out var equipment))
            equipment.enabled = false;

        // Show the Game Over UI after a short delay to allow animation to play
        Invoke(nameof(ShowGameOver), 1.5f);
    }

    void ShowGameOver()
    {
        // Displays the Game Over screen by finding and calling the GameOverUI controller (R)
        GameOverUI gameOver = FindObjectOfType<GameOverUI>();
        if (gameOver != null)
            gameOver.ShowGameOver();
    }

}
