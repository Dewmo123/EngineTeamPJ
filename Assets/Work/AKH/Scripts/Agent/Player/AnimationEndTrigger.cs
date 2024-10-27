using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEndTrigger : MonoBehaviour, IAgentComponent
{
    private Player _player;
    public void EndTriggerCall()
    {
        _player.EndTriggerCalled();
    }
    public void Initialize(Player player)
    {
        _player = player;
    }
}
