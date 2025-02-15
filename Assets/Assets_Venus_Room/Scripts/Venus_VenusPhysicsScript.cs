using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script enables the Venus physics and needed canvases

it is generally attached to the buttons in each room, enabling venus physics once pressed. This is done by tracking the attached lamps and their materials
A lamp delay has been implemented to prevent unwanted sound behavior from dependent objects
*/

public class Venus_VenusPhysicsScript : MonoBehaviour
{
    public Material redLampMaterial;
    public Material greenLampMaterial;
    public GameObject venusLampObject;
    public GameObject earthLampObject;   

    private float balloonVolume = 0.5f; 
    public GameObject balloon; 
    private Rigidbody balloonRb;
 

    
    private float venusGravity = -8.87f;
    private float airDensityVenus = 67.0f;
 

    public GameObject canvasGravity;
    public GameObject canvasAtmosphere;
    public GameObject canvasPressure;
    public GameObject canvasTemperature;
  

void Awake()
{
    balloonRb = balloon.GetComponent<Rigidbody>();
}

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
       VenusPhysicsChange();
        
    }

    public void VenusPhysicsChange()
    {

        Renderer venusLampRenderer = venusLampObject.GetComponent<Renderer>(); 
        Renderer earthLampRenderer = earthLampObject.GetComponent<Renderer>();   

        if(venusLampRenderer != null && earthLampRenderer != null)
        { 
           
            Material currentvenusLampMaterial = venusLampRenderer.sharedMaterial;
            
          
            if(currentvenusLampMaterial == redLampMaterial)
            {
                earthLampRenderer.material = redLampMaterial;
                
                Physics.gravity = new Vector3(0, venusGravity, 1); 
                    
                

             
                UpdateCanvasDisplay(canvasGravity);
                UpdateCanvasDisplay(canvasAtmosphere);
                UpdateCanvasDisplay(canvasPressure);
                UpdateCanvasDisplay(canvasTemperature);

              
                StartCoroutine(VenusLampDelay());
            }
            else{

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

              
                if (displayEarth != null) displayEarth.SetActive(false);
                if (display0 != null) display0.SetActive(true);
                if (display1 != null) display1.SetActive(false);

            
            }
        }
    }

    private void ApplyBalloonBuoyancy()
    { 
        if (balloonRb != null)
        {   
            float buoyancyForce = 0f;
            Renderer venusLampRenderer = venusLampObject.GetComponent<Renderer>();   
            Material currentVenusLampMaterial = venusLampRenderer.sharedMaterial;
            
  
            if (currentVenusLampMaterial == greenLampMaterial)
            {
                buoyancyForce = airDensityVenus * balloonVolume * Mathf.Abs(venusGravity); 
            }

       
            if (buoyancyForce > 0f)
            {
                balloonRb.AddForce(Vector3.up * buoyancyForce);
            }
        }
    }

    public IEnumerator VenusLampDelay()
    {   Renderer venusLampRenderer = venusLampObject.GetComponent<Renderer>();
        yield return new WaitForSeconds(0.5f); 
        venusLampRenderer.material = greenLampMaterial;
    }


}
