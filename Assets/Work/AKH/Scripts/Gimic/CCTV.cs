using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public struct ViewCastInfo
{
    public bool isHit;
    public Vector3 point;
    public float distance;
    public float angle;
}

public struct EdgeInfo
{
    public Vector3 pointA;
    public Vector3 pointB;
}

public class CCTV : MonoBehaviour
{
    [SerializeField] private Transform _weaponHolder;

    [Header("Sight info")]
    [Range(0, 360f)] public float viewAngle;
    [Range(1, 12f)] public float viewRadius;
    public Vector3 HolderPosition => _weaponHolder.position;

    [Header("RayCast info")]
    [SerializeField] private ContactFilter2D _enemyFilter;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _enemyFindDelay = 0.3f;
    [SerializeField] private int _maxCheckCount = 10;
    [Range(0.05f, 2f)] [SerializeField] private float _meshResolution; //메시의 해상도
    [SerializeField] private int _edgeIterationCount = 4; //몇번 반복해서 이분할 탐색할지
    [SerializeField] private float _edgeThreshold; //임계지점

    [SerializeField] private Light2D _fovLight;

    public event Action<float> OnWeaponHolderRotate;

    public List<Transform> visibleTargets = new List<Transform>();
    private Collider2D[] _enemiesInView;

    private MeshFilter _meshFilter;
    private Mesh _viewMesh;

    private Transform _viewVisual;

    public void Initialize()
    {
        _enemiesInView = new Collider2D[_maxCheckCount];

        _viewVisual = transform.Find("ViewVisual");
        _meshFilter = _viewVisual.GetComponent<MeshFilter>();
        _viewMesh = new Mesh();
        _viewMesh.name = "View Mesh";
        _meshFilter.mesh = _viewMesh;

        MeshRenderer renderer = _viewVisual.GetComponent<MeshRenderer>();
        renderer.sortingLayerName = "Agent";
        renderer.sortingOrder = 20;
    }

    [ContextMenu("Adjust light angle")]
    private void AdjustLightAngle()
    {
        _fovLight.pointLightOuterRadius = viewRadius;
        _fovLight.pointLightInnerRadius = viewRadius - 1f;
        _fovLight.pointLightOuterAngle = viewAngle;
        _fovLight.pointLightInnerAngle = viewAngle - viewAngle * 0.2f;
    }

    private void Start()
    {
        Initialize();
        StartCoroutine(FindEnemyWithDelay());
    }

    #region FindEnemy Region
    private IEnumerator FindEnemyWithDelay()
    {
        var time = new WaitForSeconds(_enemyFindDelay);
        while (true)
        {
            yield return time;
            FindVisibleEnemies();
        }
    }

    private void FindVisibleEnemies()
    {
        visibleTargets.Clear();

        int cnt = Physics2D.OverlapCircle(HolderPosition, viewRadius, _enemyFilter, _enemiesInView);

        for (int i = 0; i < cnt; i++)
        {
            Transform enemy = _enemiesInView[i].transform;
            Vector3 direction = enemy.position - HolderPosition;

            //시야범위안에 있다.
            if (Vector2.Angle(_weaponHolder.right, direction.normalized) < viewAngle * 0.5f)
            {
                if (!Physics2D.Raycast(HolderPosition, direction.normalized, direction.magnitude, _obstacleMask))
                {
                    visibleTargets.Add(enemy);
                }
            }
        }
    }
    #endregion

    private void Update()
    {
        UpdateAim();
    }

    private void LateUpdate()
    {
        DrawFieldOfView();
    }

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * _meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        List<Vector3> viewPoints = new List<Vector3>();

        ViewCastInfo oldCastInfo = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = _weaponHolder.eulerAngles.z + viewAngle * 0.5f - stepAngleSize * i;

            ViewCastInfo castInfo = ViewCast(angle);

            if (i > 0)
            {
                bool edgeThresholdExceed =
                    Mathf.Abs(oldCastInfo.distance - castInfo.distance) > _edgeThreshold;

                if (oldCastInfo.isHit != castInfo.isHit ||
                    (oldCastInfo.isHit && castInfo.isHit && edgeThresholdExceed))
                {
                    EdgeInfo edge = FindEdge(oldCastInfo, castInfo);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }

                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(castInfo.point);

            oldCastInfo = castInfo; //이거 빼먹으면 절대 안돌아간다.
        }

        int vertCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertCount];
        int[] triangles = new int[(vertCount - 2) * 3];
        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertCount - 1; i++)
        {
            vertices[i + 1] = _viewVisual.InverseTransformPoint(viewPoints[i]);

            if (i < vertCount - 2)
            {
                int tIndex = i * 3;
                triangles[tIndex + 0] = 0;
                triangles[tIndex + 1] = i + 1;
                triangles[tIndex + 2] = i + 2;
            }
        }

        _viewMesh.Clear();
        _viewMesh.vertices = vertices;
        _viewMesh.triangles = triangles;
        _viewMesh.RecalculateNormals();
    }

    private EdgeInfo FindEdge(ViewCastInfo minCastInfo, ViewCastInfo maxCastInfo)
    {
        float minAngle = minCastInfo.angle;
        float maxAngle = maxCastInfo.angle;

        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < _edgeIterationCount; i++)
        {
            float angle = (minAngle + maxAngle) * 0.5f; //중간 각도 구해내고
            var castInfo = ViewCast(angle); //중간점으로 다시한번 캐스트

            bool edgeThresholdExceed
                = Mathf.Abs(minCastInfo.distance - castInfo.distance) > _edgeThreshold;

            if (castInfo.isHit == minCastInfo.isHit &&
                edgeThresholdExceed == false)
            {
                minAngle = angle;
                minPoint = castInfo.point;
            }
            {
                maxAngle = angle;
                maxPoint = castInfo.point;
            }
        }
        return new EdgeInfo { pointA = minPoint, pointB = maxPoint };
    }

    private ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 direction = DirectionFromAngle(globalAngle, true);

        var hitInfo = Physics2D.Raycast(HolderPosition, direction, viewRadius, _obstacleMask);

        if (hitInfo.collider != null)
        {
            return new ViewCastInfo
            {
                isHit = true,
                point = hitInfo.point,
                angle = globalAngle,
                distance = hitInfo.distance
            };
        }
        else
        {
            return new ViewCastInfo
            {
                isHit = false,
                point = HolderPosition + direction * viewRadius,
                angle = globalAngle,
                distance = viewRadius
            };
        }

    }
    float a;
    private void UpdateAim()
    {
        Vector3 worldPos = new Vector3(Mathf.Cos(a*Mathf.Deg2Rad),Mathf.Sin(a*Mathf.Deg2Rad),0);
        a += 3;
        if (a >= 360) a = 0;
        _weaponHolder.right = worldPos.normalized;

        OnWeaponHolderRotate?.Invoke(_weaponHolder.eulerAngles.z);
    }

    public Vector3 DirectionFromAngle(float degree, bool isGlobalAngle)
    {
        if (!isGlobalAngle)
        {
            degree += _weaponHolder.eulerAngles.z;
            //만약 글로벌 앵글이 아니면 내 회전치를 추가해서 글로벌 앵글로 만들어준다.
        }

        float rad = degree * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
    }

}
