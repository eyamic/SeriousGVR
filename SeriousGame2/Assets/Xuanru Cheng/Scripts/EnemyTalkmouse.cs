using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

public class EnemyTalkmouse : Action
{

    private Animator anim; // Animator component to control character animations.
    private GameObject player; // Reference to the player GameObject.
    private NavMeshAgent enemyAgent; // NavMeshAgent component for controlling AI navigation.
    private AudioSource audioSource; // AudioSource component to play audio.
    private bool isPlayerSpeaking; // Boolean to track if the player is currently speaking.
    private bool isMyTurnToSpeak = false; // Boolean to manage turn-taking in the conversation.
    public GameObject TalktoYoungwomanPanel; // Reference to the UI panel that appears during dialogue.

    public override void OnStart()
    {
        anim = GetComponent<Animator>(); // Get the Animator component attached to the same GameObject.
        player = GameObject.FindGameObjectWithTag("Player"); // Find the GameObject tagged as "Player".
        enemyAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component.
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component.
        enemyAgent.enabled = false; // Initially disable the NavMeshAgent to prevent movement.
    }

    public override TaskStatus OnUpdate()
    {
        CheckPlayerSpeaking(); // Check if the player is currently speaking.
        ManageConversation(); // Manage the conversation based on who is speaking.
        return TaskStatus.Running; // Continues running this task.
    }

    void CheckPlayerSpeaking()
    {
        // Ensure the player object is not null and has a DialogueManagerMouse component.
        if (player != null && player.GetComponent<DialogueManagerMouse>() != null)
        {
            isPlayerSpeaking = player.GetComponent<DialogueManagerMouse>().IsSpeaking(); // Set isPlayerSpeaking based on the player's dialogue status.
        }
        else
        {
            Debug.LogError("Player object or DialogueManager component is missing!"); // Log error if player or required component is missing.
            isPlayerSpeaking = false; // Set a safe default value if conditions aren't met.
        }
    }

    void ManageConversation()
    {
        // 确保如果有对话正在进行，不执行任何动画状态变更Ensure no animation changes if a conversation is ongoing.
        if (!isPlayerSpeaking && !audioSource.isPlaying && isMyTurnToSpeak)
        {
            // 在此处添加敌人的其他行为，例如动画状态等 Add other behaviours of the enemy here, e.g. animation states, etc.
            anim.SetBool("Istalk", false);
            anim.SetBool("IsWalk", true); // 假设敌人可以开始走动Assuming the enemy can start walking
            TalktoYoungwomanPanel.SetActive(false);//No tips
            isMyTurnToSpeak = false; // Reset flag
        }
        else if (isPlayerSpeaking)
        {
            // Ensure the NPC does not walk while the player is speaking
            anim.SetBool("IsWalk", false);
            anim.SetBool("Istalk", true); // Listen or react
            TalktoYoungwomanPanel.SetActive(true);//Show tips
            isMyTurnToSpeak = true; // Prepare for next turn
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming DialogueManager is attached to the same GameObject
            var dialogueManager = GetComponent<DialogueManagerMouse>();
            if (dialogueManager != null && !dialogueManager.IsSpeaking())
            {
                dialogueManager.StartDialogue();
            }
        }
    }
}