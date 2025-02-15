using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script enables the grab based venus physics and needed canvases

The functions are called on either grabbing or release, giving the objects their specific behaviors. 
*/

public class Venus_GrabPhysicsScript: MonoBehaviour
{
    public GameObject venusLampObject;
    public GameObject earthLampObject;   
    public Material greenLampMaterial;
 

    public GameObject balloon;
    public GameObject canOfBeans;
    public GameObject ball;
    public GameObject canOfWater;
 

    private Vector3 venusCanScale = new Vector3(-30.0f, 0f, -30.0f);
    public AudioSource canOfBeansAudio; 
    private AudioSource canOfWaterAudio;
    private AudioSource canOfWaterBaseAudio;
    public Material evaporateWaterMaterial;
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
            canOfWaterBaseAudio = audioSources[0]; 
            canOfWaterAudio = audioSources[1]; 
        }
    }


    public void OnGrab(GameObject grabbedObject)
    {
        Renderer venusLampRenderer = venusLampObject.GetComponent<Renderer>(); 
        if(venusLampRenderer != null)
        { 
            Material currentvenusLampMaterial = venusLampRenderer.sharedMaterial;
            
            
            if(currentvenusLampMaterial == greenLampMaterial)
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
                    VenusWaterPhysics();
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
                    VenusBeansPhysics();
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
        Renderer venusLampRenderer = venusLampObject.GetComponent<Renderer>(); 
        if(venusLampRenderer != null)
        { 
            Material currentvenusLampMaterial = venusLampRenderer.sharedMaterial;
            
            if(currentvenusLampMaterial == greenLampMaterial)
            {
                if (grabbedObject == canOfWater)
                {
                    VenusWaterPhysicsRelease();
                }
                else if (grabbedObject == canOfBeans)
                {
                    VenusBeansPhysicsRelease();
                }
            }
        }
    }


    private void VenusBeansPhysics()
    {
        if (canOfBeans != null)
        {
            canOfBeans.transform.localScale += venusCanScale;
            if (canOfBeansAudio != null)
            {
                canOfBeansAudio.Play();
            }
        }
    }


    private void VenusWaterPhysics()
    {    
        canOfWaterBaseAudio.Stop(); 
        if (canOfWater != null && evaporateWaterMaterial != null && defaultWaterMaterial != null)
        {
            Renderer waterRenderer = canOfWater.GetComponent<Renderer>();

            if (waterRenderer != null)
            {
                waterRenderer.material = evaporateWaterMaterial;
                if (canOfWaterAudio != null)
                {
                    // Debug.Log("soud is playing");
                    canOfWaterAudio.Play();
                }
            }
        }
    }


    private void VenusBeansPhysicsRelease()
    {
        canOfBeans.transform.localScale -= venusCanScale;
        if (canOfBeansAudio != null)
        {
            canOfBeansAudio.Play();
        }
    }


    public void VenusWaterPhysicsRelease()
    {    
        canOfWaterBaseAudio.Play();
        if (canOfWater != null && evaporateWaterMaterial != null && defaultWaterMaterial != null)
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
