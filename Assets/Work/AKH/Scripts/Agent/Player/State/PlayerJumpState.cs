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
        _player.rbCompo.AddForce(Vector2.up * _player.movementCompo.jumpPower, ForceMode2D.Impulse);
    }
    protected override void HandleJumpEvent()
    {
    }
}
