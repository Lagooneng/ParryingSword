using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData
{
    const float SaveDataVersion = 0.30f;

    public static string SaveDate = "(non)";

    public static float SoundBGMVolume = 1.0f;
    public static float SoundSEVolume = 1.0f;
    public static int resolution = 1920;
    public static int fullScreen = 1;

    // 데이터 검사 
    static void saveDataHeader(string dataGroupName)
    {
        PlayerPrefs.SetFloat("SaveDataVersion", SaveDataVersion);
        SaveDate = System.DateTime.Now.ToString("G");
        PlayerPrefs.SetString("SaveDataDate", SaveDate);
        PlayerPrefs.SetString(dataGroupName, "on");
    }

    static bool checkSaveDataHeader(string dataGroupName)
    {
        if (!PlayerPrefs.HasKey("SaveDataVersion"))
        {
            Debug.Log("No Save Data");
            return false;
        }

        if (PlayerPrefs.GetFloat("SaveDataVersion") != SaveDataVersion)
        {
            Debug.Log("Version Error");
            return false;
        }

        if (!PlayerPrefs.HasKey(dataGroupName))
        {
            Debug.Log("No Group Data");
            return false;
        }

        SaveDate = PlayerPrefs.GetString("SaveDataDate");

        return true;
    }

    public static bool checkGamePlayData()
    {
        return checkSaveDataHeader("SDG_GamePlay");
    }

    // 플레이 데이터
    public static bool saveGamePlay()
    {
        try
        {
            saveDataHeader("SDG_GamePlay");

            DataPackingString playerData = new DataPackingString();
            playerData.add("Player_HPMAX", PlayerController.hpMax);
            playerData.add("Player_HP", PlayerController.hp);
            playerData.add("MagicCrystal_Heal", MagicCrystalManager.count);

            playerData.PlyerPrefsSetStringUTF8(
                "PlayerData", playerData.encodeDataPackingString());

            PlayerPrefs.Save();

            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("SaveData.saveGamePlay : Failed(" + e.Message + ")");
        }
        return false;
    }

    public static bool loadGamePlay()
    {
        try
        {
            if (checkSaveDataHeader("SDG_GamePlay"))
            {
                SaveDate = PlayerPrefs.GetString("SaveDataDate");

                DataPackingString playerData = new DataPackingString();
                playerData.decodeDataPackingString(
                    playerData.PlyerPrefsGetStringUTF8("PlayerData"));

                PlayerController.hpMax = (float)playerData.getData("Player_HPMAX");
                PlayerController.hp = (float)playerData.getData("Player_HP");
                MagicCrystalManager.count = (int)playerData.getData("MagicCrystal_Heal");

                return true;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("SaveData.loadGamePlay : Failed(" + e.Message + ")");
        }

        return false;
    }

    // 옵션 저장 
    public static bool saveOption()
    {
        try
        {
            saveDataHeader("SDG_Option");

            PlayerPrefs.SetFloat("SoundBGMVolume", SoundBGMVolume);
            PlayerPrefs.SetFloat("SoundSEVolume", SoundSEVolume);
            PlayerPrefs.SetInt("Resolution", resolution);
            PlayerPrefs.SetInt("FullScreen", fullScreen);

            PlayerPrefs.Save();
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("SaveData.saveOption : Failed(" + e.Message + ")");
        }

        return false;
    }

    public static bool loadOption()
    {
        try
        {
            if (checkSaveDataHeader("SDG_Option"))
            {
                SoundBGMVolume = PlayerPrefs.GetFloat("SoundBGMVolume");
                SoundSEVolume = PlayerPrefs.GetFloat("SoundSEVolume");
                resolution = PlayerPrefs.GetInt("Resolution");
                fullScreen = PlayerPrefs.GetInt("FullScreen");
                return true;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("SaveData.loadOption : Failed(" + e.Message + ")");
        }

        return false;
    }

    public static void deleteAndInit(bool init)
    {
        PlayerPrefs.DeleteAll();

        if (init)
        {
            SaveDate = "(non)";
            SoundBGMVolume = 1.0f;
            SoundSEVolume = 1.0f;
        }
    }
}
