using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanRopeState : PlayerMoveState
{
    public PlayerCanRopeState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _input.RopeEvent += HandleRope;
    }
    public override void Exit()
    {
        base.Exit();
        _input.RopeEvent -= HandleRope;
    }
    public virtual void HandleRope()
    {
        _player.movementCompo.ShootRope();
        _stateMachine.ChangeState(PlayerEnum.Rope);
    }
    protected override void HandleJumpEvent()
    {
    }
}
