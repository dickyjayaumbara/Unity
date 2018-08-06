using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class AdmobManager : MonoBehaviour {

    public BannerView bannerView;
    public InterstitialAd interstitial;
    public InterstitialAd interstitialBonus;
    private RewardBasedVideoAd rewardBasedVideo;

    public Text traceText; //for trace message
    public string appId = "ca-app-pub-xxxxxxx";
    public string adBannerUnitId = "ca-app-pub-xxxxxxx";
    public string adInterstitialUnitId = "ca-app-xxxxxxx";
    public string adRewardedUnitId = "ca-app-pub-xxxxxxx";

	// Use this for initialization
	void Start () {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        //use this if u need rewardedvideo
        /*// Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #region banner admob
    public void RequestBanner()
    {
        if (bannerView != null)
            DestroyBannerView();

        //AdSize adSize = new AdSize(Screen.width, 110);
        bannerView = new BannerView(adBannerUnitId, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        bannerView.OnAdLoaded += HandleOnAdLoadedBan;
        // Called when an ad request failed to load.
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoadBan;
        // Called when an ad is clicked.
        bannerView.OnAdOpening += HandleOnAdOpenedBan;
        // Called when the user returned from the app after an ad click.
        bannerView.OnAdClosed += HandleOnAdClosedBan;
        // Called when the ad click caused the user to leave the application.
        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplicationBan;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
        ShowBannerView();
    }

    public void ShowBannerView()
    {
        bannerView.Show();
    }

    public void DestroyBannerView()
    {
        bannerView.Destroy();
    }

    public void HandleOnAdLoadedBan(object sender, EventArgs args)
    {
        traceText.text += "banner : loaded \n";
    }

    public void HandleOnAdFailedToLoadBan(object sender, AdFailedToLoadEventArgs args)
    {
        traceText.text += "banner : failed " + args.Message + "\n";

        DestroyBannerView();
    }

    public void HandleOnAdOpenedBan(object sender, EventArgs args)
    {
        traceText.text += "banner : open \n";
    }

    public void HandleOnAdClosedBan(object sender, EventArgs args)
    {
        traceText.text += "banner : clsoed \n";
        DestroyBannerView();
    }

    public void HandleOnAdLeavingApplicationBan(object sender, EventArgs args)
    {
        traceText.text += "banner : close application \n";
        DestroyBannerView();
    }

    #endregion

    #region interstitial  admob
    public void RequestInterstitial()
    {
        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adInterstitialUnitId);

        //insterstitial
        // Called when an ad request has successfully loaded.
        interstitial.OnAdLoaded += HandleOnAdLoadedIn;
        // Called when an ad request failed to load.
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoadIn;
        // Called when an ad is shown.
        interstitial.OnAdOpening += HandleOnAdOpenedIn;
        // Called when the ad is closed.
        interstitial.OnAdClosed += HandleOnAdClosedIn;
        // Called when the ad click caused the user to leave the application.
        interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplicationIn;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            traceText.text += "interstitial : proses show \n";
            interstitial.Show();
        }
        else
        {
            traceText.text += "loaded is false \n" ;
        }
    }

    public void DestroyInterstitial()
    {
        interstitial.Destroy();
    }

    public void HandleOnAdLoadedIn(object sender, EventArgs args)
    {
        traceText.text += "interstitial : loaded \n";
        //ShowInterstitial();
    }

    public void HandleOnAdFailedToLoadIn(object sender, AdFailedToLoadEventArgs args)
    {
        traceText.text += "interstitial : failed - " + args.Message + "\n";
        DestroyInterstitial();
    }

    public void HandleOnAdOpenedIn(object sender, EventArgs args)
    {
        traceText.text += "interstitial : open \n";
    }

    public void HandleOnAdClosedIn(object sender, EventArgs args)
    {
        traceText.text += "interstitial : close \n";
        DestroyInterstitial();
    }

    public void HandleOnAdLeavingApplicationIn(object sender, EventArgs args)
    {
        traceText.text += "interstitial : cose application \n";
        DestroyInterstitial();
    }
    #endregion

    #region rewarded video  admob
    public void RequestRewardBasedVideo()
    {
        traceText.text += "request reward video \n";

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adRewardedUnitId);

        ShowRewardedVideoAdmob();
    }

    public void ShowRewardedVideoAdmob()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            traceText.text += "rewarded : proses show \n";
            this.rewardBasedVideo.Show();
        }
        else
        {
            traceText.text += "loaded is false \n";
        }
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        traceText.text += "rewarded : loaded \n";
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        traceText.text += "rewarded : failed - " + args.Message + "\n";
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        traceText.text += "rewarded : open \n";
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        traceText.text += "rewarded : started \n";
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        traceText.text += "rewarded : closed \n";
        RequestRewardBasedVideo();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        traceText.text += "rewarded : reward : " + amount.ToString() + " | " + type;
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        traceText.text += "rewarded : close apps : ";
        RequestRewardBasedVideo();
    }
    #endregion
}
