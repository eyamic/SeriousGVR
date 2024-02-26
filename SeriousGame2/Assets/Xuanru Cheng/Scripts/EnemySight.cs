using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
public class EnemySight : Conditional
{
    // Declare delegate events for attack state changes
    public delegate void AttackStateChanged(bool isAttacking);
    public static event AttackStateChanged OnAttackStateChanged;
    private Animator anim;
    private GameObject Player;
   // private float distance;
    private NavMeshAgent nma;
    public override void OnStart()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        nma = GetComponent<NavMeshAgent>();
    }

    public override TaskStatus OnUpdate()
    {
        // Determine if you're in range
        bool isAttacking = (transform.position - Player.transform.position).magnitude < 20;
        //If in range
        if (isAttacking)
        {   //Disable NavMeshAgent
            nma.enabled = false;
            //Set the Boolean variable for the attack animation to false
            anim.SetBool("isAttack",true);
        }
        else
        {
            nma.enabled = true;
            anim.SetBool("isAttack",false);
        }
        //Trigger an attack state change event
        OnAttackStateChanged?.Invoke(isAttacking);
        //Return to mission status, success for entering attack range, failure otherwise.
        return isAttacking ? TaskStatus.Success : TaskStatus.Failure;
    }
}
