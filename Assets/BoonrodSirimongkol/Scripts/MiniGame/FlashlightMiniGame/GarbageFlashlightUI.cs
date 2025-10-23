using System;
using UnityEngine;

public class GarbageFlashlightUI : MonoBehaviour
{
    [SerializeField] private GameObject garbage;
    public static event Action OnGarbageCollectedinFlashlight;
    public void OnclickGarbage() 
    {
        garbage.SetActive(false);
        OnGarbageCollectedinFlashlight.Invoke();
        Debug.Log("im coming");
    }
}
