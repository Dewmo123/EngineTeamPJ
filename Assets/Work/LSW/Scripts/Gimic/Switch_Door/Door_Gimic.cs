using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door_Gimic : MonoBehaviour
{
    [SerializeField] private MainGimicScript _mainGimicScript;

    [SerializeField] private GameObject _door;
    [SerializeField] private float _time;
    [SerializeField] private Transform _goalPos;
    private Vector2 _origin;
    private Vector2 originPos;

    private Animator buttonAnimator;

    //private AudioSource _audioSource;
    //public AudioClip _switchSound, _doorOpenSound, _doorCloseSound;

    private bool IsOpen;
    private void Awake()
    {
        _origin = _door.transform.position;
        buttonAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _mainGimicScript.AddListener(TryOpen);
    }

    private void Start()
    {
        originPos = _door.transform.position;
        //_audioSource = GetComponent<AudioSource>();
    }

    private void TryOpen()
    {
        if(!IsOpen)
            Open();
        else
            Close();
    }

    private void Open()
    {
        //_audioSource.PlayOneShot(_switchSound);
        //_audioSource.PlayOneShot(_doorOpenSound);

        _door.transform.DOMove(_goalPos.position,_time);
        IsOpen = true;
        buttonAnimator.SetBool("Click", true);
    }

    private void Close()
    {
        //_audioSource.PlayOneShot(_doorCloseSound);
        _door.transform.DOMove(_origin, _time);
        IsOpen = false;
        buttonAnimator.SetBool("Click", false);
    }

    private void OnDisable()
    {
        _mainGimicScript.RemoveListener(TryOpen);
    }
}
