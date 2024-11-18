using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInput;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(menuName = "SO/PlayerInput")]

public class InputReader : ScriptableObject, IActionActions, IPlayerComponent
{
    private PlayerInput _playerInput;

    public event Action JumpEvent;
    public event Action RopeEvent;
    public event Action DashEvent;
    public event Action RopeCancelEvent;
    public event Action AttackEvent;
    public event Action GimicEvent;
    public event Action EscEvent;

    public Vector2 Movement { get; private set; }
    public Vector2 Mouse { get; private set; }
    public bool RopeKeyDown { get; private set; }

    private void OnEnable()
    {
        if (_playerInput == null)
            _playerInput = new PlayerInput();
        _playerInput.Action.SetCallbacks(this);
        _playerInput.Action.Enable();
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            AttackEvent?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            JumpEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnRope(InputAction.CallbackContext context)
    {
        if (context.performed)
            RopeEvent?.Invoke();
        if (context.canceled)
            RopeCancelEvent?.Invoke();
    }

    public void Initialize(Player player)
    {
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        Mouse = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
            DashEvent?.Invoke();
    }

    public void OnUseGimic(InputAction.CallbackContext context)
    {
        if (context.performed)
            GimicEvent?.Invoke();
    }

    public void OnEsc(InputAction.CallbackContext context)
    {
        if (context.performed)
            EscEvent?.Invoke();
    }
}
