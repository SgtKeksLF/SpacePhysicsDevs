using System.Collections;
using UnityEngine;

public class Global_MoveDrillScript : MonoBehaviour
{
    public GameObject objectToMove;
    public GameObject objectToTeleport;
    public GameObject objectToTeleportTo;
    public float distance = 1.5f;
    public float speed = 0.5f;
    public AudioClip moveSound;  // Hier den Sound einfügen
    private AudioSource audioSource;  // AudioSource zum Abspielen des Sounds

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

    // Initialisiere die AudioSource
    audioSource = objectToMove.GetComponent<AudioSource>();
    if (audioSource == null)
    {
        audioSource = objectToMove.AddComponent<AudioSource>(); // Falls keine vorhanden ist, füge eine hinzu
    }

    if (audioSource == null)
    {
        Debug.LogError("AudioSource konnte nicht initialisiert werden für " + objectToMove.name);
    }
}

  public void MoveToTarget()
{
    if (!moving && objectToMove != null)
    {
        Renderer mercuryLampRenderer = mercuryLampObject.GetComponent<Renderer>(); // Renderer des Merkur-Objekts

        if (!moving && objectToMove != null)
        {
            TeleportObject();

            if (mercuryLampRenderer != null)
            {
                Debug.Log("Render not null");

               Material currentMercuryLampMaterial = mercuryLampRenderer.sharedMaterial;

                Debug.Log("Material Check: " + (currentMercuryLampMaterial == redLampMaterial));

                if (currentMercuryLampMaterial == redLampMaterial)
                {
                    Debug.Log("Material matches redLampMaterial");

                    Debug.Log("moveSound: " + (moveSound != null ? "Set" : "Null"));
                    Debug.Log("audioSource: " + (audioSource != null ? "Set" : "Null"));

                    if (moveSound != null && audioSource != null)
                    {
                        Debug.Log("isPlaying: " + audioSource.isPlaying);

                        if (!audioSource.isPlaying)
                        {
                            audioSource.clip = moveSound;
                            audioSource.Play();
                            Debug.Log("Sound should play");
                        }
                    }

                    if (moveSound == null)
                    {
                        Debug.LogError("No sound source");
                    }
                }

                TeleportObject();
                StartCoroutine(MoveCoroutine(targetPosition));
            }
        }
    }
}


    public void MoveToStart()
    {
        Renderer earthLampRenderer = earthLampObject.GetComponent<Renderer>(); // Renderer des Merkur-Objekts


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