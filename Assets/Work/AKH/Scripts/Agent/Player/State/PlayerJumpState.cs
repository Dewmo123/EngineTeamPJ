using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerCanRopeState
{
    public PlayerJumpState(PlayerStateMachine stateMachine, string animName, GamePlayer player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _player.rbCompo.AddForce(Vector2.up * _player.movementCompo.jumpPower, ForceMode2D.Impulse);
    }
    protected override void HandleJumpEvent()
    {
        _stateMachine.ChangeState(PlayerEnum.AirRoll);
    }
}
