using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script assigns sounds to objects being placed in the shelves

Sounds are only allowed one second after start

*/

public class SoundInShelf : MonoBehaviour
{
    private AudioSource audioSource; 
    private bool canPlaySound = false; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        StartCoroutine(DelayBeforePlayingSound());
    }

    private IEnumerator DelayBeforePlayingSound()
    {
        yield return new WaitForSeconds(1f); 
        canPlaySound = true; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canPlaySound && audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
       
    }
}
