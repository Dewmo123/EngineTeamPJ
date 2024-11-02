using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPointEnemyState : EnemyState
{
    private bool right = true;
    public GoPointEnemyState(EnemyStateMachine stateMachine, string animName, Enemy enemy) : base(stateMachine, animName, enemy)
    {
    }
    public override void UpdateState()
    {
        base.UpdateState();
        Move();
        _enemy.view.GetComponent<SpriteRenderer>().flipX = right;
    }

    private void Move() //알아서 움직이기
    {
        if (right)
        {
            _enemy.HandleSpriteFlip(_enemy.transform.position + Vector3.right);
            StartMv2();
            if (_enemy.transform.position == _enemy.movePoint1.position)
            {
                _stateMachine.ChangeState(EnemyStateType.Stop);
                right = false;
            }
        }
        else
        {
            _enemy.HandleSpriteFlip(_enemy.transform.position + Vector3.left);
            StartMv1();
            if (_enemy.transform.position == _enemy.movePoint2.position)
            {
                _stateMachine.ChangeState(EnemyStateType.Stop);
                right = true;
            }
        }
    }
    private void StartMv1() //1위치에서 출발 할 때
    {
        _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, _enemy.movePoint2.position, _enemy.speed * Time.deltaTime);
    }
    private void StartMv2() //2위치에서 출발 할 때
    {
        _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, _enemy.movePoint1.position, _enemy.speed * Time.deltaTime);
    }
}
