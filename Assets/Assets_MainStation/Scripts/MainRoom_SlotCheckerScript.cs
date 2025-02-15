using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoom_SlotCheckerScript : MonoBehaviour
{
    [SerializeField] private string correctTag;
    [SerializeField] private Material winMaterial; // Das grüne Material
    [SerializeField] private Material loseMaterial; // Das rote Material

    [SerializeField] private Renderer mainLampRenderer; // Der Renderer für das einzelne Kindobjekt
    [SerializeField] private Transform neonLightsParent; // Das leere Objekt, das die 3 anderen Objekte enthält
    private Rigidbody rbObject;
    private Collider lastCollider; // Speichert den letzten Collider für die Positionierung

    private bool isCorrect = false;

    public Vector3 fixedRotation = new Vector3(0, 0, 0); // Gewünschte Rotation in Grad

    private void Awake()
    {
        if (mainLampRenderer == null)
        {
            // Debug.LogError($"No renderer für {gameObject.name} defined!");
        }

        if (neonLightsParent == null)
        {
            // Debug.LogError($"No parent for additional objects of {gameObject.name}!");
        }

        SetMaterials(loseMaterial);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log($"Trigger object: {other.gameObject.name}, tag: {other.tag}, collider: {other}");

        if (other.CompareTag(correctTag))
        {
            // Debug.Log($"Correct Item for {gameObject.name}");
            rbObject = other.GetComponent<Rigidbody>();
            lastCollider = other; // Speichert den Collider für die spätere Positionierung
            FreezeObject();
            SetMaterials(winMaterial);
            isCorrect = true;
        }
        else
        {
            SetMaterials(loseMaterial);
            isCorrect = false;
        }

        MainRoom_PuzzleManagerScript.Instance.CheckWinCondition();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(correctTag))
        {
            Debug.Log($"🔴 Richtiger Gegenstand entfernt von {gameObject.name}");
            SetMaterials(loseMaterial);
            isCorrect = false;
        }

        MainRoom_PuzzleManagerScript.Instance.CheckWinCondition();
    }

    private void SetMaterials(Material newMaterial)
    {
        if (mainLampRenderer != null)
        {
            mainLampRenderer.material = newMaterial;
        }

        if (neonLightsParent != null)
        {
            Renderer[] childRenderers = neonLightsParent.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in childRenderers)
            {
                rend.material = newMaterial;
            }
        }
    }

    public bool IsCorrect()
    {
        return isCorrect;
    }

    void FreezeObject()
    {
        if (rbObject != null && lastCollider != null)
        {
            // Setzt die Position auf die Mitte des Slots
            rbObject.transform.position = transform.position;

            // Setzt die Rotation auf den festen Wert
            rbObject.transform.rotation = Quaternion.Euler(fixedRotation);

            rbObject.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}
