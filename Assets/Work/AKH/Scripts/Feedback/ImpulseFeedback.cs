using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseFeedback : Feedback,IPlayerComponent
{
    private Player _player;
    private CinemachineImpulseSource _source;
    [SerializeField] private float _multiplier;

    private void Awake()
    {
        _source = GetComponent<CinemachineImpulseSource>();
    }
    public override void PlayFeedback()
    {
        _source.GenerateImpulse(_player.jointCompo.distance* _multiplier);
    }

    public override void StopFeedback()
    {
    }

    public void Initialize(Player player)
    {
        _player = player;
    }
}
