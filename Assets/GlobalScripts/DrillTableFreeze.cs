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
    {
        // Überprüfe, ob currentProbe im Collider ist
        if (drill != null && other.bounds.Contains(drill.transform.position))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }

            drill.transform.position = transform.position;
        }
    }
}

}
