using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;


public class ScriptDrilling : MonoBehaviour
{
   
 
    private bool drillInZone = false;
    public bool hasBeenDrilled = false;

 

    void Update()
    {
        materialCheck();
        
        if (drillInZone && planetCorrect)
        {
            
            StartCoroutine(StartDrilling());
            drillInZone = false; 
        }
    }

    void Awake(){
     
    }

    public Material greenLampMaterial;
    public GameObject mercuryLampObject;
  

    public bool planetCorrect = false;
    public void materialCheck()
    {

        Renderer mercuryLampRenderer = mercuryLampObject.GetComponent<Renderer>();
        Material currentMercuryLampMaterial = mercuryLampRenderer.sharedMaterial;
        if(currentMercuryLampMaterial == greenLampMaterial)
        {
            
            planetCorrect = true;
        }
        else {
            planetCorrect = false;
        }
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drill"))

            {
            
            drillInZone = true;
            hasBeenDrilled = true;
            }
      
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("A"))
        {
            drillInZone = false;
        }
       
    }


 

    public ParticleSystem drillingParticleSystem;
    public Quaternion newParticleRotation = Quaternion.Euler(215, 0, 0);
    public GameObject particleSpawnTargetObject;

    public GameObject sample;
    public GameObject spawnPointObject;

    public Quaternion spawnRotation = Quaternion.Euler(0, 90, 0);
    private AudioSource audioSource;
 

    private IEnumerator StartDrilling()
    {
       
        particleTrigger();
        playDrillingSound();
        yield return new WaitForSeconds(1.0f); 
        Vector3 targetPosition = spawnPointObject.transform.position;
        sample.transform.position = targetPosition;
    }


    private void particleTrigger(){
        Vector3 targetPosition = particleSpawnTargetObject.transform.position;
        ParticleSystem particlePlay = Instantiate(drillingParticleSystem, targetPosition, newParticleRotation);
        particlePlay.gameObject.SetActive(true);
        particlePlay.Play();
        
    }

    private void playDrillingSound(){
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("Keine AudioSource auf diesem Objekt gefunden!");
        }
         else if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
