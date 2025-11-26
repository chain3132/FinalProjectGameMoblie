using System;
using BoonrodSirimongkol.Scripts.Manager;
using TMPro;
using UnityEngine;

public class QuestHint : MonoBehaviour
{
    [SerializeField] private TMP_Text hintText;

    private void OnEnable()
    {
        ShowHint(QuestManager.Instance.GetCurrentQuest().hints[0]);
    }

    public void ShowHint(string hint)
    {
        hintText.text = hint;
        gameObject.SetActive(true);
    }
}
