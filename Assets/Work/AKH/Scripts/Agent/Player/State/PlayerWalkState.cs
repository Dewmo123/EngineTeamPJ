using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerMoveState
{
    public PlayerWalkState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (_input.Movement == Vector2.zero)
            _stateMachine.ChangeState(PlayerEnum.Idle);
    }

    protected override void HandleJumpEvent()
    {
        _stateMachine.ChangeState(PlayerEnum.Jump);
    }
}
