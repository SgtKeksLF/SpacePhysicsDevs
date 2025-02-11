using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DoorTut : MonoBehaviour
{
     public GameObject door;
    public float moveDistance = 3f;
    public float moveSpeed = 2f;
    private bool isMoving = false;
    private bool isOpen = false;
    private Vector3 startPos;
    private Vector3 targetPos;
    private VideoPlayer videoPlayer;

    void Start()
    {
        if (door != null)
        {
            startPos = door.transform.position;
            targetPos = startPos + Vector3.up * moveDistance;
        }
        else
        {
            Debug.LogError("Tür-GameObject nicht zugewiesen!", this);
        }

        videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd; // Event abonnieren
        }
        else
        {
            Debug.LogError("Kein VideoPlayer an diesem GameObject gefunden!", this);
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        ToggleDoor(); // Tür öffnen, wenn das Video endet
    }

    public void ToggleDoor()
    {
        if (!isMoving && door != null)
        {
            isMoving = true;
            StartCoroutine(isOpen ? MoveDoorCoroutine(startPos) : MoveDoorCoroutine(targetPos));
            isOpen = !isOpen;
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
}
