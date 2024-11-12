using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(PlayerStateMachine stateMachine, string animName, GamePlayer player) : base(stateMachine, animName, player)
    {
    }
    public override void UpdateState()
    {
        base.UpdateState();
        _player.rbCompo.velocity = Vector2.zero;
        if (_endTriggerCalled)
            _player.playerDeadEvent?.Invoke();
    }
}
