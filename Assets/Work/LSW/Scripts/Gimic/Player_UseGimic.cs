using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_UseGimic : MonoBehaviour
{
    public LayerMask _gimicLayer;
    public bool canGimic;
    private GameObject _nowGimic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_gimicLayer == (_gimicLayer | (1 << collision.gameObject.layer)))
        {
            canGimic = true;
            _nowGimic = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_gimicLayer == (_gimicLayer | (1 << collision.gameObject.layer)))
        {
            canGimic = false;
        }
    }

    private void Update()
    {
        if(canGimic && Input.GetKeyDown(KeyCode.F))
        {
            _nowGimic.GetComponent<MainGimicScript>().UseGimic();
        }
    }
}
