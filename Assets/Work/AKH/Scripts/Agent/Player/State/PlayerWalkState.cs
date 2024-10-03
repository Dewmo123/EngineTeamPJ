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
        Vector2 move = new Vector2(_input.Movement.x, _player.rbCompo.velocity.y);
        _player.GetCompo<PlayerMovement>().AcceptMovement(move);
        if (_input.Movement == Vector2.zero)
            _stateMachine.ChangeState(PlayerEnum.Idle);
    }

    protected override void HandleJumpEvent()
    {
        _stateMachine.ChangeState(PlayerEnum.Jump);
    }
}
