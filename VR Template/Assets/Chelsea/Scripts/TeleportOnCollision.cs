using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    // Reference to the target GameObject where the player will teleport
    public GameObject teleportTarget;

    // Offset to teleport player slightly above the target position to avoid falling through the ground
    public float heightOffset = 1.0f;

    // Check for a collision with the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the VR player (use tag or name check)
        if (other.CompareTag("Player"))
        {
            // Teleport the VR player to the target GameObject's position with an offset
            TeleportPlayer(other.gameObject);
        }
    }

    // Function to teleport the player
    private void TeleportPlayer(GameObject player)
    {
        if (teleportTarget != null)
        {
            // Get the target's position
            Vector3 targetPosition = teleportTarget.transform.position;

            // Add height offset to avoid falling through the ground
            targetPosition.y += heightOffset;

            // Set the player's position to the target position with height offset
            player.transform.position = targetPosition;
        }
        else
        {
            Debug.LogWarning("Teleport target not set!");
        }
    }
}
