using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveUI : MonoBehaviour
{
    private RectTransform rTransform;
    [SerializeField]private float _duration;
    private void Awake()
    {
        rTransform = GetComponent<RectTransform>();
    }
    public void Move(Vector2 targetPos, WaitForSeconds wait = null)
    {
        if (wait != null)
            StartCoroutine(Wait(targetPos,wait));
        else
            rTransform.DOAnchorPos(targetPos, _duration);
    }
    private IEnumerator Wait(Vector2 targetPos,WaitForSeconds wait)
    {
        yield return wait;
        rTransform.DOAnchorPos(targetPos, _duration);
    }
}
