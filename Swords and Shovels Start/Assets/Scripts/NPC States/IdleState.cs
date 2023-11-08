using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : NPCStateBase
{
    public IdleState(NPCController2 manager) : base(manager)
    {
    }

    public override void Enter()
    {
        base.Enter();
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

        timer += Time.deltaTime;
        if (timer >= npcCtrl.idleTime)
        {
            npcCtrl.SetState(NPCController2.States.Patrol);
            return;
        }
    }
}
