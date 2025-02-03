using UnityEngine;

public class ScriptMercuryPhysics : MonoBehaviour
{
    public Material redLampMaterial;
    public Material greenLampMaterial;
    public GameObject mercuryLampObject;
    public GameObject earthLampObject;   


    // Die folgenden Variablen sind für die Physik selbst
    private float mercuryGravity = -3.9f;


    // Canvas Logik
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
        Renderer mercuryLampRenderer = mercuryLampObject.GetComponent<Renderer>(); // Renderer des Merkur-Objekts
        Renderer earthLampRenderer = earthLampObject.GetComponent<Renderer>();     // Renderer des Erde-Objekts
        if(mercuryLampRenderer != null && earthLampRenderer != null)
        { 
            Debug.Log("Render not null");
            Material currentMercuryLampMaterial = mercuryLampRenderer.sharedMaterial;
            
          
            if(currentMercuryLampMaterial == redLampMaterial)
            {
                earthLampRenderer.material = redLampMaterial;
                Debug.Log("Mercury physics");
                Physics.gravity = new Vector3(0, mercuryGravity, 1);  // Merkur-Schwerkraft
                    // Anwenden der spezifischen Physik für Objekte
                mercuryLampRenderer.material = greenLampMaterial;

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


 

}

  

