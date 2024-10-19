using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLineRenderer : MonoBehaviour, IPlayerComponent
{
    private InputReader _input;
    private LineRenderer _lineRenderer;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }


    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(_input.Mouse);
        Vector3[] positions = new Vector3[2] { transform.position,  (Vector3)mousePos};
        _lineRenderer.SetPositions(positions);
    }
    public void Initialize(Player player)
    {
        _input = player.GetCompo<InputReader>();
    }
}
