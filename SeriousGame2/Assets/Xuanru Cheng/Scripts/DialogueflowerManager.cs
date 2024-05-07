using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class DialogueflowerManager : MonoBehaviour
{
    public AudioSource npcAudioSource;  // NPC的音频源Audio sources for NPCs
  
    public AudioSource playerAudioSource;  // 玩家的音频源Audio sources for player
  
    public AudioClip[] npcflowerDialogueClips;  // NPC对话片段NPC dialogue clips
 
    public AudioClip[] playerflowerDialogueClips;  // 玩家对话片段Player dialogue clips
 
    private int currentflowerClipIndex = 0;
    private bool isDialoguePlaying1 = false;
    private PlayerControls controls;  // 输入系统控制类
    public GameObject currentflowerInteractable;  // 当The current of the interacting object
                                            // public GameObject GameoverPanel;
    public GameObject rose;  // 特定的物体 "rose"Special object
    public GameObject Rosepanel;
    void Awake()
    {
        controls = new PlayerControls();
        rose.SetActive(false); 
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
       // controls.Gameplay.Dialogue.performed += TriggerDialogue;
        controls.Gameplay.Flower.performed += TriggerflowerDialogue;  // 监听 LeftTriggerListen to LeftTrigger
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
        //controls.Gameplay.Dialogue.performed -= TriggerDialogue;
        controls.Gameplay.Flower.performed -= TriggerflowerDialogue;  // 移除监听Remove listener
    }
   

    public void StartflowerDialogue()
    {
        if (!isDialoguePlaying1)
        {
            currentflowerClipIndex = 0;
            isDialoguePlaying1 = true;
           rose.SetActive(false); 
            PlayflowerNPCDialogue();
        }
    }

    private void TriggerflowerDialogue(InputAction.CallbackContext context)
    {
        Debug.Log($"NPC IsPlaying: {npcAudioSource.isPlaying}, Player IsPlaying: {playerAudioSource.isPlaying}");
        if (isDialoguePlaying1 || npcAudioSource.isPlaying || playerAudioSource.isPlaying)
        {
            Debug.Log("Dialogue is currently playing or audio sources are active.");
            return;  // 如果当前正在播放对话，忽略新的输入Ignore new input if a dialogue is currently playing
        }
        
        // 检查当前互动对象的标签Check the label of the current interactive object
        if (currentflowerInteractable != null && currentflowerInteractable.tag == "Flower")
        {
            ContinueflowerDialogue();
        }
        else
        {
            Debug.Log("Interactable is not a Young woman, dialogue will not trigger.");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Flower")
        {
            currentflowerInteractable = other.gameObject;
            Rosepanel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentflowerInteractable)
        {
            currentflowerInteractable = null;
            Rosepanel.SetActive(false);
           // rose.SetActive(true);  
        }
    }

    void Update()
    {
        if (isDialoguePlaying1)
        {
            if (!npcAudioSource.isPlaying && !playerAudioSource.isPlaying)
            {
                ContinueflowerDialogue();
            }
        }
    }
    private void ContinueflowerDialogue()
    {
        Debug.Log("ContinueflowerDialogue called, currentClipIndex: " + currentflowerClipIndex);
        int totalDialogues = npcflowerDialogueClips.Length + playerflowerDialogueClips.Length;

        if (currentflowerClipIndex < totalDialogues)
        {
            if (currentflowerClipIndex == 0 || currentflowerClipIndex == 1)  // NPCs Dialogue
            {
                PlayflowerNPCDialogue();
            }
            else if (currentflowerClipIndex == 2)  // player Dialogue
            {
                PlayflowerPlayerDialogue();
            }
            currentflowerClipIndex++;
        }
        else
        {
            isDialoguePlaying1 = false;  // End of dialogue
            rose.SetActive(true);  // Activate rose
            Debug.Log("Dialogue ended. Rose should now be active.");
        }
    }
    private void PlayflowerNPCDialogue()
    {
        // NPCs should be indexed to 0 and 1 only
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
        return isDialoguePlaying1;
    }

    public void ResetflowerDialogue()
    {
        currentflowerClipIndex = 0;
        isDialoguePlaying1 = false;
    }
}