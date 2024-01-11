using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_SelectionBoxDead : StageTrigger_Interaction
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
        pos = GameObject.Find("Main Camera").transform;
        playerCtrl = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (playerCtrl.hp > 0) return;
        transform.position = new Vector3(pos.position.x, pos.position.y, 0.0f);
    }

    public override void buttonYes()
    {
        if( now == 0 )
        {
            fadeFilter.fadeOut();
            Invoke("btn0", 1.0f);
        }
        else if( now == 1 )
        {
            fadeFilter.fadeOut();
            Invoke("btn1", 1.0f);
        }
        else if( now == 2 )
        {
            fadeFilter.fadeOut();
            Invoke("btn2", 1.0f);
        }
        else if( now == 3 )
        {
            btn3();
        }
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Forest1");
    }

    private void btn1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("WatingArea");
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
}
