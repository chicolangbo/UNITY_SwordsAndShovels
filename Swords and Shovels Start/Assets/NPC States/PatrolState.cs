using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : NPCStateBase
{
    public PatrolState(NPCController2 manager) : base(manager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        npcCtrl.waypointIndex = (int)Mathf.Repeat(npcCtrl.waypointIndex + 1, npcCtrl.waypoints.Length);
        npcCtrl.agent.destination = npcCtrl.waypoints[npcCtrl.waypointIndex].position;
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        base.Update();

        // 상태 전환 우선순위 세팅
        if (distanceToPlayer < npcCtrl.aggroRange)
        {
            npcCtrl.SetState(NPCController2.States.Trace);
            return;
        }

        if (npcCtrl.agent.remainingDistance < npcCtrl.agent.stoppingDistance)
        {
            npcCtrl.SetState(NPCController2.States.Idle);
            return;
        }
    }
}
