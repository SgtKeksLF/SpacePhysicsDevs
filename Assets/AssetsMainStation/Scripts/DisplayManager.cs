using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Setting Display should be reachable from every display - niw error because of indeces

public class DisplayManager : MonoBehaviour
{

public GameObject[] displays;  // Array of stripe displays (Images)
public GameObject[] buttonGroups;  // Array of ButtonGroups: buttons grouped by display belongigns

private int currentDisplayIndex = -1; 

void Start()
{
    Debug.Log("Starte Initialisierung...");
    InitializeDisplaysAndButtons();

    Debug.Log("Displays gefunden: " + displays.Length);
    Debug.Log("Button-Gruppen gefunden: " + buttonGroups.Length);

    SetActiveDisplay(0); // show first display
}

// display and button array initialized 
private void InitializeDisplaysAndButtons()
{
    // deactivate all displays and buttons 
    foreach (var display in displays)
    {
        display.SetActive(false); 
    }

    foreach (var buttonGroup in buttonGroups)
    {
        buttonGroup.SetActive(false); 
    }
}

// change display's index with button click 
public void OnButtonClick(int newDisplayIndex)
{
    Debug.Log("Button clicked! Switching to display: " + newDisplayIndex);
    SetActiveDisplay(newDisplayIndex);
}

// change active display
private void SetActiveDisplay(int newDisplayIndex)
{
    // deactivate former display and its buttons
    if (currentDisplayIndex >= 0)
    {
        displays[currentDisplayIndex].SetActive(false);
        buttonGroups[currentDisplayIndex].SetActive(false); 
    }

    // activate new display and its buttons
    currentDisplayIndex = newDisplayIndex;
    displays[currentDisplayIndex].SetActive(true);
    buttonGroups[currentDisplayIndex].SetActive(true); 
}
}

