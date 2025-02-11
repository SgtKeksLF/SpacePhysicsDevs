using System.Collections;
using UnityEngine;

public class Script_Door : MonoBehaviour
{
    public GameObject door;
    public float moveDistance = 3f;
    public float moveSpeed = 2f;
    public float autoCloseDelay = 5f; // Zeit bis zum automatischen Schließen

    private AudioSource[] audioSources;
    private AudioSource openSound;
    private AudioSource closeSound;

    private bool isMoving = false;
    private bool isOpen = false;
    private Vector3 startPos;
    private Vector3 targetPos;

    void Start()
    {
        if (door != null)
        {
            startPos = door.transform.position;
            targetPos = startPos + Vector3.up * moveDistance;
            
            audioSources = door.GetComponents<AudioSource>();
            if (audioSources.Length >= 2)
            {
                openSound = audioSources[0];
                closeSound = audioSources[1];
            }
            else
            {
                Debug.LogError("Nicht genügend AudioSources auf der Tür vorhanden!", this);
            }
        }
        else
        {
            Debug.LogError("Tür-GameObject nicht zugewiesen!", this);
        }
    }

    public void ToggleDoor()
    {
        if (!isMoving && door != null)
        {
            isMoving = true;
            StartCoroutine(isOpen ? MoveDoorCoroutine(startPos) : MoveDoorCoroutine(targetPos));
            
            if (isOpen && closeSound != null)
            {
                closeSound.Play();
            }
            else if (!isOpen && openSound != null)
            {
                openSound.Play();
            }
            
            isOpen = !isOpen;
            
            if (isOpen)
            {
                StartCoroutine(AutoCloseDoor());
            }
        }
    }

    private IEnumerator MoveDoorCoroutine(Vector3 endPos)
    {
        Vector3 start = door.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            door.transform.position = Vector3.Lerp(start, endPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        door.transform.position = endPos;
        isMoving = false;
    }

    private IEnumerator AutoCloseDoor()
    {
        yield return new WaitForSeconds(autoCloseDelay);
        if (isOpen)
        {
            ToggleDoor();
        }
    }
}
