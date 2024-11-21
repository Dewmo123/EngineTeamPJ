using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    private Monster _monster;
    private void Awake()
    {
        _monster = GetComponentInParent<Monster>();
    }
    public void AnimationEndTrigger()
    {
        _monster.AnimationEndTrigger();
    }
    public void PlayWalkFeedback()
    {
        _monster.Walk();
    }
}
