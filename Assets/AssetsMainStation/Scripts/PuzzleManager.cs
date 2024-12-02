using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance; // Singleton-Instanz
    [SerializeField] private Renderer bigLampRenderer; // Renderer der großen Lampe
    [SerializeField] private Color winColor = Color.green; // Farbe für gewonnen

    private SlotChecker[] slots; // Liste aller Slots

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Findet alle SlotChecker automatisch
        slots = FindObjectsOfType<SlotChecker>();
        if (slots.Length == 0)
        {
            Debug.LogError("Keine Slots gefunden!");
        }
    }

    public void CheckWinCondition()
    {
        foreach (SlotChecker slot in slots)
        {
            if (!slot.IsCorrect())
            {
                // Wenn ein Slot nicht korrekt ist, große Lampe zurücksetzen
                bigLampRenderer.material.color = Color.white;
                return;
            }
        }

        // Wenn alle Slots korrekt sind
        bigLampRenderer.material.color = winColor;
        Debug.Log("Puzzle gelöst!");
    }
}

