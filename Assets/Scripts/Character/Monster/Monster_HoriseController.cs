using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_HoriseController : MonsterController
{

    protected override void Awake()
    {
        hpMax = 1.0f;
        SetHP(hpMax, hpMax);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponentInChildren<MonsterBodyCollider>();
        attackCollider = GetComponentInChildren<AttackCollider>();
        player = GameObject.Find("Player");
        playerCtrl = player.GetComponent<PlayerController>();
        activeSts = false;
        movingWeight = 90;
    }

    private void Start()
    {
        attackCollider.knockBackVector = new Vector2(500.0f, 0.0f);
        attackCollider.damage = 2.0f;
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

        if (!activeSts) return;

        attackCollider.knockBackVector = new Vector2(attackCollider.knockBackVector.x * dir,
                                            attackCollider.knockBackVector.y);

        rb.velocity = new Vector2(Mathf.Clamp(movingWeight * transform.localScale.x,
            velocityMin.x, velocityMax.x), 0.0f);
    }
}
