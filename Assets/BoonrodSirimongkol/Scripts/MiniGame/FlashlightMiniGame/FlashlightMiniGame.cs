using BoonrodSirimongkol.Scripts.Manager;
using BoonrodSirimongkol.Scripts.MiniGame;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightMiniGame : MiniGame
{
    
    [Header("Dark Area Settings")]
    public Image darkOverlay;
    public GameObject[] hiddenObject;
    public float revealSpeed = 0.5f;    
    private bool isRevealing = false;
    private float visibility = 0f;
    

    [SerializeField] private int totalGarbage = 3;
    [SerializeField] private Sprite garbageIcon;
    [SerializeField] private Sprite flashlightIcon;
    private int collected = 0;

    void Start()
    {
        darkOverlay.color = new Color(0, 0, 0, 0.8f);
        foreach (var obj in hiddenObject) 
        {
            obj.SetActive(false);
        }
    }



    public void StartFlashlight()
    {
        isRevealing = true;
    }

    void Update()
    {
        if (isRevealing)
        {
            visibility += Time.deltaTime * revealSpeed;
            float alpha = Mathf.Lerp(0.8f, 0f, visibility);
            darkOverlay.color = new Color(0, 0, 0, alpha);

            if (visibility >= 1f)
            {
                isRevealing = false;
                foreach (var obj in hiddenObject)
                {
                    obj.SetActive(true);
                    ToolManager.Instance.ChangeIcon(garbageIcon);
                }

            }
        }
    }

    private void OnEnable()
    {
        GarbageFlashlightUI.OnGarbageCollectedinFlashlight += CollectGarbage;
    }

    private void OnDisable()
    {
        GarbageFlashlightUI.OnGarbageCollectedinFlashlight -= CollectGarbage;
    }

    public void CollectGarbage()
    {
        Debug.Log("Garbage Collected!");
        collected++;

        if (collected >= totalGarbage)
        {
            EndMiniGame();
        }
    }

    public override void StartMiniGame()
    {
        throw new System.NotImplementedException();
    }

    public override void EndMiniGame()
    {
        gameObject.SetActive(false);
        ToolManager.Instance.ChangeIcon(flashlightIcon);
        collected = 0;
        MiniGameProgress.Instance.UpdateProgress(1);
        MiniGameManager.Instance.OpenNextStateOrFinish();
    }


}

