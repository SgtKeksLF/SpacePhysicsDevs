using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SlotChecker : MonoBehaviour
{
    [SerializeField] private string correctTag; 
    [SerializeField] private Material greenMaterial; // Material für die grüne Lampe
    [SerializeField] private Material redMaterial;   // Material für die rote Lampe
    [SerializeField] private Renderer lampRenderer; // Lampe wird jetzt im Inspector zugewiesen

    private bool isCorrect = false; 

    private void Awake()
    {
        // Sicherstellen, dass eine Lampe zugewiesen wurde
        if (lampRenderer == null)
        {
            Debug.LogError($"Kein Renderer für die Lampe zugewiesen in {gameObject.name}! Bitte im Inspector setzen.");
        }

        // Standardmaterial der Lampe: rot
        if (lampRenderer != null && redMaterial != null)
        {
            lampRenderer.material = redMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (lampRenderer == null) return;

        if (other.CompareTag(correctTag))
        {
            // Richtige Kugel -> grünes Material
            lampRenderer.material = greenMaterial;
            isCorrect = true;
        }
        else
        {
            // Falsche Kugel -> rotes Material
            lampRenderer.material = redMaterial;
            isCorrect = false;
        }
        PuzzleManager.Instance.CheckWinCondition();
    }

    private void OnTriggerExit(Collider other)
    {
        if (lampRenderer == null) return;

        if (other.CompareTag(correctTag))
        {
            // Wenn die richtige Kugel entfernt wird -> wieder rotes Material
            lampRenderer.material = redMaterial;
            isCorrect = false;
        }
        PuzzleManager.Instance.CheckWinCondition();
    }

    public bool IsCorrect()
    {
        return isCorrect;
    }
}
