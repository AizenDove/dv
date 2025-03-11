using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    [SerializeField] private AudioSource footstepAudioSource; // Assign in Inspector
    [SerializeField] private AudioClip footstepClip;           // Assign the audio clip in Inspector
    [SerializeField] private float movementThreshold = 0.1f;   // Sensitivity for detecting movement

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;

        // Setup the AudioSource
        footstepAudioSource.clip = footstepClip;
        footstepAudioSource.loop = true;
        footstepAudioSource.playOnAwake = false;

        // Preload and warm up the audio clip to avoid initial lag
        footstepAudioSource.Play();
        footstepAudioSource.Pause();
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
