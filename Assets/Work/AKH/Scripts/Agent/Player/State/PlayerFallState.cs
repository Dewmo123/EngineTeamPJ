using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerCanRopeState
{
    public PlayerFallState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (_player.movementCompo.isGround.Value)
            _stateMachine.ChangeState(PlayerEnum.Idle);
    }
    protected override void HandleJumpEvent()
    {
    }
}
