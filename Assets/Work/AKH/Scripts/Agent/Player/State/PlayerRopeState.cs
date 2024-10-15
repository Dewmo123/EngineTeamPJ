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
        Vector2 move;
        _player.HandleSpriteFlip(_player.rbCompo.velocity + (Vector2)_player.transform.position);

        if (Mathf.Sign(_player.rbCompo.velocity.x) != Mathf.Sign(_input.Movement.x))
            move = -_player.rbCompo.velocity.normalized;
        else
            move = _player.rbCompo.velocity.normalized;

        if (_input.Movement.x != 0)
        {//anchor.y - distance == min y 
            _player.rbCompo.AddForce(move, ForceMode2D.Force);
            _player.rbCompo.AddForce(Vector2.down * Mathf.Abs(_player.rbCompo.velocity.y*0.6F), ForceMode2D.Force);
        }

        if (_player.movementCompo.isGround.Value)
            HandleRopeCancel();
    }
    public override void Exit()
    {
        base.Exit();
        _input.RopeCancelEvent -= HandleRopeCancel;
        _player.movementCompo.EscapeRope();
    }
    private void HandleRopeCancel()
    {
        if (_player.GetCompo<GrappleGun>().launchToPoint || _player.movementCompo.isGround.Value)
            _stateMachine.ChangeState(PlayerEnum.Idle);
        else
            _stateMachine.ChangeState(PlayerEnum.AirRoll);
    }

    protected override void HandleJumpEvent()
    {
        _player.GetCompo<GrappleGun>().launchToPoint = true;
    }

    protected override void HandleDashEvent()
    {
        if (!_isDash)
        {
            _player.rbCompo.AddForce(_player.rbCompo.velocity.normalized * _player.movementCompo.dashPower, ForceMode2D.Impulse);
            _player.GetCompo<AgentVFX>().ToggleAfterImage(true);
            _player.WaitCoroutine(_player.movementCompo.ropeAfterImageTime, () => _player.GetCompo<AgentVFX>().ToggleAfterImage(false));
            _isDash = true;
        }
    }
}
