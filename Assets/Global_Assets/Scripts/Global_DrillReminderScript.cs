using System.Collections;
using UnityEngine;

/*

This script reminds the player of using the drill to extract the planet sample.

Therefore it uses the green lamp material again to make sure the reminder is only played 
when planet physics are active. When the drill spawns a 2 minute countdown starts. 
If the player does not drill before the countdown ends, the reminder audio will play.

*/

public class Global_DrillReminderScript : MonoBehaviour
{
    public Renderer buttonRenderer; 
    public Material greenMaterial; 
    public Collider drillingTrigger; 
    public GameObject drill; 
    public AudioSource reminderAudio; 

    public Global_DrillingScript scriptDrilling; // get hasBeenDrilled bool from other script


    private bool countdownStarted = false;
    private float reminderDelay = 120f; 

    void Update()
    {
        if (!countdownStarted && IsButtonGreen())
        {
            countdownStarted = true;
            StartCoroutine(StartDrillCountdown());
            // Debug.Log("Drill countdown started");
        }

        if (countdownStarted && !IsButtonGreen())
        {
            countdownStarted = false;
        }
    }

    private bool IsButtonGreen()
    {
        Material currentMaterial = buttonRenderer.sharedMaterial; 
        
        return currentMaterial.name == greenMaterial.name;
    }

    private IEnumerator StartDrillCountdown()
    {
        float timer = 0f;

        while (timer < reminderDelay)
        {
            // player drills while countdown lasts: don't play audio
            if (scriptDrilling.hasBeenDrilled) 
            {
                // Debug.Log("Drill entered trigger, cancel countdown.");
                yield break; 
            }

            timer += Time.deltaTime;
            yield return null; 
        }

        // player did not drill: play reminder audio
        if (!scriptDrilling.hasBeenDrilled && reminderAudio != null) 
        {
            reminderAudio.Play();
        }
    }
}
