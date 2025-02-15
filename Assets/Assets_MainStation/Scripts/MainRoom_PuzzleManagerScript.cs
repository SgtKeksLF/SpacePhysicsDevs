using System.Collections;
using UnityEngine;

/*

This script checks if the player successfully collected all planet samples and placed them
in their right slot.

When this is the case, a win sound is played, the large Lamp turns green, the main light is turned of
and instead the spotlight turns on and the trophy rises from the floor.

*/

public class MainRoom_PuzzleManagerScript : MonoBehaviour
{
    public static MainRoom_PuzzleManagerScript Instance;

    public Renderer bigLampRenderer;
    public Material winMaterial;
    public Material loseMaterial;
    public AudioSource winSound;

    public GameObject trophyObject;
    public Transform trophy; 
    public Vector3 targetTrophyPosition; 
    public float trophyMoveSpeed = 1f; 
    public float delayBeforeMoving = 3f; 

    public Light mainLight; 
    public Light spotLight; 

    private MainRoom_SlotCheckerScript[] slots; // list of sample slots for checking if all probes were placed
    private bool hasPlayedWinSound = false;
    private Rigidbody trophyRB;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        slots = FindObjectsOfType<MainRoom_SlotCheckerScript>();
        if (slots.Length == 0)
        {
            // Debug.LogError("no slots found!");
        }

        trophyRB = trophyObject.GetComponent<Rigidbody>();
    }

    public void CheckWinCondition()
    {
        foreach (MainRoom_SlotCheckerScript slot in slots)
        {
            if (!slot.IsCorrect())
            {
                if (bigLampRenderer != null && loseMaterial != null)
                {
                    bigLampRenderer.material = loseMaterial;
                }

                hasPlayedWinSound = false; 
                return;
            }
        }

        if (bigLampRenderer != null && winMaterial != null)
        {
            bigLampRenderer.material = winMaterial;

            // Debug.Log("all samples correct");

            StartCoroutine(DelayedTrophyMovement());
        }

        if (!hasPlayedWinSound && winSound != null)
        {
            winSound.Play();
            hasPlayedWinSound = true; 
        }
    }

    private IEnumerator DelayedTrophyMovement()
    {
        yield return new WaitForSeconds(delayBeforeMoving);

        if (mainLight != null)
        {
            mainLight.enabled = false; 
        }

        if (spotLight != null)
        {
            spotLight.enabled = true; 
        }

        StartCoroutine(MoveTrophyUp());
    }

    private IEnumerator MoveTrophyUp()
    {
        if (trophyRB != null)
        {
            trophyRB.detectCollisions = false;
            trophyRB.isKinematic = true;
        }

        Vector3 startPosition = trophy.localPosition;

        while (Mathf.Abs(trophy.localPosition.y - targetTrophyPosition.y) > 0.01f)
        {
            trophy.localPosition = new Vector3(
                trophy.localPosition.x,
                Mathf.MoveTowards(trophy.localPosition.y, targetTrophyPosition.y, trophyMoveSpeed * Time.deltaTime),
                trophy.localPosition.z
            );
            yield return null;
        }

        trophy.localPosition = targetTrophyPosition;

        if (trophyRB != null)
        {
            trophyRB.detectCollisions = true;
            trophyRB.isKinematic = false;
        }

        // Debug.Log("Y-Position reached (local): " + targetTrophyPosition.y);

        StartCoroutine(FailSafeCheck());
    }

    private IEnumerator FailSafeCheck()
    {
        yield return new WaitForSeconds(1f); 

        if (trophy.localPosition != targetTrophyPosition)
        {
            trophy.localPosition = targetTrophyPosition;
        }
    }


}
