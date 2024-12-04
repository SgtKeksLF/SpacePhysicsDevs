using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class ScriptButton : MonoBehaviour
{
    public GameObject planetSampleObject;
    public GameObject roomWall;
    public Material newPlanetMaterial;
    public Material defaultPlanetMaterial;
    private bool isNewPlanet = false;

    //The Following are all variables for Item Physics
    public GameObject bouncyBall;
    public GameObject dice;
    public GameObject canOfBeans;
    public GameObject balloon;
    public GameObject canOfWater;

    //The following are for physics themselves
    private float earthGravity = -9.8f;
    private float mercuryGravity = -3.9f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPressed()
    {
        RoomChange();
        PhysicsChange();

    }

    public void RoomChange()
    {
        if (planetSampleObject != null && newPlanetMaterial != null && defaultPlanetMaterial != null)
        {
            Renderer planetRenderer = planetSampleObject.GetComponent<Renderer>();
            Renderer wallRenderer = roomWall.GetComponent<Renderer>();

            if (planetRenderer != null && wallRenderer != null)
            {
                // Überprüfen, ob das neue Material bereits angewendet wurde
                if (isNewPlanet)
                {
                    // Material auf das Standardmaterial zurücksetzen
                    planetRenderer.material = defaultPlanetMaterial;
                    wallRenderer.material = defaultPlanetMaterial;
                   
                }
                else
                {
                    // Material auf das neue Material setzen
                    planetRenderer.material = newPlanetMaterial;
                    wallRenderer.material = newPlanetMaterial;
                   
                }

                // Den Zustand umkehren, um beim nächsten Klick das Material zu wechseln
                isNewPlanet = !isNewPlanet;
            }



        }
    }
    public void PhysicsChange()
    {
        if (isNewPlanet == true)
        {
            Debug.Log("Mercury physics");
            Physics.gravity = new Vector3(0, mercuryGravity, 0);
        }
        else
        {
            Debug.Log("Earth physics");
            Physics.gravity = new Vector3(0, earthGravity, 0);
        }
    }
    }
