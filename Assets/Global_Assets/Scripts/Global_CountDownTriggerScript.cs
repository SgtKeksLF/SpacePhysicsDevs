using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_CountDownTriggerScript : MonoBehaviour
{
    [Header("Inspector Zuweisungen")]
    [SerializeField] private GameObject buttonObject; // Das GameObject, dessen Material überprüft wird
    [SerializeField] private Material greenLampMaterial; // Das Material, das den Button als "gedrückt" markiert
    [SerializeField] private AudioSource reminderAudio; // Die Audioquelle für die Erinnerungsnachricht
    [SerializeField] private float countdownTime = 5f; // Countdown-Zeit in Sekunden
    [SerializeField] private BoxCollider mainCameraCollider; // Der BoxCollider der MainCamera

    private bool wasButtonClicked = false; // Ob der Button geklickt wurde
    private bool countdownRunning = false; // Ob der Countdown läuft
    private bool hasAudioPlayed = false; // Ob das Audio bereits abgespielt wurde

    private void OnTriggerEnter(Collider other)
    {
        // Überprüfen, ob das Objekt der MainCamera Collider ist
        if (other == mainCameraCollider)
        {
            Debug.Log("MainCamera hat den Raum betreten"); // Debug-Ausgabe, wenn die Kamera den Raum betritt

            if (!countdownRunning)
            {
                StartCoroutine(StartCountdown());
            }
        }
    }

    private IEnumerator StartCountdown()
    {
        countdownRunning = true;
        Debug.Log("Countdown ist gestartet"); // Debug-Ausgabe, wenn der Countdown startet
        float timer = countdownTime;

        while (timer > 0f)
        {
            // Während der Countdown läuft, prüfen wir, ob der Button geklickt wurde
            if (IsButtonClicked())
            {
                wasButtonClicked = true;
                countdownRunning = false;
                yield break;
            }

            timer -= Time.deltaTime;
            yield return null;
        }

        // Wenn der Countdown abläuft, aber der Button nicht geklickt wurde und die Audio noch nicht abgespielt wurde
        if (!wasButtonClicked && !hasAudioPlayed)
        {
            reminderAudio.Play();
            hasAudioPlayed = true; // Verhindert erneutes Abspielen
            Debug.Log("Reminder-Audio wurde abgespielt");
        }

        countdownRunning = false;
    }

    private bool IsButtonClicked()
    {
        // Überprüfen, ob das Material der Lampe auf "Lamp_Green" gesetzt ist
        Renderer buttonRenderer = buttonObject.GetComponent<Renderer>();
        if (buttonRenderer != null && buttonRenderer.sharedMaterial.name == greenLampMaterial.name)
        {
            return true; // Wenn das Material grün ist, wurde der Button geklickt
        }

        return false; // Ansonsten nicht
    }
}
