using UnityEngine;

public class ScriptMercuryPhysics : MonoBehaviour
{
    public Material redLampMaterial;
    public Material greenLampMaterial;
    public GameObject mercuryLampObject;
    public GameObject earthLampObject;   


    // Die folgenden Variablen sind für die Physik selbst
    private float mercuryGravity = -3.9f;





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
            }
            else{

            }
        }
    }


 

}

  

