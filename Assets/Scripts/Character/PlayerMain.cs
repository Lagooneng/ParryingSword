using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    // 캐시
    PlayerController playerCtrl;

    private void Awake()
    {
        playerCtrl = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if( !playerCtrl.activeSts ) return;

        float movingH = Input.GetAxis("Horizontal");
        // Debug.Log(string.Format("{0}", movingH));

        playerCtrl.ActionMove(movingH);

        if( Input.GetKeyDown(KeyCode.Space) )
        {
            playerCtrl.ActionJump();
        }

        if( playerCtrl.actionActive )
        {
            if( Input.GetKey(KeyCode.Z) )
            {
                playerCtrl.AttackNormal();
            }
            else if( Input.GetKey(KeyCode.X) )
            {
                playerCtrl.AttackParrying();
            }
            else if( Input.GetKey(KeyCode.C) )
            {
                playerCtrl.AttackSpecial();
            }
        }

        
    }



}
