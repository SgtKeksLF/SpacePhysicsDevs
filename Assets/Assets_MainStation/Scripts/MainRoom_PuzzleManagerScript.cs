using System.Collections;
using UnityEngine;

public class MainRoom_PuzzleManagerScript : MonoBehaviour
{
    public static MainRoom_PuzzleManagerScript Instance; // Singleton-Instanz

    [SerializeField] private Renderer bigLampRenderer; // Renderer der großen Lampe
    [SerializeField] private Material winMaterial; // Material für gewonnen (grün)
    [SerializeField] private Material loseMaterial; // Material für verloren (rot)
    [SerializeField] private AudioSource winSound; // Audioquelle für den Sieg-Sound

    [SerializeField] private Transform trophy; // Die "Probe", die aus dem Boden fahren soll
    [SerializeField] private Vector3 targetTrophyPosition; // Zielposition für die Probe
    [SerializeField] private float trophyMoveSpeed = 1f; // Geschwindigkeit, mit der die Probe sich bewegt
    [SerializeField] private float delayBeforeMoving = 3f; // Verzögerung von 3 Sekunden
    [SerializeField] private GameObject trophyObject;

    [SerializeField] private Light mainLight; // Das Hauptlicht
    [SerializeField] private Light spotLight; // Das Spotlight

    private MainRoom_SlotCheckerScript[] slots; // Liste aller Slots
    private bool hasPlayedWinSound = false; // Damit der Sound nur einmal abgespielt wird
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

        // findet alle MainRoom_SlotCheckerScript
        slots = FindObjectsOfType<MainRoom_SlotCheckerScript>();
        if (slots.Length == 0)
        {
            // Debug.LogError("no Slots found!");
        }

        trophyRB = trophyObject.GetComponent<Rigidbody>();
    }

    public void CheckWinCondition()
    {
        foreach (MainRoom_SlotCheckerScript slot in slots)
        {
            if (!slot.IsCorrect())
            {
                // wenn ein Slot nicht korrekt ist, großes Lampen-Material auf "loseMaterial" setzen
                if (bigLampRenderer != null && loseMaterial != null)
                {
                    bigLampRenderer.material = loseMaterial;
                }

                hasPlayedWinSound = false; // Falls das Puzzle später gelöst wird, Sound wieder erlauben
                return;
            }
        }

        // Wenn alle Slots korrekt sind, Material auf "winMaterial" setzen
        if (bigLampRenderer != null && winMaterial != null)
        {
            bigLampRenderer.material = winMaterial;

            // Debug-Ausgabe, um zu prüfen, ob die Funktion zum Bewegen aufgerufen wird
            // Debug.Log("all samples correct");

            // Führe die Funktion mit einer Verzögerung aus
            StartCoroutine(DelayedTrophyMovement());
        }

        // Spiele den Sieg-Sound nur, wenn er noch nicht abgespielt wurde
        if (!hasPlayedWinSound && winSound != null)
        {
            winSound.Play();
            hasPlayedWinSound = true; // Verhindert mehrfaches Abspielen
        }

        // Debug.Log("samples completed");
    }

    private IEnumerator DelayedTrophyMovement()
    {
        // Warte für die Verzögerung
        yield return new WaitForSeconds(delayBeforeMoving);

        // Deaktiviere das Hauptlicht und aktiviere das Spotlight
        if (mainLight != null)
        {
            mainLight.enabled = false; // Hauptlicht aus
        }

        if (spotLight != null)
        {
            spotLight.enabled = true; // Spotlight an
        }

        // Bewege die Probe nach oben
        StartCoroutine(MoveTrophyUp());
    }

 private IEnumerator MoveTrophyUp()
{
    if (trophyRB != null)
    {
        // Debug.Log("RB not null");
        // Debug.Log("Rigidbody found: " + trophyRB.name);
        trophyRB.detectCollisions = false;
        trophyRB.isKinematic = true;
    }

    Vector3 startPosition = trophy.localPosition;

    // Debug.Log("MoveTrophyUp coroutine started. starting position: " + startPosition);

    while (Mathf.Abs(trophy.localPosition.y - targetTrophyPosition.y) > 0.01f)
    {
        trophy.localPosition = new Vector3(
            trophy.localPosition.x,
            Mathf.MoveTowards(trophy.localPosition.y, targetTrophyPosition.y, trophyMoveSpeed * Time.deltaTime),
            trophy.localPosition.z
        );
        yield return null;
    }

    // Setze die Position explizit auf das Ziel
    trophy.localPosition = targetTrophyPosition;

    if (trophyRB != null)
    {
        trophyRB.detectCollisions = true;
        trophyRB.isKinematic = false;
    }

    // Debug.Log("Y-Position reached (local): " + targetTrophyPosition.y);

    // Fail-Safe nach kurzer Zeit
    StartCoroutine(FailSafeCheck());
}

private IEnumerator FailSafeCheck()
{
    yield return new WaitForSeconds(1f); // 1 Sekunde warten

    if (trophy.localPosition != targetTrophyPosition)
    {
        Debug.LogWarning("Fail-Safe aktiviert! Position wird korrigiert.");
        trophy.localPosition = targetTrophyPosition;
    }
}


}
