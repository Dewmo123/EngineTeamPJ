using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GamePlayer : Player
{

    private PlayerStateMachine _stateMachine;
    public UnityEvent playerDeadEvent;
    public PlayerEnum currentState => _stateMachine.GetCurType();
    protected override void Awake()
    {
        base.Awake();

        #region SetState
        _stateMachine = new PlayerStateMachine();
        _stateMachine.AddState(PlayerEnum.Idle, new PlayerIdleState(_stateMachine, "Idle", this));
        _stateMachine.AddState(PlayerEnum.Walk, new PlayerWalkState(_stateMachine, "Walk", this));
        _stateMachine.AddState(PlayerEnum.Jump, new PlayerJumpState(_stateMachine, "Jump", this));
        _stateMachine.AddState(PlayerEnum.Fall, new PlayerFallState(_stateMachine, "Fall", this));
        _stateMachine.AddState(PlayerEnum.Rope, new PlayerRopeState(_stateMachine, "Rope", this));
        _stateMachine.AddState(PlayerEnum.Dash, new PlayerDashState(_stateMachine, "Dash", this));
        _stateMachine.AddState(PlayerEnum.Dead, new PlayerDeadState(_stateMachine, "Dead", this));
        _stateMachine.AddState(PlayerEnum.AirRoll, new PlayerAirRollState(_stateMachine, "AirRoll", this));
        _stateMachine.Init(PlayerEnum.Idle,this);
        #endregion
        GetCompo<Transparent>().Enable();
    }

    private void Start()
    {
        playerDeadEvent.AddListener(GameManager.Instance.ReStart);
    }
    private void OnDestroy()
    {
        playerDeadEvent.RemoveListener(GameManager.Instance.ReStart);
    }
    public void Move(Vector2 vector)
    {
        movementCompo.AcceptMovement(vector);
        HandleSpriteFlip((Vector3)vector +transform.position);
    }
    public void EndTriggerCalled()
    {
        _stateMachine.currentState.AnimationEndTrigger();
    }
    private void Update()
    {
        _stateMachine.currentState.UpdateState();
    }
    public void WaitCoroutine(float time,Action callback)
    {
        StartCoroutine(Wait(time, callback));
    }
    public void SetDeadState()
    {
        _stateMachine.ChangeState(PlayerEnum.Dead);
    }
    private IEnumerator Wait(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback.Invoke();
    }
}
