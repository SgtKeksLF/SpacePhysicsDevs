using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetStateManager : MonoBehaviour
{
   // Diese Variable verwaltet den Zustand: false = Erde, true = Merkur
    public static bool isNewPlanet = false;

    // Eine Methode, um den Zustand zurückzusetzen, falls nötig
    public static void ResetPlanetState()
    {
        isNewPlanet = false;  // Erde als Standard
    }

    // Optional: Methode zum umschalten des Planeten
    public static void TogglePlanetState()
    {
        isNewPlanet = !isNewPlanet;  // Wechselt zwischen Erde und Merkur
    }
   
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
