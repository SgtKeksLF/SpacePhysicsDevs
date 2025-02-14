using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Global_PickingUpObjectScript : MonoBehaviour
{
    private AudioSource audioSource;
    private bool canPlaySound = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("Keine AudioSource auf diesem Objekt gefunden!");
        }
        
        // Starte das Delay für das Abspielen der Sounds
        StartCoroutine(EnableSoundAfterDelay(1f));
    }

    private IEnumerator EnableSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canPlaySound = true;
    }

    public void OnGrab()
    {   
        if (canPlaySound && audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canPlaySound && audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
