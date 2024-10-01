using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : Agent
{
    [field: SerializeField] public InputReader inputReader { get; private set; }
    private PlayerStateMachine _stateMachine;
    private void Awake()
    {
        _stateMachine = new PlayerStateMachine();
        _stateMachine.AddState(PlayerEnum.Idle, new PlayerIdleState(_stateMachine, "Idle", this));
        _stateMachine.Init(PlayerEnum.Idle,this);
    }
}
