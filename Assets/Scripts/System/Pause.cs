using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pause
{
    private static float prevTimescale = 1.0f;

    public static void gamePause()
    {
        prevTimescale = Time.timeScale;
        Time.timeScale = 0.0f;
    }

    public static void gamePlay()
    {
        Time.timeScale = prevTimescale;
    }
}
