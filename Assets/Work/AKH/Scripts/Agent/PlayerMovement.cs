using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour,IPlayerComponent
{
    private Player _player;
    public void AcceptMovement(Vector2 move)
    {
        _player.rbCompo.velocity = move;
    }

    public void Initialize(Player player)
    {
        _player = player;
    }
}
