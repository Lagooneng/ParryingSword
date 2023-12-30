using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Monster_MagmaSlimeState
{
    JUMP,
    WAIT,
    WALK
}


public class Monster_MagmaSlimeController : MonsterController
{
    private Monster_MagmaSlimeState mState;
    private bool acted = false;

    // 어택 컬라이더의 Awake에서 설정이 0으로 되어 있으니 Start에서 재지정 해야 됨
    private void Start()
    {
        attackCollider.damage = 10.0f;
        attackCollider.knockBackVector = new Vector2(1000.0f * dir, 0.0f);
    }

    protected override void Awake()
    {
        base.Awake();
        SetHP(hpMax, hpMax);
        activeSts = false;
        movingWeight = 6.0f;
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


        grounded = false;

        FixedUpdateAI();

        // attackCollider.knockBackVector = new Vector2(2000.0f * dir, 0.0f);
        rb.velocity = new Vector2(Mathf.Clamp(velocity_x, velocityMin.x - 100.0f, velocityMax.x),
                                  Mathf.Clamp(rb.velocity.y, velocityMin.y, velocityMax.y));
    }

    public override void FixedUpdateAI()
    {
        switch (mState)
        {
            case Monster_MagmaSlimeState.JUMP:
                jump();
                break;
            case Monster_MagmaSlimeState.WAIT:
                wait();
                break;
            case Monster_MagmaSlimeState.WALK:
                walk();
                break;
        }
    }

    private void jump()
    {
        velocity_x = movingWeight * dir;
        if (acted) return;

        lookPlayer(true);
        acted = true;
        animator.SetTrigger("Jump");
    }

    private void wait()
    {
        velocity_x = 0.0f;
        if (acted) return;
        acted = true;
        animator.SetTrigger("Standing");
    }

    private void walk()
    {
        velocity_x = movingWeight * dir;
        if (acted) return;

        acted = true;
        animator.SetTrigger("Standing");
    }

    public void setState(Monster_MagmaSlimeState mState, float nextDelay)
    {
        if (!timeCheck()) return;
        // Debug.Log(mState);
        dir = (Random.Range(0, 1) > 0) ? 1.0f : -1.0f;
        acted = false;
        startTime = Time.fixedTime;
        this.mState = mState;
        this.nextDelay = nextDelay;
    }
}
