using UnityEngine;

public class GrappleGun : MonoBehaviour, IPlayerComponent
{
    private Player _player;

    private Rope _grappleRope;


    [Header("Layers Settings:")]
    [SerializeField] private int _enemyLayer;
    [SerializeField] private LayerMask _canRopeLayer;

    [Header("Main Camera:")]
    public Camera m_camera;

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

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    public bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;
    private void Start()
    {
        _grappleRope.enabled = false;
        _springJoint2D.enabled = false;
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
        if (launchToPoint && _grappleRope.isGrappling)
        {
            if (launchType == LaunchType.Transform_Launch)
            {
                Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                Vector2 targetPos = grapplePoint - firePointDistnace;
                gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
            }
        }
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
                launchToPoint = false;
                grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                _grappleRope.enabled = true;
                if (hit.collider.gameObject.layer == _enemyLayer) launchToPoint = true;
            }
        }
    }

    public void Grapple()
    {
        _springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            _springJoint2D.distance = targetDistance;
            _springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                _springJoint2D.autoConfigureDistance = true;
                _springJoint2D.frequency = 0;
            }
            _springJoint2D.connectedAnchor = grapplePoint;
            _springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    _springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    _springJoint2D.distance = distanceVector.magnitude;
                    _springJoint2D.frequency = launchSpeed;
                    _springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    _rigidbody.gravityScale = 0;
                    _rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
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
    }
}
