using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.iOS;

public enum MyGimic { Door, Call, Light }
public class MainGimicScript : MonoBehaviour
{
    public Action OnActive_Door, OnActive_Call, OnActive_Light;
    public MyGimic myGimic;

    public void UseGimic()
    {
        switch (myGimic)
        {
            case MyGimic.Door:
                OnActive_Door?.Invoke();
                break;

            case MyGimic.Call:
                OnActive_Call?.Invoke();
                break;

            case MyGimic.Light:
                OnActive_Light?.Invoke();
                break;
        }

    }

}
