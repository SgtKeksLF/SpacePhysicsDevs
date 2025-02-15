using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/*
This scripts attaches sounds to collision and pick up from Items

Sounds are only allowed one second after the game starts to prevent unwanted sounds
*/


public class Global_PickingUpObjectScript : MonoBehaviour
{
    private AudioSource audioSource;
    private bool canPlaySound = false;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
           
        }
        
       
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
