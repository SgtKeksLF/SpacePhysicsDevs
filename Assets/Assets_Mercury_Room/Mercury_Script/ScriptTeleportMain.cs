using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTeleportMain : MonoBehaviour
{
    public void OnButtonPressed()
    {
        // Suchen des TeleportTargets basierend auf dem Tag "TeleportTarget"
        GameObject teleportTarget = GameObject.FindGameObjectWithTag("TeleportTarget");

        if (teleportTarget != null && XRManager.xrRig != null) // Stelle sicher, dass das Zielobjekt und das XR Rig vorhanden sind
        {
            // Teleportiere das XR Rig zum TeleportTarget
            XRManager.xrRig.transform.position = teleportTarget.transform.position;

            // Optional: Setze die Rotation des XR Rigs auf das Zielobjekt
            XRManager.xrRig.transform.rotation = teleportTarget.transform.rotation;

            Debug.Log("Teleportation erfolgreich!");  // Debugging-Log
        }
        else
        {
            Debug.Log("TeleportTarget oder XR Rig ist nicht zugewiesen!");
        }
    }

    void Start()
    {
        // Hier könnte man Initialisierungen vornehmen, falls nötig
    }

    void Update()
    {
        // Hier könntest du kontinuierliche Logik hinzufügen, falls gewünscht
    }
}
