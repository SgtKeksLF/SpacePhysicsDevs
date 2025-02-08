using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Move_Drill : MonoBehaviour
{
    public GameObject objectToMove;
    public float distance = 1.5f;
    public float speed = 0.5f;

    private Vector3 targetPosition;
    private bool moving = false;

    void Start()
    {
        if (objectToMove != null)
        {
            targetPosition = objectToMove.transform.position + Vector3.forward * distance;
        }
    }

    public void StartMovement()
    {
        if (!moving && objectToMove != null)
        {
            StartCoroutine(MoveCoroutine());
        }
    }

    private IEnumerator MoveCoroutine()
    {
        moving = true;
        Vector3 startPosition = objectToMove.transform.position;
        float journey = 0f;
        float duration = distance / speed;

        while (journey < duration)
        {
            journey += Time.deltaTime;
            objectToMove.transform.position = Vector3.Lerp(startPosition, targetPosition, journey / duration);
            yield return null;
        }

        objectToMove.transform.position = targetPosition;
        moving = false;
    }
}
