using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TalkNPC : MonoBehaviour
{
    // Declares multiple AudioSource variables for different segments of the conversation.
    // Reference to the custom input action setup.
public AudioSource player_dialogue1;
    public AudioSource npc_dialogue1;
    public AudioSource player_dialogue2;
    public AudioSource npc_dialogue2;

    public GameObject hintPanel;
    public GameObject hintPanel2;
    public GameObject hintPanel3;

    private bool inRange;
    private bool dialogue1Over;
    public bool talkOver;

    private int numOfTalk;
    
    private PlayerControls controls; // Reference to the custom input action setup.


    void Awake()
    {
        controls = new PlayerControls(); // Instantiates the PlayerControls object to handle input.
    }

    void OnEnable()
    {
        controls.Enable(); // Enables the input system.
        controls.Gameplay.NPC.performed += HandleTalkNPCControl; // Subscribes to the NPC action in the input system.
    }

    void OnDisable()
    {
        controls.Gameplay.NPC.performed -= HandleTalkNPCControl; // Unsubscribes from the NPC action when the script is disabled.
        controls.Disable(); // Disables the input system.
    }
    private void HandleTalkNPCControl(InputAction.CallbackContext context)
    {
        // Handles the input for talking when the player is in range.
        if (inRange)
        {
            numOfTalk++; // Increment the talk counter each time the action is triggered.
            BeginTalkToNpc(); // Start the dialogue logic.
            Debug.Log("Talk increment"); // Logs the interaction to the console.
            hintPanel.SetActive(false); // Hide the initial hint panel.      
        }
    }

    void Start()
    {
        // Initializes all conversation state flags and the talk counter.
        inRange = false;
        dialogue1Over = false;
      //  dialogue2Over = false;
        talkOver = false;
        numOfTalk = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            hintPanel.SetActive(true);
        }
        if (other.CompareTag("Player") && talkOver)
        {
            hintPanel3.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        hintPanel.SetActive(false);
        hintPanel2.SetActive(false);
        hintPanel3.SetActive(false);
    }

    private void BeginTalkToNpc()
    {
        if (!inRange) return; // Ensure the player is in range before processing the dialogue.

        numOfTalk++; // Increments each time L2 is pressed within range.

        if (numOfTalk == 1)
        {
            player_dialogue1.Play();
            npc_dialogue1.PlayDelayed(4.0f);
            dialogue1Over = true;
            Invoke("ShowPanel", 6.0f);
            Invoke("HidePanel", 8.0f);
        }
        else if (numOfTalk == 2 && dialogue1Over)
        {
            player_dialogue2.Play();
            npc_dialogue2.PlayDelayed(5.5f);
            talkOver = true;
        }
        else if (talkOver)
        {
            Invoke("ShowPanel2", 9.5f); // This will show the final panel if all dialogues are complete.
        }
    }

    private void ShowPanel()
    {
        hintPanel2.SetActive(true);
    }

    private void HidePanel()
    {
        hintPanel2.SetActive(false);
    }

    private void ShowPanel2()
    {
        hintPanel3.SetActive(true);
    }

   
}