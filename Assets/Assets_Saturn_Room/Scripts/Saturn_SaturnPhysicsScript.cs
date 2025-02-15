using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script enables the Saturn physics and needed canvases

it is generally attached to the buttons in each room, enabling Saturn physics once pressed. This is done by tracking the attached lamps and their materials
A lamp delay has been implemented to prevent unwanted sound behavior from dependent objects
*/

public class Saturn_SaturnPhysicsScript : MonoBehaviour
{
    public Material redLampMaterial;
    public Material greenLampMaterial;
    public GameObject saturnLampObject;
    public GameObject earthLampObject;   

    private float balloonVolume = 0.5f; 
    public GameObject balloon; 
    private Rigidbody balloonRb;

    
    private float saturnGravity = -10.44f; 
    private float airDensitySaturn = 0.006f; 

    
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
       saturnPhysicsChange();
        
    }

    public void saturnPhysicsChange()
    {

        Renderer saturnLampRenderer = saturnLampObject.GetComponent<Renderer>(); 
        Renderer earthLampRenderer = earthLampObject.GetComponent<Renderer>();   
        if(saturnLampRenderer != null && earthLampRenderer != null)
        { 
          
            Material currentsaturnLampMaterial = saturnLampRenderer.sharedMaterial;
            
          
            if(currentsaturnLampMaterial == redLampMaterial)
            {
                earthLampRenderer.material = redLampMaterial;
                
                Physics.gravity = new Vector3(0, saturnGravity, 1); 

           
                UpdateCanvasDisplay(canvasGravity);
                UpdateCanvasDisplay(canvasAtmosphere);
                UpdateCanvasDisplay(canvasPressure);
                UpdateCanvasDisplay(canvasTemperature);

                StartCoroutine(SaturnLampDelay());
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
            Renderer saturnLampRenderer = saturnLampObject.GetComponent<Renderer>();   
            Material currentsaturnLampMaterial = saturnLampRenderer.sharedMaterial;
            
 
            if (currentsaturnLampMaterial == greenLampMaterial)
            {
                buoyancyForce = airDensitySaturn * balloonVolume * Mathf.Abs(saturnGravity); 
            }


            if (buoyancyForce > 0f)
            {
                balloonRb.AddForce(Vector3.up * buoyancyForce);
            }
        }
    
    }

    public IEnumerator SaturnLampDelay()
    {   Renderer saturnLampRenderer = saturnLampObject.GetComponent<Renderer>();
        yield return new WaitForSeconds(0.3f); 
        saturnLampRenderer.material = greenLampMaterial;
    }

}
