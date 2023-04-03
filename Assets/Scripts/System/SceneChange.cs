using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public string sceneName;

    public void moveNextScene()
    {
        if (sceneName == "") return;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
