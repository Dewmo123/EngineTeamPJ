using UnityEngine;

public class PlayerRopeState : PlayerMoveState
{
    private bool _isDash;
    private GrappleGun _gun;
    private Rigidbody2D _rb;
    public PlayerRopeState(PlayerStateMachine stateMachine, string animName, GamePlayer player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _isDash = false;
        _gun = _player.GetCompo<GrappleGun>();
        _rb = _player.rbCompo;
        _input.RopeCancelEvent += HandleRopeCancel;
    }
    public override void UpdateState()
    {
        _gun.Roping();
        Vector2 move;
        _player.HandleSpriteFlip(_rb.velocity + (Vector2)_player.transform.position);

        if (Mathf.Sign(_rb.velocity.x) != Mathf.Sign(_input.Movement.x))
            move = -_rb.velocity.normalized;
        else
            move = _rb.velocity.normalized;

        if (_input.Movement.x != 0)
        {
            _player.rbCompo.AddForce(move, ForceMode2D.Force);
        }
        if(_player.transform.position.y>= _gun.grapplePoint.y*0.8f)
        {
            _rb.AddForce(Vector2.down * Mathf.Abs(_rb.velocity.y), ForceMode2D.Force);
        }

    }
    public override void Exit()
    {
        base.Exit();
        _input.RopeCancelEvent -= HandleRopeCancel;
        _player.movementCompo.EscapeRope();
    }
    private void HandleRopeCancel()
    {
        _player.GetCompo<GrappleGun>().StopLaunch();
        if (_player.movementCompo.isGround.Value)
            _stateMachine.ChangeState(PlayerEnum.Idle);
        else
            _stateMachine.ChangeState(PlayerEnum.AirRoll);
    }

    protected override void HandleJumpEvent()
    {
        _player.GetCompo<GrappleGun>().GoToPoint();
    }

    protected override void HandleDashEvent()
    {
        if (!_isDash)
        {
            _player.PlaySound("RopeSwing");
            _rb.AddForce(_rb.velocity.normalized * _player.movementCompo.dashPower, ForceMode2D.Impulse);
            _player.GetCompo<AgentVFX>().ToggleAfterImage(true);
            _player.WaitCoroutine(_player.movementCompo.ropeAfterImageTime, () => _player.GetCompo<AgentVFX>().ToggleAfterImage(false));
            _isDash = true;
        }
    }
}
