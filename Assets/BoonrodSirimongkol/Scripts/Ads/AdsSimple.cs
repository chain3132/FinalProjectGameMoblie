using System;
using TMPro;
using Unity.Services.Core;
using UnityEngine;
using Unity.Services.LevelPlay;
using UnityEngine.SceneManagement;

public class AdsSimple : MonoBehaviour 
{
    public static AdsSimple instacne;
    private LevelPlayBannerAd _bannerAd;
    private LevelPlayInterstitialAd _interstitialAd;
    private LevelPlayRewardedAd _rewardedAd;
    [SerializeField] private TextMeshProUGUI energyText;
    bool isAdEnable = false;

    private void Awake()
    {
        if (instacne == null)
        {
            instacne = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Open ads
        LevelPlay.ValidateIntegration();
        LevelPlay.OnInitSuccess += SdkInitSuccess;
        LevelPlay.OnInitFailed += SdkInitFailed;
        SceneManager.sceneLoaded += OnSceneLoaded;

        LevelPlay.Init(AdConfig.AppKey);
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        Debug.Log("Ads SDK Init Started");
    }

    private void SdkInitFailed(LevelPlayInitError obj)
    {
        Debug.Log("Ads SDK Init Failed: " + obj.ToString());
    }

    private void SdkInitSuccess(LevelPlayConfiguration obj)
    {
        Debug.Log("Ads SDK Init Success");
        EnableAds();
        isAdEnable = true;

    }

    private void EnableAds()
    {
        var configBuilder = new LevelPlayBannerAd.Config.Builder();
            configBuilder.SetSize(LevelPlayAdSize.BANNER).SetPosition(LevelPlayBannerPosition.BottomRight);
            var BannerConfig = configBuilder.Build();
        _bannerAd = new LevelPlayBannerAd(AdConfig.BannerAdUnitId, BannerConfig);
        _interstitialAd = new LevelPlayInterstitialAd(AdConfig.InterstitialAdUnitId);
        _rewardedAd = new LevelPlayRewardedAd(AdConfig.RewardedAdUnitId);
    }
    public void LoadBannerAd()
    {
        _bannerAd.LoadAd();
    }
    public void ShowBannerAd()
    {
        _bannerAd.ShowAd();
    }
    public void HideBannerAd()
    {
        _bannerAd.HideAd();
    }
    public void LoadInterstitialAds()
    {
        _interstitialAd.LoadAd();
    }
    public void ShowInterstitialAds()
    {
        if (_interstitialAd.IsAdReady())
        {
            _interstitialAd.ShowAd();
        }
        else
        {
            Debug.Log("Interstitial Ad is not ready yet.");
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);
        if (scene.name == "SectionLevel") 
        {
            Debug.Log("Show banner in MainMenu");
            _bannerAd.LoadAd();
            _bannerAd.ShowAd();
            _rewardedAd.LoadAd();
            _interstitialAd.LoadAd();
        }
        else
        {
            Debug.Log("Hide banner in other scenes");
            _bannerAd.HideAd();
        }
    }
    
    public void LoadRewardedAds()
    {
        _rewardedAd?.LoadAd();
    }
    public void ShowRewardedAds()
    {
        if (_rewardedAd.IsAdReady())
        {
            _rewardedAd.ShowAd();
            _bannerAd.HideAd();
            int energy = 30;
            energyText.text = ($"{energy} / 60 ").ToString();
            
        }
        else
        {
            Debug.Log("Interstitial Ad is not ready yet.");
        }
    }
   
    private void OnDestroy()
    {
        _bannerAd?.DestroyAd();
        
        
    }
}
