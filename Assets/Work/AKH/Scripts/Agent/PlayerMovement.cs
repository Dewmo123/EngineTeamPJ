using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerComponent
{
    private Player _player;

    [SerializeField] private Transform _groundCheckerTrm;

    [Header("Settings")]
    public float moveSpeed = 5f;
    public float jumpPower = 7f;
    public float extraGravity = 30f;
    public float gravityDelay = 0.15f;
    public float knockbackTime = 0.2f;

    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Vector2 _groundCheckerSize;

    public NotifyValue<bool> isGround = new NotifyValue<bool>();
    public bool isRope = false;
    private RaycastHit2D _ray;
    private Vector2 _point;

    private float _timeInAir;
    protected bool _canMove = true;
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
        _player.jointCompo.distance = Vector2.Distance(transform.position, _player.jointCompo.connectedAnchor);
        CheckGrounded();
        ApplyExtraGravity();
    }
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

    public void ShootRope()
    {
        _player.GetCompo<GrappleGun>().SetGrapplePoint();
        isRope = true;
    }
    public void EscapeRope()
    {
        isRope = false;
        _point = Vector2.zero;
        _player.jointCompo.connectedAnchor = _point;
        _player.jointCompo.enabled = false;
    }


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
