using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster_DecasysMain : MonoBehaviour
{
    public BossMonster_DecasysController monsterCtrl;
    public int moveToPlayer = 5;
    public int backStep = 5;
    public int attack1 = 15;
    public int attack2 = 15;
    public int attackRoar = 20;

    public int wait = 1;
    public int sum;

    private int attackCount = 0;

    private Dictionary<BossMonster_DecasysState, float> delayDict; 

    private int num;
    protected Transform roadConnection;
    private BossMonster_DecasysState prevState = BossMonster_DecasysState.WAIT;
    private BossMonster_DecasysState nextState = BossMonster_DecasysState.NON;
    private AudioSource audioSource;

    private void Awake()
    {
        delayDict = new Dictionary<BossMonster_DecasysState, float>();
        delayDict.Add(BossMonster_DecasysState.MOVETOPLAYER, 0.833f);
        delayDict.Add(BossMonster_DecasysState.BACKSTEP, 0.833f);
        delayDict.Add(BossMonster_DecasysState.ATTACK1, 1.55f);
        delayDict.Add(BossMonster_DecasysState.ATTACK2, 1.583f);
        delayDict.Add(BossMonster_DecasysState.ATTACKROAR, 1.5f);
        delayDict.Add(BossMonster_DecasysState.WAIT, 2.25f);

        sum = moveToPlayer + wait + backStep + attack1 + attackRoar;
        monsterCtrl = GetComponent<BossMonster_DecasysController>();
        roadConnection = transform.Find("RoadConnection");
        audioSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!monsterCtrl.activeSts) return;
        if (!monsterCtrl.timeCheck()) return;

        nextState = BossMonster_DecasysState.NON;

        Collider2D[] colliders = Physics2D.OverlapPointAll(roadConnection.position);

        // 특정 상황에서의 스테이트 강제 *******************************************
        /* foreach (Collider2D col in colliders)
        {
            if (col.tag == "Road")
            {
                nextState = BossMonster_DecasysState.BACKSTEP;
            }
        }
        */ 
        if( attackCount > 2 )
        {
            nextState = BossMonster_DecasysState.WAIT;
            attackCount = 0;
        }

        if( monsterCtrl.distanceToPlayerX() > 35 )
        {
            nextState = BossMonster_DecasysState.MOVETOPLAYER;
        }

        if (monsterCtrl.distanceToPlayerY() > 3.0f)
        {
            nextState = BossMonster_DecasysState.ATTACKROAR;
        }
        
        if ( nextState != BossMonster_DecasysState.NON )
        {
            monsterCtrl.setState(nextState, delayDict[nextState]);
            prevState = nextState;
            
            return;
        }
        // ******************************************************************

        num = Random.Range(0, sum);
        
        if (num < moveToPlayer)
        {
            if (prevState == BossMonster_DecasysState.MOVETOPLAYER) return;
            /*if( monsterCtrl.distanceToPlayerX() < 17 )
            {
                prevState = BossMonster_DecasysState.MOVETOPLAYER;
                return;
            }*/

            monsterCtrl.setState(BossMonster_DecasysState.MOVETOPLAYER,
                            delayDict[BossMonster_DecasysState.MOVETOPLAYER]);
            prevState = BossMonster_DecasysState.MOVETOPLAYER;

            attackCount = 0;
        }
        else if( num < moveToPlayer + backStep )
        {
            if (prevState == BossMonster_DecasysState.BACKSTEP &&
                nextState != BossMonster_DecasysState.BACKSTEP) return;

            monsterCtrl.setState(BossMonster_DecasysState.BACKSTEP,
                            delayDict[BossMonster_DecasysState.BACKSTEP]);
            prevState = BossMonster_DecasysState.BACKSTEP;

            attackCount = 0;
        }
        else if( num < moveToPlayer + backStep + attack1)
        {
            if (prevState == BossMonster_DecasysState.ATTACK1) return;

            monsterCtrl.setState(BossMonster_DecasysState.ATTACK1,
                            delayDict[BossMonster_DecasysState.ATTACK1]);
            prevState = BossMonster_DecasysState.ATTACK1;

            attackCount += 1;
        }
        else if (num < moveToPlayer + backStep + attack1 + attack2)
        {
            if (prevState == BossMonster_DecasysState.ATTACK2) return;

            monsterCtrl.setState(BossMonster_DecasysState.ATTACK2,
                            delayDict[BossMonster_DecasysState.ATTACK2]);
            prevState = BossMonster_DecasysState.ATTACK2;

            attackCount += 1;
        }
        else if (num < moveToPlayer + backStep + attack1 + attack2 + attackRoar)
        {
            if (prevState == BossMonster_DecasysState.ATTACKROAR) return;

            monsterCtrl.setState(BossMonster_DecasysState.ATTACKROAR,
                            delayDict[BossMonster_DecasysState.ATTACKROAR]);
            prevState = BossMonster_DecasysState.ATTACKROAR;

            attackCount += 1;
        }
        else if(num < moveToPlayer + backStep + attack1 + attack2 + attackRoar + wait)
        {
            if (attackCount == 0) return;
            monsterCtrl.setState(BossMonster_DecasysState.WAIT,
                            delayDict[BossMonster_DecasysState.WAIT]);
            prevState = BossMonster_DecasysState.WAIT;

            attackCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBody")
        {
            //Debug.Log("a");
            if( audioSource )
            {
                if (!audioSource.isPlaying) audioSource.Play();
            }
            
            monsterCtrl.activeSts = true;
            Destroy(this.GetComponent<BoxCollider2D>());
        }
    }
}
