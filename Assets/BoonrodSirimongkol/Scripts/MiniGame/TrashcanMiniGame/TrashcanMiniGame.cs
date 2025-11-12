using System;
using BoonrodSirimongkol.Scripts.MiniGame;
using UnityEngine;

public class TrashcanMiniGame : MiniGame
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

    public override void StartMiniGame()
    {
        throw new NotImplementedException();
    }

    public override void EndMiniGame()
    {
        gameObject.SetActive(false);
        collected = 0;
        MiniGameProgress.Instance.UpdateProgress(1);
        MiniGameManager.Instance.OpenNextStateOrFinish(); 
    }
}
