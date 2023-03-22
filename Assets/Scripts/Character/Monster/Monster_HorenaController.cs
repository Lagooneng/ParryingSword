using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Monster_HorenaState
{
    MOVETOPLAYER,
    WAIT,
}

public class Monster_HorenaController : MonsterController
{
    private float velocity_y = 0.0f;

    private Monster_HorenaState mState;

    protected override void Awake()
    {
        base.Awake();
        hpMax = 5.0f;
        SetHP(hpMax, hpMax);
        movingWeight = 7;
        activeSts = false;
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

        attackCollider.knockBackVector = new Vector2(1500.0f * dir, 0.0f);

        if ( bodyCollider.damage > 0 )
        {
            if( SetHP(hp - bodyCollider.damage, hpMax) )
            {
                Dead();
            }
            bodyCollider.damage = 0;
        }

        FixedUpdateAI();

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * dir,
                                            transform.localScale.y, transform.localScale.z);

        attackCollider.knockBackVector = new Vector2(attackCollider.knockBackVector.x * dir,
                                            attackCollider.knockBackVector.y);

        rb.velocity = new Vector2(Mathf.Clamp(velocity_x, velocityMin.x, velocityMax.x),
                                  Mathf.Clamp(velocity_y, velocityMin.y, velocityMax.y));
    }

    public override void FixedUpdateAI()
    {
        switch (mState)
        {
            case Monster_HorenaState.WAIT:
                wait();
                break;
            case Monster_HorenaState.MOVETOPLAYER:
                moveToPlayer();
                break;
        }
    }

    public void setState(Monster_HorenaState mState, float nextDelay)
    {
        if (!timeCheck()) return;
        // Debug.Log(mState);
        startTime = Time.fixedTime;
        this.mState = mState;
        this.nextDelay = nextDelay;
    }

    private void wait()
    {
        lookPlayer(true);
        velocity_x = 0;
        velocity_y = 0;
    }

    private void moveToPlayer()
    {
        lookPlayer(true);
        // 벡터화
        if( player.transform.position.x - transform.position.x != 0)
        {
            velocity_x = (player.transform.position.x - transform.position.x) /
                        distanceToPlayerX() * movingWeight;
        }

        if(player.transform.position.y - transform.position.y != 0)
        {
            velocity_y = (player.transform.position.y - transform.position.y) /
                        distanceToPlayerY() * movingWeight;
        }
    }
}
