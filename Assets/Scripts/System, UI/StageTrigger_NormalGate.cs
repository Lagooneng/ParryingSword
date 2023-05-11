using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTrigger_NormalGate : StageTrigger_Interaction
{
    public string nextSceneName;

    public override void buttonYes()
    {
        if (nextSceneName == "") return;

        fadeFilter.fadeOut();
        Invoke("sceneChange", 1.0f);
    }

    private void sceneChange()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
