using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class ApplyRemoteConfig : MonoBehaviour
{
    public static ApplyRemoteConfig instance {get ; private set;}
    public bool isHalloweenEvent ;
    public HalloweenSetup halloweenSetup;
    public struct userData
    {
        public int score;
    }
    public struct appAttributes
    {
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void AppRemoteSetting(ConfigResponse configResponse)
    {
        Debug.Log("RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());
        switch (configResponse.requestOrigin)
        {
            case ConfigOrigin.Default:
                Debug.Log("No settings loaded this session; using default values.");
                break;
            case ConfigOrigin.Cached:
                Debug.Log("No settings loaded this session; using cached values from a previous session.");
                break;
            case ConfigOrigin.Remote:
                Debug.Log("New settings loaded this session; update values accordingly.");
                isHalloweenEvent = RemoteConfigService.Instance.appConfig.GetBool("isHalloween");
                halloweenSetup.SetUpHalloween(isHalloweenEvent);
                break;
        }
    }

    async Task Start()
    {
        if (Utilities.CheckForInternetConnection())
        {
            await InitalizeRemoteConfigAsync();
        }
        userData userData = new userData();
        userData.score = 10;
        RemoteConfigService.Instance.FetchCompleted += AppRemoteSetting;
        RemoteConfigService.Instance.FetchConfigs(new userData(), new appAttributes());
    }

    private async Task InitalizeRemoteConfigAsync()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        
    }
}
