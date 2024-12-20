using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private float colorLoosingSpeed;

    private Animator anim;
    private float cloneTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius;
    private Transform closestEnemy;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));
            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }
    public void SetupClone(Transform newTransform, float cloneDuration, bool canAttack)
    {
        if (canAttack)
            anim.SetInteger("AttackNumber", Random.Range(1, 4));
        cloneTimer = cloneDuration;
        transform.position = newTransform.position;
        FaceClosesTarget();
    }
    private void AnimationTrigger()
    {
        cloneTimer = -0.1f;
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
                hit.GetComponent<Enemy>().Damage();
        }
    }
    private void FaceClosesTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5);
        float closestDistance = Mathf.Infinity;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }
        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
                transform.Rotate(0, 180, 0); //facing left original is facing right;
        }
    }
}
