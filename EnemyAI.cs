using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Reference to the player the enemy will chase and attack
    public Transform player;
    // Enemy movement speed
    public float moveSpeed = 3f;
    // Distance within which the enemy will stop moving and start attacking
    public float attackDistance = 2f;
    // Time between consecutive attacks
    public float attackCooldown = 2f;
    // Internal timer to track last attack time
    private float lastAttackTime;
    // Components for movement and animation
    private CharacterController controller;
    private Animator animator;
    // Reference to the player's health script
    private PlayerStats playerStats;

    void Start()
    {
        // Get the CharacterController attached to this enemy
        controller = GetComponent<CharacterController>();
        // Get the Animator from a child GameObject - 3D model
        animator = GetComponentInChildren<Animator>();
        // Get the PlayerStats script from the player to apply damage
        playerStats = player.GetComponent<PlayerStats>();
    }


    void Update()
    {
        // Ensures all essential references are valid before executing logic
        if (!player || !controller || !animator) return;

        // Calculate distance to player
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackDistance)
        {
            // Movement
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 horizontalDir = new Vector3(direction.x, 0f, direction.z); // no vertical
            controller.Move(horizontalDir * moveSpeed * Time.deltaTime);

            // Rotation (only horizontal)
            if (horizontalDir != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(horizontalDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            // Animation
            animator.SetFloat("Speed", 1f);
        }
        else
        {
            // Stop moving
            animator.SetFloat("Speed", 0f);

            // Attack cooldown
            if (Time.time - lastAttackTime > attackCooldown)
            {
                lastAttackTime = Time.time;
                // Trigger attack animation and apply damage to player
                if (playerStats != null)
                {
                    animator.SetTrigger("Attack");
                    playerStats.TakeDamage(30); 
                    Debug.Log("Enemy attacked!");
                }
            }
        }
    }
}

