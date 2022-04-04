using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCache : MonoBehaviour
{
    public void SetCoinCount(int coinCount)
    {
        PlayerPrefs.SetInt("TotalCoin", coinCount);
        PlayerPrefs.Save();
    }

    public int GetCoinCount()
    {
        return PlayerPrefs.GetInt("TotalCoin");
    }

    public void SetGemCount(int gemCount)
    {
        PlayerPrefs.SetInt("TotalGem", gemCount);
        PlayerPrefs.Save();
    }

    public int GetGemCount()
    {
        return PlayerPrefs.GetInt("TotalGem");
    }

    public void SetHighScore(int highScore)
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }

    public void SetSkinID(int skinID)
    {
        PlayerPrefs.SetInt("SkinID", skinID);
        PlayerPrefs.Save();
    }

    public int GetSkinID()
    {
        return PlayerPrefs.GetInt("SkinID");
    }

    public void SetName(string name)
    {
        PlayerPrefs.SetString("Name", name);
        PlayerPrefs.Save();
    }

    public string GetName()
    {
        return PlayerPrefs.GetString("Name");
    }

    public void SetClothInfo(int name, int value)
    {
        PlayerPrefs.SetInt(name.ToString(), value);
        PlayerPrefs.Save();
    }

    public int GetClothInfo(int name)
    {
        return PlayerPrefs.GetInt(name.ToString());
    }

    public void SetVolumeLevel(float value)
    {
        PlayerPrefs.SetFloat("MusicLevel", value);
        PlayerPrefs.Save();
    }

    public float GetVolumeLevel()
    {
        return PlayerPrefs.GetFloat("MusicLevel");
    }

    public void SetSliderValue(float value)
    {
        PlayerPrefs.SetFloat("SliderValue", value);
        PlayerPrefs.Save();
    }

    public float GetSliderValue()
    {
        return PlayerPrefs.GetFloat("SliderValue");
    }

    public void SetBetaInfoPanel(int value)
    {
        PlayerPrefs.SetInt("BetaInfo", value);
        PlayerPrefs.Save();
    }

    public int GetBetaInfoPanel()
    {
        return PlayerPrefs.GetInt("BetaInfo");
    }

    public void SetMusicOnOff(int value)
    {
        PlayerPrefs.SetInt("Music", value);
        PlayerPrefs.Save();
    }

    public int GetMusicOnOff()
    {
        return PlayerPrefs.GetInt("Music");
    }
}
