using System.Collections;
using UnityEngine;

/*

This script plays an audio reminder for the player to explore
the planet rooms in case he is inactive for too long.

The audio only plays once.

*/

public class RoomTimerReminder : MonoBehaviour
{
    public BoxCollider player;
    public AudioSource reminderAudio; 
    public float reminderTime = 30f; 

    private bool playerInRoom = false;
    private float timeSpentInRoom = 0f;
    private bool hasReminderPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // Debug.Log("Player entered room, countdown started.");
            playerInRoom = true;
            hasReminderPlayed = false; 
            timeSpentInRoom = 0f; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            // Debug.Log("Playerleft room.");
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
