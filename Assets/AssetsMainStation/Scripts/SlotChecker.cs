using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SlotChecker : MonoBehaviour
{
    [SerializeField] private string correctTag; // Tag für die Kugel, die zu diesem Slot gehört
    [SerializeField] private Color greenColor = Color.green; // Farbe für richtig
    [SerializeField] private Color redColor = Color.red; // Farbe für falsch

    private Renderer lampRenderer; // Renderer der kleinen Lampe
    private bool isCorrect = false; // Status, ob die Kugel richtig ist

    private void Awake()
    {
        // Sucht den Renderer der kleinen Lampe im Kind-Objekt
        lampRenderer = GetComponentInChildren<Renderer>();
        if (lampRenderer == null)
        {
            Debug.LogError($"Kein Renderer für die Lampe gefunden in {gameObject.name}. Überprüfe, ob die Lampe ein Kind dieses Slots ist.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(correctTag))
        {
            // Richtige Kugel
            lampRenderer.material.color = greenColor;
            isCorrect = true;
        }
        else
        {
            // Falsche Kugel
            lampRenderer.material.color = redColor;
            isCorrect = false;
        }
        PuzzleManager.Instance.CheckWinCondition();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(correctTag))
        {
            // Zurücksetzen, wenn die korrekte Kugel entfernt wird
            lampRenderer.material.color = Color.white;
            isCorrect = false;
        }
        PuzzleManager.Instance.CheckWinCondition();
    }

    public bool IsCorrect()
    {
        return isCorrect;
    }
}
