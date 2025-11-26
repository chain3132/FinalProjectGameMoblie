using BoonrodSirimongkol.Scripts.Manager;
using UnityEngine;
using PrimeTween;
using UnityEngine.InputSystem;

public class QuestObject : MonoBehaviour
{
    
    [SerializeField] private ToolIconType requiredTool;   

    [Header("Animation Settings")]
    public float moveDuration = 0.5f;
    public float scaleDuration = 0.5f;
    public float fadeDuration = 0.4f;
    public float targetScale = 2f;

    [Header("Move Target")]
    public Vector3 targetPoint;   

    private SpriteRenderer sr;
    private bool isCollected = false;

    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    
    public void Collect()
    {
        if (ToolManager.Instance.CurrentToolType == requiredTool && !isCollected)
        {
            sr.sortingOrder = 10;
            isCollected = true;
            Sequence.Create()
                .Group(Tween.Position(transform, targetPoint ,moveDuration, Ease.OutCubic))
                .Group(Tween.Scale(transform, Vector3.one * targetScale, scaleDuration, Ease.OutBack))
                .Chain(Tween.Custom(sr.color.a, 0f, fadeDuration,
                    v => sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, v),
                    Ease.InCubic))
                .OnComplete(() =>
                {
                    EnergyManager.Instance.IncreaseEnergy(10);
                    QuestManager.Instance.SetQuestCompleted(true);
                    Destroy(gameObject);
                });
        }
        
    }
}
