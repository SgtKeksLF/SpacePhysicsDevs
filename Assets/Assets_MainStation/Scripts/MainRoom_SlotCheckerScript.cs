using UnityEngine;

/*

This script checks if the planetsample that the player tries to place on a
sample shelf is the right one. Example: He places the Mars sample in the Venus slot so 
this script remainsthe red material for "wrong slot".

The planet samples, their sample shelves and their neon lights are each tagged with their 
slot (slot1 for Mercury, slot2 for Venus, slot3 for Mars, slot4 for Saturn)

*/

public class MainRoom_SlotCheckerScript : MonoBehaviour
{
    public string correctTag;
    public Material winMaterial; 
    public Material loseMaterial; 

    public Renderer mainLampRenderer; 
    public Transform neonLightsParent; 
    private Rigidbody rbObject;
    private Collider lastCollider; 

    private bool isCorrect = false;

    public Vector3 fixedRotation = new Vector3(0, 0, 0); 

    // at game start all slots are marked green as there are no samples collected yet
    private void Awake()
    {
        SetMaterials(loseMaterial);
    }

    // checking if same tag and so if correct placed sample
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(correctTag))
        {
            rbObject = other.GetComponent<Rigidbody>();
            lastCollider = other; 
            
            FreezeObject();
            
            SetMaterials(winMaterial);
            isCorrect = true;
        }
        else
        {
            SetMaterials(loseMaterial);
            isCorrect = false;
        }

        MainRoom_PuzzleManagerScript.Instance.CheckWinCondition();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(correctTag))
        {
            SetMaterials(loseMaterial);
            isCorrect = false;
        }

        MainRoom_PuzzleManagerScript.Instance.CheckWinCondition();
    }

    private void SetMaterials(Material newMaterial)
    {
        if (mainLampRenderer != null)
        {
            mainLampRenderer.material = newMaterial;
        }

        if (neonLightsParent != null)
        {
            Renderer[] childRenderers = neonLightsParent.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in childRenderers)
            {
                rend.material = newMaterial;
            }
        }
    }

    public bool IsCorrect()
    {
        return isCorrect;
    }

    void FreezeObject()
    {
        if (rbObject != null && lastCollider != null)
        {
            rbObject.transform.position = transform.position;

            rbObject.transform.rotation = Quaternion.Euler(fixedRotation);

            rbObject.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}
