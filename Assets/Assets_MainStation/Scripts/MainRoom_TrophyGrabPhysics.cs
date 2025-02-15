using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/*
This scripts enables the Trophy at the end to be grabbable

the kinematic function is to prevent unwanted movement while transportation into the room
*/
public class MainRoom_TrophyGrabPhysics : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }
    }


    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}
