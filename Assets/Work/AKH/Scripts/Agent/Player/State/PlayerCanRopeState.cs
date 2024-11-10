using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanRopeState : PlayerMoveState
{
    public PlayerCanRopeState(PlayerStateMachine stateMachine, string animName, GamePlayer player) : base(stateMachine, animName, player)
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
        if (_player.movementCompo.isRope)
            _stateMachine.ChangeState(PlayerEnum.Rope);
    }

    protected override void HandleDashEvent()
    {
        _stateMachine.ChangeState(PlayerEnum.Dash);
    }

    protected override void HandleJumpEvent()
    {
    }
}
