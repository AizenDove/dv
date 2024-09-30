using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    // Reference to the Animator component
    public Animator animator;

    // Name of the animation trigger or boolean parameter in the Animator
    public string animationParameter = "PlayAnimation";

    // Check if the object entering the trigger is the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start the animation by setting a trigger or a bool in the Animator
            animator.SetTrigger(animationParameter);
        }
    }

    // Optional: Check if the player exits the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Optionally stop or reset the animation
            animator.ResetTrigger(animationParameter);
        }
    }
}
