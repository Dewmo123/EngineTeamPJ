using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemyState : EnemyState
{
    public HitEnemyState(EnemyStateMachine stateMachine, string animName, Enemy enemy) : base(stateMachine, animName, enemy)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _enemy.view.gameObject.SetActive(false);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
            _stateMachine.ChangeState(_enemy.enemyType.myType);
    }
    public override void Exit()
    {
        base.Exit();
        _enemy.view.gameObject.SetActive(true);
    }
}
