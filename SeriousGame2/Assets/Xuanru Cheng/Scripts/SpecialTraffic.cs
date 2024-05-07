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
        if (playerInTriffic)
        {
            triggerTime++;
            AskToCross();
            hintPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHint.PlayDelayed(1.0f);
            Invoke("OpenHintPanel", 7.0f);
            playerInTriffic = true;
        }
    }

    public void AskToCross()
    {
        if (triggerTime == 1 || triggerTime == 2)
        {
            Invoke("HideHintPanel", 1.0f);
            playerAsk.PlayDelayed(1.0f);
            Invoke("OpenHintPanel", 3.0f);
        }
        if (triggerTime == 3)
        {
            Invoke("HideHintPanel", 1.0f);
            playerAsk.PlayDelayed(1.0f);
            answerSound.PlayDelayed(5.0f);
            talkOver = true;
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
        if(talkOver)
        {
            invisibleWall.enabled = false;
        }
    }
}