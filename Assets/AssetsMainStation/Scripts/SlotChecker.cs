using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotChecker : MonoBehaviour
{
    [SerializeField] private string correctTag; 
    [SerializeField] private Material winMaterial; // Das grüne Material
    [SerializeField] private Material loseMaterial; // Das rote Material
    
    [SerializeField] private Renderer mainLampRenderer; // Der Renderer für das einzelne Kindobjekt
    [SerializeField] private Transform neonLightsParent; // Das leere Objekt, das die 3 anderen Objekte enthält

    private bool isCorrect = false; 

    private void Awake()
    {
        // Sicherstellen, dass die Referenzen im Inspector gesetzt wurden
        if (mainLampRenderer == null)
        {
            Debug.LogError($"⚠️ Kein Haupt-Renderer für {gameObject.name} gesetzt!");
        }

        if (neonLightsParent == null)
        {
            Debug.LogError($"⚠️ Kein Parent für zusätzliche Objekte gesetzt bei {gameObject.name}!");
        }

        // Standardmaterial auf Rot setzen
        SetMaterials(loseMaterial);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"🔍 Erkanntes Objekt: {other.gameObject.name}, Tag: {other.tag}, Collider: {other}");

        if (other.CompareTag(correctTag))
        {
            Debug.Log($"✅ Richtiger Gegenstand erkannt für {gameObject.name}");
            SetMaterials(winMaterial);
            isCorrect = true;
        }
        else
        {
            SetMaterials(loseMaterial);
            isCorrect = false;
        }

        PuzzleManager.Instance.CheckWinCondition();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(correctTag))
        {
            Debug.Log($"🔴 Richtiger Gegenstand entfernt von {gameObject.name}");
            SetMaterials(loseMaterial);
            isCorrect = false;
        }

        PuzzleManager.Instance.CheckWinCondition();
    }

    private void SetMaterials(Material newMaterial)
    {
        // Das eine Kindobjekt ändern
        if (mainLampRenderer != null)
        {
            mainLampRenderer.material = newMaterial;
        }

        // Die drei Kinder des "Empty"-Objekts ändern
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
}
