using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyState : MonoBehaviour
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;

    private string animBoolName;
    protected float stateTimer;

    public EnemyState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.enemyBase = enemyBase;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        //Debug.Log("I enter " + animBoolName);
        enemyBase.anim.SetBool(animBoolName, true);
    }
    public virtual void Update()
    {
        //Debug.Log("I'm in " + animBoolName);
        stateTimer -= Time.deltaTime;
    }
    public virtual void Exit()
    {
        //Debug.Log("I exit " + animBoolName);
        enemyBase.anim.SetBool(animBoolName, false);
    }
}
