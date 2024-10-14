using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEnemy : Enemy
{
    protected override void Type()
    {
        _stateMachine.ChangeState(EnemyStateType.Stop);
    }
}
