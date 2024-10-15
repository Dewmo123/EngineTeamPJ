using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookIdleEnemyState : EnemyState
{
    public LookIdleEnemyState(EnemyStateMachine stateMachine, string animName, Enemy enemy) : base(stateMachine, animName, enemy)
    {
    }
    public override void Exit()
    {
        base.Exit();
    }
}
