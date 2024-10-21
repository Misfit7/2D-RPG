using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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
        //Debug.Log(player.isGrounded);
        if (!player.IsGoundedDetected()) stateMachine.ChangeState(player.airState);

        if (Input.GetButtonDown("Jump") && player.IsGoundedDetected())
            stateMachine.ChangeState(player.jumpState);
    }
}
