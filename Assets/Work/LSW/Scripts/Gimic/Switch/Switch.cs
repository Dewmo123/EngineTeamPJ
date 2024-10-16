using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private Sprite _openSpr;
    [SerializeField] private PlayerDoor _playerDoor;
    public bool _canOpen;

    private void OnEnable()
    {
        _playerDoor._Open += Open;
    }

    private void Start()
    {
        _door.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    private void Open()
    {
        _door.GetComponent<BoxCollider2D>().isTrigger = true;
        _door.GetComponent<SpriteRenderer>().sprite = _openSpr;
        _playerDoor._Open -= Open;
    }

    private void OnDisable()
    {
        _playerDoor._Open -= Open;
    }
}
