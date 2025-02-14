using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mars_GrabPhysics : MonoBehaviour
{
    public GameObject marsLampObject;
    public GameObject earthLampObject;   
    public Material greenLampMaterial;
    //
    public GameObject balloon;
    public GameObject canOfBeans;
    public GameObject ball;
    public GameObject canOfWater;
    //
    private Vector3 marsCanScale = new Vector3(-25.0f, 0f, -25.0f);
    public AudioSource canOfBeansAudio; 
    // 
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
                
                    // Unterscheiden zwischen den verschiedenen Objekten
                    if (grabbedObject == canOfWater)
                    {

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
                        MarsBeansPhysics();

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

   Renderer marsLampRenderer = marsLampObject.GetComponent<Renderer>(); 
        if(marsLampRenderer != null)
        { 
            Material currentmarsLampMaterial = marsLampRenderer.sharedMaterial;
            
            if(currentmarsLampMaterial == greenLampMaterial)
            {
                   
                    if (grabbedObject == canOfBeans)
                    {
                        // Aktionen für canOfBeans
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
