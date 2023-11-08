using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : NPCStateBase
{
    private new float timer = 0f;
    private float duration = 1f;

    public AttackState(NPCController2 manager) : base(manager)
    {
    }
    public override void Enter()
    {
        base.Enter();
        if (npcCtrl.gameObject.GetComponent<NavMeshAgent>().enabled)
        {
            npcCtrl.animator.SetTrigger("Attack");
            npcCtrl.agent.isStopped = true;
        }

        //Debug.Log($"{npcCtrl.gameObject.name} Attack State");
    }

    public override void Exit()
    {
        //npcCtrl.animator.SetFloat("Speed", npcCtrl.agent.velocity.magnitude);
    }

    public override void Update()
    {
        base.Update();

        // Trace로 상태 전환
        if(distanceToPlayer > npcCtrl.attackRange)
        {
            npcCtrl.SetState(NPCController2.States.Trace);
            return;
        }

        timer += Time.deltaTime;
        if (timer > duration)
        {
            timer = 0f;
            if (npcCtrl.gameObject.GetComponent<NavMeshAgent>().enabled)
            {
                npcCtrl.animator.SetTrigger("Attack");
            }
        }
    }
}
