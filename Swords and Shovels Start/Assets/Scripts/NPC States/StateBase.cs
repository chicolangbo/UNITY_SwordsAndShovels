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

    protected float agentSpeed, speed;
    protected float distanceToPlayer;
    protected float remainToPlayer;
    protected float timer;

    public NPCStateBase(NPCController2 npcCtrl)
    {
        this.npcCtrl = npcCtrl;
        //animator = npcCtrl.GetComponent<Animator>();
        //agent = npcCtrl.GetComponent<NavMeshAgent>();
        agentSpeed = npcCtrl.agent.speed;
        speed = agentSpeed / 2f;
    }

    public override void Enter()
    {
        if (npcCtrl.gameObject.GetComponent<NavMeshAgent>().enabled)
        {
            timer = 0f;
            npcCtrl.agent.speed = speed;
            npcCtrl.agent.isStopped = false;
        }
    }

    public override void Update()
    {
        if(npcCtrl.targetTr != null)
        {
            distanceToPlayer = Vector3.Distance(npcCtrl.targetTr.position, npcCtrl.transform.position);
        }
        npcCtrl.animator.SetFloat("Speed", npcCtrl.agent.velocity.magnitude);
    }

    public bool IsOnNavMesh(NavMeshAgent agent)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(agent.transform.position, out hit, agent.height * 0.5f, NavMesh.AllAreas);
    }
}
