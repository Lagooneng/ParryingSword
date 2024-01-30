using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster_ScalootMain : MonoBehaviour
{
    public BossMonster_ScalootController monsterCtrl;
    public int standing = 30;
    public int walk = 10;
    public int roar = 50;
    public int wing = 60;
    public int wingDouble = 60;
    public int flying = 40;
    public int breath = 60;
    public int burst = 20;

    public int sum;


    private int num;
    private int burstActualValue = 0;
    private Dictionary<BossMonster_ScalootState, float> delayDict;
    private BossMonster_ScalootState prevState = BossMonster_ScalootState.STANDING;
    private BossMonster_ScalootState nextState = BossMonster_ScalootState.ROAR;
    private AudioSource bgmAudioSource;

    private void Awake()
    {
        delayDict = new Dictionary<BossMonster_ScalootState, float>();
        delayDict.Add(BossMonster_ScalootState.STANDING, 3.0f);
        delayDict.Add(BossMonster_ScalootState.WALK, 2.0f);
        delayDict.Add(BossMonster_ScalootState.ROAR, 5.633f);
        delayDict.Add(BossMonster_ScalootState.WING, 2.5f);
        delayDict.Add(BossMonster_ScalootState.WINGDOUBLE, 4.0f);
        delayDict.Add(BossMonster_ScalootState.FLYING, 4.133f);
        delayDict.Add(BossMonster_ScalootState.BREATH, 5.6f);
        delayDict.Add(BossMonster_ScalootState.BURST, 9.0f);

        sum = standing + walk + roar + wing + wingDouble + flying + breath;
        monsterCtrl = GetComponent<BossMonster_ScalootController>();
        bgmAudioSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!monsterCtrl.activeSts) return;
        if (!monsterCtrl.timeCheck()) return;

        prevState = nextState;
        nextState = BossMonster_ScalootState.NON;

        // 스테이트 강제 설정 ----------------------------------------------------
        if (monsterCtrl.hp < (monsterCtrl.hpMax / 2) && burstActualValue == 0)
        {
            burstActualValue = burst;
            sum += burst;
            nextState = BossMonster_ScalootState.BURST;
        }
        else if (monsterCtrl.distanceToPlayerX() > 70)
        {
            nextState = BossMonster_ScalootState.FLYING;
        }
        else if (monsterCtrl.distanceToPlayerX() > 45)
        {
            nextState = BossMonster_ScalootState.WALK;
        }

        if (nextState != BossMonster_ScalootState.NON)
        {
            monsterCtrl.setState(nextState, delayDict[nextState]);
            return;
        }


        // 스테이트 일반 설정 ----------------------------------------------------
        num = Random.Range(0, sum);

        if (num < standing) // 가짜 포효, 딜 타이밍 
        {
            nextState = BossMonster_ScalootState.STANDING;
        }
        else if (num < standing + walk) // 걷기 
        {
            if (prevState == BossMonster_ScalootState.WALK) return;

            nextState = BossMonster_ScalootState.WALK;
        }
        else if (num < standing + walk + roar) // 포효 
        {
            if (prevState == BossMonster_ScalootState.ROAR) return;

            nextState = BossMonster_ScalootState.ROAR;
        }
        else if (num < standing + walk + roar + wing)  // 날개 찍기 1회 
        {
            if (prevState == BossMonster_ScalootState.WING) return;

            nextState = BossMonster_ScalootState.WING;
        }
        else if (num < standing + walk + roar + wing + wingDouble)  // 날개 찍기 2회 
        {
            if (prevState == BossMonster_ScalootState.WINGDOUBLE) return;

            nextState = BossMonster_ScalootState.WINGDOUBLE;
        }
        else if (num < standing + walk + roar + wing + wingDouble + flying)  // 날아서 공격 
        {
            nextState = BossMonster_ScalootState.FLYING;
        }
        else if (num < standing + walk + roar + wing + wingDouble + flying + breath)
        {
            if (prevState == BossMonster_ScalootState.BREATH) return;

            nextState = BossMonster_ScalootState.BREATH;
        }
        else if (num < standing + walk + roar + wing + wingDouble + flying + breath + burst)
        {
            if (prevState == BossMonster_ScalootState.BURST) return;

            nextState = BossMonster_ScalootState.BURST;

        }

        monsterCtrl.setState(nextState, delayDict[nextState]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBody")
        {
            // Debug.Log("a");
            if (bgmAudioSource)
            {
                if (!bgmAudioSource.isPlaying) bgmAudioSource.Play();
            }
            monsterCtrl.activeSts = true;
            Destroy(this.GetComponent<BoxCollider2D>());
        }
    }
}
