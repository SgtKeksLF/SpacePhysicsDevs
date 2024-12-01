using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotScript : MonoBehaviour
{
    public bool isCorrectSlot;  // Gibt an, ob dieser Slot der richtige für die Sphere ist
    public Material correctMaterial;  // Das Material, das den Slot grün leuchten lässt
    public Material incorrectMaterial;  // Das Material, das den Slot standardmäßig anzeigt

    private Renderer slotRenderer;

    void Start()
    {
        slotRenderer = GetComponent<Renderer>();
        slotRenderer.material = incorrectMaterial;  // Setze die Standardfarbe
    }

    // Wird aufgerufen, wenn ein Collider (z.B. die Sphere) den Slot berührt
    void OnTriggerEnter(Collider other)
    {
        // Prüfen, ob die Sphere in den richtigen Slot gelegt wird
        if (other.CompareTag("Sphere"))
        {
            SphereScript sphereScript = other.GetComponent<SphereScript>();
            
            if (sphereScript.correctSlot == this)
            {
                // Korrekte Sphere abgelegt: Slot grün leuchten lassen
                slotRenderer.material = correctMaterial;
                sphereScript.DropSphereCorrectly();
            }
            else
            {
                // Falsche Sphere abgelegt: Slot bleibt unverändert
                slotRenderer.material = incorrectMaterial;
                sphereScript.DropSphereIncorrectly();
            }
        }
    }
}

