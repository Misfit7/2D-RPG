using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.clone.CreateClone(player.transform);

        stateTimer = player.skill.dash.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, player.rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashDir * player.moveSpeed * player.skill.dash.dashForce, 0);
        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
