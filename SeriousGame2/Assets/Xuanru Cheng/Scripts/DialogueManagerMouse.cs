using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManagerMouse : MonoBehaviour
{
    public AudioSource npcAudioSource;  // NPC的音频源Audio sources for NPCs
    public AudioSource playerAudioSource;  // 玩家的音频源Audio sources for player
    public AudioClip[] npcDialogueClips;  // NPC对话片段NPC dialogue clips
    public AudioClip[] playerDialogueClips;  // 玩家对话片段Player dialogue clips
    public GameObject gameovepanel;
    private int currentClipIndex = 0;
    private bool isDialoguePlaying = false;
    private bool canTriggerDialogue = false;  // 是否可以触发对话Whether a dialogue can be triggered

    void Start()
    {
        gameovepanel.SetActive(false);
        currentClipIndex = 0;
        isDialoguePlaying = false;
    }

    void Update()
    {
        // 处理键盘输入
        if (Input.GetKeyDown(KeyCode.E) && canTriggerDialogue)
        {
            // Checks if the 'E' key is pressed and whether the dialogue can be triggered.
            if (!isDialoguePlaying)
            {
                // If no dialogue is currently playing:
                StartDialogue(); // Calls the StartDialogue function to begin a new dialogue.
            }
            else if (!npcAudioSource.isPlaying && !playerAudioSource.isPlaying)
            {
                // If a dialogue is currently active but no audio (neither NPC nor player) is playing:
                ContinueDialogue(); // Calls the ContinueDialogue function to proceed to the next part of the dialogue.
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            canTriggerDialogue = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            canTriggerDialogue = false;
        }
    }

    public void StartDialogue()
    {
        currentClipIndex = 0;
        isDialoguePlaying = true;
        PlayNPCDialogue();
    }

    private void ContinueDialogue()
    {
        Debug.Log("ContinueDialogue called, currentClipIndex: " + currentClipIndex);
        if (currentClipIndex < npcDialogueClips.Length + playerDialogueClips.Length)
        {
            if (currentClipIndex % 2 == 0)
            {
                PlayNPCDialogue();
            }
            else
            {
                PlayPlayerDialogue();
            }
            currentClipIndex++;
        }
        else
        {
            isDialoguePlaying = false; // 对话结束End of dialogue
            gameovepanel.SetActive(true);
            Debug.Log("Dialogue ended.");
        }
    }

    private void PlayNPCDialogue()
    {
        int npcIndex = currentClipIndex / 2;
        if (npcIndex < npcDialogueClips.Length)
        {
            npcAudioSource.clip = npcDialogueClips[npcIndex];
            npcAudioSource.Play();
        }
        else
        {
            Debug.LogError("NPC Dialogue clip index out of range.");
        }
    }

    private void PlayPlayerDialogue()
    {
        int playerIndex = (currentClipIndex - 1) / 2;
        if (playerIndex < playerDialogueClips.Length)
        {
            playerAudioSource.clip = playerDialogueClips[playerIndex];
            playerAudioSource.Play();
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