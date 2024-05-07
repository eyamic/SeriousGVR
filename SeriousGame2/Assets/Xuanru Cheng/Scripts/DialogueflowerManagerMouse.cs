using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueflowerManagerMouse : MonoBehaviour
{
    public AudioSource npcAudioSource; // AudioSource for playing NPC's dialogue.
    public AudioSource playerAudioSource; // AudioSource for playing player's dialogue.
    public AudioClip[] npcflowerDialogueClips; // Array of NPC dialogue audio clips related to the flower.
    public AudioClip[] playerflowerDialogueClips; // Array of player dialogue audio clips related to the flower.
    private int currentflowerClipIndex = 0; // Index to track the current dialogue clip being played.
    private bool isDialoguePlaying1 = false; // Flag to check if the dialogue is currently playing.
    private bool canTriggerDialogue = false; // Flag to check if dialogue can be triggered (player in range).
    public GameObject rose; // GameObject that represents the flower in the game.
    public GameObject Rosepanel; // UI panel that is shown when the player can interact with the flower.

    void Awake()
    {
        rose.SetActive(false);// Initially sets the rose GameObject to be inactive.
    }

     void Update()
    { // Check for player input and whether dialogue can be triggered.
        if (Input.GetKeyDown(KeyCode.E) && canTriggerDialogue)
        {
            if (!isDialoguePlaying1)
            {
                StartflowerDialogue();// Start the dialogue if it is not currently playing.
            }
            else if (!npcAudioSource.isPlaying && !playerAudioSource.isPlaying)
            {
                ContinueflowerDialogue();// Continue to the next part of the dialogue if nothing is currently playing
            }
        }
    }
    public void StartflowerDialogue()
        {// Start playing the dialogue related to the flower.
            if (!isDialoguePlaying1)
            {
                currentflowerClipIndex = 0;
                isDialoguePlaying1 = true;
                rose.SetActive(false);
                PlayflowerNPCDialogue();// Start with the NPC's dialogue.
            }
        }

        void OnTriggerEnter(Collider other)
        { // Trigger dialogue possibility when the player enters a specific area.
            if (other.gameObject.tag == "Flower")
            {
                canTriggerDialogue = true;
                Rosepanel.SetActive(true);
            }
        }

        void OnTriggerExit(Collider other)
        { // Disable dialogue interaction when the player leaves the area.
            if (other.gameObject.tag == "Flower")
            {
                canTriggerDialogue = false;
                Rosepanel.SetActive(false);
            }
        }

        private void ContinueflowerDialogue()
        { // Continue to the next part of the dialogue sequence.
            Debug.Log("ContinueflowerDialogue called, currentClipIndex: " + currentflowerClipIndex);
            int totalDialogues = npcflowerDialogueClips.Length + playerflowerDialogueClips.Length;

            if (currentflowerClipIndex < totalDialogues)
            {
                if (currentflowerClipIndex == 0 || currentflowerClipIndex == 1) // NPC 对话
                {
                    PlayflowerNPCDialogue();
                }
                else if (currentflowerClipIndex == 2) // 玩家对话
                {
                    PlayflowerPlayerDialogue();
                }

                currentflowerClipIndex++;
            }
            else
            {
                isDialoguePlaying1 = false; // 对话结束
                rose.SetActive(true); // 激活 rose
                Debug.Log("Dialogue ended. Rose should now be active.");
            }
        }

        private void PlayflowerNPCDialogue()
        { // Play one of the NPC's dialogues.
            // NPC的索引应仅为0和1NPCs should be indexed to 0 and 1 only
            int npcIndex = (currentflowerClipIndex < 2) ? currentflowerClipIndex : 1;
            if (npcIndex < npcflowerDialogueClips.Length)
            {
                npcAudioSource.clip = npcflowerDialogueClips[npcIndex];
                npcAudioSource.Play();
            }
            else
            {
                Debug.LogError("NPC Dialogue clip index out of range.");
            }
        }

        private void PlayflowerPlayerDialogue()
        {
            if (playerflowerDialogueClips.Length > 0)
            {
                playerAudioSource.clip = playerflowerDialogueClips[0];
                playerAudioSource.Play();
            }
            else
            {
                Debug.LogError("Player Dialogue clip index out of range.");
            }
        }

        public bool IsSpeaking1()
        {
            return isDialoguePlaying1;// Return whether dialogue is currently playing.
        }

        public void ResetflowerDialogue()
        {
            currentflowerClipIndex = 0;
            isDialoguePlaying1 = false;
        }
    }
