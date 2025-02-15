using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script makes it so buttons make a click noise when interacted with

public class Global_ButtonClicksScript : MonoBehaviour
{
    public AudioSource audioSource;
 

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
