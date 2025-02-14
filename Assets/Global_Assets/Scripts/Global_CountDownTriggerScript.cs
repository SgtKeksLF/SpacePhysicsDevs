using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

This script reminds the player of changing to the planets physics. 

Therefore it uses the material from the planet physics lamp to check whether the player 
has already changed the physics to those of the planet. If its material is green this 
means the planet physics were turned on and there is no need to play the reminder audio anymore.

*/
public class Global_CountDownTriggerScript : MonoBehaviour
{

    public GameObject buttonObject;
    public Material greenLampMaterial;
    public AudioSource reminderAudio;
    public float countdownTime = 5f;
    public BoxCollider mainCameraCollider;

    private bool wasButtonClicked = false;
    private bool countdownRunning = false;
    private bool hasAudioPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other == mainCameraCollider)
        {
            // Debug.Log("Player entered room"); 

            if (!countdownRunning)
            {
                StartCoroutine(StartCountdown());
            }
        }
    }

    private IEnumerator StartCountdown()
    {
        countdownRunning = true;
        // Debug.Log("Physics reminder started."); 
        float timer = countdownTime;

        while (timer > 0f)
        {
            if (IsButtonClicked())
            {
                wasButtonClicked = true;
                countdownRunning = false;
                yield break;
            }

            timer -= Time.deltaTime;
            yield return null;
        }


        if (!wasButtonClicked && !hasAudioPlayed)
        {
            reminderAudio.Play();
            hasAudioPlayed = true;
            // Debug.Log("Reminder audio played");
        }

        countdownRunning = false;
    }

    private bool IsButtonClicked()
    {
        Renderer buttonRenderer = buttonObject.GetComponent<Renderer>();
        if (buttonRenderer != null && buttonRenderer.sharedMaterial.name == greenLampMaterial.name)
        {
            return true;
        }

        return false;
    }
}
