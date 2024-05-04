using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

public class EnemyTalk : Action
{
    
    private Animator anim;
    private GameObject player;
    private NavMeshAgent enemyAgent;
    private AudioSource audioSource;
    private bool isPlayerSpeaking;
    private bool isMyTurnToSpeak = false;

    public AudioClip[] dialogueClips; // 敌人的回答语音片段
    private int currentClipIndex = 0;

    public override void OnStart()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        enemyAgent.enabled = false;
    }

    public override TaskStatus OnUpdate()
    {
        CheckPlayerSpeaking();
        ManageConversation();
        return TaskStatus.Running;
    }

    void CheckPlayerSpeaking()
    {
        // 确保player不为空且具有DialogueManager组件
        if (player != null && player.GetComponent<DialogueManager>() != null)
        {
            isPlayerSpeaking = player.GetComponent<DialogueManager>().IsSpeaking();
        }
        else
        {
            Debug.LogError("Player object or DialogueManager component is missing!");
            isPlayerSpeaking = false;  // 安全默认值
        }
    }

    void ManageConversation()
    {
        if (!isPlayerSpeaking && isMyTurnToSpeak)
        {
            if (!audioSource.isPlaying)
            {
                Talk();
            }
        }
        else if (isPlayerSpeaking && !audioSource.isPlaying)
        {
            isMyTurnToSpeak = true;  // 玩家说完后，轮到敌人说话
        }
    }

    void Talk()
    {
        if (currentClipIndex < dialogueClips.Length)
        {
            // 播放敌人的回答语音片段
            audioSource.clip = dialogueClips[currentClipIndex];
            audioSource.Play();
            currentClipIndex++;
        }
        else
        {
            // 对话结束，重置索引以便重新开始
            currentClipIndex = 0;
        }

        // 在此处添加敌人的其他行为，例如动画状态等
        anim.SetBool("Istalk", true);
        anim.SetBool("IsWalk", false);
        isMyTurnToSpeak = false;  // 敌人说完后，轮到玩家
    }
}