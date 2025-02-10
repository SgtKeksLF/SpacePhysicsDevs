using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_ObjectInShelf : MonoBehaviour
{  

    private Rigidbody rb;
    private bool isInShelf = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            FreezeObject();
        }
    }

    void Update()
    { //dev note - I dont know why this is needed, feel free to try 
       if(isInShelf == false)
       {
        rb.useGravity = true;
       }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PhysicsZone"))  // Wenn das Objekt in das Regal kommt
        {
            if (!isInShelf)  // Nur ändern, wenn es noch nicht im Regal ist
            {
                FreezeObject();  // Position und Rotation einfrieren
                rb.useGravity = false;  // Schwerkraft deaktivieren
                isInShelf = true;
                Debug.Log(gameObject.name + " ist jetzt im Regal.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PhysicsZone"))  // Wenn das Objekt das Regal verlässt
        {
            if (isInShelf)  // Nur ändern, wenn es im Regal war
            {
                UnfreezeObject();  // Position und Rotation freigeben
                rb.useGravity = true;  // Schwerkraft aktivieren
                isInShelf = false;
                Debug.Log(gameObject.name + " ist jetzt nicht mehr im Regal.");
            }
        }
    }

      void FreezeObject()
    {
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    // Methode zum Freigeben der Einschränkungen
    void UnfreezeObject()
    {
        rb.constraints = RigidbodyConstraints.None;  // Keine Einschränkungen
    }
}

