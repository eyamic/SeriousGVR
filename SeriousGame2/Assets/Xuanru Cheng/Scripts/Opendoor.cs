using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Opendoor : MonoBehaviour
{
      private Animator anim; // Reference to the Animator component to control the door's animations.
    private bool inRange = false; // Boolean to check if the player is within the interaction range of the door.
    private bool isOpen = false; // Boolean to track whether the door is currently open.
    private PlayerControls controls; // Reference to the custom PlayerControls class for input actions.
    public GameObject doorP; // GameObject reference, likely for showing a UI hint or prompt when near the door.
    private bool isAnimating = false;  // Boolean to manage whether an animation is currently playing to prevent overlapping animations.
    private float delayTime = 2f; // Delay time for auto-closing the door after the player exits the trigger zone.

    void Awake()
    {
        anim = GetComponent<Animator>(); // Assigning the Animator component from the GameObject this script is attached to.
        controls = new PlayerControls(); // Initializing the PlayerControls object.
    }

    void OnEnable()
    {
        controls.Gameplay.Enable(); // Enabling the input system's gameplay control scheme.
        controls.Gameplay.DoorControl.performed += HandleDoorControl; // Subscribing to the DoorControl action within the input system.
    }

    void OnDisable()
    {
        controls.Gameplay.DoorControl.performed -= HandleDoorControl; // Unsubscribing from the DoorControl action to clean up.
        controls.Gameplay.Disable(); // Disabling the input system's gameplay control scheme.
    }
    
    private void HandleDoorControl(InputAction.CallbackContext context)
    {
        // Handler for door control input; checks if the player is in range and if the door isn't open or animating.
        if (inRange && !isOpen && !isAnimating)
        {
            Debug.Log("Open door"); // Logging door opening for debugging.
            anim.SetTrigger("Open"); // Activating the 'Open' trigger in the Animator to start the open door animation.
            isOpen = true; // Marking the door as open.
            isAnimating = true; // Indicating that an animation is currently active.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Trigger detection when an object enters this GameObject's collider zone.
        if (other.CompareTag("Player"))
        {
            inRange = true; // Player has entered the interaction range.
            doorP.SetActive(true); // Activating the door prompt UI.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Trigger detection when an object exits this GameObject's collider zone.
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger area."); // Logging player's exit for debugging.
            inRange = false; // Player has left the interaction range.
            doorP.SetActive(false);
            if (isOpen && !inRange) {
                Invoke("CloseDoor", delayTime); // Scheduling the CloseDoor function to run after a delay, to close the door automatically.
            }
        }
    }

    void CloseDoor()
    {
        // Function to close the door.
        anim.SetTrigger("Close"); // Activating the 'Close' trigger in the Animator to start the close door animation.
    }
 
}