using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
public class EnemySight : Conditional
{
    // Declare delegate events for talk state changes
    public delegate void TalkStateChanged(bool isTalking);
    public static event TalkStateChanged OnTalkStateChanged;

    private Animator anim;
    private GameObject player;
    private NavMeshAgent nma;

    // Define a variable to control if the enemy is talking
    private bool isTalking = false;

    public override void OnStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        nma = GetComponent<NavMeshAgent>();
    }

    public override TaskStatus OnUpdate()
    {
        // Determine if the player is in range
        bool isInRange = (transform.position - player.transform.position).magnitude < 20;

        // If the player is in range
        if (isInRange)
        {
            // Disable NavMeshAgent
            nma.enabled = false;
            // Set the boolean variable for the talk animation to true
            anim.SetBool("IsWalk", false);
            anim.SetBool("Istalk", true);
       
            isTalking = true;
        }
        else
        {
            nma.enabled = true;
            anim.SetBool("Istalk", false);
            anim.SetBool("IsWalk", true);
            isTalking = false;
        }

        // Trigger a talk state change event
        OnTalkStateChanged?.Invoke(isTalking);

        // Return to mission status, success for being in talk range, failure otherwise.
        return isTalking ? TaskStatus.Success : TaskStatus.Failure;
    }
}