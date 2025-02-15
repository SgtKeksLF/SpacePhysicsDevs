using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script freezes the drill in position and rotation upon being put onto the table
It includes measure to prevent noises should clipping problems occure
*/

public class Global_DrillFreezeScript : MonoBehaviour
{
    public GameObject drill;
    public Vector3 fixedPosition;
    public Vector3 fixedRotationEuler;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Drill"))
        {
      
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation; 
            }

           
            other.transform.position = transform.position + fixedPosition;
            other.transform.rotation = Quaternion.Euler(fixedRotationEuler);
            
      
            AudioSource audioSource = other.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.mute = true;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Drill"))
        {
           
            
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.None; 
            }

           
            AudioSource audioSource = other.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.mute = false;
            }
        }
    }
}
