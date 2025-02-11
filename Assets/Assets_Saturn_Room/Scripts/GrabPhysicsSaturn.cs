using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabPhysicsSaturn : MonoBehaviour
{
    public GameObject saturnLampObject;
    public GameObject earthLampObject;   
    public Material greenLampMaterial;
    //
    public GameObject balloon;
    public GameObject canOfBeans;
    public GameObject ball;
    public GameObject canOfWater;
    //

    public AudioSource freezeOfWaterAudio;
    public Material freezingWaterMaterial;
    public Material defaultWaterMaterial;
    // 
    public GameObject canvasGravity;
    public GameObject canvasPressure;
    public GameObject canvasAtmosphere;
    public GameObject canvasTemperature;
    
    // Start is called before the first frame update
    void Start()
    {
      

       AudioSource[] audioSources = canOfWater.GetComponents<AudioSource>();

        if (audioSources.Length > 1)
        {
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
                
                    // Unterscheiden zwischen den verschiedenen Objekten
                    if (grabbedObject == canOfWater)
                    {
                        // Aktionen für canOfWater
                        SaturnWaterPhysics();

                        GameObject display0 = canvasTemperature.transform.Find("Canvas/Displays/Display0")?.gameObject;
                        GameObject display1 = canvasTemperature.transform.Find("Canvas/Displays/Display1")?.gameObject;

                        if (display0 != null && display1 != null)
                        {
                            display0.SetActive(false); // Display0 deaktivieren
                            display1.SetActive(true);  // Display1 aktivieren
                        }
                    }
                    else if (grabbedObject == canOfBeans)
                    {
                        

                        GameObject display0 = canvasPressure.transform.Find("Canvas/Displays/Display0")?.gameObject;
                        GameObject display1 = canvasPressure.transform.Find("Canvas/Displays/Display1")?.gameObject;

                        if (display0 != null && display1 != null)
                        {
                            display0.SetActive(false); // Display0 deaktivieren
                            display1.SetActive(true);  // Display1 aktivieren
                        }
                    }

                    //Hier Kann deine Logik für die Screens rein @Lisa

                    else if (grabbedObject == ball)
                    {                
                        // Zugriff auf das Display innerhalb des Canvas
                        GameObject display0 = canvasGravity.transform.Find("Canvas/Displays/Display0")?.gameObject;
                        GameObject display1 = canvasGravity.transform.Find("Canvas/Displays/Display1")?.gameObject;

                        if (display0 != null && display1 != null)
                        {
                            display0.SetActive(false); // Display0 deaktivieren
                            display1.SetActive(true);  // Display1 aktivieren
                        }
                    }
                    else if (grabbedObject == balloon)
                    {                
                        // Zugriff auf das Display innerhalb des Canvas
                        GameObject display0 = canvasAtmosphere.transform.Find("Canvas/Displays/Display0")?.gameObject;
                        GameObject display1 = canvasAtmosphere.transform.Find("Canvas/Displays/Display1")?.gameObject;

                        if (display0 != null && display1 != null)
                        {
                            display0.SetActive(false); // Display0 deaktivieren
                            display1.SetActive(true);  // Display1 aktivieren
                        }
                    }
            }

         }
       
    }

    public void onRelease(GameObject grabbedObject){
  // Unterscheiden zwischen den verschiedenen Objekten

   Renderer saturnLampRenderer = saturnLampObject.GetComponent<Renderer>(); 
        if(saturnLampRenderer != null)
        { 
            Material currentsaturnLampMaterial = saturnLampRenderer.sharedMaterial;
            
            if(currentsaturnLampMaterial == greenLampMaterial)
            {
                    if (grabbedObject == canOfWater)
                    {
                        // Aktionen für canOfWater
                        SaturnWaterPhysicsRelease();
                    }
            }
        }
    }
   
    public void SaturnWaterPhysics()
    {    
        if (canOfWater != null &freezingWaterMaterial != null && defaultWaterMaterial != null)
        {
            Renderer waterRenderer = canOfWater.GetComponent<Renderer>();

            if (waterRenderer != null)
            {
                waterRenderer.material = freezingWaterMaterial;
                if (freezeOfWaterAudio != null)
                {
                    Debug.Log("Sound is playing");
                    freezeOfWaterAudio.Play();
                }
            }
        }
    }

     public void SaturnWaterPhysicsRelease()
    {    
        if (canOfWater != null &freezingWaterMaterial != null && defaultWaterMaterial != null)
        {
            Renderer waterRenderer = canOfWater.GetComponent<Renderer>();

            if (waterRenderer != null)
            {
                    waterRenderer.material = defaultWaterMaterial;
                    freezeOfWaterAudio.Stop();
            }
        }
    }

    
            

}
