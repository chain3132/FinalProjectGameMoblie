using UnityEngine;
using UnityEngine.UI;

public class MoppingMiniGame : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image puddleImage;     
    [SerializeField] private Slider progressBar;    

    [Header("Gameplay")]
    [SerializeField] private int requiredScrubs = 5; 
    [SerializeField] private float fadePerScrub = 0.18f; 
    private int currentScrubs;

    public void StartMiniGame()
    {
        currentScrubs = 0;
        if (progressBar) { progressBar.value = 0f; progressBar.maxValue = requiredScrubs; }
        if (puddleImage)
        {
            var c = puddleImage.color;
            c.a = 1f;
            puddleImage.color = c;
        }
    }

    
    public void OnMiniGameClickEvent()
    {
        currentScrubs++;
        if (progressBar) progressBar.value = currentScrubs;

        
        if (puddleImage)
        {
            var c = puddleImage.color;
            c.a = Mathf.Clamp01(c.a - fadePerScrub);
            puddleImage.color = c;
        }

        if (currentScrubs >= requiredScrubs)
        {
            EndMiniGame();
        }
    }

    private void EndMiniGame()
    {
        
        gameObject.SetActive(false);
        currentScrubs = 0;
        MiniGameProgress.Instance.UpdateProgress(1);
        MiniGameManager.Instance.OpenNextStateOrFinish();
    }
}

