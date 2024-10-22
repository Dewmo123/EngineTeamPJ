using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.iOS;

public enum MyGimic { Door, Call, Light, Trans }
public class MainGimicScript : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] MonoBehaviour _myGimicScript;
    public Action OnActive_door, OnActive_Call, OnActive_Light, OnActive_Trans;
    public MyGimic myGimic;
    [SerializeField] private Player_UseGimic _playerUseGimic;

    public void UseGimic()
    {
        if(_playerUseGimic.canGimic)
        {
            switch (myGimic)
            {
                case MyGimic.Door:
                    OnActive_door?.Invoke();
                    break;

                case MyGimic.Call:
                    OnActive_Call?.Invoke();
                    break;

                case MyGimic.Light:
                    OnActive_Light?.Invoke();
                    break;

                case MyGimic.Trans:
                    OnActive_Trans?.Invoke();
                    break;
            }
        }

    }

}
