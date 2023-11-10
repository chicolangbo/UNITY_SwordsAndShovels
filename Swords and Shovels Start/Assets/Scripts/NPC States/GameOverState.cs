using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : NPCStateBase
{
    public GameOverState(NPCController2 npcCtrl) : base(npcCtrl)
    {
    }
    public override void Enter()
    {
        npcCtrl.agent.isStopped = true;
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        
    }
}
