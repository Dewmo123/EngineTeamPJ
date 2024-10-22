using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    [SerializeField] private MainGimicScript _mainGimicScript;
    [SerializeField] private GameObject _enemy_View;
    [SerializeField] private LayerMask _enemyLayer ,_groundLayer;
    public float _usingTime;

    private void OnEnable()
    {
        _mainGimicScript.OnActive_Trans += OnTransparent;
    }

    private void OnTransparent()
    {
        float time = Time.deltaTime / _usingTime;
        while(time != 1)
        {
            Physics.IgnoreLayerCollision(_enemy_View.layer, (int)_groundLayer, false);      
            Physics.IgnoreLayerCollision(_enemy_View.layer, (int)_enemyLayer, true);    //�߰��� ��� �ϳĿ� ���� �޶���
        }
    }

    private void OnDisable()
    {
        _mainGimicScript.OnActive_Trans -= OnTransparent;
    }
}
