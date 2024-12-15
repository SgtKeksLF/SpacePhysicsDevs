using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;


public class ScriptDrilling : MonoBehaviour
{
   
 
    private bool drillInZone = false;

 

    void Update()
    {
        materialCheck();
        
        if (drillInZone && materialCorrect)
        {
            
            StartCoroutine(StartDrilling());
            drillInZone = false; 
        }
    }

    void Awake(){
        spawnPoint = targetSpawnObject.position;
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
        if (other.CompareTag("Drill"))
        {
          drillInZone = true;
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
    public Vector3 particleSpawnPoint = new Vector3(0, 0, 0);
    public GameObject probePrefab;
    public Vector3 spawnPoint;
    public Transform targetSpawnObject;
    public Quaternion newParticleRotation = Quaternion.Euler(215, 0, 0);
    public Quaternion spawnRotation = Quaternion.Euler(0, 90, 0);
    private AudioSource audioSource;

    private IEnumerator StartDrilling()
    {
       
        particleTrigger();
        playDrillingSound();
        yield return new WaitForSeconds(1.0f); 
        GameObject spawnedSample = Instantiate(probePrefab, spawnPoint, spawnRotation);
    }


    private void particleTrigger(){
        ParticleSystem particlePlay = Instantiate(drillingParticleSystem, particleSpawnPoint, newParticleRotation);
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
