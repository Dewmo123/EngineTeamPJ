using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public Dictionary<EnemyStateType, EnemyState> stateDictionary = new Dictionary<EnemyStateType, EnemyState>();
    public EnemyState currentState;

    private Enemy _player;
    public void Init(EnemyStateType start, Enemy player)
    {
        ChangeState(start);
        _player = player;
    }
    public void ChangeState(EnemyStateType type)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = stateDictionary[type];
        currentState.Enter();
    }
    public void AddState(EnemyStateType type, EnemyState state)
    {
        stateDictionary.Add(type, state);
    }
}
