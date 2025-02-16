using UnityEngine;
using System.Collections.Generic;

/*

This script plays an success audio if the player got the samples of each planet room.
So the player has a Feedback.

*/

public class SlotTrigger : MonoBehaviour
{
    public AudioSource sound; 
    private HashSet<string> enteredSlots = new HashSet<string>();
    private bool soundPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slot1") || other.CompareTag("Slot2") || other.CompareTag("Slot3") || other.CompareTag("Slot4"))
        {
            enteredSlots.Add(other.tag); 

            if (enteredSlots.Count == 4 && !soundPlayed)
            {
                sound.Play();
                soundPlayed = true; 
            }
        }
    }
}
