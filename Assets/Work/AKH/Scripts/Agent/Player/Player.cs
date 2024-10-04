using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Player : Agent
{
    public float speed;
    public float jumpPower;
    [field: SerializeField] private InputReader _inputReader;
    private PlayerStateMachine _stateMachine;
    private Dictionary<Type, IPlayerComponent> _components;
    protected override void Awake()
    {
        base.Awake();
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
        _stateMachine.Init(PlayerEnum.Idle,this);
        #endregion
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
