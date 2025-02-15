using System.Collections;
using UnityEngine;

/*
This script connects the quizz screen with the post system and is responsible for transport of the sample to the main room. 

It uses the same logic function of ObjectInShelf to have the item hover inside the model of the post system until the quiz is succesfully cleared.
It then sends the sample to the main room. 
*/

public class Global_PostScript : MonoBehaviour
{
    public GameObject currentSample; // Variable for the room sample
    public GameObject targetObject; // Reference to object that is supposed to be transported
    public GameObject quiz5; 
    public GameObject quiz0; 
    public GameObject quiz1; 


    public float moveSpeed = 0.5f;
    private bool hasTriggered = false; 
    public bool sampleInPost = false;
    private AudioSource[] audioSources;
    private AudioSource triggerSound;
    private AudioSource arrivalSound;

    public Vector3 fixedRotation = new Vector3(0, 0, 0);

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 2)
        {
            triggerSound = audioSources[0];
            arrivalSound = audioSources[1];
        }
    }

    private void Update()
    {
        if (quiz5 != null && quiz5.activeInHierarchy && !hasTriggered)
        {
            hasTriggered = true;
            OnSolutionCorrect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentSample != null && other.bounds.Contains(currentSample.transform.position))
        {
            sampleInPost = true;

            quiz0.SetActive(false);
            quiz1.SetActive(true);

            if (triggerSound != null)
            {
                triggerSound.Play();
            }

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                FreezeObject(rb);
            }
        }
    }

    private void OnSolutionCorrect()
    {
        if (currentSample != null && targetObject != null)
        {
            StartCoroutine(MoveSampleCoroutine(targetObject.transform.position));
        }
    }

    private IEnumerator MoveSampleCoroutine(Vector3 endPos)
    {
        Vector3 start = currentSample.transform.position;
        Vector3 upPosition = start + Vector3.up * 3f;
        float elapsedTime = 0f;

        if (arrivalSound != null)
        {
            arrivalSound.Play();
        }

        while (elapsedTime < 1f)
        {
            currentSample.transform.position = Vector3.Lerp(start, upPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        currentSample.transform.position = upPosition;

        elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            currentSample.transform.position = Vector3.Lerp(upPosition, endPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        currentSample.transform.position = endPos;
        Rigidbody rb = currentSample.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    private void FreezeObject(Rigidbody rb)
    {
        if (rb != null)
        {
           
            rb.transform.position = transform.position;

           
            rb.transform.rotation = Quaternion.Euler(fixedRotation);

            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}
