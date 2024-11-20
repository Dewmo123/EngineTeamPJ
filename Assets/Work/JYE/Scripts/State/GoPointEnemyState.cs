using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPointEnemyState : EnemyState
{
    private bool right = false;
    private bool movePosIsLeft = false;
    public GoPointEnemyState(EnemyStateMachine stateMachine, string animName, Enemy enemy) : base(stateMachine, animName, enemy)
    {
    }
    public override void UpdateState()
    {
        base.UpdateState();
        CheckArrive();
    }

    private void CheckArrive()
    {
        if (right && Mathf.Approximately(_enemy.transform.position.x, _enemy.originPos.x))
        {
            Debug.Log("changeStop1");
            _stateMachine.ChangeState(EnemyStateType.Stop);
        }
        if (!right && Mathf.Approximately(_enemy.transform.position.x, _enemy.movePoint2.position.x))
        {
            Debug.Log("changeStop2");
            _stateMachine.ChangeState(EnemyStateType.Stop);
        }
    }

    public override void Enter()
    {
        base.Enter();
        right = _enemy.IsFacingRight();
        movePosIsLeft = _enemy.transform.position.x > _enemy.movePoint2.position.x;
        Move();
    }

    private void Move() //알아서 움직이기
    {
        if (right)
        {
            _enemy.HandleSpriteFlip(_enemy.transform.position + (movePosIsLeft ? Vector3.right : Vector3.left));
            StartMv2();
        }
        else
        {
            Debug.Log(_enemy.originPos);
            _enemy.HandleSpriteFlip(_enemy.transform.position + (movePosIsLeft ? Vector3.left : Vector3.right));
            StartMv1();
        }
    }
    private void StartMv1() //1위치에서 출발 할 때
    {
        _enemy.transform.DOMoveX(_enemy.movePoint2.position.x, _enemy.moveDuraion).SetEase(Ease.Linear);
    }
    private void StartMv2() //2위치에서 출발 할 때
    {
        _enemy.transform.DOMoveX(_enemy.originPos.x, _enemy.moveDuraion).SetEase(Ease.Linear);
    }
}
