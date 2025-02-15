using UnityEngine;

/*

This script sets the EventCamera for UI Canvases.

*/

public class MainRoom_EventCameraScript : MonoBehaviour
{
    public Canvas worldSpaceCanvas;  
    private Camera xrCamera;

    void Start()
    {
        
        UpdateEventCamera();
    }

    
    public void UpdateEventCamera()
    {
        
        xrCamera = FindObjectOfType<Camera>();

        if (xrCamera != null && worldSpaceCanvas != null)
        {
            
            worldSpaceCanvas.worldCamera = xrCamera;
        }
        else
        {
            // Debug.LogError("XR Camera or Canvas missing.");
        }
    }
}

