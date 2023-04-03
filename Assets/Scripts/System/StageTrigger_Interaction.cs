using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTrigger_Interaction : MonoBehaviour
{
    protected PlayerMain playerMain;

    protected virtual void Awake()
    {
        playerMain = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "PlayerBody" )
        {
            playerMain.setInteraction(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBody")
        {
            playerMain.setInteraction(false);
        }
    }

}
