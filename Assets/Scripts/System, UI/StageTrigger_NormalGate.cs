using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTrigger_NormalGate : MonoBehaviour
{
    public string nextSceneName;
    private FadeFilter fadeFilter;

    private void Awake()
    {
        fadeFilter = GameObject.Find("FadeFilter").GetComponent<FadeFilter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "PlayerBody" )
        {
            if (nextSceneName == "") return;

            fadeFilter.fadeOut();
            Invoke("sceneChange", 1.7f);
        }
    }

    private void sceneChange()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
