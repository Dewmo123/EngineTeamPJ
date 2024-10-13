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
        _input.DashEvent += HandleDashEvent;
    }


    public override void Exit()
    {
        base.Exit();
        _input.JumpEvent -= HandleJumpEvent;
    }

    public override void UpdateState()
    {
        Vector2 move = new Vector2(_input.Movement.x * _player.movementCompo.moveSpeed, _player.rbCompo.velocity.y);
        _player.Move(move);
        if (_player.rbCompo.velocity.y < 0)
            _stateMachine.ChangeState(PlayerEnum.Fall);
    }
    protected abstract void HandleDashEvent();
    protected abstract void HandleJumpEvent();
}
