using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRopeState : PlayerMoveState
{
    public PlayerRopeState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void UpdateState()
    {
        Vector2 move = new Vector2(_input.Movement.x * _player.movementCompo.moveSpeed, _player.rbCompo.velocity.y);
        _player.rbCompo.AddForce(move.normalized, ForceMode2D.Force);
        if (_player.movementCompo.isGround.Value)
            _stateMachine.ChangeState(PlayerEnum.Idle);
    }
    public override void Exit()
    {
        base.Exit();
        //_player.movementCompo.EscapeRope();
    }

    protected override void HandleJumpEvent()
    {
        _stateMachine.ChangeState(PlayerEnum.Fall);
    }

}
