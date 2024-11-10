using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerMoveState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, string animName, GamePlayer player) : base(stateMachine, animName, player)
    {
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (_input.Movement.x != 0)
            _stateMachine.ChangeState(PlayerEnum.Walk);
    }

    protected override void HandleDashEvent()
    {
    }

    protected override void HandleJumpEvent()
    {
        _stateMachine.ChangeState(PlayerEnum.Jump);
    }
}
