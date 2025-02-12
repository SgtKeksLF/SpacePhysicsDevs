using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance; // Singleton-Instanz

    [SerializeField] private Renderer bigLampRenderer; // Renderer der großen Lampe
    [SerializeField] private Material winMaterial; // Material für gewonnen (grün)
    [SerializeField] private Material loseMaterial; // Material für verloren (rot)

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
                // Wenn ein Slot nicht korrekt ist, großes Lampen-Material auf "loseMaterial" setzen
                if (bigLampRenderer != null && loseMaterial != null)
                {
                    bigLampRenderer.material = loseMaterial;
                }
                return;
            }
        }

        // Wenn alle Slots korrekt sind, Material auf "winMaterial" setzen
        if (bigLampRenderer != null && winMaterial != null)
        {
            bigLampRenderer.material = winMaterial;
        }

        Debug.Log("Puzzle gelöst!");
    }
}
