using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Door : MonoBehaviour
{
    
    public GameObject door; // Tür als zuweisbares GameObject
    public float moveDistance = 3f; // Wie weit sich die Tür bewegt
    public float moveSpeed = 2f; // Geschwindigkeit der Bewegung

    private bool isMoving = false;
    private bool isOpen = false; // Speichert, ob die Tür offen oder geschlossen ist
    private Vector3 startPos;
    private Vector3 targetPos;

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

    private System.Collections.IEnumerator MoveDoorCoroutine(Vector3 endPos)
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
