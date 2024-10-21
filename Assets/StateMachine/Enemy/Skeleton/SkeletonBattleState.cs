using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    protected Enemy_Skeleton enemy;
    private bool firstTime;

    public SkeletonBattleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        if (player.position.x - enemy.transform.position.x > 0 && enemy.facingDir < 0)
            enemy.CharacterFlip();
        else if (player.position.x - enemy.transform.position.x < 0 && enemy.facingDir > 0)
            enemy.CharacterFlip();
        firstTime = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.isPlayerDetected() && enemy.IsGoundedDetected())
        {
            if (enemy.isPlayerDetected().distance > enemy.attackDistance && firstTime)
                enemy.SetVelocity(enemy.facingDir * enemy.moveSpeed * 2, enemy.rb.velocity.y);
            else if (enemy.isPlayerDetected().distance <= enemy.attackDistance)
            {
                enemy.SetVelocity(0, 0);
                firstTime = false;
                if (enemy.attackCooldownTimer <= 0)
                {
                    enemy.attackCooldownTimer = enemy.attackCooldown;
                    enemy.stateMachine.ChangeState(enemy.attackState);
                }
            }
            else
                enemy.SetVelocity(enemy.facingDir * enemy.moveSpeed, enemy.rb.velocity.y);
        }
        else if (!enemy.isPlayerDetected() || !enemy.IsGoundedDetected() || enemy.IsWallDetected())
            enemy.stateMachine.ChangeState(enemy.idleState);

    }
}
