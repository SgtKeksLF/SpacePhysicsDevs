using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global_EarthPhysicsScript : MonoBehaviour
{
    public Material redLampMaterial;
    public Material greenLampMaterial;
    public GameObject nonEarthPlanetLampObject;
    public GameObject earthLampObject;   

    // Die folgenden Variablen sind für die Physik selbst
    private float earthGravity = -9.8f;
    
    public GameObject balloon; // Ballonobjekt

    // Ballon-Physik
    private float balloonVolume = 0.5f; 
    private float airDensityEarth = 1.225f; 

    // Canvas Logik
    public GameObject canvasGravity;
    public GameObject canvasAtmosphere;
    public GameObject canvasPressure;
    public GameObject canvasTemperature;


    private Rigidbody balloonRb;

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
    {   Renderer nonEarthPlanetLampRenderer = nonEarthPlanetLampObject.GetComponent<Renderer>(); // Renderer des Merkur-Objekts
        Renderer earthLampRenderer = earthLampObject.GetComponent<Renderer>();     // Renderer des Erde-Objekts
        if (earthLampRenderer != null)
        {
            Material currentEarthLampMaterial = earthLampRenderer.sharedMaterial;
            
            if(currentEarthLampMaterial == redLampMaterial)
            {
                nonEarthPlanetLampRenderer.material = redLampMaterial;
                Debug.Log("Earth physics");
                Physics.gravity = new Vector3(0, earthGravity, 0);  // Erde-Schwerkraft
                earthLampRenderer.material = greenLampMaterial;

                // **DisplayEarth -> Display0 auf allen relevanten Canvases**
                UpdateCanvasDisplay(canvasGravity);
                UpdateCanvasDisplay(canvasAtmosphere);
                UpdateCanvasDisplay(canvasPressure);
                UpdateCanvasDisplay(canvasTemperature);

                StartCoroutine(EarthLampDelay());
            }
            else{
                Debug.Log("Already Earth Physics");
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
        // Auftrieb nur auf der Erde anwenden, wenn isNewPlanet false ist
        if (currentEarthLampMaterial == greenLampMaterial)  // Erde
        {
           
            buoyancyForce = airDensityEarth * balloonVolume * Mathf.Abs(earthGravity);
        }
        else  // Merkur
        {
            buoyancyForce = 0f;  // Kein Auftrieb auf Merkur
        }

        // Wenn Auftriebskraft vorhanden, den Ballon anheben
        if (buoyancyForce > 0f)
        {
          // Debug.Log("Balloon should fly");
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

            Debug.Log("Displays gefunden");
        }
    }
}
public IEnumerator EarthLampDelay()
{   Renderer earthLampRenderer = earthLampObject.GetComponent<Renderer>();
    Debug.Log("Waiting");
    yield return new WaitForSeconds(0.5f); 
    earthLampRenderer.material = greenLampMaterial;
}
    
}
   
    
