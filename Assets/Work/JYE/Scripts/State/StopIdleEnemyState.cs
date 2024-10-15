using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopIdleEnemyState : EnemyState
{
    public StopIdleEnemyState(EnemyStateMachine stateMachine, string animName, Enemy enemy) : base(stateMachine, animName, enemy)
    {
    }
    public override void Exit()
    {

    }
}
