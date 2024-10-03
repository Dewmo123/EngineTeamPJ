using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    #region Components
    public Rigidbody2D rbCompo { get; private set; }
    public Animator animCompo { get; private set; }
    public PlayerMovement movementCompo { get; private set; }
    #endregion

    public event Action OnFlipEvent;

    protected virtual void Awake()
    {
        rbCompo = GetComponent<Rigidbody2D>();
        animCompo = GetComponentInChildren<Animator>();
        movementCompo = GetComponent<PlayerMovement>();  
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
