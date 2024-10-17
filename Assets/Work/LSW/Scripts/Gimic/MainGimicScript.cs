using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.iOS;

public enum MyGimic { Door, Call }
public class MainGimicScript : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] MonoBehaviour _myGimicScript;
    public Action OnActive_door, OnActive_Call;
    public MyGimic myGimic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("t");
        if ((collision.gameObject.layer & (1 << _playerLayer)) != 0)
        {
            Debug.Log("l");
            switch (myGimic)
            {
                case MyGimic.Door:
                    TryGimic(MyGimic.Door);
                    break;

                case MyGimic.Call:
                    TryGimic(MyGimic.Call);
                    break;
            }
        }
    }

    public void TryGimic(MyGimic _myGimic)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (myGimic)
            {
                case MyGimic.Door:
                    OnActive_door?.Invoke();
                    break;

                case MyGimic.Call:
                    OnActive_Call?.Invoke();
                    break;
            }
        }
    }

}
