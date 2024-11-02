using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeCalledPoint : MonoBehaviour
{
    [SerializeField] private MainGimicScript _mainGimicScript;

    private bool moveToTarget = false;

    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private GameObject[] _enemy;
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private float _disableTime;

    private Transform[] OriginmovePoint;

    private void OnEnable()
    {
        _mainGimicScript.OnActive_Call += Call;
        OriginmovePoint = new Transform[_enemy.Length];
    }

    private void Call()
    {
        moveToTarget = true;
        if (moveToTarget)
        {
            for (int i = 0; i < _enemy.Length; i++)
            {
                OriginmovePoint[i] = _enemy[i].transform;
                _enemy[i].GetComponent<Enemy>().GoToPoint(transform);
            }
        }
    }

    //void Update()
    //{
    //    if (moveToTarget)
    //    {
    //        for (int i = 0; i < _enemy.Length; i++)
    //        {
    //            _enemy[i].transform.position = Vector2.Lerp(_enemy[i].transform.position, transform.position, enemySO.speed);
    //        }
    //        Invoke("Disable", _disableTime);
    //    }
    //}

    private void Disable()
    {
        //for (int i = 0; i < _enemy.Length; i++)
        //{
        //    _enemy[i].transform.position = Vector2.Lerp(_enemy[i].transform.position, _enemy[i].transform.position, enemySO.speed);
        //}
        moveToTarget = false;
    }

    private void OnDisable()
    {
        _mainGimicScript.OnActive_Call -= Call;
    }
}
