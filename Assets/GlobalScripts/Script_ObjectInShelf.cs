using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Script_ObjectInShelf : MonoBehaviour
{

    private Rigidbody rb;
    private bool isInShelf = false;
    private Collider lastCollider; // Speichert den letzten Collider für die Mitte
    public Vector3 fixedRotation = new Vector3(90, 0, 90); // Gewünschte Rotation in Grad
    public AudioSource objectSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectSound= objectSound.GetComponent<AudioSource>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PhysicsZone")) // Betritt die Zone
        {
            lastCollider = other; // Speichert den Collider für die spätere Positionierung
            isInShelf = true;
            FreezeObject();
            objectSound.mute = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PhysicsZone")) // Verlässt die Zone
        {
            if (isInShelf)
            {
                UnfreezeObject();
                rb.useGravity = true;
                isInShelf = false;
                objectSound.mute = false;
            }
        }
    }

    void FreezeObject()
    {
        if (lastCollider != null)
        {
            // Setzt die Position auf die Mitte des Colliders
            transform.position = lastCollider.bounds.center;

            // Setzt die Rotation auf den festen Wert
            transform.rotation = Quaternion.Euler(fixedRotation);
        }

        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    void UnfreezeObject()
    {
        rb.constraints = RigidbodyConstraints.None;
    }
}

