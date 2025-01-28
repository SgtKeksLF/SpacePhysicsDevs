using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeOnSelect : MonoBehaviour
{
    public GameObject teleportTarget; // Zielobjekt in der Szene

    public void OnSelectEnter()
    {
        Debug.Log("Teleport() wurde aufgerufen!");

        // Sicherstellen, dass das Ziel und das XR-Rig vorhanden sind
        if (teleportTarget != null && XRManager.xrRig != null)
        {
            Debug.Log("TeleportTarget und XR Rig sind zugewiesen!");

            // Berechnung des Kamera-Offsets relativ zum XR-Rig
            Transform cameraTransform = Camera.main.transform;
            Vector3 cameraOffset = XRManager.xrRig.transform.position - cameraTransform.position;

            // Neue Position basierend auf Zielposition und Offset
            Vector3 newRigPosition = teleportTarget.transform.position + cameraOffset;

            // Setze die neue Position des XR-Rigs
            XRManager.xrRig.transform.position = newRigPosition;

            // Optional: Setze die Rotation des XR-Rigs, um die Zielrotation zu übernehmen
            XRManager.xrRig.transform.rotation = teleportTarget.transform.rotation;

            Debug.Log("XR Rig wurde erfolgreich teleportiert!");
        }
        else
        {
            Debug.Log("TeleportTarget oder XR Rig ist nicht zugewiesen!");
        }
    }
}
