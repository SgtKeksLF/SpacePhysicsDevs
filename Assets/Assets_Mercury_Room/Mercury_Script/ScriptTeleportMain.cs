using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptTeleportMain : MonoBehaviour
{
    public string teleportTarget = "Lisa";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnButtonPressed()
    {
        Debug.Log("OnButtonPressed wurde aufgerufen!");
        SceneManager.LoadScene(teleportTarget);
        Debug.Log("Clicky clicky");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
