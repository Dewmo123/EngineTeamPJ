using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Esc : MonoBehaviour, IPlayerComponent
{
    private Player _player;
    [SerializeField] private MoveUIManager _escUI;
    private int cnt = 0;
    private void Start()
    {
        _player.GetCompo<InputReader>().EscEvent += ShowEsc;
    }
    private void ShowEsc()
    {
        cnt++;
        if (cnt == 1)
        {
            _escUI.MoveTargetPos();
            StartCoroutine(WaitMove());
        }
        else if (cnt == 2)
        {
            Time.timeScale = 1;
            _escUI.MoveOriginPos();
            cnt = 0;
        }
    }

    private IEnumerator WaitMove()
    {
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0;
    }

    public void Initialize(Player player)
    {
        _player = player;
    }
    public void Close()
    {
        ShowEsc();
    }
}
