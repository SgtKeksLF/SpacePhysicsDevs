using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScriptPickkingUpObject : MonoBehaviour
{

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void Awake()
    {
        // Referenz zur AudioSource des Objekts
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("Keine AudioSource auf diesem Objekt gefunden!");
        }
    }

    public void OnGrab()
    {   
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Prüfe, ob das Objekt mit bestimmten Tags oder Layers kollidiert (optional)
      

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

}
