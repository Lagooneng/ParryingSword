using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTrigger_NormalGate : StageTrigger_Interaction
{
    private SceneChange sceneChange;

    protected override void Awake()
    {
        base.Awake();
        sceneChange = GetComponent<SceneChange>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.X) && playerMain.getInteraction())
        {
            sceneChange.moveNextScene();
        }
    }
}
