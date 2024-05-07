using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class SpecialTraffic : MonoBehaviour
{
    public GameObject hintPanel; // A reference to the hint panel.
    public AudioSource answerSound; // The sound that is played at the end of a dialogue.
    public AudioSource playerHint; // Sound that prompts the player to interact.
    public AudioSource playerAsk; // sound for player ask; // sound for player interaction.

    private int triggerTime = 0; // the number of times the interaction was triggered
    private bool playerInTriffic = false; // Indicates if the player is in a traffic zone or not
    private bool talkOver = false; // Flag if the dialogue is over or not
    public Collider invisibleWall; // Invisible wall, used to block the player until the end of the dialogue

    private PlayerControls controls; // Controllers

   
    void Awake() {
        controls = new PlayerControls(); // Initialise the controls.
    }


    void OnEnable()
    {
        controls.Enable();
        controls.Gameplay.Traffic.performed += HandleTrafficControl;
    }

    void OnDisable()
    {
        controls.Gameplay.Traffic.performed -= HandleTrafficControl;
        controls.Disable();
    }

    private void HandleTrafficControl(InputAction.CallbackContext context)
    {
        if (playerInTriffic) // If the player is in a traffic area
        {
            triggerTime++; // Increase the number of triggers
            AskToCross(); // Handle the request to cross the street
            hintPanel.SetActive(false); // hide the hint panel
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // if the collision is with a player
        {
            playerHint.PlayDelayed(1.0f); // delay playing the player hint for 1 second
            Invoke("OpenHintPanel", 7.0f); // show hint panel after 7 seconds
            playerInTriffic = true; // mark the player as entering the traffic area
        }
    }

    public void AskToCross()
    {
        if (triggerTime == 1 || triggerTime == 2) // first or second trigger
        {
            Invoke("HideHintPanel", 1.0f); // hide hint panel after 1 second
            playerAsk.PlayDelayed(1.0f); // play the player ask sound after 1 second
            Invoke("OpenHintPanel", 3.0f); // show hint panel again after 3 seconds
        }
        if (triggerTime == 3) // trigger for the third time
        {
            Invoke("HideHintPanel", 1.0f); // hide hint panel after 1 second
            playerAsk.PlayDelayed(1.0f); // play the player ask sound after 1 second
            answerSound.PlayDelayed(5.0f); // play answer sound after 5 seconds
            talkOver = true; // Mark the end of the conversation.
        }
    }

    private void OpenHintPanel()
    {
        hintPanel.SetActive(true);
    }

    private void HideHintPanel()
    {
        hintPanel.SetActive(false);
    }

    void Update()
    {
        if(talkOver) // if the conversation is over
        {
            invisibleWall.enabled = false; // disable the invisible wall and allow the player to pass through it
        }
    }
}