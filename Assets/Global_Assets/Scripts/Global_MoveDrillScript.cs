using System.Collections;
using UnityEngine;

/*
This script moves the drill and the table it is placed upon

To prevent unwanted movement this script teleports the drill onto the table every time it is used. 
Due to the placement of the drill it needs to have its collission detection be turned off while moving. 
*/

public class Global_MoveDrillScript : MonoBehaviour
{
    public GameObject objectToMove;
    public GameObject objectToTeleport;
    public GameObject objectToTeleportTo;
    public float distance = 1.5f;
    public float speed = 0.5f;
    public AudioClip moveSound;  
    private AudioSource audioSource;  
  

    public Material redLampMaterial;
    public Material greenLampMaterial;
    public GameObject mercuryLampObject;
    public GameObject earthLampObject;


    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 teleportStartPosition;
    private Quaternion teleportStartRotation;
    private bool moving = false;
    private Rigidbody objectRigidbody;
  

   void Start()
{
    if (objectToMove != null)
    {
       
        startPosition = objectToMove.transform.position;
        targetPosition = startPosition + Vector3.forward * distance;
    }

    if (objectToTeleport != null)
    {
        teleportStartRotation = objectToTeleport.transform.rotation;
    }

 
    audioSource = objectToMove.GetComponent<AudioSource>();
    if (audioSource == null)
    {
        audioSource = objectToMove.AddComponent<AudioSource>();
    }

}

  
    public void MoveToTarget()
    {
        if (!moving && objectToMove != null)
        {
            Renderer mercuryLampRenderer = mercuryLampObject.GetComponent<Renderer>();

            if (!moving && objectToMove != null)
            {
              
                TeleportObject();

                if (mercuryLampRenderer != null)
                {
                    Material currentMercuryLampMaterial = mercuryLampRenderer.sharedMaterial;

                    if (currentMercuryLampMaterial == redLampMaterial)
                    {
                        if (moveSound != null && audioSource != null)
                        {
                            if (!audioSource.isPlaying)
                            {
                               
                                audioSource.clip = moveSound;
                                audioSource.Play();
                               
                            }
                        }
                    }
                 
                    StartCoroutine(MoveCoroutine(targetPosition));
                }
            }
        }
    }

    
    public void MoveToStart()
    {
        Renderer earthLampRenderer = earthLampObject.GetComponent<Renderer>();


        if (!moving && objectToMove != null)
        {
          
            TeleportObject();
            if (earthLampRenderer != null)
            {
                Debug.Log("Render not null");
                Material currentMercuryLampMaterial = earthLampRenderer.sharedMaterial;
                if (currentMercuryLampMaterial == redLampMaterial)
                {

                    if (moveSound != null && !audioSource.isPlaying)
                    {
                        
                        audioSource.clip = moveSound;
                        audioSource.Play();
                    }

                }
             
                StartCoroutine(MoveCoroutine(startPosition));
            }
        }
     }

    private void TeleportObject()
    {
        if (objectToTeleport != null)
        {
           
            objectToTeleport.transform.position = objectToTeleportTo.transform.position;
            objectToTeleport.transform.rotation = teleportStartRotation;
        }
        if (objectRigidbody != null)
        {
            
            objectRigidbody.detectCollisions = false;
        }
    }

    private IEnumerator MoveCoroutine(Vector3 destination)
    {
        moving = true; 
        Vector3 startPos = objectToMove.transform.position;
        float journey = 0f;
        float duration = Vector3.Distance(startPos, destination) / speed;

       
        while (journey < duration)
        {
            journey += Time.deltaTime;
            objectToMove.transform.position = Vector3.Lerp(startPos, destination, journey / duration);
            yield return null;
        }

        objectToMove.transform.position = destination;
        moving = false; 

        if (objectRigidbody != null)
        {
          
            objectRigidbody.detectCollisions = true;
        }
    }
}