using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public AudioClip[] dialogueClips;
    public AudioSource dialogueSource;

    private int currentClipIndex = 0;
    private bool isDialoguePlaying = false;

    void Start()
    {
        // 初始化
        dialogueSource.clip = dialogueClips[currentClipIndex];
    }

    void Update()
    {
        // 检查对话是否播放完毕
        if (isDialoguePlaying && !dialogueSource.isPlaying)
        {
            PlayNextClip();
        }
    }

    public void StartDialogue()
    {
        if (!isDialoguePlaying)
        {
            dialogueSource.clip = dialogueClips[currentClipIndex];
            dialogueSource.Play();
            isDialoguePlaying = true;
        }
    }

    private void PlayNextClip()
    {
        currentClipIndex++;

        if (currentClipIndex < dialogueClips.Length)
        {
            dialogueSource.clip = dialogueClips[currentClipIndex];
            dialogueSource.Play();
        }
        else
        {
            // 对话结束
            isDialoguePlaying = false;
            currentClipIndex = 0; // 重置索引以便重新开始
        }
    }
    public bool IsSpeaking()
    {
        return isDialoguePlaying;
    }
}