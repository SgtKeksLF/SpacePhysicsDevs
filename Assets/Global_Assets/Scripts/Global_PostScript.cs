using System.Collections;
using UnityEngine;

public class Global_PostScript : MonoBehaviour
{
    public GameObject currentProbe; // Wird im Inspector zugewiesen
    public GameObject targetObject;
    public GameObject quiz5; // Referenz zu "Quiz5"
    public float moveSpeed = 0.5f;
    private bool hasTriggered = false; // Stellt sicher, dass die Methode nur einmal ausgelöst wird
    public bool probeInPost = false;
    private AudioSource[] audioSources;
    private AudioSource triggerSound;
    private AudioSource arrivalSound;

    public Vector3 fixedRotation = new Vector3(0, 0, 0); // Gewünschte Rotation in Grad

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
        if (quiz5 != null && quiz5.activeInHierarchy && !hasTriggered)
        {
            hasTriggered = true;
            OnSolutionCorrect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentProbe != null && other.bounds.Contains(currentProbe.transform.position))
        {
            probeInPost = true;

            if (triggerSound != null)
            {
                triggerSound.Play();
            }

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                FreezeObject(rb);
            }
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
        Vector3 upPosition = start + Vector3.up * 3f;
        float elapsedTime = 0f;

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

        currentProbe.transform.position = upPosition;

        elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            currentProbe.transform.position = Vector3.Lerp(upPosition, endPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        currentProbe.transform.position = endPos;
        Rigidbody rb = currentProbe.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    private void FreezeObject(Rigidbody rb)
    {
        if (rb != null)
        {
            // Setzt die Position auf die Mitte des Slots
            rb.transform.position = transform.position;

            // Setzt die Rotation auf den festen Wert
            rb.transform.rotation = Quaternion.Euler(fixedRotation);

            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}
