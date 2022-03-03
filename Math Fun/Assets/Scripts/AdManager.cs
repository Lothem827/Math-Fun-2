using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{

    private InterstitialAd interstitialAd;
    private RewardedInterstitialAd rewardedAd;

    public static AdManager instance;
    public mainScript mains;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(InitializationStatus => { });
        loadRewardedAd();
    }

    private AdRequest createAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    public void RequestInterstitial()
    {
        string adUnitID = "ca-app-pub-8570623541807082/9839585592";
        if (this.interstitialAd != null)
            this.interstitialAd.Destroy();

        this.interstitialAd = new InterstitialAd(adUnitID);

        this.interstitialAd.LoadAd(this.createAdRequest());
    }

    public void showInterstitial()
    {
        if (this.interstitialAd.IsLoaded())
            interstitialAd.Show();
        else
            Debug.Log("Not Ready Yet!");
    }


    //Rewarded Ads (Double Coins)
    public void loadRewardedAd()
    {
        string adUnitID = "ca-app-pub-8570623541807082/8948705558";
        AdRequest request = new AdRequest.Builder().Build();
        RewardedInterstitialAd.LoadAd(adUnitID, request, adLoadCallback);
    }

    private void adLoadCallback(RewardedInterstitialAd arg1, AdFailedToLoadEventArgs arg2)
    {
        rewardedAd = arg1;
    }

    public void showRewarded()
    {
        if (rewardedAd != null)
            rewardedAd.Show(userEarnedRewardCallback);
    }

    private void userEarnedRewardCallback(Reward obj)
    {
        mains.receiveExtraCoins();
        Debug.Log("testReward Earned");
    }
}
