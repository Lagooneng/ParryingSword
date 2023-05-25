using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossMonster_DecasysState
{
    MOVETOPLAYER,
    BACKSTEP,
    WAIT,
    ATTACK1,
    ATTACK2,
    ATTACKROAR,
    NON
}

public class BossMonster_DecasysController : MonsterController
{
    private BossMonster_DecasysState mState;
    private CameraEffects camEf;
    private bool acted = false;
    private AnimatorStateInfo animStateInfo;

    public readonly static int AnimWalk =
        Animator.StringToHash("Base Layer.BossMonster_Decasys_Walk");

    protected override void Awake()
    {
        base.Awake();
        camEf = GameObject.Find("CameraManager").GetComponent<CameraEffects>();
        SetHP(hpMax, hpMax);
        movingWeight = 23;
        activeSts = false;
    }

    // 어택 컬라이더의 Awake에서 설정이 0으로 되어 있으니 Start에서 재지정 해야 됨
    private void Start()
    {
        attackCollider.damage = 20.0f;
        attackCollider.knockBackVector = new Vector2(1500.0f * dir, 0.0f);
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

        FixedUpdateAI();

        grounded = false;

        Collider2D[][] groundColliderLists = new Collider2D[3][];
        groundColliderLists[0] = Physics2D.OverlapPointAll(groundConnection_Left.position);
        groundColliderLists[1] = Physics2D.OverlapPointAll(groundConnection_Center.position);
        groundColliderLists[2] = Physics2D.OverlapPointAll(groundConnection_Right.position);

        foreach (Collider2D[] groundColliderList in groundColliderLists)
        {
            foreach (Collider2D groundCollider in groundColliderList)
            {
                if (groundCollider != null &&
                    groundCollider.CompareTag("Road") ||
                    groundCollider.CompareTag("EnemyPhysicalBody"))
                {
                    grounded = true;
                }
            }
        }

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * dir,
                                            transform.localScale.y, transform.localScale.z);

        attackCollider.knockBackVector = new Vector2(1500.0f * dir, 0.0f);

        rb.velocity = new Vector2(Mathf.Clamp( velocity_x, velocityMin.x - 100.0f, velocityMax.x),
                                  Mathf.Clamp(rb.velocity.y, velocityMin.y, velocityMax.y));

        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if( animStateInfo.fullPathHash == AnimWalk && velocity_x == 0.0f )
        {
            animator.Play("BossMonster_Decasys_Standing");
        }

        if( grounded && velocity_x * dir > 0.0f )
        {
            animator.Play("BossMonster_Decasys_Walk");
        }

    }

    public void setState(BossMonster_DecasysState mState, float nextDelay)
    {
        if (!timeCheck()) return;
        // Debug.Log(mState);
        // Debug.Log(distanceToPlayerX());
        acted = false;
        startTime = Time.fixedTime;
        this.mState = mState;
        this.nextDelay = nextDelay;
    }

    public override void FixedUpdateAI()
    {
        switch (mState)
        {
            case BossMonster_DecasysState.WAIT:
                ActionWait();
                break;
            case BossMonster_DecasysState.MOVETOPLAYER:
                ActionMoveToPlayer();
                break;
            case BossMonster_DecasysState.BACKSTEP:
                ActionBackStep();
                break;
            case BossMonster_DecasysState.ATTACK1:
                ActionAttack1();
                break;
            case BossMonster_DecasysState.ATTACK2:
                ActionAttack2();
                break;
            case BossMonster_DecasysState.ATTACKROAR:
                ActionAttackRoar();
                break;
        }
    }

    private void ActionWait()
    {
        animator.SetTrigger("Standing");
        lookPlayer(true);
        velocity_x = 0;
    }


    private void ActionMoveToPlayer()
    {
        if( !acted )
        {
            lookPlayer(true);
            animator.SetTrigger("Walk");
            acted = true;
        }

        // 벡터화
        /*if ( distanceToPlayerX() > 16 )
        {
            velocity_x = (player.transform.position.x - transform.position.x) /
                        Mathf.Abs(player.transform.position.x - transform.position.x) * movingWeight;
        }
        else
        {
            nextDelay = 0.0f;
        }*/

        velocity_x = (movingWeight + 10 ) * dir;
    }

    private void ActionBackStep()
    {
        if (acted) return;
        acted = true;
        // Debug.Log("a");
        animator.SetTrigger("BackStep");
    }

    private void ActionAttack1()
    {
        if (acted) return;
        lookPlayer(true);
        velocity_x = 0.0f;
        attackCollider.damage = 20.0f;
        acted = true;
        animator.SetTrigger("Attack1");
    }

    private void ActionAttack2()
    {
        if (acted) return;
        lookPlayer(true);
        velocity_x = 0.0f;
        attackCollider.damage = 30.0f;
        acted = true;
        animator.SetTrigger("Attack2");
    }

    private void ActionAttackRoar()
    {
        velocity_x = 0.0f;
        if (acted) return;
        lookPlayer(true);
        velocity_x = 0.0f;
        attackCollider.damage = 10.0f;
        acted = true;
        animator.SetTrigger("Roar");
    }


    // 애니메이션 지원
    private void backStepForce()
    {
        velocity_x = -20.0f * dir;
        rb.AddForce(new Vector2(-20000.0f * dir, 10000.0f));
    }


    public void sawy()
    {
        camEf.setState(CameraState.SWAY);
    }

    public void setSwayTime(float time)
    {
        camEf.setSwayTime(time);
    }
}
