using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
This script enables the Mercury physics and needed canvases

it is generally attached to the buttons in each room, enabling Mercury physics once pressed. This is done by tracking the attached lamps and their materials
A lamp delay has been implemented to prevent unwanted sound behavior from dependent objects
*/

public class Mercury_MercuryPhysicsScript : MonoBehaviour
{
    public Material redLampMaterial;
    public Material greenLampMaterial;
    public GameObject mercuryLampObject;
    public GameObject earthLampObject;   


    private float mercuryGravity = -3.9f;



    public GameObject canvasGravity;
    public GameObject canvasAtmosphere;
    public GameObject canvasPressure;
    public GameObject canvasTemperature;


    void Start()
    {
        
    }


      public void OnButtonPressed()
    {
       MercuryPhysicsChange();
        
    }

    public void MercuryPhysicsChange()
    {
        Renderer mercuryLampRenderer = mercuryLampObject.GetComponent<Renderer>(); 
        Renderer earthLampRenderer = earthLampObject.GetComponent<Renderer>();    
        if(mercuryLampRenderer != null && earthLampRenderer != null)
        { 
            // Debug.Log("Render not null");
            Material currentMercuryLampMaterial = mercuryLampRenderer.sharedMaterial;
            
          
            if(currentMercuryLampMaterial == redLampMaterial)
            {
                earthLampRenderer.material = redLampMaterial;
                // Debug.Log("Mercury physics");
                Physics.gravity = new Vector3(0, mercuryGravity, 1);  
                  
                


                UpdateCanvasDisplay(canvasGravity);
                UpdateCanvasDisplay(canvasAtmosphere);
                UpdateCanvasDisplay(canvasPressure);
                UpdateCanvasDisplay(canvasTemperature);
                StartCoroutine(MercuryLampDelay());
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

            //Debug.Log("Displays found");
        }
    }
}

public IEnumerator MercuryLampDelay()
{   Renderer mercuryLampRenderer = mercuryLampObject.GetComponent<Renderer>();
    // Debug.Log("waiting");
    yield return new WaitForSeconds(0.5f); 
    mercuryLampRenderer.material = greenLampMaterial;
}


 

}

  

