using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainRoom_EventCameraScript : MonoBehaviour
{
    public Canvas worldSpaceCanvas;  // Dein World Space Canvas
    private Camera xrCamera;

    void Start()
    {
        // Eventuelle Initialisierungen oder Prüfungen
        UpdateEventCamera();
    }

    // Diese Methode wird aufgerufen, wenn das XR Rig geladen wurde
    public void UpdateEventCamera()
    {
        // Suche nach der Kamera im XR Rig, wenn das XR Rig in der Szene ist
        xrCamera = FindObjectOfType<Camera>();

        if (xrCamera != null && worldSpaceCanvas != null)
        {
            // Setze die Event Camera des Canvas auf die Kamera des XR Rigs
            worldSpaceCanvas.worldCamera = xrCamera;
        }
        else
        {
            // Debug.LogError("XR Camera or Canvas missing.");
        }
    }
}

