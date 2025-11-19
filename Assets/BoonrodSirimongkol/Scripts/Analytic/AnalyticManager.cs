using System;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UnityConsent;

public class AnalyticManager : MonoBehaviour
{
    public static AnalyticManager Instance { get; private set; }
    private bool isInitialized = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async void Start()
    {
        if (!isInitialized)
        {
            await UnityServices.InitializeAsync();
            EndUserConsent.SetConsentState(new ConsentState
            {
                AnalyticsIntent = ConsentStatus.Granted
            });
            isInitialized = true;
            Debug.Log("Unity Analytics Initialized.");
        }
    }
    public void StartLevel(string levelName)
    {
        CustomEvent levelStartEvent = new CustomEvent("level_start")
        {
            {"level_name", levelName},
        };
        AnalyticsService.Instance.RecordEvent(levelStartEvent);
        AnalyticsService.Instance.Flush();
        Debug.Log("Level Start Event Recorded: " + levelName);
    }
    public void EndLevel(string levelName, float timeTaken, int score)
    {
        CustomEvent levelEndEvent = new CustomEvent("level_end")
        {
            {"level_name", levelName},
            {"time_taken", timeTaken},
            {"score", score}
        };
        AnalyticsService.Instance.RecordEvent(levelEndEvent);
        AnalyticsService.Instance.Flush();
        Debug.Log("Level End Event Recorded: " + levelName);
    }
    public void MiniGameStart(string miniGameName)
    {
        CustomEvent miniGameStartEvent = new CustomEvent("mini_game_start")
        {
            {"mini_game_name", miniGameName},
        };
        AnalyticsService.Instance.RecordEvent(miniGameStartEvent);
        AnalyticsService.Instance.Flush();
        Debug.Log("Mini-Game Start Event Recorded: " + miniGameName);
    }
}
