using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRopeState : PlayerMoveState
{
    private bool _isDash;
    public PlayerRopeState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _isDash = false;
        _input.RopeCancelEvent += HandleRopeCancel;
    }
    public override void UpdateState()
    {
        _player.GetCompo<GrappleGun>().Roping();

        Vector2 move = new Vector2(_input.Movement.x * _player.movementCompo.moveSpeed, _player.rbCompo.velocity.y);
        _player.rbCompo.AddForce(move.normalized, ForceMode2D.Force);
        _player.HandleSpriteFlip(_player.rbCompo.velocity+(Vector2)_player.transform.position);

        if (_player.movementCompo.isGround.Value)
            HandleRopeCancel();
    }
    public override void Exit()
    {
        base.Exit();
        if (_player.movementCompo.isRope)
            HandleRopeCancel();
        _input.RopeCancelEvent -= HandleRopeCancel;
    }
    private void HandleRopeCancel()
    {
        _player.movementCompo.EscapeRope();
        _stateMachine.ChangeState(PlayerEnum.Fall);
    }

    protected override void HandleJumpEvent()
    {
        _player.movementCompo.EscapeRope();
        _stateMachine.ChangeState(PlayerEnum.AirRoll);
    }

    protected override void HandleDashEvent()
    {
        if (!_isDash)
        {
            _player.rbCompo.AddForce(_input.Movement*10, ForceMode2D.Force);
            _isDash = true;
        }
    }
}
