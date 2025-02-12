using System.Collections;
using UnityEngine;

public class RoomTimerReminder : MonoBehaviour
{
    public GameObject player; 
    public AudioSource reminderAudio; 
    public float reminderTime = 30f; 

    private bool playerInRoom = false;
    private float timeSpentInRoom = 0f;
    private bool hasReminderPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Spieler hat Raum betreten");
            // Reset, falls der Spieler erneut eintritt
            playerInRoom = true;
            hasReminderPlayed = false; 
            timeSpentInRoom = 0f; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Spieler hat Raum verlassen");
            playerInRoom = false;
            timeSpentInRoom = 0f; 
        }
    }

    private void Update()
    {
        if (playerInRoom && !hasReminderPlayed)
        {
            timeSpentInRoom += Time.deltaTime;

            if (timeSpentInRoom >= reminderTime)
            {
                PlayReminder();
            }
        }
    }

    private void PlayReminder()
    {
        if (reminderAudio != null)
        {
            reminderAudio.Play();
            hasReminderPlayed = true; 
        }
    }
}
