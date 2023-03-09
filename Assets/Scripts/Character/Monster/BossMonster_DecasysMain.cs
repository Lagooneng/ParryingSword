using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster_DecasysMain : MonoBehaviour
{
    public BossMonster_DecasysController monsterCtrl;
    public int moveToPlayer = 10;
    public int backStep = 10;
    public int attack1 = 10;
    public int attackRoar = 10;

    public int wait = 10;
    public int sum;

    private Dictionary<BossMonster_DecasysState, float> delayDict; 

    private int num;
    protected Transform roadConnection;
    private BossMonster_DecasysState prevState = BossMonster_DecasysState.WAIT;
    private BossMonster_DecasysState nextState = BossMonster_DecasysState.NON;

    private void Awake()
    {
        delayDict = new Dictionary<BossMonster_DecasysState, float>();
        delayDict.Add(BossMonster_DecasysState.MOVETOPLAYER, 0.6f);
        delayDict.Add(BossMonster_DecasysState.BACKSTEP, 1.3f);
        delayDict.Add(BossMonster_DecasysState.ATTACK1, 1.2f);
        delayDict.Add(BossMonster_DecasysState.ATTACKROAR, 1.5f);
        delayDict.Add(BossMonster_DecasysState.WAIT, 1.3f);


        sum = moveToPlayer + wait + backStep + attack1 + attackRoar;
        monsterCtrl = GetComponent<BossMonster_DecasysController>();
        roadConnection = transform.Find("RoadConnection");
    }

    private void FixedUpdate()
    {
        if (!monsterCtrl.activeSts) return;
        if (!monsterCtrl.timeCheck()) return;

        nextState = BossMonster_DecasysState.NON;

        Collider2D[] colliders = Physics2D.OverlapPointAll(roadConnection.position);

        foreach (Collider2D col in colliders)
        {
            if (col.tag == "Road")
            {
                nextState = BossMonster_DecasysState.BACKSTEP;
            }
        }

        if( nextState != BossMonster_DecasysState.NON )
        {
            monsterCtrl.setState(nextState, delayDict[nextState]);
            prevState = nextState;

            return;
        }

        num = Random.Range(0, sum);

        if (num < moveToPlayer)
        {
            if (prevState == BossMonster_DecasysState.MOVETOPLAYER) return;
            if( monsterCtrl.distanceToPlayerX() < 23 )
            {
                prevState = BossMonster_DecasysState.MOVETOPLAYER;
                return;
            }

            monsterCtrl.setState(BossMonster_DecasysState.MOVETOPLAYER,
                            delayDict[BossMonster_DecasysState.MOVETOPLAYER]);
            prevState = BossMonster_DecasysState.MOVETOPLAYER;
        }
        else if( num < moveToPlayer + backStep )
        {
            if (prevState == BossMonster_DecasysState.BACKSTEP &&
                nextState != BossMonster_DecasysState.BACKSTEP) return;

            monsterCtrl.setState(BossMonster_DecasysState.BACKSTEP,
                            delayDict[BossMonster_DecasysState.BACKSTEP]);
            prevState = BossMonster_DecasysState.BACKSTEP;
        }
        else if( num < moveToPlayer + backStep + attack1)
        {
            if (prevState == BossMonster_DecasysState.ATTACK1) return;

            monsterCtrl.setState(BossMonster_DecasysState.ATTACK1,
                            delayDict[BossMonster_DecasysState.ATTACK1]);
            prevState = BossMonster_DecasysState.ATTACK1;
        }
        else if (num < moveToPlayer + backStep + attack1 + attackRoar)
        {
            if (prevState == BossMonster_DecasysState.ATTACKROAR) return;

            monsterCtrl.setState(BossMonster_DecasysState.ATTACKROAR,
                            delayDict[BossMonster_DecasysState.ATTACKROAR]);
            prevState = BossMonster_DecasysState.ATTACKROAR;
        }
        else
        {
            monsterCtrl.setState(BossMonster_DecasysState.WAIT,
                            delayDict[BossMonster_DecasysState.WAIT]);
            prevState = BossMonster_DecasysState.WAIT;
        }
    }
}
