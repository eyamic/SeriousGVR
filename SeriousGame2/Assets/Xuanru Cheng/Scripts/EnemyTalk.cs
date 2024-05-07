using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
using Action = BehaviorDesigner.Runtime.Tasks.Action;
using UnityEngine.UI;
public class EnemyTalk : Action
{

    private Animator anim;  // Animator component reference to control animations.
    private GameObject player;  // GameObject reference to the player.
    private NavMeshAgent enemyAgent;  // NavMeshAgent component for AI pathfinding.
    private AudioSource audioSource;  // AudioSource component to play audio.
    private bool isPlayerSpeaking;  // Boolean to check if the player is currently speaking.
    private bool isMyTurnToSpeak = false;  // Boolean to manage turn-taking in the conversation.
    public GameObject TalktoYoungwomanPanel;  // UI panel that is displayed during conversations.
 //   public AudioClip[] dialogueClips; // 敌人的回答语音片段
   // private int currentClipIndex = 0;
   // Called when the task is started; initializes components
    public override void OnStart()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        enemyAgent.enabled = false;
        TalktoYoungwomanPanel.SetActive(true);
    }
// Called every frame while the task is active.
    public override TaskStatus OnUpdate()
    {
        CheckPlayerSpeaking();
        ManageConversation();
        return TaskStatus.Running;
    }
    // Checks if the player is currently speaking by accessing the DialogueManager.
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
// Manages the enemy's conversation behavior based on who is currently speaking.
    void ManageConversation()
    {
        // 确保如果有对话正在进行，不执行任何动画状态变更Ensure that no animation state changes are performed if there is a dialogue going on
        if (!isPlayerSpeaking && !audioSource.isPlaying && isMyTurnToSpeak)
        {
            // 在此处添加敌人的其他行为，例如动画状态等Add other behaviours of the enemy here, e.g. animation states, etc.
            anim.SetBool("Istalk", false);
            anim.SetBool("IsWalk", true); // 假设敌人可以开始走动
            //TalktoYoungwomanPanel.SetActive(false);//No tips
            isMyTurnToSpeak = false; // Reset flag
        }
        else if (isPlayerSpeaking)
        {
            // Ensure the NPC does not walk while the player is speaking
            anim.SetBool("IsWalk", false);
            //TalktoYoungwomanPanel.SetActive(true);//Show tips
            anim.SetBool("Istalk", true); // Listen or react
            isMyTurnToSpeak = true; // Prepare for next turn
        }
    }
    // Triggered when the player enters a predefined trigger area.
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