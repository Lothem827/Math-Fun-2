using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class AdManager : MonoBehaviour
{
    public Button playAd;
    private RewardedAd rewardedAd;

    private void Awake()
    {
        LoadRewardedAd();
    }

    public void Start()
    {
        MobileAds.Initialize(initStatus => { });
        //MobileAds.Initialize(initStatus => { Debug.Log("[TestAd] " + initStatus); });
        LoadRewardedAd();
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {

    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        this.LoadRewardedAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Debug.Log("Worked");
        playAd.interactable = false;
    }

    public void LoadRewardedAd()
    {
        string adUnitId;
            #if UNITY_ANDROID
                    adUnitId = "ca-app-pub-3940256099942544/5224354917";
            #elif UNITY_IPHONE
                                adUnitId = "ca-app-pub-3940256099942544/5224354917";
            #else
                                adUnitId = "unexpected_platform";
            #endif

        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        //this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
      //  this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
       // this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

    }

    public void ShowRewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
            this.rewardedAd.Show();
    }

}
