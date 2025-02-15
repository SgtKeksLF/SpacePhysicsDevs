using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoom_DisplayManagerScript : MonoBehaviour
{
    private GameObject currentDisplay; // Das aktuelle aktive Display
    private GameObject previousDisplay; // Das zuletzt aktive Display vor DisplaySettings

    private const string SETTINGS_DISPLAY_NAME = "DisplaySettings";
    private GameObject settingsDisplay;

    void Start()
    {
        // Durchlaufe alle Kinder, um auch deaktivierte Objekte zu finden
        settingsDisplay = FindChildByName(transform, SETTINGS_DISPLAY_NAME);

        if (settingsDisplay == null)
        {
            Debug.LogError($"Settings Display ({SETTINGS_DISPLAY_NAME}) konnte nicht gefunden werden!");
            return;
        }

        // Initiales Setup, falls nötig
        currentDisplay = FindActiveDisplay();
        if (currentDisplay == null)
        {
            Debug.LogError("Kein aktives Display gefunden!");
        }
    }

    void Update()
    {
        // Aktualisiere das aktuelle Display in jedem Frame
        GameObject newCurrentDisplay = FindActiveDisplay();

        if (newCurrentDisplay != currentDisplay)
        {
            currentDisplay = newCurrentDisplay;
        }
    }

    private GameObject FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    // Methode für den Settings-Button
    public void OpenSettings()
    {
        if (settingsDisplay == null || currentDisplay == null)
            return;

        // Vorheriges Display speichern
        previousDisplay = currentDisplay;

        // Aktuelles Display deaktivieren und Settings-Display aktivieren
        currentDisplay.SetActive(false);
        settingsDisplay.SetActive(true);

        // Aktuelles Display auf Settings setzen
        currentDisplay = settingsDisplay;
    }

    // Methode für den Zurück-Button im Settings-Display
    public void ReturnToPreviousDisplay()
    {
        if (previousDisplay == null || settingsDisplay == null)
            return;

        // Settings-Display deaktivieren und vorheriges Display aktivieren
        settingsDisplay.SetActive(false);
        previousDisplay.SetActive(true);

        // Aktuelles Display auf vorheriges Display setzen
        currentDisplay = previousDisplay;

        // Vorheriges Display zurücksetzen, da wir wieder auf dem ursprünglichen sind
        previousDisplay = null;
    }

    // Hilfsmethode: Finde das aktuell aktive Display
    private GameObject FindActiveDisplay()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                return child.gameObject;
            }
        }
        return null;
    }
}
