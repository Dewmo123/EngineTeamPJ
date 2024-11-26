using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class GrappleGun : MonoBehaviour, IPlayerComponent
{
    private Player _player;

    private Rope _grappleRope;


    [Header("Layers Settings:")]
    [SerializeField] private int _enemyLayer;
    [SerializeField] private LayerMask _canRopeLayer;

    private Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    private SpringJoint2D _springJoint2D;
    private Rigidbody2D _rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)][SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

    [Header("Launching:")]
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;
    private Collider2D _connectedCol;
    private void Start()
    {
        m_camera = Camera.main;
    }


    public void Roping()
    {
        if (_grappleRope.enabled)
        {
            RotateGun(grapplePoint, false);
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);
        }

    }
    public void Launch()
    {
        if (_grappleRope.isGrappling&&!_player.movementCompo.isGround.Value)
        {
            StopLaunch();
            Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
            Vector2 targetPos = grapplePoint - firePointDistnace;
            gunHolder.transform.DOMove(targetPos,0.5f);
        }
    }
    public void StopLaunch()
    {
        gunHolder.transform.DOKill();
    }
    public void EscapeGrapple()
    {
        _grappleRope.enabled = false;
        _springJoint2D.enabled = false;
        _rigidbody.gravityScale = 1;
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    public void GoToPoint()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, (grapplePoint - (Vector2)transform.position).normalized, 1000, _canRopeLayer);
        SetGrapplePoint(ray);
        Launch();
    }
    public void SetGrapplePoint()
    {
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized, 1000, _canRopeLayer);
        if (hit)
        {
            if (Vector2.Distance(hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
            {
                grapplePoint = hit.point;
                _player.movementCompo.isRope = true;
                grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                _grappleRope.enabled = true;
                _connectedCol = hit.collider;
                if (hit.collider.gameObject.layer == _enemyLayer)
                {
                    hit.collider.GetComponent<Enemy>().Hit();
                    Launch();
                }
            }
        }
    }
    public void SetGrapplePoint(RaycastHit2D ray)
    {
        grapplePoint = ray.point;
        _player.movementCompo.isRope = true;
        grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
        _grappleRope.enabled = true;
        _connectedCol = ray.collider;
        if (ray.collider.gameObject.layer == _enemyLayer)
        {
            ray.collider.GetComponent<Enemy>().Hit();
            Launch();
        }
    }

    public void Grapple()
    {
        _springJoint2D.autoConfigureDistance = false;
        if (!autoConfigureDistance)
        {
            _springJoint2D.distance = targetDistance;
            _springJoint2D.frequency = targetFrequncy;
        }
            if (autoConfigureDistance)
            {
                _springJoint2D.autoConfigureDistance = true;
                _springJoint2D.frequency = 0;
            }
            _springJoint2D.connectedAnchor = grapplePoint;
            _springJoint2D.enabled = true;
        _player.GrappleEvent?.Invoke();
        if (_springJoint2D.distance > _player.movementCompo.maxDistance)
        {
            _springJoint2D.distance = _player.movementCompo.maxDistance;
        }
        //_player.rbCompo.AddForce(_player.rbCompo.velocity.normalized*_springJoint2D.distance, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }

    public void Initialize(Player player)
    {
        _player = player;
        _grappleRope = _player.GetCompo<Rope>();
        _rigidbody = _player.rbCompo;
        _springJoint2D = _player.jointCompo;
        _springJoint2D.enabled = false;
    }
}
