using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
This script enables the grab based Saturn physics and needed canvases

The functions are called on either grabbing or release, giving the objects their specific behaviors. 
*/

public class Saturn_GrabPhysicsScript : MonoBehaviour
{
    public GameObject saturnLampObject;
    public GameObject earthLampObject;   
    public Material greenLampMaterial;


    public GameObject balloon;
    public GameObject canOfBeans;
    public GameObject ball;
    public GameObject canOfWater;


    public AudioSource freezeOfWaterAudio;
    public AudioSource defaultWaterAudio;
    public Material freezingWaterMaterial;
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
            defaultWaterAudio = audioSources[0]; 
            freezeOfWaterAudio = audioSources[1]; 
        }

     
    }


    public void OnGrab(GameObject grabbedObject)
    {
        Renderer saturnLampRenderer = saturnLampObject.GetComponent<Renderer>(); 
        if(saturnLampRenderer != null)
        { 
            Material currentsaturnLampMaterial = saturnLampRenderer.sharedMaterial;



            if(currentsaturnLampMaterial == greenLampMaterial)
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

                        SaturnWaterPhysics();
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

    // Upon release of the object the physical changes are reverted, the displays stay to display the information continiously
    public void onRelease(GameObject grabbedObject)
    {
        Renderer saturnLampRenderer = saturnLampObject.GetComponent<Renderer>(); 
        if(saturnLampRenderer != null)
        { 
            Material currentsaturnLampMaterial = saturnLampRenderer.sharedMaterial;
            
            if(currentsaturnLampMaterial == greenLampMaterial)
            {
                    if (grabbedObject == canOfWater)
                    {
                        
                        SaturnWaterPhysicsRelease();
                    }
            }
        }
    }
   
    public void SaturnWaterPhysics()
    {    
        defaultWaterAudio.Stop(); // Stops the default water audio from playing when grabbing

        if (canOfWater != null &freezingWaterMaterial != null && defaultWaterMaterial != null)
        {
            Renderer waterRenderer = canOfWater.GetComponent<Renderer>();

            if (waterRenderer != null)
            {   
                waterRenderer.material = freezingWaterMaterial; //Changes appearance of the water to simulate frozen water
                if (freezeOfWaterAudio != null)
                {
                    // Debug.Log("sound is playing");
                    freezeOfWaterAudio.Play();
                }
            }
        }
    }

//Reverts changes made in SaturnWaterPhysics()
     public void SaturnWaterPhysicsRelease()
    {    defaultWaterAudio.Play();
        if (canOfWater != null &freezingWaterMaterial != null && defaultWaterMaterial != null)
        {
            Renderer waterRenderer = canOfWater.GetComponent<Renderer>();

            if (waterRenderer != null)
            {
                    waterRenderer.material = defaultWaterMaterial;
                    freezeOfWaterAudio.Stop();
                    defaultWaterAudio.mute = false;
            }
        }
    }

    
            

}
