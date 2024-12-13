using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] scenesToLoad;
    // Start is called before the first frame update
    void Start()
    {
        foreach (string sceneName in scenesToLoad)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
