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
        Vector3[] positions = new Vector3[2] { transform.position, Camera.main.ScreenToWorldPoint(_input.Mouse) };
        _lineRenderer.SetPositions(positions);
    }
    public void Initialize(Player player)
    {
        _input = player.GetCompo<InputReader>();
    }
}
