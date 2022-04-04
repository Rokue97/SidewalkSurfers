using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    /*
    [SerializeField] GameManager gameManager;

    string appID = "ca-app-pub-4739284234344311~8539259403";
    private string _fullScreenAdId = "ca-app-pub-4739284234344311/4681218522";
    private string _rewardedAdId = "ca-app-pub-4739284234344311/6746831434";
    private InterstitialAd _fullscreenAd;
    private RewardedAd _rewardedAd;
    private void Start()
    {
        MobileAds.Initialize(appID);
        RequestInterstitialAd();
        RequestRewardedAd();
    }
    
    void RequestInterstitialAd()
    {
        
        _fullscreenAd = new InterstitialAd(_fullScreenAdId);       

        AdRequest adRequest = new AdRequest.Builder().Build();
        _fullscreenAd.LoadAd(adRequest);
    }

    public void ShowInterstitialAd()
    {       
        if (_fullscreenAd.IsLoaded())
        {
            _fullscreenAd.Show();
        }
        else
        {
            Debug.Log("FullScreenAd daha yüklenmedi!!");
        }
        RequestInterstitialAd();
    }

    public void RequestRewardedAd()
    {
        this._rewardedAd = new RewardedAd(_rewardedAdId);

        // Called when an ad request has successfully loaded.
        _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        _rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        _rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(request);
    }

    public void UserChoseToWatchAd()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }
        else
        {
            RequestRewardedAd();
            _rewardedAd.Show();
        }
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
        gameManager.InfoPanel("Please Try Again Later.", "Warning!");
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        gameManager.ResumeWithAd();
    }
    */
}
