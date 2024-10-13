using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public EnemyStateType stateType;

    public void StateEnter() //state변경 되었을 때 (시작)
    {

    }

    public void StateUpdate() //그 state 실행 되는 동안 실행
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
