using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Player : Agent
{
    public SpringJoint2D jointCompo { get; private set; }

    [field: SerializeField] private InputReader _inputReader;
    private PlayerStateMachine _stateMachine;

    public UnityEvent GrappleEvent;
    
    private Dictionary<Type, IPlayerComponent> _components;
    public PlayerEnum currentState => _stateMachine.GetCurType();
    protected override void Awake()
    {
        base.Awake();
        jointCompo = GetComponent<SpringJoint2D>();
        #region SetIPlayerCompo
        _components = new Dictionary<Type, IPlayerComponent>();
        GetComponentsInChildren<IPlayerComponent>().ToList().ForEach(x => _components.Add(x.GetType(), x));
        _components.Add(_inputReader.GetType(), _inputReader);
        _components.Values.ToList().ForEach(compo => compo.Initialize(this));
        #endregion
        #region SetState
        _stateMachine = new PlayerStateMachine();
        _stateMachine.AddState(PlayerEnum.Idle, new PlayerIdleState(_stateMachine, "Idle", this));
        _stateMachine.AddState(PlayerEnum.Walk, new PlayerWalkState(_stateMachine, "Walk", this));
        _stateMachine.AddState(PlayerEnum.Jump, new PlayerJumpState(_stateMachine, "Jump", this));
        _stateMachine.AddState(PlayerEnum.Fall, new PlayerFallState(_stateMachine, "Fall", this));
        _stateMachine.AddState(PlayerEnum.Rope, new PlayerRopeState(_stateMachine, "Rope", this));
        _stateMachine.AddState(PlayerEnum.Dash, new PlayerDashState(_stateMachine, "Dash", this));
        _stateMachine.AddState(PlayerEnum.AirRoll, new PlayerAirRollState(_stateMachine, "AirRoll", this));
        _stateMachine.Init(PlayerEnum.Idle,this);
        #endregion
        GetCompo<Transparent>().Enable();
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

    private IEnumerator Wait(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback.Invoke();
    }

    public T GetCompo<T>() where T : class
    {
        Type t = typeof(T);
        if(_components.TryGetValue(t,out IPlayerComponent compo))
        {
            return compo as T;
        }
        return default;
    }
}
