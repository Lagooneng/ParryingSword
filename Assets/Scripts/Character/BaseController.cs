using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    // 외부 파라미터, 인스펙터 표시
    public Vector2 velocityMin = new Vector2(-100.0f, -100.0f);
    public Vector2 velocityMax = new Vector2(100.0f, 100.0f);
    public float movingWeight = 20.0f;
    public float hpMax = 10.0f;

    // 외부 파라미터
    [System.NonSerialized] public float hp = 10.0f;
    [System.NonSerialized] public float dir = 1.0f;
    [System.NonSerialized] public float prevDir = 1.0f;
    [System.NonSerialized] public float speed = 10.0f;
    [System.NonSerialized] public bool activeSts = false;
    [System.NonSerialized] public bool jumped = false;
    [System.NonSerialized] public bool grounded = false;
    [System.NonSerialized] public bool groundedPrev = false;

    // 캐시
    [System.NonSerialized] public Animator animator;
    protected Transform groundConnection_Left;
    protected Transform groundConnection_Center;
    protected Transform groundConnection_Right;

    // 내부 파라미터
    protected float velocity_x = 0.0f;
    protected float force_y = 0.0f;
    protected float jumpAccel_y;
    protected Rigidbody2D rb;

    // =======================================================================
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jumpAccel_y = 2000 / rb.mass + 10 * rb.gravityScale;
        groundConnection_Left = transform.Find("GroundConnection_Left");
        groundConnection_Center = transform.Find("GroundConnection_Center");
        groundConnection_Right = transform.Find("GroundConnection_Right");

        activeSts = true;
    }

    protected virtual void FixedUpdate()
    {
        // 낙하 체크
        if( transform.position.y < -60.0f )
        {
            Dead();
        }

        // 지면 체크
        groundedPrev = grounded;
        grounded = false;

        Collider2D[][] groundColliderLists = new Collider2D[3][];
        groundColliderLists[0] = Physics2D.OverlapPointAll(groundConnection_Left.position);
        groundColliderLists[1] = Physics2D.OverlapPointAll(groundConnection_Center.position);
        groundColliderLists[2] = Physics2D.OverlapPointAll(groundConnection_Right.position);

        foreach ( Collider2D[] groundColliderList in groundColliderLists )
        {
            foreach( Collider2D groundCollider in groundColliderList )
            {
                if( groundCollider != null )
                {
                    grounded = true;
                }
            }
        }

        transform.localScale = new Vector3( Mathf.Abs( transform.localScale.x ) * dir,
                                            transform.localScale.y, transform.localScale.z);

        rb.velocity = new Vector2(Mathf.Clamp(velocity_x, velocityMin.x, velocityMax.x),
                                  Mathf.Clamp(rb.velocity.y, velocityMin.y, velocityMax.y));
        rb.AddForce(new Vector2(0.0f, force_y));
        force_y = 0;

        if (!grounded)
        {
            animator.SetTrigger("Jump");
        }

    }

    public virtual void ActionMove(float n)
    {
        if( n != 0.0f )
        {
            prevDir = dir;
            dir = (n >= 0) ? 1.0f : -1.0f;
            if( grounded ) animator.SetTrigger("Walk");
        }
        else
        {
            if (grounded) animator.SetTrigger("Standing");
        }

        velocity_x = n * movingWeight;
    }

    public virtual void ActionJump()
    {
        if( grounded )
        {
            force_y = jumpAccel_y * rb.mass;
        }
    }

    public virtual void Dead()
    {
        if( activeSts )
        {
            return;
        }
        activeSts = false;
    }

    public virtual bool SetHP(float hp, float hpMax)
    {
        this.hp = hp;
        this.hpMax = hpMax;
        return (this.hp <= 0);
    }

}
