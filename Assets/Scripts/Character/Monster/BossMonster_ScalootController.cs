using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossMonster_ScalootState
{
    STANDING,
    WALK,
    ROAR,
    WING,
    WINGDOUBLE,
    FLYING,
    BREATH,
    BURST,
    NON
}

public class BossMonster_ScalootController : MonsterController
{
    public GameObject breath;

    private BossMonster_ScalootState mState;
    private CameraEffects camEf;
    private bool acted = false;
    private AnimatorStateInfo animStateInfo;
    private bool nowFlying = false;

    protected override void Awake()
    {
        base.Awake();
        camEf = GameObject.Find("CameraManager").GetComponent<CameraEffects>();
        SetHP(hpMax, hpMax);
        movingWeight = 15;
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

        attackCollider.knockBackVector = new Vector2(1500.0f * dir, 0.0f);

        rb.velocity = new Vector2(Mathf.Clamp(velocity_x, velocityMin.x - 100.0f, velocityMax.x),
                                  Mathf.Clamp(rb.velocity.y, velocityMin.y, velocityMax.y));

    }

    public void setState(BossMonster_ScalootState mState, float nextDelay)
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
            case BossMonster_ScalootState.STANDING:
                ActionStanding();
                break;
            case BossMonster_ScalootState.WALK:
                ActionWalk();
                break;
            case BossMonster_ScalootState.ROAR:
                ActionAttackRoar();
                break;
            case BossMonster_ScalootState.WING:
                ActionAttackWing();
                break;
            case BossMonster_ScalootState.WINGDOUBLE:
                ActionAttackWingDouble();
                break;
            case BossMonster_ScalootState.FLYING:
                ActionAttackFlying();
                break;
            case BossMonster_ScalootState.BREATH:
                ActionAttackBreath();
                break;
            case BossMonster_ScalootState.BURST:
                ActionAttackBurst();
                break;
        }
    }

    public void ActionStanding()
    {
        if (acted) return;
        lookPlayer(true);
        velocity_x = 0.0f;
        acted = true;
        animator.SetTrigger("Standing");
    }

    private void ActionWalk()
    {
        if (!acted)
        {
            lookPlayer(true);
            animator.SetTrigger("Walk");
            acted = true;
        }

        velocity_x = (movingWeight + 10) * dir;
    }

    public void ActionAttackRoar()
    {
        if (acted) return;
        lookPlayer(true);
        velocity_x = 0.0f;
        acted = true;
        attackCollider.damage = 10.0f;
        animator.SetTrigger("Roar");
    }

    public void ActionAttackWing()
    {
        if (acted) return;
        lookPlayer(true);
        velocity_x = 0.0f;
        acted = true;
        attackCollider.damage = 15.0f;
        animator.SetTrigger("Wing");
    }

    public void ActionAttackWingDouble()
    {
        if (acted) return;
        lookPlayer(true);
        velocity_x = 0.0f;
        acted = true;
        attackCollider.damage = 15.0f;
        animator.SetTrigger("WingDouble");
    }

    public void ActionAttackFlying()
    {
        if( nowFlying )
        {
            velocity_x = 250.0f * dir;
        }
        else
        {
            velocity_x = 0.0f;
        }
        
        if (acted) return;
        lookPlayer(true);
        acted = true;
        attackCollider.damage = 10.0f;
        animator.SetTrigger("Flying");
    }

    public void ActionAttackBreath()
    {
        if (acted) return;
        lookPlayer(true);

        velocity_x = 0.0f;
        acted = true;
        attackCollider.damage = 20.0f;
        breath.transform.localScale = new Vector3(dir * Mathf.Abs(breath.transform.localScale.x),
                                                    breath.transform.localScale.y, breath.transform.localScale.z);
        animator.SetTrigger("Breath");
    }

    public void ActionAttackBurst()
    {
        if (acted) return;
        lookPlayer(true);

        velocity_x = 0.0f;
        acted = true;
        attackCollider.damage = 50.0f;

        animator.SetTrigger("Burst");
    }

    // 애니메이션 지원
    public void sawy()
    {
        camEf.setState(CameraState.SWAY);
    }

    public void setSwayTime(float time)
    {
        camEf.setSwayTime(time);
    }

    public void onFlying()
    {
        nowFlying = true;
    }

    public void offFlying()
    {
        nowFlying = false;
    }

    public void onDarkEffect()
    {
        camEf.switchDarkEffect(true);
    }

    public void offDarkEffect()
    {
        camEf.switchDarkEffect(false);
    }
}
