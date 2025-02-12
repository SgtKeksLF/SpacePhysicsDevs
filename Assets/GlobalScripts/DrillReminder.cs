

using System.Collections;
using UnityEngine;

public class DrillReminder : MonoBehaviour
{
    public Renderer buttonRenderer; // Der Renderer des Buttons
    public Material greenMaterial; // Das grüne Material für den Button
    public Collider drillingTrigger; // Der Collider, in den der Bohrer eintreten soll
    public GameObject drill; // Der Drill, der im Inspector zugewiesen wird
    public AudioSource reminderAudio; // Die Audioquelle für die Erinnerung

    private bool countdownStarted = false;
    private bool drillInTrigger = false;
    private float reminderDelay = 30f; // Sekunden bis zur Erinnerung

    void Update()
    {
        // Falls der Button das grüne Material hat und der Countdown noch nicht gestartet wurde
        if (!countdownStarted && IsButtonGreen())
        {
            countdownStarted = true;
            StartCoroutine(StartDrillCountdown());
        }

        // Falls der Button nicht mehr grün ist, den Countdown stoppen und den Zustand zurücksetzen
        if (countdownStarted && !IsButtonGreen())
        {
            countdownStarted = false;
            drillInTrigger = false; // Rücksetzen, falls der Button wieder nicht grün ist
        }
    }

    private bool IsButtonGreen()
{
    // Überprüfen, ob das aktuelle Material des Buttons das grüne Material ist
    Material currentMaterial = buttonRenderer.sharedMaterial; // Holen des aktuellen Materials
    return currentMaterial.name == greenMaterial.name;
}

    private IEnumerator StartDrillCountdown()
    {
        float timer = 0f;

        while (timer < reminderDelay)
        {
            // Falls der Bohrer in den Trigger eintritt, abbrechen
            if (drillInTrigger) yield break;

            timer += Time.deltaTime;
            yield return null;
        }

        // Falls 30 Sekunden vergangen sind und nicht gebohrt wurde, Audio abspielen
        if (!drillInTrigger && reminderAudio != null)
        {
            reminderAudio.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Überprüfen, ob der Bohrer in den Collider eintritt
        if (other.gameObject == drill)
        {
            drillInTrigger = true;
        }
    }
}

