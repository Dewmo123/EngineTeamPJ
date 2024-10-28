using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeCalledPoint : MonoBehaviour
{
    [SerializeField] private MainGimicScript _mainGimicScript;

    private Vector3 originPos;
    private bool moveToTarget = false;
    private bool moveToBack = false;

    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private GameObject[] _enemy;
    [SerializeField] private Transform[] _backPos;
    [SerializeField] private EnemySO enemySO;

    private void OnEnable()
    {
        _mainGimicScript.OnActive_Call += Call;
    }

    private void Call()
    {
        moveToTarget = true;
    }

    void Update()
    {
        if (moveToTarget)
        {
            for(int i = 0; i < _enemy.Length; i++)
            {
                _enemy[i].transform.position = Vector2.MoveTowards(_enemy[i].transform.position, transform.position, enemySO.speed * Time.deltaTime);
            }
        }

        if(moveToBack && !moveToTarget)
        {
            for (int i = 0; i < _enemy.Length; i++)
            {
                _enemy[i].transform.position = Vector2.MoveTowards(_enemy[i].transform.position, _backPos[i].transform.position, enemySO.speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_enemyLayer == (_enemyLayer | (1 << collision.gameObject.layer)))
        {
            moveToTarget = false;
            moveToBack = true;
        }
    }

    private void OnDisable()
    {
        _mainGimicScript.OnActive_Call -= Call;
    }
}
