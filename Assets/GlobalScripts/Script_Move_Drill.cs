using System.Collections;
using UnityEngine;

public class Script_Move_Drill : MonoBehaviour
{
    public GameObject objectToMove;
    public GameObject objectToTeleport;
    public GameObject objectToTeleportTo;
    public float distance = 1.5f;
    public float speed = 0.5f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool moving = false;
    private bool isAtTarget = false;

    void Start()
    {
        if (objectToMove != null)
        {
            startPosition = objectToMove.transform.position;
            targetPosition = startPosition + Vector3.forward * distance;
        }
    }

    public void MoveToTarget()
    {
        if (!moving && objectToMove != null)
        {
            if (objectToTeleport != null)
            {
                objectToTeleport.transform.position = objectToTeleportTo.transform.position;
            }
            StartCoroutine(MoveCoroutine(targetPosition));
            isAtTarget = true;
        }
    }

    public void MoveToStart()
    {
        if (!moving && objectToMove != null)
        {
            if (objectToTeleport != null)
            {
                objectToTeleport.transform.position = objectToTeleportTo.transform.position;
            }
            StartCoroutine(MoveCoroutine(startPosition));
            isAtTarget = false;
        }
    }

    private IEnumerator MoveCoroutine(Vector3 destination)
    {
        moving = true;
        Vector3 startPos = objectToMove.transform.position;
        float journey = 0f;
        float duration = Vector3.Distance(startPos, destination) / speed;

        while (journey < duration)
        {
            journey += Time.deltaTime;
            objectToMove.transform.position = Vector3.Lerp(startPos, destination, journey / duration);
            yield return null;
        }

        objectToMove.transform.position = destination;
        moving = false;
    }
}
