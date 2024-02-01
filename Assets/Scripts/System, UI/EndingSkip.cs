using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSkip : MonoBehaviour
{
    private FadeFilter fadeFilter;

    private void Awake()
    {
        fadeFilter = GameObject.Find("FadeFilter").GetComponent<FadeFilter>();
    }

    private void Update()
    {
        if( Input.GetKeyDown(KeyCode.X) )
        {
            fadeFilter.fadeOut();
            Invoke("sceneChange", 1.0f);
        }
    }

    private void sceneChange()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
