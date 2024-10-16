using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDoor : MonoBehaviour
{
    public Action _Open;
    public LayerMask _switchLayer;
    private bool _canOpening;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _switchLayer) != 0)
            _canOpening = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _switchLayer) != 0)
            _canOpening = false;
    }

    private void Update()
    {
        if (_canOpening)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _Open?.Invoke();
            }
        }
    }
}
