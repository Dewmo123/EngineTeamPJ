using GGMPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFeedback : Feedback,IPlayerComponent
{
    [SerializeField] private PoolTypeSO _particleType;
    [SerializeField] private PoolManagerSO _pool;
    private Player _player;
    public void Initialize(Player player)
    {
        _player = player;
    }

    public override void PlayFeedback()
    {
        IPoolable item = _pool.Pop(_particleType);
    }

    public override void StopFeedback()
    {
    }
}
