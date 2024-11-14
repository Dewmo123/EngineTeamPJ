using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEndTrigger : MonoBehaviour, IPlayerComponent
{
    private GamePlayer _player;
    public UnityEvent onWalkEvent;
    public void EndTriggerCall()
    {
        _player.EndTriggerCalled();
    }
    public void PlayWalkSound()
    {
        onWalkEvent?.Invoke();
    }
    public void Initialize(Player player)
    {
        _player = player as GamePlayer;
    }
}
