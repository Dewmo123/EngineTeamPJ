using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerMoveState : PlayerState
{
    protected InputReader _input;
    public PlayerMoveState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _input = _player.GetCompo<InputReader>();
        _input.JumpEvent += HandleJumpEvent;
    }

    protected abstract void HandleJumpEvent();
}
