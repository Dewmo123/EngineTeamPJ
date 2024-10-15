using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkIdleEnemyState : EnemyState
{
    float nowTime;
    public WalkIdleEnemyState(EnemyStateMachine stateMachine, string animName, Enemy enemy) : base(stateMachine, animName, enemy)
    {
    }
    public override void Enter()
    {
        base.Enter();
        nowTime = 0;
    }

    public override void UpdateState()
    {
        ChangeMove();
    }

    public void ChangeMove() //��ٸ� �� �ٽ� ���� �ٲٱ�
    {
        nowTime += Time.deltaTime;
        if(nowTime >= _enemy.waitTime)
        {
            _stateMachine.ChangeState(EnemyStateType.Move);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}