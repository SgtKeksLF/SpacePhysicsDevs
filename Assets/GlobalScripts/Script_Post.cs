using System.Collections;
using UnityEngine;

public class Script_Post : MonoBehaviour
{
    public GameObject currentProbe; // Wird im Inspector zugewiesen
    public GameObject targetObject;
    public GameObject quiz5; // Referenz zu "Quiz5"
    public float moveSpeed = 0.5f;
    private bool hasTriggered = false; // Stellt sicher, dass die Methode nur einmal ausgelöst wird

    private AudioSource[] audioSources;
    private AudioSource triggerSound;
    private AudioSource arrivalSound;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 2)
        {
            triggerSound = audioSources[0];
            arrivalSound = audioSources[1];
        }
        else
        {
            Debug.LogError("Nicht genügend AudioSources auf dem Objekt vorhanden!", this);
        }
    }

    private void Update()
    {
        // Prüft, ob Quiz5 aktiviert wurde und ob OnSolutionCorrect noch nicht ausgelöst wurde
        if (quiz5 != null && quiz5.activeInHierarchy && !hasTriggered)
        {
            hasTriggered = true; // Verhindert mehrfaches Auslösen
            OnSolutionCorrect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Überprüfe, ob currentProbe im Collider ist
        if (currentProbe != null && other.bounds.Contains(currentProbe.transform.position))
        {
            if (triggerSound != null)
            {
                triggerSound.Play();
            }
            
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
            
            currentProbe.transform.position = transform.position;
        }
    }

    private void OnSolutionCorrect()
    {
        if (currentProbe != null && targetObject != null)
        {
            StartCoroutine(MoveProbeCoroutine(targetObject.transform.position));
        }
    }

    private IEnumerator MoveProbeCoroutine(Vector3 endPos)
    {
        Vector3 start = currentProbe.transform.position;
        Vector3 upPosition = start + Vector3.up * 3f; // Berechne die Position 3 Meter nach oben
        float elapsedTime = 0f;

        // Bewege die Probe 3 Meter nach oben
          if (arrivalSound != null)
        {
            arrivalSound.Play();
        }
        while (elapsedTime < 1f)
        {
            currentProbe.transform.position = Vector3.Lerp(start, upPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        currentProbe.transform.position = upPosition; // Setze die endgültige Position nach oben

        // Setze die Bewegung zur Endposition
        elapsedTime = 0f;
       
        while (elapsedTime < 1f)
        {
            currentProbe.transform.position = Vector3.Lerp(upPosition, endPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        
        currentProbe.transform.position = endPos; // Endgültige Position setzen
        Rigidbody rb = currentProbe.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}
