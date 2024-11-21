using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    public UnityEvent WalkEvent;

    [SerializeField] private float _speed;
    private bool _endTriggerCalled = false;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (_endTriggerCalled)
        {
            _animator.SetBool("Show", false);
        }
    }
    private void FixedUpdate()
    {
        if (_endTriggerCalled)
        {
            _rb.velocity = new Vector2(_speed, _rb.velocity.y);
        }
    }
    public void Flip()
    {
        _speed = -_speed;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y == 0 ? 180 : 0,0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GamePlayer player;
        if(collision.gameObject.TryGetComponent(out player))
        {
            player.SetDeadState();
            _rb.velocity = Vector2.zero;
        }

    }
    public void AnimationEndTrigger()
    {
        _endTriggerCalled = true;
    }
    public void Walk()
    {
        WalkEvent?.Invoke();
    }
}
