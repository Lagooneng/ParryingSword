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
    public GameObject bgm, se;
    private TextMesh bgmText, seText;

    private SoundController soundBGMController;

    protected override void Awake()
    {
        fadeFilter = GameObject.Find("FadeFilter").GetComponent<FadeFilter>();
        menuManagerWithoutPlayer = GameObject.Find("MenuManagerWithoutPlayer").GetComponent<MenuManagerWithoutPlayer>();
        menu_SelectionBox_MainMenu = GameObject.Find("SelectionBox_MainMenu").GetComponent<Menu_SelectionBox_MainMenu>();
        bgmText = bgm.GetComponent<TextMesh>();
        seText = se.GetComponent<TextMesh>();
        soundBGMController = GameObject.FindGameObjectWithTag("BGM").GetComponent<SoundController>();

        SaveData.loadOption();
        soundBGMVolume = (int)(SaveData.SoundBGMVolume * 10);
        soundSEVolume = (int)(SaveData.SoundSEVolume * 10);
        bgmText.text = soundBGMVolume.ToString();
        seText.text = soundSEVolume.ToString();
    }

    private void Start()
    {
        btnLength = btns.Length;
        orgButtonColor = btns[0].GetComponent<SpriteRenderer>().color;
        updateButtonSelection();
    }

    public override void buttonRight()
    {
        if( now == 0 )
        {
            soundBGMVolume += 1;
            if (soundBGMVolume > 10) soundBGMVolume = 10;
            SaveData.SoundBGMVolume = ((float)soundBGMVolume / 10);
            bgmText.text = soundBGMVolume.ToString();
        }
        else if( now == 1 )
        {
            soundSEVolume += 1;
            if (soundSEVolume > 10) soundSEVolume = 10;
            SaveData.SoundSEVolume = ((float)soundSEVolume / 10);
            seText.text = soundSEVolume.ToString();
        }
        soundBGMController.updateVolume();
    }

    public override void buttonLeft()
    {
        if (now == 0)
        {
            soundBGMVolume -= 1;
            if (soundBGMVolume < 0.0f) soundBGMVolume = 0;
            SaveData.SoundBGMVolume = ((float)soundBGMVolume / 10);
            bgmText.text = soundBGMVolume.ToString();
        }
        else if (now == 1)
        {
            soundSEVolume -= 1;
            if (soundSEVolume < 0) soundSEVolume = 0;
            SaveData.SoundSEVolume = ((float)soundSEVolume / 10);
            seText.text = soundSEVolume.ToString();
        }
        soundBGMController.updateVolume();
    }

    public override void buttonNo()
    {
        menu_SelectionBox_MainMenu.transform.position = transform.position;
        transform.position = new Vector3(-400.0f, -400.0f, 0.0f);
        menuManagerWithoutPlayer.setInteractingObj(menu_SelectionBox_MainMenu);

        SaveData.saveOption();
    }

    public override void buttonUp()
    {
        now = now - 1 < 0 ? 1 : now - 1;
        updateButtonSelection();
    }

    public override void buttonDown()
    {
        now = (now + 1) % 2;
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
