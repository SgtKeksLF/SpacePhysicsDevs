using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScriptDrilling : MonoBehaviour
{
    public XRBaseController leftController;  // Linker Controller
    public XRBaseController rightController; // Rechter Controller
    private bool leftControllerInZone = false;
    private bool rightControllerInZone = false;
 
    // Update is called once per frame
    private bool bothControllersInZone = false;  // Zustand für beide Controller in der Zone

    // Update ist weiterhin aktiv, um den Status zu überwachen
    void Update()
    {
        materialCheck();
        
        if (bothControllersInZone && materialCorrect)
        {
            // Starte den Bohrvorgang nur einmal
            StartCoroutine(StartDrilling());
            bothControllersInZone = false;  // Zustand zurücksetzen
        }
    }


    public Material targetMaterial;
    public GameObject planetSample;
    public bool materialCorrect = false;
    public void materialCheck()
    {

        Renderer objectRenderer = planetSample.GetComponent<Renderer>();
        if (objectRenderer != null && objectRenderer.material.name == targetMaterial.name + " (Instance)")
        {
            // Code ausführen, wenn das Material übereinstimmt
           
            materialCorrect = true;
        }
        else
        {
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("A"))
        {
           
            leftControllerInZone = true;
            CheckBothControllersInZone();
        }
        if (other.CompareTag("B"))
        {
            
            rightControllerInZone = true;
            CheckBothControllersInZone();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("A"))
        {
          
            leftControllerInZone = false;
            bothControllersInZone = false;  // Beide Controller müssen gleichzeitig in der Zone sein
        }
        if (other.CompareTag("B"))
        {
            
            rightControllerInZone = false;
            bothControllersInZone = false;  // Beide Controller müssen gleichzeitig in der Zone sein
        }
    }

    // Überprüft, ob beide Controller in der Zone sind
    private void CheckBothControllersInZone()
    {
        if (leftControllerInZone && rightControllerInZone)
        {
            
            bothControllersInZone = true;
        }
    }

    public ParticleSystem drillingParticleSystem;
    public Vector3 particleSpawnPoint = new Vector3(0, 0, 0);
    public GameObject probePrefab;
    public Vector3 spawnPoint = new Vector3(1, 1, 2);
    Quaternion newParticleRotation = Quaternion.Euler(215, 0, 0);
    public Quaternion spawnRotation = Quaternion.identity;

    private IEnumerator StartDrilling()
    {
        
        TriggerHapticFeedback(); // Haptisches Feedback an beide Controller senden
        ParticleSystem particlePlay = Instantiate(drillingParticleSystem, particleSpawnPoint, newParticleRotation);
        particlePlay.Play();
        yield return new WaitForSeconds(1.0f); // Wartezeit, um den Bohrvorgang zu simulieren

        // Erstelle das Bohrprobe-Objekt an der angegebenen Position
        GameObject spawnedSample = Instantiate(probePrefab, spawnPoint, spawnRotation);
      

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
        {
           
            leftController.SendHapticImpulse(1.0f, 2.0f); // Stärke 0.5, Dauer 1 Sekunde
            
        }

        if (rightController != null)
        {
            
            rightController.SendHapticImpulse(1.0f, 2.0f);
        }
           
    }
}
