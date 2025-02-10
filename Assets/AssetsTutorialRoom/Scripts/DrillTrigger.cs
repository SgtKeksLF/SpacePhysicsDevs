using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class DrillTrigger : MonoBehaviour
{
    public GameObject drill;       
    public GameObject probePrefab; 
    public Transform spawnPoint;   
    public AudioSource audioSource; 
    public VideoPlayer videoPlayer; // Referenz zum VideoPlayer
    public GameObject player; // Spieler-Referenz
    public GameObject displayVideo; // Display, das das Video zeigt (RawImage)
    public GameObject display5; // Das neue Display nach dem Video
    public GameObject display4; // Das neue Display nach dem Video


    private bool hasTriggered = false; 

    private void OnTriggerEnter(Collider other)
    {
        // Überprüfen, ob das Objekt "Drill" in den Collider eintritt
        if (!hasTriggered && other.gameObject == drill)
        {
            hasTriggered = true; 
            
            // Spiele den Audio-Sound ab
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Erzeuge die "Probe" an der vorgesehenen Stelle
            if (probePrefab != null && spawnPoint != null)
            {
                Instantiate(probePrefab, spawnPoint.position, spawnPoint.rotation);

                // Spielerbewegung einfrieren
                FreezePlayerMovement();

                // Starte die Verzögerung für das Video
                StartCoroutine(PlayVideoWithDelay(10f)); 
            }
        }
    }

    private IEnumerator PlayVideoWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 10 Sekunden warten

        if (displayVideo != null)
        {
            displayVideo.SetActive(true); 
            display4.SetActive(false);
        }

        if (videoPlayer != null)
        {
            videoPlayer.Play(); // Video starten
            videoPlayer.loopPointReached += OnVideoFinished; // Event für das Ende des Videos
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        if (displayVideo != null)
        {
            displayVideo.SetActive(false); // Video-Display ausblenden
        }

        if (display5 != null)
        {
            display5.SetActive(true); // Neues Display aktivieren
        }

        // Spieler wieder freigeben
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
                rb.constraints = RigidbodyConstraints.None; // Alle Einschränkungen entfernen
            }
        }
    }
}
