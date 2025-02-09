using UnityEngine;

public class MaterialAndScriptManager : MonoBehaviour
{
    public Material newMaterial; // Das Material, auf das gewechselt wird
    public MonoBehaviour targetScript; // Das Script, das deaktiviert/aktiviert werden soll

    private Material originalMaterial;
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
    }

    public void ChangeMaterialAndDisableScript()
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

    public void RestoreMaterialAndEnableScript()
    {
        if (objectRenderer != null && originalMaterial != null)
        {
            objectRenderer.material = originalMaterial;
        }

        if (targetScript != null)
        {
            targetScript.enabled = true;
        }
    }
}
