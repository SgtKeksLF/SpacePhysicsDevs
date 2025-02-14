using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venus_VenusPhysicsScript : MonoBehaviour
{
    public Material redLampMaterial;
    public Material greenLampMaterial;
    public GameObject venusLampObject;
    public GameObject earthLampObject;   

    private float balloonVolume = 0.5f; 
    public GameObject balloon; // Ballonobjekt
    private Rigidbody balloonRb;

    // Die folgenden Variablen sind für die Physik selbst
    private float venusGravity = -8.87f;
    private float airDensityVenus = 67.0f;


    // Canvas Logik
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
        Renderer venusLampRenderer = venusLampObject.GetComponent<Renderer>(); // Renderer des Merkur-Objekts
        Renderer earthLampRenderer = earthLampObject.GetComponent<Renderer>();     // Renderer des Erde-Objekts
        if(venusLampRenderer != null && earthLampRenderer != null)
        { 
            Debug.Log("Render not null");
            Material currentvenusLampMaterial = venusLampRenderer.sharedMaterial;
            
          
            if(currentvenusLampMaterial == redLampMaterial)
            {
                earthLampRenderer.material = redLampMaterial;
                Debug.Log("venus physics");
                Physics.gravity = new Vector3(0, venusGravity, 1);  // Merkur-Schwerkraft
                    // Anwenden der spezifischen Physik für Objekte
                venusLampRenderer.material = greenLampMaterial;

                // **DisplayEarth -> Display0 auf allen relevanten Canvases**
                UpdateCanvasDisplay(canvasGravity);
                UpdateCanvasDisplay(canvasAtmosphere);
                UpdateCanvasDisplay(canvasPressure);
                UpdateCanvasDisplay(canvasTemperature);
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

            Debug.Log("Displays gefunden");
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
        
        // Auftrieb nur auf der Venus anwenden
        if (currentVenusLampMaterial == greenLampMaterial)  // Venus
        {
            buoyancyForce = airDensityVenus * balloonVolume * Mathf.Abs(venusGravity);
        }

        // Wenn Auftriebskraft vorhanden, den Ballon anheben
        if (buoyancyForce > 0f)
        {
            


            balloonRb.AddForce(Vector3.up * buoyancyForce);
        }
    }
    else{
        Debug.Log("Ballon hat kein RB");
    }
}


}
