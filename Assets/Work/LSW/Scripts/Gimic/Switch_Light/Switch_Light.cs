using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Light : MonoBehaviour
{
    [SerializeField] private MainGimicScript _mainGimicScript;

    private void OnEnable()
    {
        _mainGimicScript.OnActive_Light += OnDarkLight;
    }

    private void OnDarkLight()
    {
        
    }

    private void OnDisable()
    {
        _mainGimicScript.OnActive_Light -= OnDarkLight;
    }
}
