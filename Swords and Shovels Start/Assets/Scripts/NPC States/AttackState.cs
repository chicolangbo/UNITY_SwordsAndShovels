using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : NPCStateBase
{
    private new float lastAttackTime;

    public AttackState(NPCController2 manager) : base(manager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        lastAttackTime = Time.time;
        if (npcCtrl.gameObject.GetComponent<NavMeshAgent>().enabled)
        {
            npcCtrl.animator.SetTrigger("Attack");
            npcCtrl.agent.isStopped = true;
        }
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        base.Update();

        // Trace로 상태 전환
        if(distanceToPlayer > npcCtrl.attackDef.range || npcCtrl.RaycastToTarget)
        {
            npcCtrl.SetState(NPCController2.States.Trace);
            return;
        }

        if (lastAttackTime + npcCtrl.attackDef.coolDown < Time.time)
        {
            lastAttackTime = Time.time;

            if (npcCtrl.gameObject.GetComponent<NavMeshAgent>().enabled)
            {
                var lookPos = npcCtrl.targetTr.transform.position;
                lookPos.y = npcCtrl.transform.position.y;
                npcCtrl.transform.LookAt(lookPos);
                npcCtrl.animator.SetTrigger("Attack");
            }
        }
    }
}
