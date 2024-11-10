using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAlphaFeedback : Feedback
{
    [SerializeField] private float _alpha;
    private Player _agent;
    private void Awake()
    {
        _agent = GetComponentInParent<Player>();
    }
    public override void PlayFeedback()
    {
        Color color = _agent.rendererCompo.color;
        color.a = _alpha;
        _agent.rendererCompo.color = color;
    }

    public override void StopFeedback()
    {
    }
}
