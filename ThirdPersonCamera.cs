using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // The target the camera will follow (usually the player)
    public float mouseSensitivity = 2f;  // Sensitivity for mouse movement
    public float distance = 4f;  // Distance behind the player
    public float verticalOffset = 2f;  // Vertical offset to position the camera above the player

    // Current rotation values for the camera
    private float yaw = 0f;
    private float pitch = 10f;

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity; // Update yaw (left/right) and pitch (up/down) based on mouse movement
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        pitch = Mathf.Clamp(pitch, 0, 40); // limits look down/up

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0); // Converts yaw and pitch into a rotation

        Vector3 targetPos = target.position + Vector3.up * verticalOffset; // Set the target camera focus point (above the player’s head)

        Vector3 offset = rotation * new Vector3(0, 0, -distance); // Calculates the camera position behind the player based on rotation and distance
        transform.position = targetPos + offset;
        transform.LookAt(targetPos); // Makes the camera look at the player’s head position

    }
}
