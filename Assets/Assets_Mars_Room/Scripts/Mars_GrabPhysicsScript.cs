using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script enables the grab based Mars physics and needed canvases

The functions are called on either grabbing or release, giving the objects their specific behaviors. 
*/

public class Mars_GrabPhysicsScript : MonoBehaviour
{
    public GameObject marsLampObject;
    public GameObject earthLampObject;   
    public Material greenLampMaterial;


    public GameObject balloon;
    public GameObject canOfBeans;
    public GameObject ball;
    public GameObject canOfWater;



    private Vector3 marsCanScale = new Vector3(-25.0f, 0f, -25.0f);
    public AudioSource canOfBeansAudio; 


    public GameObject canvasGravity;
    public GameObject canvasPressure;
    public GameObject canvasAtmosphere;
    public GameObject canvasTemperature;



    public void OnGrab(GameObject grabbedObject)
    {
        Renderer marsLampRenderer = marsLampObject.GetComponent<Renderer>(); 

        if(marsLampRenderer != null)
        { 
            Material currentmarsLampMaterial = marsLampRenderer.sharedMaterial;

      

            if(currentmarsLampMaterial == greenLampMaterial)
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

                         MarsBeansPhysics();
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
        Renderer marsLampRenderer = marsLampObject.GetComponent<Renderer>(); 
        if(marsLampRenderer != null)
        { 
            Material currentmarsLampMaterial = marsLampRenderer.sharedMaterial;
            
            if(currentmarsLampMaterial == greenLampMaterial)
            {
                   
                    if (grabbedObject == canOfBeans)
                    {
                      
                        MarsBeansPhysicsRelease();
                    }
            }
        }
    }

    private void MarsBeansPhysics()
    {
        if (canOfBeans != null)
        {
            canOfBeans.transform.localScale += marsCanScale;
   
            if (canOfBeansAudio != null)
            {
                canOfBeansAudio.Play();
            }
        }
    }

     private void MarsBeansPhysicsRelease()
    {
        canOfBeans.transform.localScale -= marsCanScale;
        if (canOfBeansAudio != null)
            {
               canOfBeansAudio.Play();
            }
    }

}
