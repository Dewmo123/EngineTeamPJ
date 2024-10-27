using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseFeedback : Feedback
{
    private CinemachineImpulseSource _source;
    [SerializeField] private float _value;

    private void Awake()
    {
        _source = GetComponent<CinemachineImpulseSource>();
    }
    public override void PlayFeedback()
    {
        _source.GenerateImpulse(_value);
    }

    public override void StopFeedback()
    {
    }
}
