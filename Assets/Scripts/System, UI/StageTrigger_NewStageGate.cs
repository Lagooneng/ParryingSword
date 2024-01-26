using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTrigger_NewStageGate : StageTrigger_Interaction
{
    public string nextSceneName;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void buttonYes()
    {
        if (nextSceneName == "") return;

        fadeFilter.fadeOut();
        fadeSound.fadeOut();
        Invoke("sceneChange", 1.7f);
    }


    private void sceneChange()
    {
        SaveData.saveGamePlay();
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
