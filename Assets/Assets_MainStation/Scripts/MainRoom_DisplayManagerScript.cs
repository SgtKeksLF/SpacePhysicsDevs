using UnityEngine;

/*

This is a helper script to coordinate the activation of the settings display on UI canvases.

This makes it possible to navigate to the previously activated display from the settings display.

*/

public class MainRoom_DisplayManagerScript : MonoBehaviour
{
    private GameObject currentDisplay; 
    private GameObject previousDisplay; 

    private const string SETTINGS_DISPLAY_NAME = "DisplaySettings";
    private GameObject settingsDisplay;

    void Start()
    {
        settingsDisplay = FindChildByName(transform, SETTINGS_DISPLAY_NAME);

        if (settingsDisplay == null)
        {
            // Debug.LogError($"Settings Display ({SETTINGS_DISPLAY_NAME}) missing");
            return;
        }

        currentDisplay = FindActiveDisplay();
        if (currentDisplay == null)
        {
            // Debug.LogError("no active display");
        }
    }

    void Update()
    {
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

    public void OpenSettings()
    {
        if (settingsDisplay == null || currentDisplay == null)
            return;

        previousDisplay = currentDisplay;

        currentDisplay.SetActive(false);
        settingsDisplay.SetActive(true);

        currentDisplay = settingsDisplay;
    }

    public void ReturnToPreviousDisplay()
    {
        if (previousDisplay == null || settingsDisplay == null)
            return;

        settingsDisplay.SetActive(false);
        previousDisplay.SetActive(true);

        currentDisplay = previousDisplay;

        previousDisplay = null;
    }

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
