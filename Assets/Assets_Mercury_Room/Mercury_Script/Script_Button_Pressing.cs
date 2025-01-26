using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Script_Button_Pressing : MonoBehaviour
{
public float deadTime = 1.0f;
private bool deadTimeActive = false; 

public UnityEvent onPressed, onReleased;

private void OnTriggerEnter(Collider other)
{
    if(other.tag == "PushButton" && !deadTimeActive)
    {
        onPressed?.Invoke();
        Debug.Log("I have been pressed!");
    }
}
private void OnTriggerExit(Collider other)
{
    if(other.tag == "PushButton" && !deadTimeActive)
    {
        onReleased?.Invoke();
        Debug.Log("I have been released!");
        StartCoroutine(WaitForDeadTime());
    }
}

IEnumerator WaitForDeadTime(){
    deadTimeActive = true;
    yield return new WaitForSeconds(deadTime);
    deadTimeActive = false;
}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
