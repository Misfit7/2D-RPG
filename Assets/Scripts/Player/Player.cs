using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Player : Entity
{
    #region States
    //StateMachine
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttack playerPrimaryAttack { get; private set; }
    public PlayerCounterAttackState playerCounterAttack { get; private set; }
    public PlayerAimSwordState aimSword { get; private set; }
    public PlayerCatchSwordState catchSword { get; private set; }

    #endregion

    #region Components
    public SkillManager skill { get; private set; }
    public GameObject sword { get; private set; }
    #endregion

    [Header("Controller info")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;

    [Header("Attack details")]
    public float counterAttackDuration;
    public float dashDir { get; private set; }

    public bool isAttacking;
    public int comboCounter;
    private float comboTime = 1.0f;
    private float comboCooldown;

    public bool isCatchSword;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        dashState = new PlayerDashState(stateMachine, this, "Dash");

        airState = new PlayerAirState(stateMachine, this, "Air");
        wallSlideState = new PlayerWallSlideState(stateMachine, this, "WallSlide");
        wallJumpState = new PlayerWallJumpState(stateMachine, this, "WallJump");

        playerPrimaryAttack = new PlayerPrimaryAttack(stateMachine, this, "Attack");
        playerCounterAttack = new PlayerCounterAttackState(stateMachine, this, "CounterAttack");

        aimSword = new PlayerAimSwordState(stateMachine, this, "AimSword");
        catchSword = new PlayerCatchSwordState(stateMachine, this, "CatchSword");
    }

    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        PlayerControllers();
        stateMachine.currentState.Update();
        //Debug.Log(IsWallDetected());
    }

    public void AssignNewSword(GameObject newSword)
    {
        sword = newSword;
    }
    public void CatchTheSword()
    {
        stateMachine.ChangeState(catchSword);
        Destroy(sword);
    }
    private void PlayerControllers()
    {
        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && skill.dash.CanUseSkill())
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0) dashDir = facingDir;
            stateMachine.ChangeState(dashState);
        }

        //Attack
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            comboCooldown = comboTime;
            stateMachine.ChangeState(playerPrimaryAttack);
        }
        if (comboCooldown > 0) comboCooldown -= Time.deltaTime;
        else if (comboCooldown <= 0) comboCounter = 0;

        //Counter Attack
        if (Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangeState(playerCounterAttack);

        //Throw Attack
        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())
            stateMachine.ChangeState(aimSword);

    }
    private bool HasNoSword()
    {
        if (!sword) return true;
        sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }

    public void AttackOver()
    {
        isAttacking = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

    }

    void OnCollisionStay2D(Collision2D collision)
    {

    }

}
