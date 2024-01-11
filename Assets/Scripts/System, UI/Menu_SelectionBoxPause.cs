using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_SelectionBoxPause : StageTrigger_Interaction
{
    public GameObject[] btns;
    private int now = 0;
    private int btnLength;
    private Color orgButtonColor;
    private Transform pos;
    private PlayerController playerCtrl;

    private void Start()
    {
        btnLength = btns.Length;
        orgButtonColor = btns[0].GetComponent<SpriteRenderer>().color;
        updateButtonSelection();
        playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pos = GameObject.Find("Main Camera").transform;
    }

    public override void buttonYes()
    {
        if( now == 0 )
        {
            btn0();
        }
        else if( now == 1 )
        {
            playerCtrl.superMode = true;
            Time.timeScale = 1.0f;
            fadeFilter.fadeOut();
            Invoke("btn1", 1.0f);
        }
        else if( now == 2 )
        {
            playerCtrl.superMode = true;
            Time.timeScale = 1.0f;
            fadeFilter.fadeOut();
            Invoke("btn2", 1.0f);
        }
        else if( now == 3 )
        {
            playerCtrl.superMode = true;
            Time.timeScale = 1.0f;
            btn3();
        }
    }

    public override void buttonNo()
    {
        undoPause();
    }

    public override void buttonUp()
    {
        now = now - 1 < 0 ? 3 : now - 1;
        updateButtonSelection();
    }

    public override void buttonDown()
    {
        now = (now + 1) % 4;
        updateButtonSelection();
    }

    private void btn0()
    {
        undoPause();
    }

    private void btn1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Forest1");
    }

    private void btn2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void btn3()
    {
        quitGame();
    }

    private void updateButtonSelection()
    {
        for (int i = 0; i < btnLength; i++)
        {
            if (i != now)
            {
                btns[i].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.3f);
            }
            else
            {
                btns[i].GetComponent<SpriteRenderer>().color = orgButtonColor;
            }
        }
    }

    public void pause()
    {
        Time.timeScale = 0.0f;
        playerCtrl.actionActive = false;
        transform.position = new Vector3(pos.position.x, pos.position.y, 0.0f);
    }

    public void undoPause()
    {
        Time.timeScale = 1.0f;
        playerCtrl.actionActive = true;
        transform.position = new Vector3(-400.0f, -400.0f, 0.0f);
    }
}
