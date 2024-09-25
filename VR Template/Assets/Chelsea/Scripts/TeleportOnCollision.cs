using UnityEngine;
using UnityEngine.UI;  // Needed for UI components
using System.Collections; // For coroutines

public class TeleportWithBlink : MonoBehaviour
{
    // Reference to the target GameObject where the player will teleport
    public GameObject teleportTarget;

    // Reference to the fade screen UI Image
    public Image fadeScreen;

    // Offset to teleport player slightly above the target position
    public float heightOffset = 1.0f;

    // Time for fading duration
    public float fadeDuration = 0.5f;

    // Check for a collision with the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the VR player (use tag or name check)
        if (other.CompareTag("Player"))
        {
            // Start the teleportation process with the blink effect
            StartCoroutine(TeleportWithBlinkEffect(other.gameObject));
        }
    }

    // Coroutine to handle teleportation with blinking effect
    private IEnumerator TeleportWithBlinkEffect(GameObject player)
    {
        // Start fading to black
        yield return StartCoroutine(FadeToBlack());

        // Teleport the player
        TeleportPlayer(player);

        // Fade back to clear
        yield return StartCoroutine(FadeToClear());
    }

    // Function to teleport the player
    private void TeleportPlayer(GameObject player)
    {
        if (teleportTarget != null)
        {
            // Get the target's position and add height offset
            Vector3 targetPosition = teleportTarget.transform.position;
            targetPosition.y += heightOffset;

            // Set the player's position to the target position
            player.transform.position = targetPosition;
        }
        else
        {
            Debug.LogWarning("Teleport target not set!");
        }
    }

    // Coroutine to fade to black
    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0;
        Color color = fadeScreen.color;
        fadeScreen.enabled = true;  // Enable the image if it was disabled

        // Gradually increase the alpha value over time
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);  // Fade in the alpha
            fadeScreen.color = color;
            yield return null;
        }
    }

    // Coroutine to fade back to clear
    private IEnumerator FadeToClear()
    {
        float elapsedTime = 0;
        Color color = fadeScreen.color;

        // Gradually decrease the alpha value over time
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));  // Fade out the alpha
            fadeScreen.color = color;
            yield return null;
        }

        // Disable the image after fading
        fadeScreen.enabled = false;
    }
}

