using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour, IPlayerComponent
{
    private Player _player;

    [SerializeField] private Transform _groundCheckerTrm;

    [Header("Settings")]
    public float moveSpeed = 5f;
    public float dashPower= 30f;
    public float jumpPower = 7f;
    public float extraGravity = 30f;
    public float gravityDelay = 0.15f;
    public float knockbackTime = 0.2f;
    public float afterImageTime = 0.2f;
    public float ropeAfterImageTime = 0.5f;
    public float maxDistance = 10;

    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Vector2 _groundCheckerSize;

    public NotifyValue<bool> isGround = new NotifyValue<bool>();
    public bool isRope = false;

    public UnityEvent landEvent;

    private float _timeInAir;
    protected bool _canMove = true;
    protected bool _isDash = false;
    private void Awake()
    {
        isGround.OnValueChanged += HandleGrounded;
    }

    private void HandleGrounded(bool prev, bool next)
    {
        if (next)
        {
            landEvent?.Invoke();
        }
    }

    public void AcceptMovement(Vector2 move)
    {
        _player.rbCompo.velocity = move;
    }
    private void Update()
    {
        if (isGround.Value == false && !isRope)
            _timeInAir += Time.deltaTime;
        else
            _timeInAir = 0;
    }
    private void FixedUpdate()
    {
        if (_isDash) return;
        _player.jointCompo.distance = Vector2.Distance(transform.position, _player.jointCompo.connectedAnchor);
        CheckGrounded();
        ApplyExtraGravity();
    }

    public void OnDash(Vector2 direction,float dashTime,Action endTrigger)
    {
        StartCoroutine(Dash(direction,dashTime,endTrigger));
    }

    private IEnumerator Dash(Vector2 direction, float dashTime, Action endTrigger)
    {
        _isDash = true;
        _player.rbCompo.velocity = direction * dashPower;
        _player.rbCompo.gravityScale = 0;
        _player.GetCompo<AgentVFX>().ToggleAfterImage(true);

        yield return new WaitForSeconds(dashTime);

        _player.GetCompo<AgentVFX>().ToggleAfterImage(false);
        _player.rbCompo.gravityScale = 1;
        _isDash = false;
        endTrigger.Invoke();
    }

    #region Physics
    public void CheckGrounded()
    {
        Collider2D collider = Physics2D.OverlapBox(_groundCheckerTrm.position, _groundCheckerSize, 0, _whatIsGround);
        isGround.Value = collider != null;
    }
    private void ApplyExtraGravity()
    {
        if (_timeInAir > gravityDelay)
            _player.rbCompo.AddForce(new Vector2(0, -extraGravity));
    }
    public void Initialize(Player player)
    {
        _player = player;
    }
    #endregion
    #region Rope
    public void ShootRope()
    {
        _player.GetCompo<GrappleGun>().SetGrapplePoint();
    }
    public void ShootRope(Vector2 direction)
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction.normalized,100);
        _player.GetCompo<GrappleGun>().SetGrapplePoint(ray);
    }
    public void EscapeRope()
    {
        _player.GetCompo<GrappleGun>().EscapeGrapple();
        isRope = false;
    }
    #endregion

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (_groundCheckerTrm == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_groundCheckerTrm.position, _groundCheckerSize);
        Gizmos.color = Color.white;
    }

#endif
}
