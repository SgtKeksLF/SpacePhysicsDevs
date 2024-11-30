using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptButton : MonoBehaviour
{
    public GameObject targetObject;
    public Material newMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      public void OnButtonPressed()
    {
         if (targetObject != null && newMaterial != null)
        {
            // Hole den Renderer des Ziel-GameObjects und setze das neue Material
            Renderer renderer = targetObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = newMaterial;
                Debug.Log("Material wurde geändert!");
            }
        }
        else
        {
            Debug.LogWarning("Ziel-Objekt oder Material wurde nicht zugewiesen.");
        }
    }
}
