using UnityEngine;

public static class AdConfig 
{
    public static string AppKey => GetAppKey();
    public static string BannerAdUnitId => GetBannerAdUnitId();
    public static string InterstitialAdUnitId => GetInterstitialAdUnitId();
    public static string RewardedAdUnitId => GetRewardedAdUnitId();

    static string GetAppKey()
    {
#if UNITY_ANDROID
        return "5966853";
#elif UNITY_IOS
        return "5966852";
#else
        return string.Empty;
#endif
    }

    static string GetRewardedAdUnitId()
    {
#if UNITY_ANDROID
        return "53kfrx62romcx1wu";
#elif UNITY_IOS
        return "53kfrx62romcx1wu";
#else
        return string.Empty;
#endif
    }

    static string GetInterstitialAdUnitId()
    {
#if UNITY_ANDROID
        return "8cjfdg6mrkrz8upu";
#elif UNITY_IOS
        return "8cjfdg6mrkrz8upu";
#else
        return string.Empty;
#endif
    }

    static string GetBannerAdUnitId()
    {
#if UNITY_ANDROID
        return "jtpwwmkzgdoqww7w";
#elif UNITY_IOS
        return "jtpwwmkzgdoqww7w";
#else
        return string.Empty;
#endif
    }
}
     

