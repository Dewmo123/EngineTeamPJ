using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : Enemy
{
    protected override void Update()
    {
        base.Update();
        movePoint1.position = new Vector3(movePoint1.position.x, transform.position.y);
        movePoint2.position = new Vector3(movePoint2.position.x, transform.position.y);
    }

    public override void EnemyDie()
    {
        Destroy(gameObject.transform.parent.gameObject); //위치들도 지우려고
    }

    protected override void Type()
    {
        _stateMachine.ChangeState(enemyType.myType);

        speed = enemyType.speed; //속도

        waitTime = enemyType.waitTime; //다시 움직이기 위해 기다릴 시간

        transform.position = movePoint1.position; //처음 위치 정하기
    }
}
