using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerStateMachine stateMachine, string animName, GamePlayer player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _player.rbCompo.velocity = Vector2.zero;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
            if (_player.GetCompo<InputReader>().Movement != Vector2.zero)
                _stateMachine.ChangeState(PlayerEnum.Walk);
            else
                _stateMachine.ChangeState(PlayerEnum.Idle);
    }
}
