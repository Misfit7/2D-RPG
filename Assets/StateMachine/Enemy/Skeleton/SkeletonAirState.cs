using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SkeletonAirState : EnemyState
{
    protected Enemy_Skeleton enemy;

    public SkeletonAirState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
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
        if (enemy.IsGoundedDetected() || !enemy.isInAir)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
