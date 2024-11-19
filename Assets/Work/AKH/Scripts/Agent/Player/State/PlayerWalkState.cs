using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerCanRopeState,IAttackableState
{
    public PlayerWalkState(PlayerStateMachine stateMachine, string animName, GamePlayer player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _player.GetCompo<InputReader>().AttackEvent += HandleAttack;
    }
    public void HandleAttack()
    {
        _stateMachine.ChangeState(PlayerEnum.Attack);
    }
    public override void Exit()
    {
        base.Exit();
        _player.GetCompo<InputReader>().AttackEvent -= HandleAttack;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (_input.Movement == Vector2.zero)
            _stateMachine.ChangeState(PlayerEnum.Idle);
    }


    protected override void HandleJumpEvent()
    {
        _stateMachine.ChangeState(PlayerEnum.Jump);
    }
}
