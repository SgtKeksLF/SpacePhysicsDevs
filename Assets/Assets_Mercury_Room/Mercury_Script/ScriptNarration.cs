using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptNarration : MonoBehaviour
{ private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnButtonPressed()
    {
       
        Narration();

    }

       public void Narration(){
         audioSource = GetComponent<AudioSource>();
         if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
