using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistent_Objects : MonoBehaviour
{
    //private static Persistent_Objects instance;

    private void Awake()
    {
     /*   if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;*/
        DontDestroyOnLoad(gameObject);
    }
}
