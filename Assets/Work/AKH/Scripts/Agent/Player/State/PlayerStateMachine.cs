using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public Dictionary<PlayerEnum, PlayerState> stateDictionary = new Dictionary<PlayerEnum, PlayerState>();
    public PlayerState currentState;

    private Player _player;
    public void Init(PlayerEnum start, Player player)
    {
        ChangeState(start);
        _player = player;
    }
    public void ChangeState(PlayerEnum type)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = stateDictionary[type];
        currentState.Enter();
    }
    public void AddState(PlayerEnum type, PlayerState state)
    {
        stateDictionary.Add(type, state);
    }
}
