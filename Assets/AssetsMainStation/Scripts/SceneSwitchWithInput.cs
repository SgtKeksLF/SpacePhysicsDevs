using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneSwitchWithInput : MonoBehaviour
{
    public string sceneName;
    public InputActionProperty switchAction; // Verbindung zur Input Action

    private void OnEnable()
    {
        // Event abonnieren
        switchAction.action.performed += OnSwitchAction;
    }

    private void OnDisable()
    {
        // Event abbestellen
        switchAction.action.performed -= OnSwitchAction;
    }

    private void OnSwitchAction(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(sceneName);
    }
}


