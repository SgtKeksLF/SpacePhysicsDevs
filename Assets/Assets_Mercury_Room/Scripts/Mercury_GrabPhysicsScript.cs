using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
This script enables the grab based Mercury physics and needed canvases

The functions are called on either grabbing or release, giving the objects their specific behaviors. 
*/

public class Mercury_GrabPhysicsScript : MonoBehaviour
{

    public GameObject mercuryLampObject;
    public GameObject earthLampObject;   
    public Material greenLampMaterial;


    public GameObject balloon;
    public GameObject canOfBeans;
    public GameObject ball;
    public GameObject canOfWater;


    private Vector3 mercuryCanScale = new Vector3(+10.0f, 0f, +10.0f);
    public AudioSource canOfBeansAudio; 
    private AudioSource canOfWaterAudio;
    public Material newWaterMaterial;
    public Material defaultWaterMaterial;


    public GameObject canvasGravity;
    public GameObject canvasPressure;
    public GameObject canvasAtmosphere;
    public GameObject canvasTemperature;


    void Start()
    {
      
        AudioSource[] audioSources = canOfWater.GetComponents<AudioSource>();

        if (audioSources.Length > 1)
        {
            canOfWaterAudio = audioSources[1]; 
        }
    }

    
    public void OnGrab(GameObject grabbedObject)
    {
        Renderer mercuryLampRenderer = mercuryLampObject.GetComponent<Renderer>(); 
        if (mercuryLampRenderer != null)
        { 
            Material currentMercuryLampMaterial = mercuryLampRenderer.sharedMaterial;
            
         
            if (currentMercuryLampMaterial == greenLampMaterial)
            {
              
                if (grabbedObject == canOfWater)
                {
                    
                 
                    GameObject display0 = canvasTemperature.transform.Find("Canvas/Displays/Display0")?.gameObject;
                    GameObject display1 = canvasTemperature.transform.Find("Canvas/Displays/Display1")?.gameObject;

                  
                    if (display0 != null && display1 != null)
                    {
                        display0.SetActive(false); 
                        display1.SetActive(true);
                    }
                    MercuryWaterPhysics();
                }
                else if (grabbedObject == canOfBeans)
                {
                    
                   
                   
                    GameObject display0 = canvasPressure.transform.Find("Canvas/Displays/Display0")?.gameObject;
                    GameObject display1 = canvasPressure.transform.Find("Canvas/Displays/Display1")?.gameObject;

                    
                    if (display0 != null && display1 != null)
                    {
                        display0.SetActive(false); 
                        display1.SetActive(true);
                    }

                    MercuryBeansPhysics();
                }

                
                else if (grabbedObject == ball)
                {    
                
                    GameObject display0 = canvasGravity.transform.Find("Canvas/Displays/Display0")?.gameObject;
                    GameObject display1 = canvasGravity.transform.Find("Canvas/Displays/Display1")?.gameObject;

                
                    if (display0 != null && display1 != null)
                    {
                        display0.SetActive(false); 
                        display1.SetActive(true);
                    }
                }
                else if (grabbedObject == balloon)
                {     
                 
                    GameObject display0 = canvasAtmosphere.transform.Find("Canvas/Displays/Display0")?.gameObject;
                    GameObject display1 = canvasAtmosphere.transform.Find("Canvas/Displays/Display1")?.gameObject;

                  
                    if (display0 != null && display1 != null)
                    {
                        display0.SetActive(false); 
                        display1.SetActive(true);
                    }
                }
            }
        }
    }


    public void onRelease(GameObject grabbedObject)
    {
        Renderer mercuryLampRenderer = mercuryLampObject.GetComponent<Renderer>(); 
        if (mercuryLampRenderer != null)
        { 
            Material currentMercuryLampMaterial = mercuryLampRenderer.sharedMaterial;
            
            if (currentMercuryLampMaterial == greenLampMaterial)
            {
                if (grabbedObject == canOfWater)
                {
                    
                    MercuryWaterPhysicsRelease();
                }
                else if (grabbedObject == canOfBeans)
                {
                    
                    MercuryBeansPhysicsRelease();
                }
            }
        }
    }


    public void MercuryBeansPhysics()
    {
        if (canOfBeans != null)
        {
            canOfBeans.transform.localScale += mercuryCanScale;

            if (canOfBeansAudio != null)
            {
                canOfBeansAudio.Play();
            }
        }
    }

    public void MercuryWaterPhysics()
    {    
        if (canOfWater != null && newWaterMaterial != null && defaultWaterMaterial != null)
        {
            Renderer waterRenderer = canOfWater.GetComponent<Renderer>();

            if (waterRenderer != null)
            {
                waterRenderer.material = newWaterMaterial;
                if (canOfWaterAudio != null)
                {
                    canOfWaterAudio.Play();
                }
            }
        }
    }

    public void MercuryBeansPhysicsRelease()
    {
        canOfBeans.transform.localScale -= mercuryCanScale;
        if (canOfBeansAudio != null)
        {
            canOfBeansAudio.Play();
        }
    }

    public void MercuryWaterPhysicsRelease()
    {    
        if (canOfWater != null && newWaterMaterial != null && defaultWaterMaterial != null)
        {
            Renderer waterRenderer = canOfWater.GetComponent<Renderer>();

            if (waterRenderer != null)
            {
                waterRenderer.material = defaultWaterMaterial;
                canOfWaterAudio.Stop();
            }
        }
    }
}
