using UnityEngine;

public class ScriptMercuryPhysics : MonoBehaviour
{
    public Material redLampMaterial;
    public Material greenLampMaterial;
    public GameObject mercuryLampObject;
    public GameObject earthLampObject;   


    // Die folgenden Variablen sind für die Physik selbst
    private float mercuryGravity = -3.9f;
    
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
            Debug.Log(currentMercuryLampMaterial);
          
            if(currentMercuryLampMaterial == redLampMaterial)
            {
                Debug.Log("In der Funktuon");
                earthLampRenderer.material = redLampMaterial;
                Debug.Log("Mercury physics");
                Physics.gravity = new Vector3(0, mercuryGravity, 1);  // Merkur-Schwerkraft
                    // Anwenden der spezifischen Physik für Objekte
                MercuryBeansPhysics();
                MercuryWaterPhysics();
                mercuryLampRenderer.material = greenLampMaterial;
            }
            else{

            }
        }
    }


     
    

    
    public void MercuryBeansPhysics()
    {
       if (canOfBeans != null)
        {
            Debug.Log($"Current Scale: {canOfBeans.transform.localScale}");
            canOfBeans.transform.localScale += mercuryCanScale;
            Debug.Log($"New Scale: {canOfBeans.transform.localScale}");
        }
      



        
        if (canOfBeansAudio != null)
        {
            Debug.Log("Sound is playing");
            canOfBeansAudio.Play();
        }
    }

    public void MercuryWaterPhysics()
    {    
        if (canOfWater != null && newWaterMaterial != null && defaultWaterMaterial != null)
        {
            Renderer waterRenderer = canOfWater.GetComponent<Renderer>();

            if (waterRenderer != null)
            {
                
                    waterRenderer.material = newWaterMaterial;
                    if (canOfWaterAudio != null)
                    {
                        Debug.Log("Sound is playing");
                        canOfWaterAudio.Play();
                    }
               
            }
        }
    }

}

  

