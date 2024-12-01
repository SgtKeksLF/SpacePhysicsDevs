using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    public SlotScript correctSlot;  // Der Slot, in dem diese Sphere abgelegt werden muss
    private bool isInCorrectSlot = false;

    // Wird aufgerufen, wenn die Sphere korrekt abgelegt wird
    public void DropSphereCorrectly()
    {
        isInCorrectSlot = true;
        Debug.Log("Sphere abgelegt im richtigen Slot!");
        // Weitere Logik (z.B. Animation oder Punkte)
    }

    // Wird aufgerufen, wenn die Sphere im falschen Slot abgelegt wird
    public void DropSphereIncorrectly()
    {
        isInCorrectSlot = false;
        Debug.Log("Falscher Slot!");
        // Weitere Logik (z.B. zurücksetzen oder Fehlschlag melden)
    }

    // Optionale Methode für visuelles Feedback oder Interaktion
    void OnMouseDown()
    {
        if (!isInCorrectSlot)
        {
            // Der Spieler könnte die Sphere durch die Szene bewegen, bis sie den richtigen Slot erreicht
            // Hier kannst du Drag-and-Drop oder ähnliche Logiken integrieren
        }
    }
}

