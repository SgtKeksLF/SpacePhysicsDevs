using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistent_Objects : MonoBehaviour
{
     // Statische Liste für die persistierenden Objekte
    private static List<GameObject> persistentObjects = new List<GameObject>();

    private void Awake()
    {
        // Überprüfen, ob das Objekt der richtigen Art ist (z.B. XR Rig oder XR Interaction Manager)
        if (gameObject.CompareTag("Persistent"))
        {
            // Überprüfen, ob dieses Objekt schon in der Liste der persistierenden Objekte ist
            if (persistentObjects.Contains(gameObject))
            {
                // Wenn es bereits existiert, zerstöre dieses Objekt
                Destroy(gameObject);
            }
            else
            {
                // Andernfalls füge dieses Objekt zur Liste hinzu und mache es persistent
                persistentObjects.Add(gameObject);
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            // Wenn das Objekt nicht als persistent markiert ist, lösche es
            Destroy(gameObject);
        }
    }
}
