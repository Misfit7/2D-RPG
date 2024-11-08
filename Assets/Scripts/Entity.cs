using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public EntityFX fx { get; private set; }
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform attackCheck;
    #endregion

    [Header("Collision Detection")]
    [SerializeField] protected float groundDistance = 0.1f;
    [SerializeField] protected float wallDistance = 0.1f;

    [Header("Attack details")]
    public float attackCheckRadius;

    [Header("Knockback Info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    [SerializeField] public float stunDuration;
    [SerializeField] public Vector2 stunDirection;

    public bool isKnocked { get; private set; }

    public int facingDir = 1;
    public bool facingRight = true;
    protected LayerMask whatIsGround;
    protected LayerMask whatIsWall;

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        fx = GetComponentInChildren<EntityFX>();
        groundCheck = transform.Find("GroundCheck");
        wallCheck = transform.Find("WallCheck");
        attackCheck = transform.Find("AttackCheck");
        whatIsGround = LayerMask.GetMask("Ground");
    }
    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }
    public virtual void Damage()
    {
        Debug.Log(gameObject.name + " was damaged!");
        StartCoroutine("HitKnockback");
        fx.StartCoroutine("FlashFX");
    }

    protected virtual void Update()
    {

    }

    public bool IsGoundedDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallDistance, whatIsGround);

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked) return;
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipControllers(xVelocity);
    }
    protected void FlipControllers(float x)
    {
        if (x > 0 && !facingRight)
            CharacterFlip();
        else if (x < 0 && facingRight)
            CharacterFlip();
    }
    public void CharacterFlip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    protected virtual void OnDrawGizmos()
    {
        if (groundCheck)
            Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundDistance));
        if (wallCheck)
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallDistance, wallCheck.position.y));
        if (attackCheck)
            Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
}
