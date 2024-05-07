using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class DialogueManager : MonoBehaviour
{
    public AudioSource npcAudioSource;  // Public AudioSource reference for the NPC's dialogue.
    public AudioSource playerAudioSource;  // Public AudioSource reference for the player's dialogue.
    public AudioClip[] npcDialogueClips;  // Array of AudioClips that stores the NPC's dialogue audio.
    public AudioClip[] playerDialogueClips;  // Array of AudioClips that stores the player's dialogue audio.
    private int currentClipIndex = 0;  // Tracks the index of the current dialogue clip being played.
    private bool isDialoguePlaying = false;  // Flag to check if a dialogue is currently playing.
    private PlayerControls controls;  // Reference to a custom PlayerControls class that handles input.
    public GameObject currentInteractable;  // Reference to the currently interactable object.
    public GameObject GameoverPanel;  // Reference to a GameObject that acts as a panel displayed at the end of the game or dialogue.

    void Awake()
    {
        controls = new PlayerControls();
        GameoverPanel.SetActive(false);
       // rose.SetActive(false); 
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Dialogue.performed += TriggerDialogue;
        //controls.Gameplay.Flower.performed += TriggerFlowerDialogue;  // 监听 LeftTrigger
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
        controls.Gameplay.Dialogue.performed -= TriggerDialogue;
        //controls.Gameplay.Flower.performed -= TriggerFlowerDialogue;  // 移除监听
    }
   

    public void StartDialogue()
    {
        if (!isDialoguePlaying)  // Checks if dialogue is not already playing.
        {
            currentClipIndex = 0;  // Resets the clip index to 0.
            isDialoguePlaying = true;  // Sets the dialogue playing flag to true.
            PlayNPCDialogue();  // Starts the dialogue by playing the first NPC audio clip.
        }
    }

    private void TriggerDialogue(InputAction.CallbackContext context)
    {
        Debug.Log($"NPC IsPlaying: {npcAudioSource.isPlaying}, Player IsPlaying: {playerAudioSource.isPlaying}");
        if (isDialoguePlaying || npcAudioSource.isPlaying || playerAudioSource.isPlaying)
        {
            Debug.Log("Dialogue is currently playing or audio sources are active.");
            return;  // If dialogue is already playing, ignore new input.
        }
        
        if (currentInteractable != null && currentInteractable.tag == "NPC")
        {
            ContinueDialogue();  // If the current interactable object is an NPC, continue with the dialogue.
        }
        else
        {
            Debug.Log("Interactable is not a Young woman, dialogue will not trigger.");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            currentInteractable = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentInteractable)
        {
            currentInteractable = null;
        }
    }

    void Update()
    {
        if (isDialoguePlaying)
        {
            if (!npcAudioSource.isPlaying && !playerAudioSource.isPlaying)
            {
                ContinueDialogue();
            }
        }
    }
    private void ContinueDialogue()
    {
        Debug.Log("ContinueDialogue called, currentClipIndex: " + currentClipIndex);
        if (currentClipIndex < npcDialogueClips.Length + playerDialogueClips.Length)  // Checks if the current index is within the bounds of the dialogue arrays.
        {
            if (currentClipIndex % 2 == 0)  // Determines which dialogue to play based on the index.
            {
                PlayNPCDialogue();  // Plays the NPC dialogue at the current index.
            }
            else
            {
                PlayPlayerDialogue();  // Plays the player dialogue at the current index.
            }
            currentClipIndex++;  // Increments the clip index.
        }
        else
        {
            isDialoguePlaying = false;  // Ends the dialogue sequence.
            GameoverPanel.SetActive(true);  // Activates the game over panel.
            Debug.Log("Dialogue ended.");
        }
    }
    private void PlayNPCDialogue()
    {
        int npcIndex = currentClipIndex / 2;  // Calculates the correct NPC clip index.
        if (npcIndex < npcDialogueClips.Length)
        {
            npcAudioSource.clip = npcDialogueClips[npcIndex];  // Sets the NPC audio source to the correct clip.
            npcAudioSource.Play();  // Plays the NPC audio clip.
        }
        else
        {
            Debug.LogError("NPC Dialogue clip index out of range.");
        }
    }

    private void PlayPlayerDialogue()
    {
        int playerIndex = (currentClipIndex - 1) / 2;  // Calculates the correct player clip index.
        if (playerIndex < playerDialogueClips.Length)
        {
            playerAudioSource.clip = playerDialogueClips[playerIndex];  // Sets the player audio source to the correct clip.
            playerAudioSource.Play();  // Plays the player audio clip.
        }
        else
        {
            Debug.LogError("Player Dialogue clip index out of range.");
        }
    }
   

    public bool IsSpeaking()
    {
        return isDialoguePlaying;
    }

    public void ResetDialogue()
    {
        currentClipIndex = 0;
        isDialoguePlaying = false;
    }
}