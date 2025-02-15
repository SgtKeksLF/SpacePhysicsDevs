using System.Collections;
using UnityEngine;

/*

This Script manages the quizz canvases placed in ech planet room.

With a delay it changes the shown displays as on the last quiz display is no button
and the player is not meant to change the display himself.

*/

public class Global_QuizManagerScript : MonoBehaviour
{
    public GameObject quiz3;
    public GameObject quiz5;

    private bool isCountdownRunning = false;

    void Update()
    {
        if (quiz3 != null && quiz5 != null && quiz3.activeSelf && !isCountdownRunning)
        {
            StartCoroutine(CountdownToSwitch());
        }
    }

    private IEnumerator CountdownToSwitch()
    {
        isCountdownRunning = true;

        yield return new WaitForSeconds(1f);

        if (quiz3 != null && quiz5 != null)
        {
            quiz3.SetActive(false);
            quiz5.SetActive(true);
        }

        isCountdownRunning = false;
    }
}

