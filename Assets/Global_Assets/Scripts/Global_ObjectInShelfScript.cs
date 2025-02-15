using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

/*This script allows for items to be put into the shelf and stay in a neutral position.
The only allowed items are the experiments

This is achived by freezing position and rotation inside the collider. 
*/

public class Global_ObjectInShelfScript : MonoBehaviour
{



    private Rigidbody rb; 
    private bool isInShelf = false; 
    private Collider lastCollider; 
    public Vector3 fixedRotation = new Vector3(90, 0, 90); 
    public AudioSource objectSound; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectSound= objectSound.GetComponent<AudioSource>();

    }

    void OnTriggerEnter(Collider other)
    {
        //Only allows the Experiement objects in the shelves 
        if (other.CompareTag("PhysicsZone")) 
        {
            lastCollider = other; // Saves the collider
            isInShelf = true; 
            //Freezes Object in rotation and position
            FreezeObject();
            objectSound.mute = true; //mutes objects 
        }
    }

    //reverts the done changes
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PhysicsZone")) 
        {
            if (isInShelf)
            {
                //unfreezes Object
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
            // Sets position into the middle of the collider
            transform.position = lastCollider.bounds.center;

            // Sets rotation
            transform.rotation = Quaternion.Euler(fixedRotation);
        }

        //freezes both position and rotation
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    void UnfreezeObject()
    {
        //Unfreezes object
        rb.constraints = RigidbodyConstraints.None;
    }
}

