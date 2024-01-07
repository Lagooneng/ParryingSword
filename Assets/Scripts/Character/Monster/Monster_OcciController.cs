using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Monster_OcciState
{
    WALK,
    ATTACK,
    WAIT,
}

public class Monster_OcciController : MonsterController
{
    private Monster_OcciState mState;
    public float damage = 10.0f;
    private bool acted = false;

    protected override void Awake()
    {
        base.Awake();
        SetHP(hpMax, hpMax);
        movingWeight = 13.0f;
        activeSts = false;
    }

    private void Start()
    {
        attackCollider.damage = damage;
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

        attackCollider.knockBackVector = new Vector2(1500.0f * dir, 0.0f);

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * dir,
                                            transform.localScale.y, transform.localScale.z);

        rb.velocity = new Vector2(Mathf.Clamp(velocity_x, velocityMin.x, velocityMax.x),
                                  Mathf.Clamp(rb.velocity.y, velocityMin.y, velocityMax.y));
    }

    public override void FixedUpdateAI()
    {
        switch (mState)
        {
            case Monster_OcciState.WAIT:
                wait();
                break;
            case Monster_OcciState.WALK:
                walk();
                break;
            case Monster_OcciState.ATTACK:
                attack();
                break;
        }
    }

    public void setState(Monster_OcciState mState, float nextDelay)
    {
        if (!timeCheck()) return;
        // Debug.Log(mState);
        startTime = Time.fixedTime;
        acted = false;
        this.mState = mState;
        this.nextDelay = nextDelay;
    }

    private void wait()
    {
        lookPlayer(true);
        velocity_x = 0.0f;
        if (acted) return;
        animator.SetTrigger("Standing");
        acted = true;
    }

    private void walk()
    {
        lookPlayer(true);
        velocity_x = movingWeight * dir;
        if (acted) return;
        animator.SetTrigger("Walk");
        acted = true;
    }

    private void attack()
    {
        lookPlayer(true);
        velocity_x = 0.0f;
        if (acted) return;
        animator.SetTrigger("Attack");
        acted = true;
    }

}
