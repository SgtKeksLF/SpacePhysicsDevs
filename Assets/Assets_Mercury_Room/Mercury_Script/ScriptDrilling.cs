using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;


public class ScriptDrilling : MonoBehaviour
{
    public XRBaseController leftController;  
    public XRBaseController rightController;

    public InputAction leftHapticAction, rightHapticAction;
    private bool leftControllerInZone = false;
    private bool rightControllerInZone = false;
 
    private bool bothControllersInZone = false;

    public void OnEnable()
    {
        leftHapticAction.Enable();
        rightHapticAction.Enable();
    }

    public void OnDisable()
    {
        leftHapticAction.Disable();
        rightHapticAction.Disable();
    }

    void Update()
    {
        materialCheck();
        
        if (bothControllersInZone && materialCorrect)
        {
            
            StartCoroutine(StartDrilling());
            bothControllersInZone = false; 
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
            
           
            materialCorrect = true;
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
            bothControllersInZone = false;  
        }
        if (other.CompareTag("B"))
        {
            
            rightControllerInZone = false;
            bothControllersInZone = false;  
        }
    }


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
    public Quaternion spawnRotation = Quaternion.Euler(0, 90, 0);

    private IEnumerator StartDrilling()
    {
        TriggerHapticFeedback(); 
        ParticleSystem particlePlay = Instantiate(drillingParticleSystem, particleSpawnPoint, newParticleRotation);
        particlePlay.Play();
        yield return new WaitForSeconds(1.0f); 

        GameObject spawnedSample = Instantiate(probePrefab, spawnPoint, spawnRotation);
    }

    private void TriggerHapticFeedback()
    {
        
        if (leftController != null)
        {
            Debug.Log("Linker Impuls");
           
            if (!leftController.SendHapticImpulse(1.0f, 5.0f))
            {
                Debug.Log("No impulse");
            }
            
        }

        if (rightController != null)
        {
            Debug.Log("Rechter Impuls");
            rightController.SendHapticImpulse(1.0f, 2.0f);
        }
           
    }
}
