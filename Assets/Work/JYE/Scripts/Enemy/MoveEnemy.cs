using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : Enemy
{
    protected override void Update()
    {
        base.Update();
    }

    public override void EnemyDie()
    {
        Destroy(gameObject.transform.parent.gameObject); //��ġ�鵵 �������
    }

    protected override void Type()
    {
        _stateMachine.ChangeState(EnemyStateType.Move);

        speed = enemyType.speed; //�ӵ�

        waitTime = enemyType.waitTime; //�ٽ� �����̱� ���� ��ٸ� �ð�

        transform.position = movePoint1.position; //ó�� ��ġ ���ϱ�
    }
}
