using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Move info")]
    [SerializeField] public float moveSpeed;

    [Header("Detection info")]
    [SerializeField] public float playerCheckDistance;
    [SerializeField] public float playerCheckPositionY;
    protected LayerMask whatIsPlayer;

    [Header("Attack info")]
    [SerializeField] public float attackCooldown = 2.0f;
    public float attackCooldownTimer = 0;

    [Header("Stunned info")]
    [SerializeField] protected GameObject counterImage;
    protected bool canBeStunned;

    public bool isAttacking;
    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

    }

    protected override void Start()
    {
        base.Start();
        whatIsPlayer = LayerMask.GetMask("Player");
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        if (attackCooldownTimer > 0)
            attackCooldownTimer -= Time.deltaTime;
    }
    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }
    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }
    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }
    public void AttackOver()
    {
        isAttacking = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }
    public RaycastHit2D isPlayerDetected() => Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + playerCheckPositionY), Vector2.right, facingDir * playerCheckDistance, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y + playerCheckPositionY), new Vector3(transform.position.x + playerCheckDistance * facingDir, transform.position.y + playerCheckPositionY));
    }
}
