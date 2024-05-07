using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Pickupphone : MonoBehaviour
{
   public GameObject panel1; // Public reference to the first UI panel.
    public GameObject panel2; // Public reference to the second UI panel.
    public GameObject panel3; // Public reference to the third UI panel.

    public AudioSource phone; // AudioSource for playing phone ring or pickup sounds.
    public AudioSource npc1; // AudioSource for the first NPC dialogue.
    public AudioSource player1; // AudioSource for the player's response.
    public AudioSource npc2; // AudioSource for a second NPC dialogue.

    private bool inRange = false; // Boolean flag to check if the player is within interaction range.
    private int numOfTalk = 0; // Counter for the number of interactions (phone pickups or dialogues).
    private bool talkOver = false; // Boolean to determine if the full dialogue sequence is over.
    private bool dialogue1Over = false; // Boolean to check if the first dialogue exchange is completed.
    private PlayerControls controls; // Reference to custom PlayerControls class for input.

    void Awake()
    {
        controls = new PlayerControls(); // Instantiate the Input Actions.
    }

    void OnEnable()
    {
        controls.Enable(); // Enable the input system's control scheme.
        controls.Gameplay.PickupPhone.performed += HandlePickupControl; // Subscribe to the PickupPhone action's performed event.
    }

    void OnDisable()
    {
        controls.Gameplay.PickupPhone.performed -= HandlePickupControl; // Unsubscribe from the PickupPhone action to avoid memory leaks or unintended behavior when the script is disabled.
        controls.Disable(); // Disable the input system's control scheme.
    }

    private void HandlePickupControl(InputAction.CallbackContext context)
    {
        // Handle the input for picking up the phone when the player is in range.
        if (inRange)
        {
            numOfTalk++; // Increment the interaction counter.
            panel1.SetActive(false); // Hide the first panel when the phone is picked up.
            BeginPhone(); // Begin the sequence of phone interaction.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Triggered when an object (presumably the player) enters the trigger collider attached to this GameObject.
        phone.Play(); // Play the phone ringing sound immediately.
        inRange = true; // Set the player as being in range for interaction.
        Invoke("ShowPanel2", 2.0f); // Schedule the first panel to show after a 2 second delay.
    }

    private void OnTriggerExit(Collider other)
    {
        // Triggered when an object exits the trigger collider.
        inRange = false; // Set the player as out of range, ending interaction possibility.
        panel3.SetActive(false); // Deactivate the third panel, usually shown last in a sequence.
    }

    private void BeginPhone()
    {
        // Handles the logic for the phone conversation sequence based on interaction count and conditions.
        if (numOfTalk == 1 && inRange)
        {
            phone.enabled = false; // Disable the phone sound to prevent it from playing again.
            npc1.Play(); // Play the first NPC dialogue.
            player1.PlayDelayed(4.0f); // Play the player's response after a 4 second delay.
            dialogue1Over = true; // Mark the first part of the dialogue as complete.
            Invoke("ShowPanel", 5.0f); // Schedule showing the second panel after 5 seconds.
            Invoke("HidePanel", 6.0f); // Schedule hiding the second panel after 6 seconds.
        }
        if (numOfTalk == 2 && inRange && dialogue1Over)
        {
            npc2.Play(); // Play the second NPC dialogue.
            talkOver = true; // Indicate that the entire dialogue sequence is complete.
        }
        if (talkOver && inRange)
        {
            panel3.SetActive(true); // Activate the third panel, typically indicating completion or next steps.
        }
    }

    private void ShowPanel()
    {
        panel2.SetActive(true); // Show the second UI panel.
    }

    private void HidePanel()
    {
        panel2.SetActive(false); // Hide the second UI panel.
    }

    private void ShowPanel2()
    {
        panel1.SetActive(true); // Show the first UI panel.
    }
}