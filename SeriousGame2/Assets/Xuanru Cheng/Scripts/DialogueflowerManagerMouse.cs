using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueflowerManagerMouse : MonoBehaviour
{
    public AudioSource npcAudioSource;
    public AudioSource playerAudioSource;
    public AudioClip[] npcflowerDialogueClips;
    public AudioClip[] playerflowerDialogueClips;
    private int currentflowerClipIndex = 0;
    private bool isDialoguePlaying1 = false;
    private bool canTriggerDialogue = false;
    public GameObject rose;
    public GameObject Rosepanel;

    void Awake()
    {
        rose.SetActive(false);
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canTriggerDialogue)
        {
            if (!isDialoguePlaying1)
            {
                StartflowerDialogue();
            }
            else if (!npcAudioSource.isPlaying && !playerAudioSource.isPlaying)
            {
                ContinueflowerDialogue();
            }
        }
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

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Flower")
            {
                canTriggerDialogue = true;
                Rosepanel.SetActive(true);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Flower")
            {
                canTriggerDialogue = false;
                Rosepanel.SetActive(false);
            }
        }

        private void ContinueflowerDialogue()
        {
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
        {
            // NPC的索引应仅为0和1
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
