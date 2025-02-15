using System.Collections;
using UnityEngine;
using UnityEngine.Video;
/*

The tutorial version of the drilling script

Attached to the drill trigger is the start of the video

*/


public class Tutorial_DrillTriggerScript : MonoBehaviour
{
    public GameObject drill;       
    public GameObject samplePrefab; 
    public Transform spawnPoint;   
    public AudioSource audioSource; 
    public VideoPlayer videoPlayer; 
    public GameObject player; 
    public GameObject displayVideo; 
    public GameObject display5; 
    public GameObject display4;

    public Light mainLight;
    public Light spotLightCanvas;



    private bool hasTriggered = false; 

    private void OnTriggerEnter(Collider other)
    {
     
        if (!hasTriggered && other.gameObject == drill)
        {
            hasTriggered = true; 
            
    
            if (audioSource != null)
            {
                audioSource.Play();
            }

            if (samplePrefab != null && spawnPoint != null)
            {
                Instantiate(samplePrefab, spawnPoint.position, spawnPoint.rotation);

              
                FreezePlayerMovement();

              
                StartCoroutine(PlayVideoWithDelay(3f)); 
            }
        }
    }

    private IEnumerator PlayVideoWithDelay(float delay)
    {


        mainLight.enabled = false;


        spotLightCanvas.enabled = true;

        yield return new WaitForSeconds(delay); 

        if (displayVideo != null)
        {
            displayVideo.SetActive(true); 
            display4.SetActive(false);
        }

        if (videoPlayer != null)
        {
            videoPlayer.Play(); 
            videoPlayer.loopPointReached += OnVideoFinished; 
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        if (displayVideo != null)
        {
            displayVideo.SetActive(false); 
        }

        if (display5 != null)
        {
            display5.SetActive(true); 
        }

  
        UnfreezePlayerMovement();
    }

    private void FreezePlayerMovement()
    {
        if (player != null)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    private void UnfreezePlayerMovement()
    {
        if (player != null)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.None; 
            }
        }
    }
}
