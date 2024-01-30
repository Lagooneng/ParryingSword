using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResolutionList
{
    r1280,
    r1920,
    r2560,
    r3840,
    NON
}

public class DisplaySetting : MonoBehaviour
{
    public int resolNum = 1;
    public readonly int orgWidth = Screen.width;

    private void Awake()
    {
        Debug.Log(orgWidth);
        Application.targetFrameRate = 144;
        Screen.SetResolution(SaveData.resolution, SaveData.resolution / 16 * 9, FullScreenMode.MaximizedWindow);
    }

    public void setResol( ResolutionList resolution, FullScreenMode screenMode )
    {
        switch( resolution )
        {
            case ResolutionList.r1280:
                Screen.SetResolution(1280, 720, screenMode);
                break;
            case ResolutionList.r1920:
                Screen.SetResolution(1920, 1080, screenMode);
                break;
            case ResolutionList.r2560:
                Screen.SetResolution(2560, 1440, screenMode);
                break;
            case ResolutionList.r3840:
                Screen.SetResolution(3840, 2160, screenMode);
                break;
        }
    }
}
