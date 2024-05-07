using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class NormalTraffic : MonoBehaviour
{
    public AudioSource TrifficSound; // Reference to an AudioSource that will play traffic sounds.
    public GameObject trifficPanel; // A UI GameObject that will be displayed when traffic interaction is relevant.
    private bool playerInRange_Traffic = false; // Boolean flag to track if the player is within the traffic interaction zone.
    private bool trifficP = false; // A flag to ensure traffic-related actions are only initiated once per entry.
    public AudioSource TeachTriffic; // An AudioSource to provide instructional traffic sounds.
    private PlayerControls controls; // Reference to the custom input action setup.

    private void Awake()
    {
        controls = new PlayerControls(); // Instantiates the PlayerControls object.
    }

    private void OnEnable()
    {
        controls.Enable(); // Enables the control scheme.
        controls.Gameplay.Traffic.performed += HandleTrafficControl; // Subscribes to the traffic control input action.
    }

    private void OnDisable()
    {
        controls.Gameplay.Traffic.performed -= HandleTrafficControl; // Unsubscribes from the traffic control input action.
        controls.Disable(); // Disables the control scheme.
    }

    private void HandleTrafficControl(InputAction.CallbackContext context)
    {
        if (playerInRange_Traffic) // Checks if the player is in the traffic area.
        {
            Debug.Log("play sound"); // Logs the action for debugging purposes.
            TrifficSound.PlayDelayed(5.0f); // Delays the traffic sound by 5 seconds.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && trifficP == false) // Checks if the collider is the player and the traffic action hasn't been triggered yet.
        {
            playerInRange_Traffic = true; // Sets the flag to true indicating the player is in range.
            trifficPanel.SetActive(true); // Activates the traffic UI panel.
            trifficP = true; // Ensures the traffic event is marked as triggered.
            TeachTriffic.PlayDelayed(1.0f); // Plays instructional traffic sound after a delay of 1 second.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && trifficP == true) // Checks if the exiting collider is the player and if the traffic action had been triggered.
        {
            playerInRange_Traffic = false; // Resets the in-range flag.
            trifficPanel.SetActive(false); // Deactivates the traffic UI panel.
            trifficP = false; // Resets the flag to allow future traffic interactions.
        }
    }
}