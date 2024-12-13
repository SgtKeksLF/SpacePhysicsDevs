using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnSelect : MonoBehaviour
{
    public GameObject teleportTarget; // Zielobjekt in der Szene

    public void OnSelectEnter()
    {
        Debug.Log("Teleport() wurde aufgerufen!");
        if (teleportTarget != null && XRManager.xrRig != null)  // Zugriff auf XR-Rig über XRManager
        {
            Debug.Log("TeleportTarget und XR Rig sind zugewiesen!");
            // Setze die Position des XR-Rigs auf die Position des Zielobjekts
            XRManager.xrRig.transform.position = teleportTarget.transform.position;

            // Optional: Setze die Rotation, falls gewünscht
            XRManager.xrRig.transform.rotation = teleportTarget.transform.rotation;
        }
        else
        {
            Debug.Log("TeleportTarget oder XR Rig ist nicht zugewiesen!");
        }
    }
}

