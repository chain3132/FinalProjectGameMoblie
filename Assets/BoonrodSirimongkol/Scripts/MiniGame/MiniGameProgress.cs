using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameProgress : MonoBehaviour
{
    [SerializeField] private Image progressFill; 
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField]private int totalMiniGames = 3;
    [SerializeField] private GameObject winPanel;


    private int completedMiniGames = 0;
    public static MiniGameProgress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateProgress(0);
        

    }


    public void UpdateProgress(int completed)
    {
        completedMiniGames += completed;
        float progress = (float)completedMiniGames / totalMiniGames;
        progress = Mathf.Clamp01(progress);

        if (progressFill)
            progressFill.fillAmount = progress;

        if (progressText)
            progressText.text = Mathf.RoundToInt(progress * 100f) + "%";

        
        if (completedMiniGames >= totalMiniGames)
        {
            GameFinished();
        }
    }

    private void GameFinished()
    {
        Debug.Log("All mini-games completed!");

        Time.timeScale = 0f;

        if (winPanel != null)
            winPanel.SetActive(true);
        else
            Debug.LogWarning("Win Panel is not assigned in the inspector!");
        
    }
    [ContextMenu("AnalysissStartLevel")]
    public void AnalysissStartLevel()
    {
        AnalyticManager.Instance.StartLevel("Level1");
    }
    [ContextMenu("AnalysissEndLevel")]
    public void AnalysissEndLevel()
    {
        AnalyticManager.Instance.EndLevel("Level1",30f,100);
    }
    [ContextMenu("AnalysissMiniGame")]
    public void AnalysissMiniGame()
    {
        AnalyticManager.Instance.MiniGameStart("MoppingMiniGame");
    }
}
