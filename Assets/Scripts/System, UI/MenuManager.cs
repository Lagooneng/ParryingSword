using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private PlayerController playerCtrl;
    private PlayerMain playerMain;
    private StageTrigger_Interaction interactingObject;

    private void Awake()
    {
        playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerMain = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
        interactingObject = null;
    }

    private void Update()
    {
        interactingObject = playerMain.interactingObject;
        if (interactingObject == null) return;

        if( Input.GetKeyDown(KeyCode.X) )
        {
            interactingObject.buttonYes();
        }
        else if( Input.GetKeyDown(KeyCode.Z) )
        {
            interactingObject.buttonNo();
        }
        else if( Input.GetKeyDown(KeyCode.Escape) )
        {
            interactingObject.buttonESC();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            interactingObject.buttonRight();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            interactingObject.buttonLeft();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            interactingObject.buttonDown();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            interactingObject.buttonUp();
        }

    }

    public void pause()
    {
        Time.timeScale = 0.0f;
        playerCtrl.actionActive = false;
    }

    public void undoPause()
    {
        Time.timeScale = 1.0f;
        playerCtrl.actionActive = true;
    }
}