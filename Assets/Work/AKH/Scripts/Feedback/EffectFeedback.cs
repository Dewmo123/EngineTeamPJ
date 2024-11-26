using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFeedback : Feedback
{
    [SerializeField] private string _itemName;
    public override void PlayFeedback()
    {
        var s = GameManager.Instance.poolManager.Pop(GameManager.Instance.poolItemDic[_itemName].poolType) as Smoke;
        s.Play(transform);
    }

    public override void StopFeedback()
    {
    }
}
