using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    #region States
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonHitState hitState { get; private set; }
    public SkeletonAirState airState { get; private set; }

    #endregion

    [SerializeField] public float attackDistance = 1.5f;

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(stateMachine, this, "Idle", this);
        moveState = new SkeletonMoveState(stateMachine, this, "Move", this);
        battleState = new SkeletonBattleState(stateMachine, this, "Move", this);
        attackState = new SkeletonAttackState(stateMachine, this, "Attack", this);
        hitState = new SkeletonHitState(stateMachine, this, "Hit", this);
        airState = new SkeletonAirState(stateMachine, this, "Move", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.R))
            stateMachine.ChangeState(hitState);

    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(hitState);
            return true;
        }
        return false;
    }
}
