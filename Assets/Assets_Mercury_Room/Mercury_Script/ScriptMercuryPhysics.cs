using UnityEngine;

public class ScriptMercuryPhysics : MonoBehaviour
{
    // Die folgenden Variablen sind für die Physik selbst
    private float mercuryGravity = -3.9f;
    
   public GameObject balloon; // Ballonobjekt

    // Can of Beans Variablen
    public GameObject canOfBeans;
    private Vector3 mercuryCanScale = new Vector3(+2.0f, 0.0f, 0.0f);
    public AudioSource pickupBeanCanAudio;     
    public AudioSource canOfBeansAudio; 

    // Water Can Variablen
    public GameObject canOfWater;
    public AudioSource pickupWaterCanAudio;   
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
        void FixedUpdate()
    {
    MercuryBalloonPhysics();
    }


      public void OnButtonPressed()
    {
       MercuryPhysicsChange();
        
    }

    public void MercuryPhysicsChange()
    {
 
        Debug.Log("Mercury physics");
        Physics.gravity = new Vector3(0, mercuryGravity, 1);  // Merkur-Schwerkraft
            // Anwenden der spezifischen Physik für Objekte
    MercuryBeansPhysics();
    MercuryWaterPhysics();
    }


     
    

    
    public void MercuryBeansPhysics()
    {
        canOfBeans.transform.localScale += mercuryCanScale;
        
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
 
    private void MercuryBalloonPhysics()
    {
        if (balloonRb != null)
        {   
            balloonRb.AddForce(Vector3.up * 0);
        }
    }
}

  

