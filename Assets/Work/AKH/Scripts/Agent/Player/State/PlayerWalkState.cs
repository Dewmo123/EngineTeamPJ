using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerCanRopeState
{
    public PlayerWalkState(PlayerStateMachine stateMachine, string animName, GamePlayer player) : base(stateMachine, animName, player)
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
