using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTrigger_SpecialGate : StageTrigger_Interaction
{
    public GameObject menu;
    public GameObject[] buttons;

    
    private Color orgButtonColor;
    private bool menuOpened = false;
    private int buttonLength;
    private int nowButton = 0;
    private string nextSceneName;
    
    private void Start()
    {
        buttonLength = buttons.Length;
        orgButtonColor = buttons[0].GetComponent<SpriteRenderer>().color;
        updateButtonSelection();
    }

    public override void buttonYes()
    {
        if( !menuOpened )
        {
            menuManager.pause();
            openMenu();
            return;
        }

        switch( nowButton )
        {
            case 0:
                nextSceneName = "Forest";
                menuManager.undoPause();
                closeMenu();
                fadeFilter.fadeOut();

                Invoke("sceneChange", 1.0f);

                break;
            default:
                break;
        }

    }

    public override void buttonNo()
    {
        menuManager.undoPause();
        closeMenu();
    }

    public override void buttonESC()
    {
        closeMenu();
    }

    public override void buttonDown()
    {
        if( nowButton + 1 < buttonLength )
        {
            nowButton += 1;
        }

        updateButtonSelection();
    }

    public override void buttonUp()
    {
        if (nowButton - 1 > -1)
        {
            nowButton -= 1;
        }

        updateButtonSelection();
    }


    private void updateButtonSelection()
    {
        for( int i = 0; i < buttonLength; i++ )
        {
            if( i == nowButton )
            {
                buttons[i].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                buttons[i].GetComponent<SpriteRenderer>().color = orgButtonColor;
            }
        }
    }

    private void openMenu()
    {
        menu.transform.position = new Vector3(  Camera.main.transform.position.x,
                                                Camera.main.transform.position.y,
                                                0.0f );
        menuOpened = true;
    }

    private void closeMenu()
    {
        menu.transform.position = new Vector3(-100.0f, -30.0f, 0.0f);
        menuOpened = false;
    }

    private void sceneChange()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
