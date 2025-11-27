using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance;
    public int CurrentEnergy { get; private set; }
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
    public bool IsEnoughEnergy(int amount)
    {
        if (CurrentEnergy >= amount)
        {
            return true;
        }
        return false;
        
    }
}
