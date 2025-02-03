using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class ScriptButton : MonoBehaviour
{
    public GameObject planetSampleObject;
    public GameObject roomWall;
    public Material newPlanetMaterial;
    public Material defaultPlanetMaterial;
    private AudioSource audioSource;

    
 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonPressed()
    {
        RoomChange();
        SoundFeedback();

    }

    public void RoomChange()
    {
        if (planetSampleObject != null && newPlanetMaterial != null && defaultPlanetMaterial != null)
    {
        Renderer planetRenderer = planetSampleObject.GetComponent<Renderer>();
        Renderer wallRenderer = roomWall.GetComponent<Renderer>();

        if (planetRenderer != null && wallRenderer != null)
        {
            // Überprüfen, ob das neue Material bereits angewendet wurde
            if (!PlanetStateManager.isNewPlanet)
            {
                // Material auf das neue Material setzen (Merkur)
                planetRenderer.material = newPlanetMaterial;
                wallRenderer.material = newPlanetMaterial;

                
            }
            else
            {
                // Material auf das Standardmaterial zurücksetzen (Erde)
                planetRenderer.material = defaultPlanetMaterial;
                wallRenderer.material = defaultPlanetMaterial;

                // DisplayEarth auf kleinen Display ändert sich jeweils zu Display0
            }
        }
    }
    }


    public void SoundFeedback(){
         if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    
    }
