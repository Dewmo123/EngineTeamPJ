using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door_Gimic : MonoBehaviour
{
    [SerializeField] private MainGimicScript _mainGimicScript;

    [SerializeField] private GameObject _door;
    [SerializeField] private float _doorSpeed, _IsclosingTime;
    [SerializeField] private Transform _goalPos;
    private Vector2 originPos;

    //private AudioSource _audioSource;
    //public AudioClip _switchSound, _doorOpenSound, _doorCloseSound;

    private bool IsOpen;

    private void OnEnable()
    {
        _mainGimicScript.OnActive_door += TryOpen;
    }

    private void Start()
    {
        originPos = _door.transform.position;
        //_audioSource = GetComponent<AudioSource>();
    }

    private void TryOpen()
    {
        if(!IsOpen)
            StartCoroutine(Open());
        else
            StartCoroutine(Close());
    }

    private IEnumerator Open()
    {
        //_audioSource.PlayOneShot(_switchSound);
        //_audioSource.PlayOneShot(_doorOpenSound);

        float time = Time.deltaTime / _doorSpeed;
        while(time >= 1)
            _door.transform.position = Vector2.Lerp(originPos, _goalPos.position, time);

        IsOpen = true;
        yield return new WaitForSeconds(_IsclosingTime);
    }

    private IEnumerator Close()
    {
        //_audioSource.PlayOneShot(_doorCloseSound);

        float time = Time.deltaTime / _doorSpeed;
        while (time >= 1)
            _door.transform.position = Vector2.Lerp(_goalPos.position, originPos, time);

        IsOpen = false;
        yield return null;
    }

    private void OnDisable()
    {
        _mainGimicScript.OnActive_door -= TryOpen;
    }
}
