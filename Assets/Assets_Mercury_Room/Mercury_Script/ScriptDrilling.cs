using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScriptDrilling : MonoBehaviour
{
    public XRBaseController leftController;  // Linker Controller
    public XRBaseController rightController; // Rechter Controller
    private bool leftControllerInZone = false;
    private bool rightControllerInZone = false;

    public GameObject probePrefab;
    public Vector3 spawnPoint = new Vector3(1, 1, 2);
    public Quaternion spawnRotation = Quaternion.identity;

    // Update is called once per frame
    private bool bothControllersInZone = false;  // Zustand für beide Controller in der Zone

    // Update ist weiterhin aktiv, um den Status zu überwachen
    void Update()
    {
        if (bothControllersInZone)
        {
            // Starte den Bohrvorgang nur einmal
            StartCoroutine(StartDrilling());
            bothControllersInZone = false;  // Zustand zurücksetzen
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("A"))
        {
            Debug.Log("Left Controller drinnen");
            leftControllerInZone = true;
            CheckBothControllersInZone();
        }
        if (other.CompareTag("B"))
        {
            Debug.Log("Right Controller drinnen");
            rightControllerInZone = true;
            CheckBothControllersInZone();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("A"))
        {
            Debug.Log("Left Controller draußen");
            leftControllerInZone = false;
            bothControllersInZone = false;  // Beide Controller müssen gleichzeitig in der Zone sein
        }
        if (other.CompareTag("B"))
        {
            Debug.Log("Right Controller draußen");
            rightControllerInZone = false;
            bothControllersInZone = false;  // Beide Controller müssen gleichzeitig in der Zone sein
        }
    }

    // Überprüft, ob beide Controller in der Zone sind
    private void CheckBothControllersInZone()
    {
        if (leftControllerInZone && rightControllerInZone)
        {
            Debug.Log("Beide Controller in der Zone!");
            bothControllersInZone = true;
        }
    }


    private IEnumerator StartDrilling()
    {
        Debug.Log("StartDrilling Coroutine gestartet");
        TriggerHapticFeedback(); // Haptisches Feedback an beide Controller senden
        yield return new WaitForSeconds(1.0f); // Wartezeit, um den Bohrvorgang zu simulieren

        // Erstelle das Bohrprobe-Objekt an der angegebenen Position
        GameObject spawnedSample = Instantiate(probePrefab, spawnPoint, spawnRotation);
        Debug.Log("Bohrprobe erstellt: " + spawnedSample.name);

        // Füge dem Bohrprobenobjekt Interaktivität und Physik hinzu
        if (spawnedSample.GetComponent<XRGrabInteractable>() == null)
        {
            spawnedSample.AddComponent<XRGrabInteractable>();
        }

        if (spawnedSample.GetComponent<Rigidbody>() == null)
        {
            spawnedSample.AddComponent<Rigidbody>();
        }

        if (spawnedSample.GetComponent<BoxCollider>() == null)
        {
            spawnedSample.AddComponent<BoxCollider>();
        }

        // Gib das Objekt nach dem Bohrvorgang zurück, um keine ungewollten Instanzen zu erstellen
    }

    private void TriggerHapticFeedback()
    {
        if (leftController != null)
            leftController.SendHapticImpulse(0.5f, 1.0f); // Stärke 0.5, Dauer 1 Sekunde

        if (rightController != null)
            rightController.SendHapticImpulse(0.5f, 1.0f);
    }
}
