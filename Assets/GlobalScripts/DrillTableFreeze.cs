using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillTableFreeze : MonoBehaviour
{
    public GameObject drill;
    private void OnTriggerEnter(Collider other)
{
    // Überprüfe, ob das kollidierende Objekt den Tag "Drill" hat
    if (other.CompareTag("Drill"))
    {Debug.Log("Drill im Ding");
       
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }

            drill.transform.position = transform.position;
       
    }
}
private void OnTriggerExit(Collider other)
{
    // Überprüfe, ob das kollidierende Objekt den Tag "Drill" hat
    if (other.CompareTag("Drill"))
    {
        Debug.Log("Drill hat das Ding verlassen");

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None; // Hebt alle Einschränkungen auf
        }
    }
}

}
