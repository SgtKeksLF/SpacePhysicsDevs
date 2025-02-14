using System.Collections;
using UnityEngine;

/*

This script reminds the player of taking the plante sample to the mail system.

Therefore it starts a countdown when the sample is spawned. If the player does
not place the sample in the mail system before the coundtown ends the reminder audio is played.

*/

public class ProbeReminder : MonoBehaviour
{
    public AudioSource reminderAudio; 
    public Global_PostScript ScriptPost; 
    public Global_DrillingScript scriptDrilling; 


    private bool countdownStarted = false;
    private float reminderDelay = 120f;

    void Update()
    {
        if (!countdownStarted && scriptDrilling.hasBeenDrilled)
        {
            countdownStarted = true;
            StartCoroutine(StartProbeCountdown());
            // Debug.Log("Sample countdown started");
        }

    }

    private IEnumerator StartProbeCountdown()
    {
        float timer = 0f;

        while (timer < reminderDelay)
        {
            if (ScriptPost.probeInPost) 
            {
                // Debug.Log("Sample in mail, cancel countdown.");
                yield break; 
            }

            timer += Time.deltaTime;
            yield return null; 
        }

        if (!ScriptPost.probeInPost && reminderAudio != null) 
        {
            reminderAudio.Play();
            // Debug.Log("Reminder audio has played");
        }
    }
}
