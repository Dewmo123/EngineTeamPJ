using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemyState : EnemyState
{
    public WalkEnemyState(EnemyStateMachine stateMachine, string animName, Enemy enemy) : base(stateMachine,animName,enemy)
    {
    }
    public override void Exit()
    {
        
    }
}
