using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireAction : MonoBehaviour
{
    private PlayerController playerCtrl;
    private GameObject player;
    private Transform wire;
    private Transform wireHook;
    private SpriteRenderer[] sprites;
    private Transform wirePosition;
    private float distance = 0;
    

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerCtrl = player.GetComponent<PlayerController>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        wire = transform.Find("Wire");
        wireHook = transform.Find("WireHook");
        wirePosition = player.transform.Find("WirePosition");
    }

    // 템플릿 메소드 *********************************************************
    // -1 탐색 실패, 0 너무 가까움 1 가능
    public int ActionWireJump()
    {
        moveAtPlayer();
        int check = checkDistance();
        if (check < 1) return check;
        modifyTransform();
        activeWire();
        addForce();

        return check;
    }
    // ********************************************************************


    private void moveAtPlayer()
    {
        transform.position = wirePosition.position;
    }

    // -1 탐색 실패, 0 너무 가까움 1 가능
    private int checkDistance()
    {
        Collider2D[] colliders;

        for( int i = 0; i < 2; i++ )
        {
            colliders = Physics2D.OverlapPointAll(new Vector2(transform.position.x + i * playerCtrl.dir,
                                                              transform.position.y + i));
            // Debug.Log(i);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Road"))
                {
                    return 0;
                }
            }
        }

        
        for( int i = 1; i < 26; i++ )
        {
            colliders = Physics2D.OverlapPointAll(new Vector2(transform.position.x + i * playerCtrl.dir,
                                                              transform.position.y + i));
            // Debug.Log(i);
            foreach( Collider2D collider in colliders )
            {
                if( collider.CompareTag("Road"))
                {
                    distance = i;
                    return 1;
                }
            }
        }

        return -1;
    }

    private void modifyTransform()
    {
        wire.localScale = new Vector3(distance * 1.4f, wire.localScale.y,
                                                    wire.localScale.z);

        transform.localScale = new Vector3( Mathf.Abs( transform.localScale.x ),
                                            transform.localScale.y,
                                            transform.localScale.z);

        wireHook.transform.position = new Vector3(transform.position.x + ( distance + 1 ) * playerCtrl.dir,
                                                transform.position.y + distance,
                                                transform.position.z);
    }

    private void activeWire()
    {
        foreach( SpriteRenderer sprite in sprites )
        {
            sprite.enabled = true;
        }
    }

    private void addForce()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(70.0f * playerCtrl.dir, 60.0f);
    }

    // 스프라이트 끄기
    public void inactiveWire()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }
    }
}
