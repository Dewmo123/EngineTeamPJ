using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door_Gimic : MonoBehaviour
{
    [SerializeField] private MainGimicScript _mainGimicScript;

    [SerializeField] private GameObject _door;
    [SerializeField] private float _doorSpeed;
    [SerializeField] private Transform _goalPos;
    private Vector2 originPos;

    //private AudioSource _audioSource;
    //public AudioClip _switchSound, _doorOpenSound, _doorCloseSound;

    private bool isOpening = false;

    private void OnEnable()
    {
        _mainGimicScript.OnActive_Door += OpenDoor;
    }


    void Update()
    {
        if (isOpening)
        {
            _door.transform.position = Vector2.Lerp(_door.transform.position, _goalPos.position, Time.deltaTime * _doorSpeed);

            if (Vector3.Distance(_door.transform.position, _goalPos.position) < 0.01f)
            {
                _door.transform.position = _goalPos.position;
                isOpening = false;
            }
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
    }

    private void OnDisable()
    {
        _mainGimicScript.OnActive_Door -= OpenDoor;
    }
}
