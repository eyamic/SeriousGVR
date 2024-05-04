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

 //   public AudioClip[] dialogueClips; // 敌人的回答语音片段
   // private int currentClipIndex = 0;

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
            isPlayerSpeaking = false; // 安全默认值
        }
    }

    void ManageConversation()
    {
        // 确保如果有对话正在进行，不执行任何动画状态变更
        if (!isPlayerSpeaking && !audioSource.isPlaying && isMyTurnToSpeak)
        {
            // 在此处添加敌人的其他行为，例如动画状态等
            anim.SetBool("Istalk", false);
            anim.SetBool("IsWalk", true); // 假设敌人可以开始走动
            isMyTurnToSpeak = false; // Reset flag
        }
        else if (isPlayerSpeaking)
        {
            // Ensure the NPC does not walk while the player is speaking
            anim.SetBool("IsWalk", false);
            anim.SetBool("Istalk", true); // Listen or react
            isMyTurnToSpeak = true; // Prepare for next turn
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming DialogueManager is attached to the same GameObject
            var dialogueManager = GetComponent<DialogueManager>();
            if (dialogueManager != null && !dialogueManager.IsSpeaking())
            {
                dialogueManager.StartDialogue();
            }
        }
    }
}