using System;
using UnityEngine;

public class TrashcanMiniGame : MonoBehaviour
{
    [SerializeField] private int totalGarbage = 3; 
    private int collected = 0;

    private void OnEnable()
    {
        GarbageUI.OnGarbageCollected += CollectGarbage;
    }

    private void OnDisable()
    {
        GarbageUI.OnGarbageCollected -= CollectGarbage;
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

    private void EndMiniGame()
    {
        gameObject.SetActive(false);
        collected = 0;
        MiniGameProgress.Instance.UpdateProgress(1);
        MiniGameManager.Instance.OpenNextStateOrFinish(); 
    }
}
