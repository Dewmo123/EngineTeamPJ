using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public EnemyStateType stateType;

    public void StateEnter() //state���� �Ǿ��� �� (����)
    {

    }

    public void StateUpdate() //�� state ���� �Ǵ� ���� ����
    {

    }

    protected virtual void Idle()
    {

    }

    protected virtual void Movement()
    {

    }

    protected virtual void Die()
    {

    }
}

public enum EnemyStateType
{
    Idle, Move, Die
}
