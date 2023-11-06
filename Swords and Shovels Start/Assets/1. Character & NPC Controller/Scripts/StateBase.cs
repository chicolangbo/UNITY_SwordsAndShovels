using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class StateBase
{
    abstract public void Enter();
    abstract public void Update();
    abstract public void Exit();
}

public abstract class NPCStateBase : StateBase
{
    protected NPCController2 npcCtrl;

    //private Animator animator; // reference to the animator component
    //protected NavMeshAgent agent; // reference to the NavMeshAgent
    protected float agentSpeed, speed;
    protected Transform player;
    protected float distanceToPlayer;
    protected float timer;

    public NPCStateBase(NPCController2 npcCtrl)
    {
        this.npcCtrl = npcCtrl;
        //animator = npcCtrl.GetComponent<Animator>();
        //agent = npcCtrl.GetComponent<NavMeshAgent>();
        player = player = GameObject.FindWithTag("Player").transform;
        agentSpeed = npcCtrl.agent.speed;
        speed = agentSpeed / 2f;
    }

    public override void Enter()
    {
        timer = 0f;
        npcCtrl.agent.speed = speed;
        npcCtrl.agent.isStopped = false;
    }

    public override void Update()
    {
        distanceToPlayer = Vector3.Distance(player.position, npcCtrl.transform.position);
        npcCtrl.animator.SetFloat("Speed", npcCtrl.agent.velocity.magnitude);
    }
}
