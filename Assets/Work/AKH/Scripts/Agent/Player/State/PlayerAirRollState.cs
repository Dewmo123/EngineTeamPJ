using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirRollState : PlayerCanRopeState
{
    public PlayerAirRollState(PlayerStateMachine stateMachine, string animName, GamePlayer player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _player.rbCompo.AddForce(Vector2.up * _player.movementCompo.jumpPower, ForceMode2D.Impulse);
    }
    public override void UpdateState()
    {
        Vector2 move = new Vector2(_input.Movement.x * _player.movementCompo.moveSpeed, _player.rbCompo.velocity.y);
        _player.Move(move);
        if (_endTriggerCalled)
            _stateMachine.ChangeState(PlayerEnum.Fall);
    }
    protected override void HandleJumpEvent()
    {
    }
}
