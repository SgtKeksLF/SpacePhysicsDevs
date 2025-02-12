using UnityEngine;

public class MaterialAndScriptManager : MonoBehaviour
{  
   
    public Material newMaterial;
    public GameObject quiz5;

    private Material originalMaterial;
    private Renderer objectRenderer;
    private bool hasQuizActivated = false;
    private MonoBehaviour targetScript1;
    private MonoBehaviour targetScript2;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        if (scripts.Length > 2)
        {
            targetScript1 = scripts[1];
            targetScript2 = scripts[2];
        }

        ChangeMaterialAndDisableScripts();
    }

    void Update()
    {
        if (!hasQuizActivated && quiz5 != null && quiz5.activeInHierarchy)
        {
            hasQuizActivated = true;
            RestoreMaterialAndEnableScripts();
        }
    }

    private void ChangeMaterialAndDisableScripts()
    {
        if (objectRenderer != null && newMaterial != null)
        {
            objectRenderer.material = newMaterial;
        }

        if (targetScript1 != null)
        {
            targetScript1.enabled = false;
        }

        if (targetScript2 != null)
        {
            targetScript2.enabled = false;
        }
    }

    private void RestoreMaterialAndEnableScripts()
    {
        if (objectRenderer != null && originalMaterial != null)
        {
            objectRenderer.material = originalMaterial;
        }

        if (targetScript1 != null)
        {
            targetScript1.enabled = true;
        }

        if (targetScript2 != null)
        {
            targetScript2.enabled = true;
        }

        enabled = false; // Deaktiviert dieses Skript, um Update() zu stoppen
    }
}
