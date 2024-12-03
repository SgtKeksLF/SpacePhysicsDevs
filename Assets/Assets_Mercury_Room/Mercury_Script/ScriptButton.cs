using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class ScriptButton : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject prefabSample;
    public Material newMaterial;
    public Vector3 spawnPosition = new Vector3 (1, 1, 2);
    public Quaternion spawnRotation = Quaternion.identity;


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

        GameObject spawnedSample = Instantiate(prefabSample, spawnPosition, spawnRotation);

        spawnedSample.AddComponent<XRGrabInteractable>();
        if (spawnedSample.GetComponent<Rigidbody>() == null)
        {
            spawnedSample.AddComponent<Rigidbody>();
        }
        if (spawnedSample.GetComponent<BoxCollider>() == null)
        {
            spawnedSample.AddComponent<BoxCollider>();
        }

        // Verwende eine konkrete XRBaseGrabTransformer-Klasse
        spawnedSample.AddComponent<XRSingleGrabFreeTransformer>();

    }
}
