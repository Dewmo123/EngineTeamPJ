using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Transparent : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private int _ignoreLayer;
    [SerializeField] private int _playerLayer;
    public bool isActive { get; private set; }
    private NotifyValue<Vector2> _movement = new NotifyValue<Vector2>();
    private Player _player;
    public UnityEvent HideEvent;
    public UnityEvent ShowEvent;

    private Coroutine _hide;

    public void Enable()
    {
        isActive = true;
        _movement.OnValueChanged += HandleMoveChanged;
    }

    private void HandleMoveChanged(Vector2 prev, Vector2 next)
    {
        if (next == Vector2.zero&&_player.movementCompo.isGround.Value)
        {
            if(_hide==null)
            _hide = StartCoroutine(Hide());
        }
        else
        {
            StopAllCoroutines();
            _hide = null;
            _player.gameObject.layer = _playerLayer;
            ShowEvent?.Invoke();
        }
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.1f);
        _player.gameObject.layer = _ignoreLayer;
        HideEvent?.Invoke();
    }

    private void Update()
    {
        if (isActive)
        {
            _movement.Value = _player.rbCompo.velocity;
        }
    }

    public void Disable()
    {
        isActive = false;
        _movement.OnValueChanged -= HandleMoveChanged;
    }
    private void OnDestroy()
    {
        if (isActive)
            Disable();
    }

    public void Initialize(Player player)
    {
        _player = player;
        _playerLayer = _player.gameObject.layer;
    }
}
