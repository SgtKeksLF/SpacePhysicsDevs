using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public GameObject quiz3;  // Das Display, das für 10 Sekunden aktiv sein soll
    public GameObject quiz5;  // Das Display, das danach aktiviert wird

    private bool isCountdownRunning = false;

    void Update()
    {
        // Wenn Quiz3 aktiv ist und der Countdown noch nicht läuft, starte ihn
        if (quiz3 != null && quiz5 != null && quiz3.activeSelf && !isCountdownRunning)
        {
            StartCoroutine(CountdownToSwitch());
        }
    }

    private IEnumerator CountdownToSwitch()
    {
        isCountdownRunning = true;  // Countdown-Flag setzen, damit er nicht mehrfach startet

        yield return new WaitForSeconds(1f);  // 5 Sekunden warten

        if (quiz3 != null && quiz5 != null)  
        {
            quiz3.SetActive(false);  // Quiz3 ausblenden
            quiz5.SetActive(true);   // Quiz5 anzeigen
        }

        isCountdownRunning = false; // Countdown zurücksetzen, falls Quiz3 später erneut aktiviert wird
    }
}

