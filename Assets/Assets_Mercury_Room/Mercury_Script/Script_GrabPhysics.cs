using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GrabPhysics : MonoBehaviour
{
    public GameObject mercuryLampObject;
    public GameObject earthLampObject;   
    public Material greenLampMaterial;
    //
    public GameObject balloon;
    public GameObject canOfBeans;
    public GameObject cube;
    public GameObject ball;
    public GameObject canOfWater;
    //
    private Vector3 mercuryCanScale = new Vector3(+2.0f, 0.0f, 0.0f);
    public AudioSource canOfBeansAudio; 

   
    private AudioSource canOfWaterAudio;
    public Material newWaterMaterial;
    public Material defaultWaterMaterial;

    
    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = canOfWater.GetComponents<AudioSource>();

        if (audioSources.Length > 1)
        {
            canOfWaterAudio = audioSources[1]; 
        }

      
     
    }

  

    // Update is called once per frame
    void Update()
    {
        
    }





    public void OnGrab(GameObject grabbedObject)
    {
        Renderer mercuryLampRenderer = mercuryLampObject.GetComponent<Renderer>(); 
        if(mercuryLampRenderer != null)
        { 
            Material currentMercuryLampMaterial = mercuryLampRenderer.sharedMaterial;
            
            if(currentMercuryLampMaterial == greenLampMaterial)
            {
                
                    // Unterscheiden zwischen den verschiedenen Objekten
                    if (grabbedObject == canOfWater)
                    {
                        // Aktionen für canOfWater
                        MercuryWaterPhysics();
                    }
                    else if (grabbedObject == canOfBeans)
                    {
                        // Aktionen für canOfBeans
                        MercuryBeansPhysics();
                    }

                    //Hier Kann deine Logik für die Screens rein @Lisa
            }

         }
       
    }

    public void onRelease(GameObject grabbedObject){
  // Unterscheiden zwischen den verschiedenen Objekten

   Renderer mercuryLampRenderer = mercuryLampObject.GetComponent<Renderer>(); 
        if(mercuryLampRenderer != null)
        { 
            Material currentMercuryLampMaterial = mercuryLampRenderer.sharedMaterial;
            
            if(currentMercuryLampMaterial == greenLampMaterial)
            {
                    if (grabbedObject == canOfWater)
                    {
                        // Aktionen für canOfWater
                        MercuryWaterPhysicsRelease();
                    }
                    else if (grabbedObject == canOfBeans)
                    {
                        // Aktionen für canOfBeans
                        MercuryBeansPhysicsRelease();
                    }

                    //Hier ggf screenlogic? idk

            }
        }
    }

    public void MercuryBeansPhysics()
    {
        if (canOfBeans != null)
        {
            canOfBeans.transform.localScale += mercuryCanScale;

            if (canOfBeansAudio != null)
            {
                Debug.Log("Sound is playing");
                canOfBeansAudio.Play();
            }
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

     public void MercuryBeansPhysicsRelease()
    {
        canOfBeans.transform.localScale -= mercuryCanScale;
        if (canOfBeansAudio != null)
            {
                Debug.Log("Sound is playing");
                canOfBeansAudio.Play();
            }
    }
     public void MercuryWaterPhysicsRelease()
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

    
            

}
