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
        Destroy(gameObject.transform.parent.gameObject); //��ġ�鵵 �������
    }

    protected override void Type()
    {
        _stateMachine.ChangeState(enemyType.myType);

        speed = enemyType.speed; //�ӵ�

        waitTime = enemyType.waitTime; //�ٽ� �����̱� ���� ��ٸ� �ð�

        transform.position = movePoint1.position; //ó�� ��ġ ���ϱ�
    }
}
