using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabPhysicsMercury : MonoBehaviour
{
    public GameObject mercuryLampObject;
    public GameObject earthLampObject;   
    public Material greenLampMaterial;
    //
    public GameObject balloon;
    public GameObject canOfBeans;
    public GameObject ball;
    public GameObject canOfWater;
    //
    private Vector3 mercuryCanScale = new Vector3(+10.0f, 0f, +10.0f);
    public AudioSource canOfBeansAudio; 
    private AudioSource canOfWaterAudio;
    public Material newWaterMaterial;
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
            canOfWaterAudio = audioSources[1]; 
        }

      
     
    }

  

    // Update is called once per frame
    void Update()
    {
        
    }





    public void OnGrab(GameObject grabbedObject)
    {
        Renderer mercuryLampRenderer = mercuryLampObject.GetComponent<Renderer>(); 
        if(mercuryLampRenderer != null)
        { 
            Material currentMercuryLampMaterial = mercuryLampRenderer.sharedMaterial;
            
            if(currentMercuryLampMaterial == greenLampMaterial)
            {
                
                    // Unterscheiden zwischen den verschiedenen Objekten
                    if (grabbedObject == canOfWater)
                    {
                        // Aktionen für canOfWater
                        MercuryWaterPhysics();

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
                        // Aktionen für canOfBeans
                        MercuryBeansPhysics();

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

   Renderer mercuryLampRenderer = mercuryLampObject.GetComponent<Renderer>(); 
        if(mercuryLampRenderer != null)
        { 
            Material currentMercuryLampMaterial = mercuryLampRenderer.sharedMaterial;
            
            if(currentMercuryLampMaterial == greenLampMaterial)
            {
                    if (grabbedObject == canOfWater)
                    {
                        // Aktionen für canOfWater
                        MercuryWaterPhysicsRelease();
                    }
                    else if (grabbedObject == canOfBeans)
                    {
                        // Aktionen für canOfBeans
                        MercuryBeansPhysicsRelease();
                    }

                    //Hier ggf screenlogic? idk

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
                    Debug.Log("Sound is playing");
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
