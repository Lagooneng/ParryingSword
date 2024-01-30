using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Monster_BlancState
{
    JUMP,
    WAIT,
}


public class Monster_BlancController : MonsterController
{
    private Monster_BlancState mState;
    private bool acted = false;

    // 어택 컬라이더의 Awake에서 설정이 0으로 되어 있으니 Start에서 재지정 해야 됨
    private void Start()
    {
        attackCollider.damage = 5.0f;
        attackCollider.knockBackVector = new Vector2(2000.0f * dir, 0.0f);
    }

    protected override void Awake()
    {
        base.Awake();
        SetHP(hpMax, hpMax);
        activeSts = false;
    }

    protected override void FixedUpdate()
    {
        if (!activeSts) return;

        if (bodyCollider.damage > 0)
        {
            if (SetHP(hp - bodyCollider.damage, hpMax))
            {
                Dead();
            }
            bodyCollider.damage = 0;
        }

        Collider2D[][] groundColliderLists = new Collider2D[3][];
        groundColliderLists[0] = Physics2D.OverlapPointAll(groundConnection_Left.position);
        groundColliderLists[1] = Physics2D.OverlapPointAll(groundConnection_Center.position);
        groundColliderLists[2] = Physics2D.OverlapPointAll(groundConnection_Right.position);

        grounded = false;

        foreach (Collider2D[] groundColliderList in groundColliderLists)
        {
            foreach (Collider2D groundCollider in groundColliderList)
            {
                if ( groundCollider != null &&
                    groundCollider.CompareTag("Road") /*|| groundCollider.CompareTag("EnemyPhysicalBody")*/ )
                {
                    // Debug.Log("a");
                    grounded = true;
                }
            }
        }

        if( grounded )
        {
            animator.SetTrigger("Standing");
        }
        else
        {
            animator.SetTrigger("Jump");
            // Debug.Log("a");
        }

        FixedUpdateAI();

        // attackCollider.knockBackVector = new Vector2(2000.0f * dir, 0.0f);
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * dir,
                                            transform.localScale.y, transform.localScale.z);

    }

    public override void FixedUpdateAI()
    {
        switch (mState)
        {
            case Monster_BlancState.JUMP:
                if( grounded )
                {
                    jump();
                }
                break;
            case Monster_BlancState.WAIT:
                wait();
                break;
        }
    }

    private void jump()
    {
        if( acted && grounded )
        {
            stop();
        }

        if (acted) return;

        lookPlayer(true);
        acted = true;
        rb.AddForce(new Vector2( 4000.0f * dir, 13000.0f ));
        animator.SetTrigger("Jump");
    }

    private void wait()
    {
        if (acted) return;
        acted = true;
        stop();
    }

    private void stop()
    {
        rb.velocity = new Vector2(0.0f, 0.0f);
    }

    public void setState(Monster_BlancState mState, float nextDelay)
    {
        if (!timeCheck()) return;
        // Debug.Log(mState);
        acted = false;
        startTime = Time.fixedTime;
        this.mState = mState;
        this.nextDelay = nextDelay;
    }
}
