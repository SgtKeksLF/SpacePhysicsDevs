using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Post : MonoBehaviour
{

     public GameObject currentProbe; // Wird im Inspector zugewiesen
    public GameObject targetObject;
    public float moveSpeed = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        // Überprüfe, ob currentProbe im Collider ist
        if (currentProbe != null && other.bounds.Contains(currentProbe.transform.position))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }

            currentProbe.transform.position = transform.position;
        }
    }

    public void OnSolutionCorrect()
    {
        // Hier wird nicht mehr nach currentProbe gesucht, da es im Inspector zugewiesen wird.
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
        rb.constraints = RigidbodyConstraints.None;
    }
}
    