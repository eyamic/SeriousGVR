using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TalkBus : MonoBehaviour
{
    // Declare public AudioSource variables for different sound clips related to the bus interaction.
    public AudioSource busArrive;
    public AudioSource busPass;
    public AudioSource driverAnswer;
    public AudioSource driverAnswer_ok;
    public AudioSource playerAsk;
    public AudioSource playerThankU;

    // Public GameObject references for UI panels to provide visual feedback during interaction.
    public GameObject hintPanel;
    public GameObject hintPanel2;

    // Public boolean variables to track the interaction state and flags.
    public bool inRange;
    public bool talkOver;
    public int numOfTalk = 0; // Counter for the number of times the player has interacted with the bus.

    private PlayerControls controls; // Private variable to hold a reference to the PlayerControls object for input handling.


    void Awake()
    {
        controls = new PlayerControls(); // // Instantiate the PlayerControls object.
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Gameplay.Bus.performed += HandleTalkControl;
    }

    void OnDisable()
    {
        controls.Gameplay.Bus.performed -= HandleTalkControl;
        controls.Disable();
    }

    private void HandleTalkControl(InputAction.CallbackContext context)
    {// Handle the input for bus interaction when the player is in range.
        if (inRange)
        {
            numOfTalk++; // Increment the interaction counter each time the bus interaction input is performed.
            TalkToBus(); // Call the function to handle the bus interaction sequence.
            hintPanel.SetActive(false); // Hide the first hint panel.
        }
    }

    public void Start()
    {
        inRange = false; // Initialize the inRange flag to false.
        talkOver = false; // Initialize the talkOver flag to false.
    }

    public void OnTriggerEnter(Collider other)
    {// Detect when the player enters the collider zone assigned to the bus.
        if (other.CompareTag("Player") && !inRange)
        {
            inRange = true;
            hintPanel.SetActive(true);
        }
    }

    public void TalkToBus()
    {// Handle the sequence of bus interaction dialogues based on the interaction count.
        if (numOfTalk == 1)
        {
            playerAsk.PlayDelayed(1.0f); // Delay the player's question by 1 second.
            driverAnswer.PlayDelayed(4.5f); // Delay the driver's first response by 4.5 seconds.
            busPass.PlayDelayed(4.0f); // Play the bus passing sound at 4 seconds.
            Invoke("ShowHintPanel2", 6f); // Show the second hint panel after 6 seconds.
        }
        else if (numOfTalk == 2)
        {
            Invoke("HideHintPanel2", 1.0f);
            playerAsk.PlayDelayed(2.0f);
            Invoke("ShowHintPanel2", 5f);
        }
        else if (numOfTalk == 3)
        {
            Invoke("HideHintPanel2", 1f);// Hide the second hint panel after 1 second.
            playerAsk.PlayDelayed(2.0f);
            driverAnswer_ok.PlayDelayed(4.5f);
            playerThankU.PlayDelayed(7.0f);
            talkOver = true;
        }
    }

    public void ShowHintPanel2()
    {
        hintPanel2.SetActive(true);// Activate the second hint panel.
    }

    public void HideHintPanel2()
    {
        hintPanel2.SetActive(false);// Deactivate the second hint panel.
    }
}
