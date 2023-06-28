using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class LiquidController : MonoBehaviour
{
    private SpriteShapeController spriteShapeController;
    private Spline spline;
    private GameObject player;
    private PlayerController playerCtrl;
    private Rigidbody2D playerRB;
    private BoxCollider2D col;
    private float orgMovingWeight;
    private float orgGravity;
    private float width, height;
    private int waterLineCount;
    private float[] wave;
    private int length;
    private float waveConstant = 50.0f;
    private int interval = 1;

    public bool onWaterLine = false;
    public float waveWegiht = 1.5f;
    public float density = 1.65f;
    public GameObject heightObj, widthObj;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
        playerCtrl = player.GetComponent<PlayerController>();
        playerRB = player.GetComponent<Rigidbody2D>();
        spriteShapeController = GetComponent<SpriteShapeController>();
        spline = spriteShapeController.spline;
        orgMovingWeight = playerCtrl.movingWeight;
        orgGravity = playerRB.gravityScale;

        width = widthObj.transform.position.x - transform.position.x;
        height = heightObj.transform.position.y - transform.position.y;

        waterLineCount = 0;

        spline.Clear();

        Vector3 pos = new Vector3(0, height, 0);
        spline.InsertPointAt(0, pos);

        // 1번 ~ waterLineCount 까지 중간 지점
        for( int i = 1; i * interval < width; i++ )
        {
            waterLineCount++;
            pos = new Vector3(i * interval, height, 0);
            spline.InsertPointAt(i, pos);
        }

        pos = new Vector3(width, height, 0);
        spline.InsertPointAt(waterLineCount + 1, pos);

        pos = new Vector3(width, 0, 0);
        spline.InsertPointAt(waterLineCount + 2, pos);

        pos = new Vector3(0, 0, 0);
        spline.InsertPointAt(waterLineCount + 3, pos);

        length = waterLineCount + 1;

        col.offset = new Vector2(width / 2.0f, height / 2.0f);
        col.size = new Vector2(width, height);

        wave = new float[waterLineCount + 2];
        length = waterLineCount + 2;

        for( int i = 0; i < length; i++ )
        {
            wave[i] = 0.0f;
        }

        spline.SetHeight(0, 0.1f);
        spline.SetHeight(waterLineCount + 2, 0.1f);
    }


    private void Update()
    {
        if (!onWaterLine) return;

        for( int i = 0; i < length - 1; i++ )
        {
            Vector3 pos = new Vector3(i * interval, height + wave[i] * Mathf.Sin(i * waveConstant / 150), 0.0f);

            spline.SetPosition(i, pos);

            wave[i] = wave[i] / 1.03f;
            if( wave[i] < 0.15f )
            {
                //wave[i] = 0.149f;
                wave[i] = 0.0f;
            }
            
        }

        if( waveConstant < 360.0f || waveConstant > 0.0f)
        {
            waveConstant = (waveConstant + 1.4f * interval);
        }
        else
        {
            waveConstant = (waveConstant - 1.4f * interval);
        }
        
    }
    

    private void waveUpdate(int index, float weight, int direction)  // direction = 0 > 시작 -1 왼쪽 1 오른쪽
    {
        if (index < 0 || index >= length) return;
        if (weight < 0.1f) return;

        wave[index] = weight;
        if ( direction != 1 )
        {
            waveUpdate(index - 1,  weight / ( 2.0f * interval ), -1);
        }

        if( direction != -1 )
        {
            waveUpdate(index + 1,  weight / ( 2.0f * interval ) , 1);
        }
    }

    private void makeWave()
    {
        int index = (int)(player.transform.position.x - transform.position.x) / interval;

        if (index < 0 || index >= length) return;

        waveUpdate(index, waveWegiht, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "PlayerBody") return;


        if( onWaterLine & !playerCtrl.grounded )
        {
            makeWave();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "PlayerBody") return;

        playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Clamp(playerRB.velocity.y, -20.0f, 40.0f));

        playerCtrl.movingWeight = orgMovingWeight / density;
        playerRB.gravityScale = orgGravity / 5.0f;
        playerCtrl.weakJump(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "PlayerBody") return;
        

        playerCtrl.movingWeight = orgMovingWeight;
        playerRB.gravityScale = orgGravity;
        playerCtrl.weakJump(false);

        if (onWaterLine && player.transform.position.y > transform.position.y + height * transform.localScale.y)
        {
            makeWave();
        }  
    }
}
