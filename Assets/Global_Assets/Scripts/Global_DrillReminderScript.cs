using System.Collections;
using UnityEngine;

public class Global_DrillReminderScript : MonoBehaviour
{
    public Renderer buttonRenderer; // Der Renderer des Buttons
    public Material greenMaterial; // Das grüne Material für den Button
    public Collider drillingTrigger; // Der Collider, in den der Bohrer eintreten soll
    public GameObject drill; // Der Drill, der im Inspector zugewiesen wird
    public AudioSource reminderAudio; // Die Audioquelle für die Erinnerung

    private bool countdownStarted = false;
    private float reminderDelay = 20f; // Sekunden bis zur Erinnerung

    // Referenz zu DrillTableFreeze
    public Global_DrillingScript scriptDrilling; // Hier referenzieren wir das SkriptDrilling

    void Update()
    {
        // Falls der Button das grüne Material hat und der Countdown noch nicht gestartet wurde
        if (!countdownStarted && IsButtonGreen())
        {
            countdownStarted = true;
            StartCoroutine(StartDrillCountdown());
            Debug.Log("Bohr-Countdown Start");

        }

        // Falls der Button nicht mehr grün ist, den Countdown stoppen und den Zustand zurücksetzen
        if (countdownStarted && !IsButtonGreen())
        {
            countdownStarted = false;
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
            // Statt drillInTrigger verwenden wir jetzt hasBeenDrilled von DrillTableFreeze
            if (scriptDrilling.hasBeenDrilled) // Überprüfe, ob der Bohrer bereits gebohrt hat
            {
                Debug.Log("Bohrer hat den Trigger betreten, Countdown wird abgebrochen.");
                yield break; // Abbrechen, wenn der Bohrer den Trigger betreten hat
            }

            timer += Time.deltaTime;
            yield return null; // Warten auf den nächsten Frame
        }

        // Falls 20 Sekunden vergangen sind und der Bohrer nicht in den Trigger eingetreten ist, Audio abspielen
        if (!scriptDrilling.hasBeenDrilled && reminderAudio != null) // Hier ebenfalls den bool verwenden
        {
            reminderAudio.Play();
            Debug.Log("Reminder-Audio wurde abgespielt");
        }
    }
}
