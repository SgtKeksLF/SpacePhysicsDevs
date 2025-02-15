using UnityEngine;

/*
This script disables the buttons inside of the planet rooms, making the player complete the quest before leaving again

This script required the DoorScript() and XR GRab Interactable to be the first two scripts attached to the button

*/

public class Global_MaterialAndScriptManagerScript : MonoBehaviour
{  
    public Material newMaterial; 

    public GameObject quiz5; 


    private Material originalMaterial; 
    private Renderer buttonObjectRenderer; 
    private bool hasQuizActivated = false; 

    private MonoBehaviour targetScript1;
    private MonoBehaviour targetScript2;


    void Start()
    {
        buttonObjectRenderer = GetComponent<Renderer>();

       
        if (buttonObjectRenderer != null)
        {
            originalMaterial = buttonObjectRenderer.material;
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
        if (buttonObjectRenderer != null && newMaterial != null)
        {
            buttonObjectRenderer.material = newMaterial;
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
        if (buttonObjectRenderer != null && originalMaterial != null)
        {
            buttonObjectRenderer.material = originalMaterial;
            // @Lisa Text �ber bUTTON AUF "t�R �FFNEN �NDERN"
            // text "t�r �ffnen" aktiv
            // text quiz beantworten inaktiv
        }

        if (targetScript1 != null)
        {
            targetScript1.enabled = true;
        }

        if (targetScript2 != null)
        {
            targetScript2.enabled = true;
        }

        enabled = false; 
    }
}
