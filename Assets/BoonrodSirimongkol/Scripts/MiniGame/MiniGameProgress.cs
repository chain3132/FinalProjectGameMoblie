using System;
using BoonrodSirimongkol.Scripts.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        
        
    }

    private void Update()
    {
        if (completedMiniGames >= totalMiniGames && QuestManager.Instance.GetQuestCompleted())
        {
            GameFinished();
        }
    }

    private  void GameFinished()
    {

        switch (GameManager.Instance.currentLevel)
        {
            case 1:
                GameManager.Instance.level2Unlocked = true;
                break;
            case 2:
                GameManager.Instance.level3Unlocked = true;
                break;
            default:
                break;
        }
        Time.timeScale = 0f;

        if (winPanel != null)
        {
            AudioManager.Instance.PlaySFX("Win");
            winPanel.SetActive(true);
        }
            
        else
            Debug.LogWarning("Win Panel is not assigned in the inspector!");
        StartCoroutine(ReturnToSceneSelection());
        
    }
    
    private System.Collections.IEnumerator ReturnToSceneSelection()
    {
        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1f; 
        SceneManager.LoadSceneAsync(1); 
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
