using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerMoveState
{
    public PlayerFallState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (_player.rbCompo.velocity.y >= 0)
            _stateMachine.ChangeState(PlayerEnum.Idle);
    }
    protected override void HandleJumpEvent()
    {
    }
}
