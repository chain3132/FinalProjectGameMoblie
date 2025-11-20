using System;
using System.Collections.Generic;
using BoonrodSirimongkol.Scripts.Manager;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private AYellowpaper.SerializedCollections.SerializedDictionary<LevelType, Quest[]> _levelQuests;
    [SerializeField]
    private GameObject level2LockIcon;
    private bool isLevel2Unlocked;
    

    private void OnEnable()
    {
        isLevel2Unlocked = GameManager.Instance.IsLevel2Unlocked();
        Debug.Log(isLevel2Unlocked);
        UnlockSelectLevel2(isLevel2Unlocked);
    }
    public void UnlockSelectLevel2(bool unlocked)
    {
        level2LockIcon.SetActive(!unlocked);
    }
    public void SelectLevel1()
    {
        GetQuestFromList(LevelType.Level1);
        EnergyManager.Instance.DecreaseEnergy(30);
        SceneManager.LoadScene(1);
    }
    public void GetQuestFromList(LevelType level)
    {
        if (_levelQuests.TryGetValue(level, out Quest[] quests))
        {
            int rnd = Random.Range(0, quests.Length);
            QuestManager.Instance.SetCurrentQuest(quests[rnd]);
        }
        else
        {
            Debug.LogError("No quests found for level: " + level);
        }
    }
    

    
}
