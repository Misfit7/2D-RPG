using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player.IsGoundedDetected())
            stateMachine.ChangeState(player.idleState);

        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, player.rb.velocity.y);
    }
}
