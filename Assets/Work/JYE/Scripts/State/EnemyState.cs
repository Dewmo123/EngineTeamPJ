using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy _enemy;
    protected EnemyStateMachine _stateMachine;
    protected int _animBoolHash;
    protected bool _endTriggerCalled;
    public EnemyState(EnemyStateMachine stateMachine, string animName, Enemy enemy)
    {
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animName);
        _enemy = enemy;
    }
    public virtual void Enter()
    {
        _enemy.animCompo.SetBool(_animBoolHash, true);
        _endTriggerCalled = false;
    }
    public virtual void Exit()
    {
        _enemy.animCompo.SetBool(_animBoolHash, false);
    }
    public virtual void UpdateState()
    {

    }
    public void AnimationEndTrigger()
    {
        _endTriggerCalled = true;
    }
}

public enum EnemyStateType
{
    Stop, Move, MoveIdle, Die, Look, Hit,GoToPoint
}
