using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    // Define the teleport target location (can be set in the Inspector)
    public Vector3 teleportLocation = new Vector3(10, 2, 10); // Default teleport coordinates

    // Check for a collision with the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the VR player (use tag or name check)
        if (other.CompareTag("Player"))
        {
            // Teleport the VR player to the specified location
            TeleportPlayer(other.gameObject);
        }
    }

    // Function to teleport the player
    private void TeleportPlayer(GameObject player)
    {
        // Set the player's position to the teleport location
        player.transform.position = teleportLocation;
    }
}
