using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManagerMouse : MonoBehaviour
{
    public AudioSource npcAudioSource;  // NPC的音频源
    public AudioSource playerAudioSource;  // 玩家的音频源
    public AudioClip[] npcDialogueClips;  // NPC对话片段
    public AudioClip[] playerDialogueClips;  // 玩家对话片段
    public GameObject gameovepanel;
    private int currentClipIndex = 0;
    private bool isDialoguePlaying = false;

    void Start()
    {
        gameovepanel.SetActive(false);
        currentClipIndex = 0;
        isDialoguePlaying = false;
    }

    void Update()
    {
        // 处理键盘输入
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialoguePlaying)
            {
                StartDialogue();
            }
            else if (!npcAudioSource.isPlaying && !playerAudioSource.isPlaying)
            {
                ContinueDialogue();
            }
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
            currentClipIndex++; // 确保此行逻辑正确执行
        }
        else
        {
            isDialoguePlaying = false; // 对话结束
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