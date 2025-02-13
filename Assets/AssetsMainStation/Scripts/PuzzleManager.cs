using System.Collections;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance; // Singleton-Instanz

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

    private SlotChecker[] slots; // Liste aller Slots
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

        // findet alle SlotChecker
        slots = FindObjectsOfType<SlotChecker>();
        if (slots.Length == 0)
        {
            Debug.LogError("Keine Slots gefunden!");
        }

        trophyRB = trophyObject.GetComponent<Rigidbody>();
    }

    public void CheckWinCondition()
    {
        foreach (SlotChecker slot in slots)
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
            Debug.Log("Alle Slots korrekt! Starte die Bewegung der Probe.");

            // Führe die Funktion mit einer Verzögerung aus
            StartCoroutine(DelayedTrophyMovement());
        }

        // Spiele den Sieg-Sound nur, wenn er noch nicht abgespielt wurde
        if (!hasPlayedWinSound && winSound != null)
        {
            winSound.Play();
            hasPlayedWinSound = true; // Verhindert mehrfaches Abspielen
        }

        Debug.Log("🎉 Puzzle gelöst!");
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
            Debug.Log("RB Not null");
            Debug.Log("Rigidbody gefunden: " + trophyRB.name);
            trophyRB.detectCollisions = false;
            trophyRB.isKinematic = true;
        }
        // Setzt die Anfangsposition des Parents (lokale Position)
        Vector3 startPosition = trophy.localPosition; // Nutze die lokale Position des Parent-Objekts

        // Debug-Ausgabe, um zu prüfen, ob die Coroutine gestartet wurde
        Debug.Log("MoveTrophyUp Coroutine gestartet. Startposition: " + startPosition);

        // Bewege das Parent-Objekt in Richtung der Ziel Y-Position
        while (Mathf.Abs(trophy.localPosition.y - targetTrophyPosition.y) > 0.01f)
        {
            // Debug-Ausgabe, um die aktuelle Position während der Bewegung zu überwachen
           // Debug.Log("Aktuelle Y-Position der Probe (lokal): " + trophy.localPosition.y);

            // Bewege die Probe nur in der Y-Richtung (lokale Position des Parent-Objekts)
            trophy.localPosition = new Vector3(trophy.localPosition.x, Mathf.MoveTowards(trophy.localPosition.y, targetTrophyPosition.y, trophyMoveSpeed * Time.deltaTime), trophy.localPosition.z);
            yield return null; // Warten bis zum nächsten Frame
        }

        // Wenn die Zielposition erreicht ist, setze die Y-Position auf exakt die Zielposition (lokal)
        trophy.localPosition = new Vector3(trophy.localPosition.x, targetTrophyPosition.y, trophy.localPosition.z);
        if (trophyRB != null)
        {
            trophyRB.detectCollisions = true;
            trophyRB.isKinematic = false;
        }

        // Debug-Ausgabe, wenn die Bewegung abgeschlossen ist
        Debug.Log("Ziel Y-Position erreicht (lokal): " + targetTrophyPosition.y);
    }
}
