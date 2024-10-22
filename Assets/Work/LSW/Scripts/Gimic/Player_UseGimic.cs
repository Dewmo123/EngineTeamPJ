using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_UseGimic : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    public LayerMask _gimicLayer;
    public bool canGimic = false;
    public UnityEvent OnGimic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer & (1 << _gimicLayer)) != 0)
        {
            _inputReader.GimicEvent += collision.GetComponent<MainGimicScript>().UseGimic;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer & (1 << _gimicLayer)) != 0)
        {
            _inputReader.GimicEvent -= collision.GetComponent<MainGimicScript>().UseGimic;
        }
    }
}
