using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected Enemy_Skeleton enemy;
    protected Transform player;

    public SkeletonGroundedState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.isPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) <= enemy.playerCheckDistance * 0.5f)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
        if (!enemy.IsGoundedDetected() || enemy.IsWallDetected())
        {
            enemy.CharacterFlip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
