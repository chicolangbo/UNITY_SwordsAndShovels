using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceState : NPCStateBase
{
    public TraceState(NPCController2 manager) : base(manager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        npcCtrl.agent.isStopped = false;
        npcCtrl.agent.speed = agentSpeed; // �پ���� ����
        npcCtrl.agent.destination = player.transform.position;

    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        base.Update();

        //agent.destination = player.position; // ���� 1�ʿ� 60�� ������ �ʿ� ����
        if (distanceToPlayer > npcCtrl.aggroRange)
        {
            npcCtrl.SetState(NPCController2.States.Idle);
            return;
        }

        timer += Time.deltaTime;
        if (timer >= npcCtrl.traceInterval)
        {
            timer = 0f;
            npcCtrl.agent.destination = player.position;
        }
    }
}
