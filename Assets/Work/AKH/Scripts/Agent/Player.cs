using GGMPool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class Player : MonoBehaviour
{
    #region Components
    public Rigidbody2D rbCompo { get; private set; }
    public Animator animCompo { get; private set; }
    public PlayerMovement movementCompo { get; private set; }
    public SpriteRenderer rendererCompo { get; private set; }
    public SpringJoint2D jointCompo { get; private set; }
    protected Dictionary<Type, IPlayerComponent> _components;
    [field: SerializeField] protected InputReader _inputReader;
    #endregion

    public UnityEvent GrappleEvent;

    public event Action OnFlipEvent;

    protected virtual void Awake()
    {
        Debug.Log(gameObject.name);
        rbCompo = GetComponent<Rigidbody2D>();
        animCompo = GetComponentInChildren<Animator>();
        rendererCompo = GetComponentInChildren<SpriteRenderer>();
        movementCompo = GetComponent<PlayerMovement>();
        jointCompo = GetComponent<SpringJoint2D>();
        #region SetIPlayerCompo
        _components = new Dictionary<Type, IPlayerComponent>();
        GetComponentsInChildren<IPlayerComponent>().ToList().ForEach(x => _components.Add(x.GetType(), x));
        _components.Add(_inputReader.GetType(), _inputReader);
        _components.Values.ToList().ForEach(compo => compo.Initialize(this));
        #endregion
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
    public void PlaySound(string name)
    {
        PoolTypeSO type = GameManager.Instance.poolItemDic["SoundPlayer"].poolType;
        var soundPlayer = GameManager.Instance.poolManager.Pop(type) as SoundPlayer;
        soundPlayer.PlaySound(GameManager.Instance._soundDic[name]);
    }
    #region Flip Charater
    public bool IsFacingRight()
    {
        return Mathf.Approximately(transform.eulerAngles.y, 0);
    }
    public void HandleSpriteFlip(Vector3 targetPosition)
    {
        bool isRight = IsFacingRight();
        if (targetPosition.x < transform.position.x && isRight)
        {
            transform.eulerAngles = new Vector3(0, -180F, 0);
            OnFlipEvent?.Invoke();
        }
        else if (targetPosition.x > transform.position.x && !isRight)
        {
            transform.eulerAngles = Vector3.zero;
            OnFlipEvent?.Invoke();
        }

    }
    #endregion
}
