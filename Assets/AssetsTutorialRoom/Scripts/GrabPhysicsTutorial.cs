using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPhysicsTutorial : MonoBehaviour
{

    public GameObject ball;
    public GameObject canvasGravity;
 
    
    public void OnGrab(GameObject grabbedObject)
    {
       
        if (grabbedObject == ball)
        {                
             // Zugriff auf das Display innerhalb des Canvas
            GameObject display0 = canvasGravity.transform.Find("Canvas/Displays/Display0")?.gameObject;
            GameObject display1 = canvasGravity.transform.Find("Canvas/Displays/Display1")?.gameObject;

            if (display0 != null && display1 != null)
                {
                display0.SetActive(false); // Display0 deaktivieren
                display1.SetActive(true);  // Display1 aktivieren
                }
        }
                  
    }

}
