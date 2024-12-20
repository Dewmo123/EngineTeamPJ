using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeCalledPoint : MonoBehaviour
{
    [SerializeField] private MainGimicScript _mainGimicScript;

    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _disableTime;

    private void OnEnable()
    {
        _mainGimicScript.AddListener(Call);
    }

    private void Call()
    {
        _enemy.GoToPoint(transform);
        StartCoroutine(WaitArrive());
    }

    private IEnumerator WaitArrive()
    {
        _mainGimicScript.RemoveListener(Call);
        yield return new WaitForSeconds(_enemy.moveDuraion * 2.5f);
        _mainGimicScript.AddListener(Call);
    }

    private void OnDisable()
    {
        if (_mainGimicScript.useGimicEvent != null)
            _mainGimicScript.RemoveListener(Call);
    }
}
