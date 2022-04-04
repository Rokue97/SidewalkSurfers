using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource myAudio;
    [SerializeField] GameObject buttonCredits, buttonFAQ, buttonResume, buttonManinMenu, musicOnButton, musicOffButton;
    [SerializeField] GameManager gameManager;
    [SerializeField] Player player;
    [SerializeField] AdsManager adsManager;
    [SerializeField] LocalCache cache;
    [SerializeField] Slider slider;

    public GameObject settingsMenu;

    bool timer = false;
    public bool startTimer = false;

    private void Start()
    {
        mixer.SetFloat("MusicVol", cache.GetVolumeLevel());
        slider.value = cache.GetSliderValue();
        CheckMusicPlaying();
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        cache.SetVolumeLevel(Mathf.Log10(sliderValue) * 20);
        cache.SetSliderValue(sliderValue);
    }

    public void OnClickOpenSettings(int id)
    {
        //adsManager.ShowInterstitialAd();
        if(id == 1)
        {
            Time.timeScale = 0;
            buttonCredits.SetActive(false);
            buttonFAQ.SetActive(false);
            buttonManinMenu.SetActive(true);
            buttonResume.SetActive(true);
            player.isGamePaused = true;
            timer = true;
        }
        else if(id == 0)
        {
            buttonCredits.SetActive(true);
            buttonFAQ.SetActive(true);
            buttonManinMenu.SetActive(false);
            buttonResume.SetActive(false);
            timer = false;
        }
        
        settingsMenu.SetActive(true);
    }

    public void OnClickCloseSettings()
    {        
        settingsMenu.SetActive(false);
        player.isGamePaused = false;

        if (timer)
            gameManager.Timer();
        else
            Time.timeScale = 1;
    }

    public void OnClickStopMusic()
    {
        myAudio.Stop();
        cache.SetMusicOnOff(1);
    }

    public void OnClickStartMusic()
    {
        myAudio.Play();
        cache.SetMusicOnOff(0);
    }

    public void CheckMusicPlaying()
    {
        if (cache.GetMusicOnOff() == 1)
        {
            myAudio.Stop();
            musicOffButton.SetActive(true);
            musicOnButton.SetActive(false);
        }
        else
        {
            myAudio.Play();
            musicOffButton.SetActive(false);
            musicOnButton.SetActive(true);
        }
    }


}

