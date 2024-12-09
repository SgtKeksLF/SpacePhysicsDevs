using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ScriptPersistentObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        // Markiere das Objekt als persistent
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        // Abonniere den Szenenwechsel-Event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Entferne den Event, um Speicherlecks zu vermeiden
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        XRBaseInteractor handInteractor = FindObjectOfType<XRDirectInteractor>();

        if (handInteractor != null)
        {
            // Hänge das Objekt an die Hand an
            transform.SetParent(handInteractor.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            Debug.Log($"Objekt {gameObject.name} wurde nach Szenenwechsel wieder an die Hand angehängt.");
        }
        else
        {
            Debug.LogWarning("Keine XRDirectInteractor (Hand) gefunden!");
        }
    }
}
