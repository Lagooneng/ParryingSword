using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTrigger_Interaction : MonoBehaviour
{
    protected PlayerMain playerMain;
    protected MenuManager menuManager;
    protected FadeFilter fadeFilter;

    protected virtual void Awake()
    {
        playerMain = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        fadeFilter = GameObject.Find("FadeFilter").GetComponent<FadeFilter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "PlayerBody" )
        {
            playerMain.setInteraction(true);
            playerMain.interactingObject = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBody")
        {
            playerMain.setInteraction(false);
            playerMain.interactingObject = null;
        }
    }

    public virtual void buttonYes() { }
    public virtual void buttonNo() { }
    public virtual void buttonESC() { }

    public virtual void buttonRight() { }
    public virtual void buttonLeft() { }
    public virtual void buttonUp() { }
    public virtual void buttonDown() { }

    public void quitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
