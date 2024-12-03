using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnSelect : MonoBehaviour
{
    public string sceneName;

    public void OnSelectEnter()
    {
        SceneManager.LoadScene(sceneName);
    }
}

