using System;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance;
    public int CurrentEnergy { get; private set; }
    public CurrencyView currencyView;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        SetEnergy(60);
    }

    

    public void SetEnergy(int energy)
    {
        CurrentEnergy = energy;
    }
    
    public void DecreaseEnergy(int amount)
    {
        CurrentEnergy -= amount;
        if (CurrentEnergy < 0)
        {
            CurrentEnergy = 0;
        }
    }
    public void IncreaseEnergy(int amount)
    {
        CurrentEnergy += amount;
    }
    public void UpdateEnergyView()
    {
        if (currencyView == null)
        {
            var curerencyViewObj = GameObject.FindAnyObjectByType<CurrencyView>();
            currencyView = curerencyViewObj;
        }
        currencyView.UpdateText();
    }
    public bool IsEnoughEnergy(int amount)
    {
        if (CurrentEnergy >= amount)
        {
            return true;
        }
        return false;
        
    }
}
