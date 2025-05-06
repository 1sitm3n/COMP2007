using UnityEngine;


// Ensures a CharacterController is attached to the GameObject
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Movement speed in units per second
    public float moveSpeed = 5f;
    // Rotation speed in degrees per second
    public float turnSpeed = 720f;
    // Jump force (used in vertical velocity calculation)
    public float jumpForce = 5f;
    // Downward acceleration due to gravity
    public float gravity = -9.81f;

    // Components and state tracking
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        // Cache the CharacterController component on start
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded; // Ground Check.
        if (isGrounded && velocity.y < 0) // Rest downward velocity if player grounded.
            velocity.y = -2f;
        // Movement Inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(h, 0, v).normalized;
        // Only move and rotate if input is above threshold
        if (inputDir.magnitude >= 0.1f)
        {
            // Calculates target rotation angle based on the input and camera direction
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            // Convert angle to world direction
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            // Smoothly rotates the character to face movement direction
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            // Applies movement using CharacterController
            controller.Move(moveDir * moveSpeed * Time.deltaTime);
        }
        // Jump Input
        if (Input.GetButtonDown("Jump") && isGrounded)
            // Apply jump velocity (using physics formula for jump height)
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        // Apply gravity.
        velocity.y += gravity * Time.deltaTime;
        // Moves the character vertically
        controller.Move(velocity * Time.deltaTime);

        // Animation control
        float moveAmount = new Vector2(h, v).magnitude;
        GetComponentInChildren<Animator>().SetFloat("Speed", moveAmount);

    }
}
