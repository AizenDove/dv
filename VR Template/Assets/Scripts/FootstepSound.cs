using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource footstepAudioSource; // Assign in Inspector
    public float movementThreshold = 0.1f;  // Sensitivity for detecting movement

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
        footstepAudioSource.loop = true; // Ensure the AudioSource is set to loop
        footstepAudioSource.playOnAwake = false;
    }

    void Update()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);

        if (distanceMoved > movementThreshold)
        {
            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Play();
            }
        }
        else
        {
            if (footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Pause();
            }
        }

        lastPosition = transform.position;
    }
}
