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
    private bool hasTalked;

    public override void OnStart()
    {
        
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAgent = gameObject.GetComponent<NavMeshAgent>();
        enemyAgent.enabled = false;
        hasTalked = false;
    }

    public override TaskStatus OnUpdate()
    {
        Talk();
        return TaskStatus.Running;
    }

    void Talk()
    {
        if (!hasTalked && Vector3.Distance(transform.position, player.transform.position) <= 2f)
        {
            anim.SetBool("Istalk", true);
            anim.SetBool("IsWalk", false);
            hasTalked = true;
        }
    }
}