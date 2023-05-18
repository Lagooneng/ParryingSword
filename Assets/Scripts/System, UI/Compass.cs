using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 서브 카메라 하위에서 움직임, 가로 반경 17 세로 반경 10

public class Compass : MonoBehaviour
{
    private Transform normalGate;
    private Transform player;
    private SpriteRenderer sprite;
    private float inclination;
    private Vector3 normalGatePos;


    // Awake 단계에서 맵이 생성되니 Start에서 찾아야 함
    private void Start()
    {
        GameObject gateobject;
        gateobject = GameObject.Find("NormalGate");

        if (!gateobject)
        {
            this.gameObject.SetActive(false);
            return;
        }

        normalGate = gateobject.transform;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").transform; 
    }


    private void FixedUpdate()
    {
        if( Mathf.Abs( normalGate.position.x - player.position.x ) < 19.0 &&
            Mathf.Abs( normalGate.position.y - player.position.y ) < 12.0f )
        {
            sprite.enabled = false;
        }
        else
        {
            sprite.enabled = true;
        }

        inclination = getInclination();

        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f,
                            Mathf.Atan2(    normalGate.position.y - player.position.y,
                                            normalGate.position.x - player.position.x)  * Mathf.Rad2Deg - 90.0f ));
        // Debug.Log(Vector2.Angle(player.position, normalGate.position));

        if (inclination > 10.0f / 17.0f || inclination < - (10.0f / 17.0f))  // 기울기가 커서 직선이 위아래를 지나가는 경우
        {
            if (player.position.y < normalGate.position.y)   // 위
            {
                transform.position = new Vector3(xPosfunctionForCeiling() + transform.parent.position.x,
                                                10.0f + transform.parent.position.y, transform.position.z);
            }
            else // 아래
            {
                transform.position = new Vector3(xPosfunctionForBottom() + transform.parent.position.x,
                                                -10.0f + transform.parent.position.y, transform.position.z);
            }
        }
        else                        // 기울기가 작아서 직선이 좌우를 지나가는 경우
        {
            if (player.position.x < normalGate.position.x)   // 오른쪽
            {
                transform.position = new Vector3(17.0f + transform.parent.position.x,
                                                yPosFunctionForRight() + transform.parent.position.y, transform.position.z);
            }
            else // 왼쪽
            {
                transform.position = new Vector3(-17.0f + transform.parent.position.x,
                                                yPosFunctionForLeft() + transform.parent.position.y, transform.position.z);
            }
        }
    }



    private float getInclination()
    {
        float denominator = normalGate.position.x - player.position.x;
        float numerator = normalGate.position.y - player.position.y;

        if( numerator == 0.0f )
        {
            return 0.01f;
        }

        if ( denominator == 0.0f)
        {
            denominator = 0.01f;
        }

        return numerator / denominator;
    }

    private float xPosfunctionForCeiling()
    {
        return (player.position.y + 10 - normalGate.position.y) /
            inclination + normalGate.position.x - player.position.x;
    }

    private float xPosfunctionForBottom()
    {
        return (player.position.y - 10 - normalGate.position.y) /
            inclination + normalGate.position.x - player.position.x;
    }

    private float yPosFunctionForRight()
    {
        return inclination * (player.position.x + 17 - normalGate.position.x) +
            normalGate.position.y + transform.parent.position.y - player.position.y;
    }

    private float yPosFunctionForLeft()
    {
        return inclination * (player.position.x - 17 - normalGate.position.x) +
            normalGate.position.y - player.position.y;
    }
}