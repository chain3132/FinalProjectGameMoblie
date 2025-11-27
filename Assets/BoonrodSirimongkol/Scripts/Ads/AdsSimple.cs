using System;
using TMPro;
using Unity.Services.Core;
using UnityEngine;
using Unity.Services.LevelPlay;
using UnityEngine.SceneManagement;

public class AdsSimple : MonoBehaviour 
{
    public static AdsSimple instacne;
    private LevelPlayInterstitialAd _interstitialAd;
    private LevelPlayRewardedAd _rewardedAd;
    [SerializeField] private TextMeshProUGUI energyText;
    bool isAdEnable = false;

    private void Awake()
    {
        if (instacne == null)
        {
            instacne = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelPlay.SetMetaData("is_test_suite", "enable");

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
        _rewardedAd.LoadAd();
        _interstitialAd.LoadAd();

    }

    private void EnableAds()
    {
        var configBuilder = new LevelPlayBannerAd.Config.Builder();
            configBuilder.SetSize(LevelPlayAdSize.BANNER).SetPosition(LevelPlayBannerPosition.BottomRight);
            var BannerConfig = configBuilder.Build();
        _interstitialAd = new LevelPlayInterstitialAd(AdConfig.InterstitialAdUnitId);
        _rewardedAd = new LevelPlayRewardedAd(AdConfig.RewardedAdUnitId);
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
            _rewardedAd.LoadAd();
            _interstitialAd.LoadAd();
        }
        else
        {
            Debug.Log("Hide banner in other scenes");
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
            int energy = 30;
            EnergyManager.Instance.IncreaseEnergy(energy);
            
        }
        else
        {
            Debug.Log("Interstitial Ad is not ready yet.");
        }
    }
   
    private void OnDestroy()
    {
        
        
    }
}
