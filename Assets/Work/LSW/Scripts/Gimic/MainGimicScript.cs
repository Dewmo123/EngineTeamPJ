using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainGimicScript : MonoBehaviour, IGimic
{
    private Action useGimic;
    [SerializeField] private Player_UseGimic _playerUseGimic;

    public Action useGimicEvent => useGimic;

    public void UseGimic()
    {
        useGimic?.Invoke();
    }
    public void AddListener(Action action)
    {
        useGimic += action;
    }
    public void RemoveListener(Action action)
    {
        useGimic -= action;
    }
}
