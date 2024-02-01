using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_SelectionBox_Option : StageTrigger_Interaction
{
    public GameObject[] btns;
    private int now = 0;
    private int btnLength;
    private Color orgButtonColor;
    private Transform pos;
    private MenuManagerWithoutPlayer menuManagerWithoutPlayer;
    private Menu_SelectionBox_MainMenu menu_SelectionBox_MainMenu;
    private int soundBGMVolume;
    private int soundSEVolume;
    public GameObject bgm, se, resol, fullScreen;
    private TextMesh bgmText, seText, resolText, fullScreenText;
    private DisplaySetting display;
    private FullScreenMode fullScreenMode = FullScreenMode.ExclusiveFullScreen;

    private SoundController soundBGMController, soundSEController;

    protected override void Awake()
    {
        fadeFilter = GameObject.Find("FadeFilter").GetComponent<FadeFilter>();
        menuManagerWithoutPlayer = GameObject.Find("MenuManagerWithoutPlayer").GetComponent<MenuManagerWithoutPlayer>();
        menu_SelectionBox_MainMenu = GameObject.Find("SelectionBox_MainMenu").GetComponent<Menu_SelectionBox_MainMenu>();
        bgmText = bgm.GetComponent<TextMesh>();
        seText = se.GetComponent<TextMesh>();
        resolText = resol.GetComponent<TextMesh>();
        fullScreenText = fullScreen.GetComponent<TextMesh>();
        soundBGMController = GameObject.FindGameObjectWithTag("BGM").GetComponent<SoundController>();
        soundSEController = GameObject.Find("SE").GetComponent<SoundController>();
        display = GameObject.Find("Resolution").GetComponent<DisplaySetting>();
        SaveData.loadOption();
        soundBGMVolume = (int)(SaveData.SoundBGMVolume * 10);
        soundSEVolume = (int)(SaveData.SoundSEVolume * 10);
        bgmText.text = soundBGMVolume.ToString();
        seText.text = soundSEVolume.ToString();

        if (SaveData.fullScreen == 1)
        {
            fullScreenMode = FullScreenMode.MaximizedWindow;
            // fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            fullScreenMode = FullScreenMode.Windowed;
        }
    }

    private void Start()
    {
        btnLength = btns.Length;
        orgButtonColor = btns[0].GetComponent<SpriteRenderer>().color;
        updateButtonSelection();
    }

    public override void buttonRight()
    {
        if (now == 0)
        {
            soundBGMVolume += 1;
            if (soundBGMVolume > 10) soundBGMVolume = 10;
            SaveData.SoundBGMVolume = ((float)soundBGMVolume / 10);
            bgmText.text = soundBGMVolume.ToString();
            soundBGMController.updateVolume();
        }
        else if (now == 1)
        {
            soundSEVolume += 1;
            if (soundSEVolume > 10) soundSEVolume = 10;
            SaveData.SoundSEVolume = ((float)soundSEVolume / 10);
            seText.text = soundSEVolume.ToString();
            soundSEController.updateVolume();
            soundSEController.playClip();
        }
        else if (now == 2)
        {
            if (display.resolNum == 0)
            {
                if (display.orgWidth < 1920) return;

                display.resolNum++;
                display.setResol(ResolutionList.r1920, fullScreenMode);
                resolText.text = "1920 x 1080";
                SaveData.resolution = 1920;
            }
            else if (display.resolNum == 1)
            {
                if (display.orgWidth < 2560) return;
                display.resolNum++;
                display.setResol(ResolutionList.r2560, fullScreenMode);
                resolText.text = "2560 x 1440";
                SaveData.resolution = 2560;
            }
            else if (display.resolNum == 2)
            {
                if (display.orgWidth < 3840) return;
                display.resolNum++;
                display.setResol(ResolutionList.r3840, fullScreenMode);
                resolText.text = "3840 x 2160";
                SaveData.resolution = 3840;
            }
        }
        else if (now == 3)
        {
            if (SaveData.fullScreen == 1)
            {
                SaveData.fullScreen = 0;
                fullScreenMode = FullScreenMode.Windowed;
                fullScreenText.text = "Off";
            }
            else
            {
                SaveData.fullScreen = 1;
                fullScreenMode = FullScreenMode.MaximizedWindow;
                // fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                fullScreenText.text = "On";
            }
        }
    }

    public override void buttonLeft()
    {
        if (now == 0)
        {
            soundBGMVolume -= 1;
            if (soundBGMVolume < 0.0f) soundBGMVolume = 0;
            SaveData.SoundBGMVolume = ((float)soundBGMVolume / 10);
            bgmText.text = soundBGMVolume.ToString();
            soundBGMController.updateVolume();
        }
        else if (now == 1)
        {
            soundSEVolume -= 1;
            if (soundSEVolume < 0) soundSEVolume = 0;
            SaveData.SoundSEVolume = ((float)soundSEVolume / 10);
            seText.text = soundSEVolume.ToString();
            soundSEController.updateVolume();
            soundSEController.playClip();
        }
        else if (now == 2)
        {
            if (display.resolNum == 1)
            {
                display.resolNum--;
                display.setResol(ResolutionList.r1280, fullScreenMode);
                resolText.text = "1280 x 720";
                SaveData.resolution = 1280;
            }
            else if (display.resolNum == 2)
            {
                display.resolNum--;
                display.setResol(ResolutionList.r1920, fullScreenMode);
                resolText.text = "1920 x 1080";
                SaveData.resolution = 1920;
            }
            else if (display.resolNum == 3)
            {
                display.resolNum--;
                display.setResol(ResolutionList.r2560, fullScreenMode);
                resolText.text = "2560 x 1440";
                SaveData.resolution = 2560;
            }
        }
        else if (now == 3)
        {
            if (SaveData.fullScreen == 1)
            {
                SaveData.fullScreen = 0;
                fullScreenMode = FullScreenMode.Windowed;
                fullScreenText.text = "Off";
            }
            else
            {
                SaveData.fullScreen = 1;
                fullScreenMode = FullScreenMode.MaximizedWindow;
                // fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                fullScreenText.text = "On";
            }
        }
    }

    public override void buttonNo()
    {
        menu_SelectionBox_MainMenu.transform.position = transform.position;
        transform.position = new Vector3(-400.0f, -400.0f, 0.0f);
        menuManagerWithoutPlayer.setInteractingObj(menu_SelectionBox_MainMenu);

        if (display.resolNum == 0)
        {
            display.setResol(ResolutionList.r1280, fullScreenMode);
        }
        else if (display.resolNum == 1)
        {
            display.setResol(ResolutionList.r1920, fullScreenMode);
        }
        else if (display.resolNum == 2)
        {
            display.setResol(ResolutionList.r2560, fullScreenMode);
        }
        else if (display.resolNum == 3)
        {
            display.setResol(ResolutionList.r3840, fullScreenMode);
        }

        SaveData.saveOption();
    }

    public override void buttonUp()
    {
        now = now - 1 < 0 ? 3 : now - 1;
        updateButtonSelection();
    }

    public override void buttonDown()
    {
        now = (now + 1) % 4;
        updateButtonSelection();
    }

    private void updateButtonSelection()
    {
        for (int i = 0; i < btnLength; i++)
        {
            if (i != now)
            {
                btns[i].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.3f);
            }
            else
            {
                btns[i].GetComponent<SpriteRenderer>().color = orgButtonColor;
            }
        }
    }
}
