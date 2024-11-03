using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Light : MonoBehaviour
{
    [SerializeField] private MainGimicScript _mainGimicScript;
    [SerializeField] private GameObject _dark, _player;
    [SerializeField] private LayerMask _playerOriginLayer, _playerTransLayer;
    private bool _onDark;

    private void OnEnable()
    {
        _mainGimicScript.OnActive_Light += OnDarkLight;
    }

    private void OnDarkLight()
    {
        if(!_onDark)
        {
            _dark.SetActive(true);
            _onDark = true;
            _player.layer = (int)Mathf.Log(_playerTransLayer.value, 2);
            
        }
        else if(_onDark)
        {
            _dark.SetActive(false);
            _onDark = false;
            _player.layer = (int)Mathf.Log(_playerOriginLayer.value, 2);

        }
    }

    private void OnDisable()
    {
        _mainGimicScript.OnActive_Light -= OnDarkLight;
    }
}
