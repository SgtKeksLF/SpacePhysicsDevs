using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInShelf : MonoBehaviour
{
      private AudioSource audioSource; // Private AudioSource-Variable
    private bool canPlaySound = false; // Flag, um zu steuern, wann der Sound abgespielt werden kann

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Greift auf die AudioSource des GameObjects zu
        StartCoroutine(DelayBeforePlayingSound()); // Starte die Coroutine, die den Delay handhabt
    }

    private IEnumerator DelayBeforePlayingSound()
    {
        yield return new WaitForSeconds(1f); // Warte 1 Sekunde
        canPlaySound = true; // Erlaube das Abspielen von Sounds
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canPlaySound && audioSource != null && !audioSource.isPlaying) // Überprüfen, ob Sound abgespielt werden kann
        {
            audioSource.Play(); // Spielt den Sound ab
        }
        Debug.Log("Sound played");
    }
}
