using GGMPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFeedback : Feedback
{
    [SerializeField] private SoundSO _sound; 
    
    public override void PlayFeedback()
    {
        Debug.Log(GameManager.Instance);
        PoolingItemSO item = GameManager.Instance.poolItemDic["SoundPlayer"];
        PoolManagerSO poolManager = GameManager.Instance.poolManager;
        SoundPlayer soundPlayer = poolManager.Pop(item.poolType) as SoundPlayer;
        soundPlayer.PlaySound(_sound);
    }

    public override void StopFeedback()
    {
    }
}
