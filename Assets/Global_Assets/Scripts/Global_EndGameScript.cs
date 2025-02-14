using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_EndGameScript : MonoBehaviour
{
    // Funktion, um das Spiel zu beenden
    public void QuitGame()
    {
        // Überprüfen, ob das Spiel im Editor läuft
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Beendet das Spiel in einer Build-Version
            Application.Quit();
        #endif
    }
}
