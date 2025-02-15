using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script enables the normal earth physics and needed canvases

it is generally attached to the buttons in each room, enabling earth physics by default and once pressed. This is done by tracking the attached lamps and their materials
A lamp delay has been implemented to prevent unwanted sound behavior from dependent objects
*/

public class Mars_MarsPhysicsScript : MonoBehaviour
{
    public Material redLampMaterial;
    public Material greenLampMaterial;
    public GameObject marsLampObject;
    public GameObject earthLampObject;   


    private float balloonVolume = 0.5f; 
    public GameObject balloon; 
    private Rigidbody balloonRb;
    

    private float marsGravity = -3.72f;  
    private float airDensityMars = 0.02f;  
    private float atmosphereAddition = 0.8f; 

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
       marsPhysicsChange();
        
    }

    public void marsPhysicsChange()
    {
        
        Renderer marsLampRenderer = marsLampObject.GetComponent<Renderer>(); 
        Renderer earthLampRenderer = earthLampObject.GetComponent<Renderer>();    

        if(marsLampRenderer != null && earthLampRenderer != null)
        { 
            
            Material currentmarsLampMaterial = marsLampRenderer.sharedMaterial;
            
          
            if(currentmarsLampMaterial == redLampMaterial)
            {  
                earthLampRenderer.material = redLampMaterial;
              
                Physics.gravity = new Vector3(0, marsGravity, 1);  
                   
                     
                

              
                UpdateCanvasDisplay(canvasGravity);
                UpdateCanvasDisplay(canvasAtmosphere);
                UpdateCanvasDisplay(canvasPressure);
                UpdateCanvasDisplay(canvasTemperature);

           
                StartCoroutine(MarsLampDelay());
                 
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
            Renderer marsLampRenderer = marsLampObject.GetComponent<Renderer>();   
            Material currentmarsLampMaterial = marsLampRenderer.sharedMaterial;
            
          
            if (currentmarsLampMaterial == greenLampMaterial) 
            {
                buoyancyForce = airDensityMars * balloonVolume * Mathf.Abs(marsGravity);
                buoyancyForce = buoyancyForce + atmosphereAddition;
            }

       
            if (buoyancyForce > 0f)
            {
                balloonRb.AddForce(Vector3.up * buoyancyForce);
            }
        }
    }

    public IEnumerator MarsLampDelay()
    {   Renderer marsLampRenderer = marsLampObject.GetComponent<Renderer>();
        yield return new WaitForSeconds(0.3f); 
        marsLampRenderer.material = greenLampMaterial;
    }

}
