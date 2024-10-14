using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookEnemy : Enemy
{
    protected override void Type()
    {
        _stateMachine.ChangeState(EnemyStateType.Look);
    }
}
