using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

This script shows a confirmation menu when trying to leave the game.

*/


public class Global_EndGameScript : MonoBehaviour
{
    public GameObject quitConfirmationCanvas; 

    void Start()
    {
        quitConfirmationCanvas.SetActive(false); 
    }

    public void ShowQuitConfirmation()
    {
        quitConfirmationCanvas.SetActive(true); 
    }

    // player cancels quit
    public void CancelQuit()
    {
        quitConfirmationCanvas.SetActive(false); 
    }

    // player conforms quit
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

