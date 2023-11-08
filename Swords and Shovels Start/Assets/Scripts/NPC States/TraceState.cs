using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TraceState : NPCStateBase
{
    public TraceState(NPCController2 manager) : base(manager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (npcCtrl.gameObject.GetComponent<NavMeshAgent>().enabled)
        {
            npcCtrl.agent.speed = agentSpeed; // 뛰어가도록 세팅
            npcCtrl.agent.destination = player.transform.position;
        }
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        base.Update();

        //agent.destination = player.position; // 굳이 1초에 60번 연산할 필요 없음
        if (distanceToPlayer > npcCtrl.aggroRange)
        {
            npcCtrl.SetState(NPCController2.States.Idle);
            return;
        }

        // 공격 range 검사 후 상태 전환
        if(distanceToPlayer < npcCtrl.attackRange)
        {
            npcCtrl.SetState(NPCController2.States.Attack);
            return;
        }

        timer += Time.deltaTime;
        if (timer >= npcCtrl.traceInterval)
        {
            timer = 0f;
             if(npcCtrl.agent.enabled == true)
            {
                 npcCtrl.agent.destination = player.position;
            }
        }
    }
}
