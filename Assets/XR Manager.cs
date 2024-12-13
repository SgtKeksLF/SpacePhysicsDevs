using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRManager : MonoBehaviour
{
    public static GameObject xrRig;  // Statische Referenz zum XR Rig

    void Awake()
    {
        // Falls das XR-Rig nicht schon gespeichert ist, speichere es
        if (xrRig == null)
        {
            xrRig = this.gameObject; // Setze das XR-Rig (kann auch über den Inspector zugewiesen werden)
        }
    }
}

