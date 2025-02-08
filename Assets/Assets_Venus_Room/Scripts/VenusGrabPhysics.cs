using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusGrabPhysics : MonoBehaviour
{
    public GameObject venusLampObject;
    public GameObject earthLampObject;   
    public Material greenLampMaterial;
    //
    public GameObject balloon;
    public GameObject canOfBeans;
    public GameObject ball;
    public GameObject canOfWater;
    //
    private Vector3 venusCanScale = new Vector3(-30.0f, -30.0f, -30.0f);
    public AudioSource canOfBeansAudio; 
    private AudioSource canOfWaterAudio;
    public Material evaporateWaterMaterial;
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
        Renderer venusLampRenderer = venusLampObject.GetComponent<Renderer>(); 
        if(venusLampRenderer != null)
        { 
            Material currentvenusLampMaterial = venusLampRenderer.sharedMaterial;
            
            if(currentvenusLampMaterial == greenLampMaterial)
            {
                
                    // Unterscheiden zwischen den verschiedenen Objekten
                    if (grabbedObject == canOfWater)
                    {
                        // Aktionen für canOfWater
                        VenusWaterPhysics();

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
                        VenusBeansPhysics();

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

   Renderer venusLampRenderer = venusLampObject.GetComponent<Renderer>(); 
        if(venusLampRenderer != null)
        { 
            Material currentvenusLampMaterial = venusLampRenderer.sharedMaterial;
            
            if(currentvenusLampMaterial == greenLampMaterial)
            {
                    if (grabbedObject == canOfWater)
                    {
                        // Aktionen für canOfWater
                        VenusWaterPhysicsRelease();
                    }
                    else if (grabbedObject == canOfBeans)
                    {
                        // Aktionen für canOfBeans
                        VenusBeansPhysicsRelease();
                    }

                    //Hier ggf screenlogic? idk

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
        if (canOfWater != null && evaporateWaterMaterial != null && defaultWaterMaterial != null)
        {
            Renderer waterRenderer = canOfWater.GetComponent<Renderer>();

            if (waterRenderer != null)
            {
                waterRenderer.material = evaporateWaterMaterial;
                if (canOfWaterAudio != null)
                {
                    Debug.Log("Sound is playing");
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
