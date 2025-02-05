using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
            Destroy(gameObject, audioSource.clip.length); // Destroy after sound plays
        }
        else
        {
            Destroy(gameObject); // Failsafe
        }
    }
}
