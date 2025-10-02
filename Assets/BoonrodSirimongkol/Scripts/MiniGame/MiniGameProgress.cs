using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameProgress : MonoBehaviour
{
    [SerializeField] private Image progressFill; 
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField]private int totalMiniGames = 3;  
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
        progressFill.fillAmount = progress;
        progressText.text = Mathf.RoundToInt(progress * 100f) + "%";
        if (completedMiniGames >= totalMiniGames)
        {
            
        }
    }
}
