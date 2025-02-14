using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_ButtonClicksScript : MonoBehaviour
{
   public AudioSource audioSource; // Referenz zur Audioquelle

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayAudio()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
