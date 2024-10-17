using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private Sprite _openSpr;
    public bool _canOpen;
    private AudioSource _audioSource;
    public AudioClip _switchSound, _doorSound;
    private MainGimicScript _gimic;

    private void OnEnable()
    {
        _gimic = GetComponent<MainGimicScript>();
        _gimic.OnActive_door += Open;
    }

    private void Start()
    {
        _door.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    private void Open()
    {
        _door.GetComponent<BoxCollider2D>().isTrigger = true;
        _door.GetComponent<SpriteRenderer>().sprite = _openSpr;
        _gimic.OnActive_door -= Open;
        _audioSource.GetComponent<AudioSource>().PlayOneShot(_switchSound);
        _door.GetComponent<AudioSource>().PlayOneShot(_doorSound);
    }

    private void OnDisable()
    {
        _gimic.OnActive_door -= Open;
    }
}
