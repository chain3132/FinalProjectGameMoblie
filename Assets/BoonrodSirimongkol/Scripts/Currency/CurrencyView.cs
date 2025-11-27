using System;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI currencyText;

    private void Start()
    {
        UpdateText();
    }
    
    public void UpdateText()
    {
        var currency = EnergyManager.Instance.CurrentEnergy;
        currencyText.text = $"{currency} / 60";
    }
}
