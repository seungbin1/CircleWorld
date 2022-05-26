using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdMobManager : MonoBehaviour
{
    public static AdMobManager _adMobManager;

    string android_appId;
    string ios_appId;

    string android_banner_id;
    string ios_banner_id;

    string android_interstitial_id;
    string ios_interstitial_id;

    string android_reward_id;
    string ios_reward_id;

    private BannerView bannerView;
    private InterstitialAd interstitialAd;

    private RewardBasedVideoAd rewardBasedVideo;

    private bool noAds; //광고 제거 상품을 구매했는지 확인
    bool _showAds; // 10 스테이지 이상에서만 광고를 보여주기 위한 장치

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _adMobManager = this;
        if (_adMobManager != null && _adMobManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _adMobManager = this;
        }
    }

    public void Start()
    {
        if (!PlayerPrefs.HasKey("NoAds"))
        {
            PlayerPrefs.SetString("NoAds", "False");
        }
        CheckShowAds();

        android_appId = AdmobIds.admobAppIdAndroid;
        android_banner_id = AdmobIds.admobBannerIdAndroid;
        ios_banner_id = AdmobIds.admobBannerIdIos;
        android_interstitial_id = AdmobIds.admobInterstitialIdAndroid;
        ios_interstitial_id = AdmobIds.admobInterstitialIdIos;
        android_reward_id = AdmobIds.admobRewardIdAndroid;
        ios_reward_id = AdmobIds.admobRewardIdIos;

#if UNITY_ANDROID
        MobileAds.Initialize(android_appId);
#elif UNITY_IOS
        MobileAds.Initialize(ios_appId);
#endif

        CheckNoAds();

        RequestBannerAd(); //하단 배너를 준비 

        RequestInterstitialAd();    //전면 광고를 준비

        // Get singleton reward based video ad reference.
        //전면 광고를 준비 
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        this.RequestRewardBasedVideo();
    }

    //하단 배너 준비 
    public void RequestBannerAd()
    {
        if (!noAds)
        {
            string adUnitId = string.Empty;

#if UNITY_ANDROID
            adUnitId = android_banner_id;
#elif UNITY_IOS
        adUnitId = ios_banner_id;
#endif

            bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.BottomLeft);

            AdRequest request = new AdRequest.Builder().Build();

            bannerView.LoadAd(request);
            ShowBannerAd();
        }
    }

    //전면 광고 준비 
    private void RequestInterstitialAd()
    {
        if (!noAds)
        {
            string adUnitId = string.Empty;

#if UNITY_ANDROID
            adUnitId = android_interstitial_id;
#elif UNITY_IOS
        adUnitId = ios_interstitial_id;
#endif

            interstitialAd = new InterstitialAd(adUnitId);
            AdRequest request = new AdRequest.Builder().Build();

            interstitialAd.LoadAd(request);

            interstitialAd.OnAdClosed += HandleOnInterstitialAdClosed;
        }
    }
    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
    {
        print("HandleOnInterstitialAdClosed event received.");

        interstitialAd.Destroy();

        RequestInterstitialAd();
    }

    //리워드 광고 준비
    private void RequestRewardBasedVideo()
    {
        string adUnitId = string.Empty;

#if UNITY_ANDROID
        adUnitId = android_reward_id;
#elif UNITY_IPHONE
        adUnitId = ios_reward_id;
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }
    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideo();
    }
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        //리워드 광고를 보고온 후 실행되는 명령을 이곳에 작성 
        //PlayMgr.instance.ShowHint();
        //ShopMgr.instance.SendMessage("GetRewardRuby");
    }

    //하단 배너를 보이게 한다
    public void ShowBannerAd()
    {
        if (!noAds)
        {
            bannerView.Show();
        }
    }
    //하단 배너를 감춘다
    public void HideBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Hide();
        }
    }

    //전면 광고를 보이게 한다
    public void ShowInterstitialAd()
    {
        if (!noAds)
        {
            if (!interstitialAd.IsLoaded())
            {
                RequestInterstitialAd();
                return;
            }

            if (_showAds)
            {
                interstitialAd.Show();
            }
        }
    }

    //리워드 광고를 보이게 한다
    public void ShowRewardAd()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }

    public void CheckNoAds()
    {
        noAds = bool.Parse(PlayerPrefs.GetString("NoAds"));
        if (noAds)
        {
            HideBannerAd();
        }
    }

    void CheckShowAds() //10스테이지를 넘겼는지 확인, 그 이후로만 광고를 보여준다
    {
        //if (PlayerPrefs.GetInt("Stage_12") >= 1) //1던전을 클리어했다면
        //{
        //    _showAds = true;
        //}
        _showAds = true;
    }

}