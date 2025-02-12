using System.Collections;
using UnityEngine;

public class ProbeReminder : MonoBehaviour
{

    // nachdem gedrillt wurde, startet countdown der Probe, wird abgebrochen, wenn Probe in Post war
    public AudioSource reminderAudio; 

    private bool countdownStarted = false;
    private float reminderDelay = 20f; // Sekunden bis zur Erinnerung

    // Referenz zu anderen Skripten
    public Script_Post ScriptPost; 
    public ScriptDrilling scriptDrilling; 

    void Update()
    {
        // Falls gebohrt wurde und der Countdown noch nicht gestartet wurde
        if (!countdownStarted && scriptDrilling.hasBeenDrilled)
        {
            countdownStarted = true;
            StartCoroutine(StartProbeCountdown());
            Debug.Log("Probe-Countdown Start");

        }

    }

    private IEnumerator StartProbeCountdown()
    {
        float timer = 0f;

        while (timer < reminderDelay)
        {
            // Probe schon in Post?
            if (ScriptPost.probeInPost) 
            {
                Debug.Log("Probe in Post, Countdown wird abgebrochen.");
                yield break; 
            }

            timer += Time.deltaTime;
            yield return null; 
        }

        // Falls 20 Sekunden vergangen sind und die Probe nicht in die Post eingetreten ist, Audio abspielen
        if (!ScriptPost.probeInPost && reminderAudio != null) 
        {
            reminderAudio.Play();
            Debug.Log("Reminder-Audio wurde abgespielt");
        }
    }
}
