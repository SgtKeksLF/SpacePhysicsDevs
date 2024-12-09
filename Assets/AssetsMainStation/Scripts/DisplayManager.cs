using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    public GameObject[] displays; // Array mit allen Display-GameObjects

    public void ShowDisplay(int displayIndex)
    {
        // Alle Displays deaktivieren
        foreach (GameObject display in displays)
        {
            display.SetActive(false);
        }

        // Gewünschtes Display aktivieren
        if (displayIndex >= 0 && displayIndex < displays.Length)
        {
            displays[displayIndex].SetActive(true);
        }
    }
}


