using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_FireGummyController : MonsterController
{

    protected override void Awake()
    {
        hpMax = 20.0f;
        SetHP(hpMax, hpMax);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponentInChildren<MonsterBodyCollider>();
        attackCollider = GetComponentInChildren<AttackCollider>();
        player = GameObject.Find("Player");
        playerCtrl = player.GetComponent<PlayerController>();
        activeSts = false;
    }

    private void Start()
    {
        attackCollider.knockBackVector = new Vector2(1000.0f * playerCtrl.dir * (-1.0f), 0.0f);
        attackCollider.damage = 10.0f;
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
    }
}
