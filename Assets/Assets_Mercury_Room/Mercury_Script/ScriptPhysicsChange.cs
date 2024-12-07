using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class ScriptPhysicsChange : MonoBehaviour
{
    // Die folgenden Variablen sind für die Physik selbst
    private float earthGravity = -9.8f;
    private float mercuryGravity = -3.9f;
    
    public GameObject balloon; // Ballonobjekt

    // Can of Beans Variablen
    public GameObject canOfBeans;
    private Vector3 mercuryCanScale = new Vector3(+2.0f, 0.0f, 0.0f);
    public AudioSource pickupBeanCanAudio;     
    public AudioSource canOfBeansAudio; 

    // Water Can Variablen
    public GameObject canOfWater;
    public AudioSource pickupWaterCanAudio;   
    private AudioSource canOfWaterAudio;
    public Material newWaterMaterial;
    public Material defaultWaterMaterial;

    // Ballon-Physik
    private float balloonVolume = 0.5f; 
    private float airDensityEarth = 1.225f; 


    private Rigidbody balloonRb;

    void Start()
    {
        
        AudioSource[] audioSources = canOfWater.GetComponents<AudioSource>();
        
      
        if (audioSources.Length > 1)
        {
            canOfWaterAudio = audioSources[1]; 
        }

      
        if (balloon != null)
        {
            balloonRb = balloon.GetComponent<Rigidbody>();
            if (balloonRb == null)
            {
                balloonRb = balloon.AddComponent<Rigidbody>();
            }
        }
    }
       void FixedUpdate()
    {
        ApplyBalloonBuoyancy();
    }

    // Update wird einmal pro Frame aufgerufen
    void Update()
    {
        
    }

    public void OnButtonPressed()
    {
        GravityPhysicsChange();
        
    }

    public void GravityPhysicsChange()
    {
        // Zuerst den Zustand umschalten
    PlanetStateManager.TogglePlanetState();
       // Setze die Schwerkraft des Planeten entsprechend dem Zustand
    if (PlanetStateManager.isNewPlanet)
    {
        Debug.Log("Mercury physics");
        Physics.gravity = new Vector3(0, mercuryGravity, 1);  // Merkur-Schwerkraft
    }
    else
    {
        Debug.Log("Earth physics");
        Physics.gravity = new Vector3(0, earthGravity, 0);  // Erde-Schwerkraft
    }

    // Anwenden der spezifischen Physik für Objekte
    CanOfBeansPhysics();
    CanOfWaterPhysics();


     
    }

    public void CanOfBeansPhysics()
    {
        if (PlanetStateManager.isNewPlanet)
        {
            canOfBeans.transform.localScale += mercuryCanScale;
        }
        else
        {
            canOfBeans.transform.localScale -= mercuryCanScale;
        }

        if (canOfBeansAudio != null)
        {
            Debug.Log("Sound is playing");
            canOfBeansAudio.Play();
        }
    }

    public void CanOfWaterPhysics()
    {    
        if (canOfWater != null && newWaterMaterial != null && defaultWaterMaterial != null)
        {
            Renderer waterRenderer = canOfWater.GetComponent<Renderer>();

            if (waterRenderer != null)
            {
                if (PlanetStateManager.isNewPlanet)
                {
                    waterRenderer.material = newWaterMaterial;
                    if (canOfWaterAudio != null)
                    {
                        Debug.Log("Sound is playing");
                        canOfWaterAudio.Play();
                    }
                }
                else
                {
                    waterRenderer.material = defaultWaterMaterial;
                    canOfWaterAudio.Stop();
                }
            }
        }
    }

    // Berechnet und wendet die Auftriebskraft auf den Ballon an
    private void ApplyBalloonBuoyancy()
    {
       if (balloonRb != null)
    {   
        Debug.Log("Ballon if erreicht");
        float buoyancyForce = 0f;

        // Auftrieb nur auf der Erde anwenden, wenn isNewPlanet false ist
        if (!PlanetStateManager.isNewPlanet)  // Erde
        {
            Debug.Log("Ballon sollte auf der Erde schweben");
            buoyancyForce = airDensityEarth * balloonVolume * Mathf.Abs(earthGravity);
        }
        else  // Merkur
        {
            buoyancyForce = 0f;  // Kein Auftrieb auf Merkur
        }

        // Wenn Auftriebskraft vorhanden, den Ballon anheben
        if (buoyancyForce > 0f)
        {
            Debug.Log("Ballon sollte schweben");
            balloonRb.AddForce(Vector3.up * buoyancyForce);
        }
    }
    }
}
