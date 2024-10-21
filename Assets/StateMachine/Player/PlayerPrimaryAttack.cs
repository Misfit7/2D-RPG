using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    public PlayerPrimaryAttack(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isAttacking = true;
        player.anim.SetInteger("comboCounter", player.comboCounter);

        float attackDir = player.facingDir;
        if (xInput != 0)
            attackDir = xInput;
        player.SetVelocity(xInput * player.rb.velocity.x, player.rb.velocity.y * 0.5f);
    }

    public override void Exit()
    {
        base.Exit();
        player.comboCounter++;
        if (player.comboCounter > 2) player.comboCounter = 0;
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(xInput * player.moveSpeed * 0.5f, player.rb.velocity.y);
        if (!player.isAttacking && player.rb.velocity.y != 0)
            stateMachine.ChangeState(player.airState);
        else if (!player.isAttacking)
            stateMachine.ChangeState(player.idleState);
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGoundedDetected())
            player.SetVelocity(player.rb.velocity.x, player.jumpForce);
    }
}
