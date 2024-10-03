using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInput;
[CreateAssetMenu(menuName = "SO/PlayerInput")]

public class InputReader : ScriptableObject, IActionActions, IPlayerComponent
{
    private PlayerInput _playerInput;

    public event Action JumpEvent;
    public event Action RopeEvent;
    public event Action AttackEvent;

    public Vector2 Movement { get; private set; }

    private void OnEnable()
    {
        if (_playerInput == null)
            _playerInput = new PlayerInput();
        _playerInput.Action.SetCallbacks(this);
        _playerInput.Action.Enable();
    }
    public void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed)
            AttackEvent?.Invoke();
    }

    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed)
            JumpEvent?.Invoke();
    }

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnRope(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed)
            RopeEvent?.Invoke();
    }

    public void Initialize(Player player)
    {
    }
}
