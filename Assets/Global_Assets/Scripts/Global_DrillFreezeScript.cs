using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_DrillFreezeScript : MonoBehaviour
{
    public GameObject drill;
    public Vector3 fixedPosition;
    public Vector3 fixedRotationEuler;

    private void OnTriggerEnter(Collider other)
    {
        // Überprüfe, ob das kollidierende Objekt den Tag "Drill" hat
        if (other.CompareTag("Drill"))
        {
      
            
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }

            // Objekt in die Mitte des Colliders mit fester Position und Rotation setzen
            other.transform.position = transform.position + fixedPosition;
            other.transform.rotation = Quaternion.Euler(fixedRotationEuler);
            
            // Audio stummschalten
            AudioSource audioSource = other.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.mute = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Überprüfe, ob das kollidierende Objekt den Tag "Drill" hat
        if (other.CompareTag("Drill"))
        {
           
            
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.None; // Hebt alle Einschränkungen auf
            }

            // Audio wieder aktivieren
            AudioSource audioSource = other.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.mute = false;
            }
        }
    }
}
