using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    [System.NonSerialized] public bool actionActive = true;

    public readonly static int AnimStanding =
        Animator.StringToHash("Base Layer.Player_Standing");
    public readonly static int AnimWalk =
        Animator.StringToHash("Base Layer.Player_Walk");
    public readonly static int AnimJump =
        Animator.StringToHash("Base Layer.Player_Jump");
    public readonly static int AnimAttackHauen =
        Animator.StringToHash("Base Layer.Player_AttackHauen");
    public readonly static int AnimAttackParrying =
        Animator.StringToHash("Base Layer.Player_AttackParrying");
    public readonly static int AnimAttackParryingSuccess =
        Animator.StringToHash("Base Layer.Player_AttackParryingSuccess");

    public override void Dead()
    {
        base.Dead();
        // 리로드
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    
    protected override void Awake()
    {
        base.Awake();
    }
    
    protected override void FixedUpdate()
    {
        // 낙하 체크
        if (transform.position.y < -60.0f)
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

        foreach (Collider2D[] groundColliderList in groundColliderLists)
        {
            foreach (Collider2D groundCollider in groundColliderList)
            {
                if (groundCollider != null)
                {
                    grounded = true;
                }
            }
        }


        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Camera.main.transform.position =
            new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

        if( animStateInfo.fullPathHash == AnimStanding ||
            animStateInfo.fullPathHash == AnimWalk ||
            animStateInfo.fullPathHash == AnimJump )
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * dir,
                                            transform.localScale.y, transform.localScale.z);

            rb.velocity = new Vector2(Mathf.Clamp(velocity_x, velocityMin.x, velocityMax.x),
                                      Mathf.Clamp(rb.velocity.y, velocityMin.y, velocityMax.y));
        }

        rb.AddForce(new Vector2(0.0f, force_y));
        force_y = 0;

        if (!grounded)
        {
            animator.SetTrigger("Jump");
        }

    }

    public void AttackNormal()
    {
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if ( !actionActive ) return;

        if( animStateInfo.fullPathHash == AnimAttackParryingSuccess )
        {
            animator.Play("Player_AttackStechen");
        }
        else if( animStateInfo.fullPathHash != AnimAttackHauen )
        {
            animator.SetTrigger("Normal");
        }
        else
        {
            animator.Play("Player_AttackStechen");
        }
    }

    public void AttackParrying()
    {
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if(!actionActive) return;

        if( animStateInfo.fullPathHash != AnimAttackParrying )
        {
            animator.SetTrigger("Parrying");
        }   
    }

    public void AttackSpecial()
    {
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (!actionActive) return;

        if (animStateInfo.fullPathHash == AnimAttackParryingSuccess)
        {
            animator.Play("Player_AttackZornhauw");
        }
    }

    // 애니메이션 지원 메소드
    public void changeActionActive()
    {
        actionActive = actionActive ? false : true;
    }
}
