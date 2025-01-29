using UnityEngine;

public class ScriptEarthPhysics : MonoBehaviour
{
    public Material redLampMaterial;
    public Material greenLampMaterial;
    public GameObject mercuryLampObject;
    public GameObject earthLampObject;   

    // Die folgenden Variablen sind für die Physik selbst
    private float earthGravity = -9.8f;
    
    public GameObject balloon; // Ballonobjekt

    // Can of Beans Variablen
    public GameObject canOfBeans;
    private Vector3 mercuryCanScale = new Vector3(+2.0f, 0.0f, 0.0f);  
    public AudioSource canOfBeansAudio; 

    // Water Can Variablen
    public GameObject canOfWater;
    private AudioSource canOfWaterAudio;
    public Material newWaterMaterial;
    public Material defaultWaterMaterial;

    // Ballon-Physik
    private float balloonVolume = 0.5f; 
    private float airDensityEarth = 1.225f; 


    private Rigidbody balloonRb;

    void Start()
    {
        
        AudioSource[] audioSources = canOfWater.GetComponents<AudioSource>();
        
      
        if (audioSources.Length > 1)
        {
            canOfWaterAudio = audioSources[1]; 
        }

      
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
    {   Renderer mercuryLampRenderer = mercuryLampObject.GetComponent<Renderer>(); // Renderer des Merkur-Objekts
        Renderer earthLampRenderer = earthLampObject.GetComponent<Renderer>();     // Renderer des Erde-Objekts
        if (earthLampRenderer != null)
        {
            Material currentEarthLampMaterial = earthLampRenderer.sharedMaterial;
            
            if(currentEarthLampMaterial == redLampMaterial)
            {
                mercuryLampRenderer.material = redLampMaterial;
                Debug.Log("Earth physics");
                Physics.gravity = new Vector3(0, earthGravity, 0);  // Erde-Schwerkraft
                EarthBeansPhysics();
                EarthWaterPhysics();
                earthLampRenderer.material = greenLampMaterial;
            }
            else{
                Debug.Log("Already Earth Physics");
            }
        }
      
    }

     public void EarthBeansPhysics()
    {
        canOfBeans.transform.localScale -= mercuryCanScale;
        if (canOfBeansAudio != null)
            {
                Debug.Log("Sound is playing");
                canOfBeansAudio.Play();
            }
    }
     public void EarthWaterPhysics()
    {    
        if (canOfWater != null && newWaterMaterial != null && defaultWaterMaterial != null)
        {
            Renderer waterRenderer = canOfWater.GetComponent<Renderer>();

            if (waterRenderer != null)
            {
                    waterRenderer.material = defaultWaterMaterial;
                    canOfWaterAudio.Stop();
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
           
            balloonRb.AddForce(Vector3.up * buoyancyForce);
        }
    }
    }

    
}
   
    
