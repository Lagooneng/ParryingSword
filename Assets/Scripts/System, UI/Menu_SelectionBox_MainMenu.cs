using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_SelectionBox_MainMenu : StageTrigger_Interaction
{
    public GameObject[] btns;
    private int now = 0;
    private int btnLength;
    private Color orgButtonColor;
    private Transform pos;
    private MenuManagerWithoutPlayer menuManagerWithoutPlayer;
    private Menu_SelectionBox_Option menu_SelectionBox_Option;

    protected override void Awake()
    {
        fadeFilter = GameObject.Find("FadeFilter").GetComponent<FadeFilter>();
        menuManagerWithoutPlayer = GameObject.Find("MenuManagerWithoutPlayer").GetComponent<MenuManagerWithoutPlayer>();
        menu_SelectionBox_Option = GameObject.Find("SelectionBox_Option").GetComponent<Menu_SelectionBox_Option>();
        menuManagerWithoutPlayer.setInteractingObj(this);
    }

    private void Start()
    {
        btnLength = btns.Length;
        orgButtonColor = btns[0].GetComponent<SpriteRenderer>().color;
        updateButtonSelection();
        pos = GameObject.Find("Main Camera").transform;
        fadeFilter.fadeIn();
    }

    public override void buttonYes()
    {
        if (now == 0)
        {
            fadeFilter.fadeOut();
            Invoke("btn0", 1.0f);
        }
        else if (now == 1)
        {
            btn1();
        }
        else if (now == 2)
        {
            btn2();
        }
        else if (now == 3)
        {
            menuManagerWithoutPlayer.setInteractingObj(null);
            fadeFilter.fadeOut();
            Invoke("btn3", 1.0f);
        }
    }

    private void btn0()
    {
        if( SaveData.checkGamePlayData() )
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("WatingArea");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
        }
        
    }

    private void btn1()
    {
        menu_SelectionBox_Option.transform.position = transform.position;
        transform.position = new Vector3(-400.0f, -400.0f, 0.0f);
        menuManagerWithoutPlayer.setInteractingObj(menu_SelectionBox_Option);
    }

    private void btn2()
    {
        quitGame();
    }

    private void btn3()
    {
        SaveData.deleteAndInit(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
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

    private void reLoad()
    {
        
    }
}
