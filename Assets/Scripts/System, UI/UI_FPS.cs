using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FPS : MonoBehaviour
{
    public int fontSize = 50;
    public float width = 150.0f, height = 100.0f;

    private void Awake()
    {
        // Application.targetFrameRate = 60;
    }

    private void OnGUI()
    {
        float fps = 1.0f / Time.deltaTime;
        //Debug.Log(fps);
        string text = string.Format("{0:N1} FPS", fps);

        Rect rect = new Rect(width, height, Screen.width, Screen.height);

        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;

        GUI.Label(rect, text, style);

    }
}
