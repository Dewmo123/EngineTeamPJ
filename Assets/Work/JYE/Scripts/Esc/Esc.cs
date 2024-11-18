using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Esc : MonoBehaviour,IPlayerComponent
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
        if (cnt == 1)
            _escUI.MoveTargetPos();
        else if (cnt == 2)
        {
            _escUI.MoveOriginPos();
            cnt = 0;
        }
    }

    public void Initialize(Player player)
    {
        _player = player;
    }
}
