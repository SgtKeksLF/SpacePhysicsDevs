using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
This script enables the normal earth physics and needed canvases

it is generally attached to the buttons in each room, enabling earth physics by default and once pressed. This is done by tracking the attached lamps and their materials
A lamp delay has been implemented to prevent unwanted sound behavior from dependent objects
*/

public class Global_EarthPhysicsScript : MonoBehaviour
{
    public Material redLampMaterial;
    public Material greenLampMaterial;
    public GameObject nonEarthPlanetLampObject;
    public GameObject earthLampObject;   

    
    private float earthGravity = -9.8f;
    private float airDensityEarth = 1.225f; 


    public GameObject balloon; 
    private float balloonVolume = 0.5f; 
    private Rigidbody balloonRb;
 

    
    public GameObject canvasGravity;
    public GameObject canvasAtmosphere;
    public GameObject canvasPressure;
    public GameObject canvasTemperature;
 


    void Start()
    {
        
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

 
    public void OnButtonPressed()
    {
       EarthPhysicsChange();
        
    }
    
    public void EarthPhysicsChange()
    {   
        
        Renderer nonEarthPlanetLampRenderer = nonEarthPlanetLampObject.GetComponent<Renderer>(); 
        Renderer earthLampRenderer = earthLampObject.GetComponent<Renderer>();    
        if (earthLampRenderer != null)
        {
            Material currentEarthLampMaterial = earthLampRenderer.sharedMaterial;
            
            if(currentEarthLampMaterial == redLampMaterial)
            {
                nonEarthPlanetLampRenderer.material = redLampMaterial;
                Physics.gravity = new Vector3(0, earthGravity, 0);  
               

                UpdateCanvasDisplay(canvasGravity);
                UpdateCanvasDisplay(canvasAtmosphere);
                UpdateCanvasDisplay(canvasPressure);
                UpdateCanvasDisplay(canvasTemperature);

           
                StartCoroutine(EarthLampDelay());
            }
        }
      
    }

    
   
   private void ApplyBalloonBuoyancy()
    {
       if (balloonRb != null)
        {      
 
            float buoyancyForce = 0f;
            Renderer earthLampRenderer = earthLampObject.GetComponent<Renderer>();   
            Material currentEarthLampMaterial = earthLampRenderer.sharedMaterial;

          
            if (currentEarthLampMaterial == greenLampMaterial)  
            {
            
                buoyancyForce = airDensityEarth * balloonVolume * Mathf.Abs(earthGravity); 
            }
            else  
            {
                buoyancyForce = 0f;  
            }

         
            if (buoyancyForce > 0f)
            {
                balloonRb.AddForce(Vector3.up * buoyancyForce);
            }
        }
    }

    private void UpdateCanvasDisplay(GameObject canvas)
    {
        if (canvas != null)
        {
            Transform displayParent = canvas.transform.Find("Canvas");
            if (displayParent != null)
            {
            
                GameObject displayEarth = displayParent.Find("Displays/DisplayEarth")?.gameObject;
                GameObject display0 = displayParent.Find("Displays/Display0")?.gameObject;
                GameObject display1 = displayParent.Find("Displays/Display1")?.gameObject;

              
                if (displayEarth != null) displayEarth.SetActive(true);
                if (display0 != null) display0.SetActive(false);
                if (display1 != null) display1.SetActive(false);

            
            }
        }
    }

    public IEnumerator EarthLampDelay()
    {   Renderer earthLampRenderer = earthLampObject.GetComponent<Renderer>();
        yield return new WaitForSeconds(0.3f); 
        earthLampRenderer.material = greenLampMaterial;
    }
    
}
   
    
