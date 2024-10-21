using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    private Transform sword;
    public PlayerCatchSwordState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        sword = player.sword.transform;
        if (sword.position.x < player.transform.position.x && player.facingDir > 0)
            player.CharacterFlip();
        else if (sword.position.x > player.transform.position.x && player.facingDir < 0)
            player.CharacterFlip();

        player.rb.velocity = new Vector2(5 * -player.facingDir, player.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        player.isCatchSword = false;
    }

    public override void Update()
    {
        base.Update();
        if (player.isCatchSword)
            stateMachine.ChangeState(player.idleState);
    }
}
