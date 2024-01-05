using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_ObserverController : MonsterController
{
    public GameObject[] muzzles;
    public GameObject fireSphere;
    public float fireSpeed = 40.0f;
    private GameObject[] go;
    private int muzzleLength = 0;

    protected override void Awake()
    {
        hpMax = 10.0f;
        SetHP(hpMax, hpMax);
        bodyCollider = GetComponentInChildren<MonsterBodyCollider>();
        attackCollider = GetComponentInChildren<AttackCollider>();
        player = GameObject.Find("Player");
        playerCtrl = player.GetComponent<PlayerController>();
        activeSts = false;
    }

    private void Start()
    {
        attackCollider.knockBackVector = new Vector2(2000.0f * playerCtrl.dir * (-1.0f), 0.0f);
        attackCollider.damage = 10.0f;
        muzzleLength = muzzles.Length;
        go = new GameObject[muzzleLength];
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

    }

    public void fire()
    {
        if (!activeSts) return;
        if (!fireSphere) return;

        for (int i = 0; i < muzzleLength; i++)
        {
            go[i] = Instantiate(fireSphere, muzzles[i].transform.position, Quaternion.identity);
            Invoke("track", 1.0f);
            Destroy(go[i], 5.0f);
        }
    }

    private void track()
    {
        if (muzzleLength == 0) return;

        for (int i = 0; i < muzzleLength; i++)
        {
            float distanceX = player.transform.position.x - go[i].transform.position.x;
            float distanceY = player.transform.position.y - go[i].transform.position.y;
            float distance = Mathf.Sqrt(distanceX * distanceX + distanceY + distanceY);

            // 방향만 필요하니까 정규화 해야됨 
            Vector2 vector = new Vector2(distanceX / distance, distanceY / distance);
            rb = go[i].GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(vector.x * fireSpeed, vector.y * fireSpeed);
        }
    }

    public override void Dead()
    {
        for (int i = 0; i < muzzleLength; i++)
        {
            Destroy(go[i]);
        }
        base.Dead();
    }
}
