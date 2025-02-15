using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

The tutorial version of the grabPhysics script

*/

public class Tutorial_GrabPhysicsScript : MonoBehaviour
{

    public GameObject ball;
    private Rigidbody rbBall;
    public GameObject canvasGravity;


    public void Start()
    {
        rbBall = ball.GetComponent<Rigidbody>();
    }

    public void OnGrab(GameObject grabbedObject)
    {
       
        if (grabbedObject == ball)
        {                
          
            GameObject display0 = canvasGravity.transform.Find("Canvas/Displays/Display0")?.gameObject;
            GameObject display1 = canvasGravity.transform.Find("Canvas/Displays/Display1")?.gameObject;

            if (display0 != null && display1 != null)
                {
                display0.SetActive(false); 
                display1.SetActive(true);
                }
        }
                  
    }

    public void BallRelease()
    {
        if (rbBall != null)
        {
            rbBall.constraints = RigidbodyConstraints.None;
        }
    }

}
