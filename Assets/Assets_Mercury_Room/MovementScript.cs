using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
     public InputActionAsset inputActions;  // Referenz zu deinem Input Actions Asset
    private InputAction moveAction;  // Die Action für die Bewegung
    private Vector2 moveInput;
    public float speed = 1.0f;  // Geschwindigkeit der Bewegung

    private void OnEnable()
    {
        // Hole die ActionMap "Player" und die "Move"-Action
        var playerActionMap = inputActions.FindActionMap("Player");
        moveAction = playerActionMap.FindAction("Move");

        // Aktivieren der Action
        moveAction.Enable();
    }

    private void OnDisable()
    {
        // Deaktivieren der Action
        moveAction.Disable();
    }

    private void Update()
    {
        // Abrufen der Joystick-Werte als Vector2
        moveInput = moveAction.ReadValue<Vector2>();

        // Berechnung der Bewegung basierend auf Joystick-Werten
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);  // Orientierung der Kamera verwenden
        moveDirection.y = 0;  // Y-Komponente ignorieren, um nicht in die Luft zu bewegen

        // Spielerbewegung anwenden
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
