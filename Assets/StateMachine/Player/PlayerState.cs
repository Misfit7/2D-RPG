using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    private string animBoolName;
    protected float xInput;
    protected float yInput;
    protected float stateTimer;

    public PlayerState(PlayerStateMachine stateMachine, Player player, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        //Debug.Log("I enter " + animBoolName);
        player.anim.SetBool(animBoolName, true);
    }
    public virtual void Update()
    {
        //Debug.Log("I'm in " + animBoolName);

        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", player.rb.velocity.y);
    }
    public virtual void Exit()
    {
        //Debug.Log("I exit " + animBoolName);
        player.anim.SetBool(animBoolName, false);
    }
}
