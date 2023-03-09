using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossMonster_DecasysState
{
    MOVETOPLAYER,
    BACKSTEP,
    WAIT,
    ATTACK1,
    ATTACKROAR,
    NON
}

public class BossMonster_DecasysController : MonsterController
{
    private BossMonster_DecasysState mState;
    private CameraEffects camEf;
    private bool acted = false;

    protected override void Awake()
    {
        base.Awake();
        camEf = GameObject.Find("CameraManager").GetComponent<CameraEffects>();
        hpMax = 60.0f;
        SetHP(hpMax, hpMax);
        movingWeight = 23;
    }

    // 어택 컬라이더의 Awake에서 설정이 0으로 되어 있으니 Start에서 재지정 해야 됨
    private void Start()
    {
        attackCollider.damage = 1.0f;
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

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * dir,
                                            transform.localScale.y, transform.localScale.z);

        rb.velocity = new Vector2(Mathf.Clamp( velocity_x, velocityMin.x - 100.0f, velocityMax.x),
                                  Mathf.Clamp(rb.velocity.y, velocityMin.y, velocityMax.y));

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
        lookPlayer(true);
        animator.SetTrigger("Walk");

        // 벡터화
        if ( distanceToPlayerX() > 22 )
        {
            velocity_x = (player.transform.position.x - transform.position.x) /
                        Mathf.Abs(player.transform.position.x - transform.position.x) * movingWeight;
        }
        else
        {
            nextDelay = 0.0f;
        }
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
        acted = true;
        animator.SetTrigger("Attack1");
    }

    private void ActionAttackRoar()
    {
        if (acted) return;
        lookPlayer(true);
        velocity_x = 0.0f;
        acted = true;
        animator.SetTrigger("Roar");
    }

    // 애니메이션 지원
    private void backStepForce()
    {
        velocity_x = -20.0f * dir;
        rb.AddForce(new Vector2(-20000.0f * dir, 10000.0f));
    }


    public void setState(CameraState cState)
    {
        camEf.setState(cState);
    }

    public void setSwayTime(float time)
    {
        camEf.setSwayTime(time);
    }
}
