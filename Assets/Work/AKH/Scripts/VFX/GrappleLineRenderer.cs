using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleLineRenderer : MonoBehaviour, IPlayerComponent
{
    private LineRenderer _lineRenderer;
    private Player _player;
    private InputReader _input;
    [SerializeField] private LayerMask _canRopeLayer;
    public void Initialize(Player player)
    {
        _player = player;
        _input = player.GetCompo<InputReader>();
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(_input.Mouse);
        RaycastHit2D ray = Physics2D.Raycast(transform.position, (Vector3)mousePos - transform.position, 1000, _canRopeLayer);
        if (ray)
        {
            _lineRenderer.enabled = true;
            Vector3[] positions = new Vector3[2] { transform.position, ray.point };
            _lineRenderer.SetPositions(positions);
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}
