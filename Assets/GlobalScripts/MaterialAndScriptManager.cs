using UnityEngine;

public class MaterialAndScriptManager : MonoBehaviour
{
    public Material newMaterial; // Neues Material (wird direkt gesetzt)
    public MonoBehaviour targetScript; // Skript, das deaktiviert werden soll
    public GameObject quiz5; // Referenz zum GameObject "Quiz5"

    private Material originalMaterial;
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }

        // Direkt beim Start das Material wechseln und das Skript deaktivieren
        ChangeMaterialAndDisableScript();
    }

    void Update()
    {
        // Sobald "Quiz5" aktiv wird, wird das Material und das Skript wiederhergestellt
        if (quiz5 != null && quiz5.activeInHierarchy)
        {
            RestoreMaterialAndEnableScript();
        }
    }

    private void ChangeMaterialAndDisableScript()
    {
        if (objectRenderer != null && newMaterial != null)
        {
            objectRenderer.material = newMaterial;
        }

        if (targetScript != null)
        {
            targetScript.enabled = false;
        }

    }

    private void RestoreMaterialAndEnableScript()
    {
        if (objectRenderer != null && originalMaterial != null)
        {
            objectRenderer.material = originalMaterial;
        }

        if (targetScript != null)
        {
            targetScript.enabled = true;
        }

        // Da sich der Zustand nicht mehr ändern muss, deaktivieren wir dieses Skript
        enabled = false;
    }
}
