using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerMoveState
{
    public PlayerJumpState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _player.rbCompo.AddForce(Vector2.up, ForceMode2D.Impulse);
    }
    public override void UpdateState()
    {
        if (_endTriggerCalled)
            _stateMachine.ChangeState(PlayerEnum.Idle);
    }
    protected override void HandleJumpEvent()
    {
    }
}
