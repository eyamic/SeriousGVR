using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class DialogueManager : MonoBehaviour
{
    public AudioSource npcAudioSource;  // NPC的音频源
    public AudioSource playerAudioSource;  // 玩家的音频源
    public AudioClip[] npcDialogueClips;  // NPC对话片段
    public AudioClip[] playerDialogueClips;  // 玩家对话片段
    private int currentClipIndex = 0;
    private bool isDialoguePlaying = false;
    private PlayerControls controls;  // 输入系统控制类
    public GameObject currentInteractable;  // 当前互动的对象
    public GameObject GameoverPanel;

    void Awake()
    {
        controls = new PlayerControls();
        GameoverPanel.SetActive(false);
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Dialogue.performed += TriggerDialogue;
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
        controls.Gameplay.Dialogue.performed -= TriggerDialogue;
    }

    public void StartDialogue()
    {
        if (!isDialoguePlaying)
        {
            currentClipIndex = 0;
            isDialoguePlaying = true;
            PlayNPCDialogue();
        }
    }

    private void TriggerDialogue(InputAction.CallbackContext context)
    {
        Debug.Log($"NPC IsPlaying: {npcAudioSource.isPlaying}, Player IsPlaying: {playerAudioSource.isPlaying}");
        if (isDialoguePlaying || npcAudioSource.isPlaying || playerAudioSource.isPlaying)
        {
            Debug.Log("Dialogue is currently playing or audio sources are active.");
            return;  // 如果当前正在播放对话，忽略新的输入
        }
        
        // 检查当前互动对象的标签
        if (currentInteractable != null && currentInteractable.tag == "NPC")
        {
            ContinueDialogue();
        }
        else
        {
            Debug.Log("Interactable is not a Young woman, dialogue will not trigger.");
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
            GameoverPanel.SetActive(true);
            Debug.Log("Dialogue ended.");
        }
    }
    private void PlayNPCDialogue()
    {
        int npcIndex = currentClipIndex / 2;
        Debug.Log(npcIndex);
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