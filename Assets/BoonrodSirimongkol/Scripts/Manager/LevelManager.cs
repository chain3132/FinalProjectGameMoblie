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
    private bool _isLevel2Unlocked;
    [SerializeField]
    private GameObject level3LockIcon;
    private bool _isLevel3Unlocked;
    

    private void OnEnable()
    {
        Time.timeScale = 1f;
        _isLevel2Unlocked = GameManager.Instance.IsLevel2Unlocked();
        UnlockSelectLevel2(_isLevel2Unlocked);
        _isLevel3Unlocked = GameManager.Instance.IsLevel3Unlocked();
        UnlockSelectLevel3(_isLevel3Unlocked);
    }
    public void UnlockSelectLevel2(bool unlocked)
    {
        level2LockIcon.SetActive(!unlocked);
    }
    public void UnlockSelectLevel3(bool unlocked)
    {
        level3LockIcon.SetActive(!unlocked);
    }
    public void SelectLevel1()
    {
        if (EnergyManager.Instance.IsEnoughEnergy(30))
        {
            AudioManager.Instance.PlaySFX("Click");
            GetQuestFromList(LevelType.Level1);
            GameManager.Instance.currentLevel = 1;
            EnergyManager.Instance.DecreaseEnergy(30);
            SceneManager.LoadScene(2);
        }
        
    }
    public void SelectLevel2()
    {
        if (EnergyManager.Instance.IsEnoughEnergy(30))
        {
            AudioManager.Instance.PlaySFX("Click");
            GetQuestFromList(LevelType.Level2);
            GameManager.Instance.currentLevel = 2;
            EnergyManager.Instance.DecreaseEnergy(30);
            SceneManager.LoadScene(3);
            Time.timeScale = 1f;

        }
    }
    public void SelectLevel3()
    {
        if (EnergyManager.Instance.IsEnoughEnergy(30))
        {
            AudioManager.Instance.PlaySFX("Click");
            GetQuestFromList(LevelType.Level3);
            EnergyManager.Instance.DecreaseEnergy(30);
            SceneManager.LoadScene(4);
            Time.timeScale = 1f;

        }
    }
    public void GetQuestFromList(LevelType level)
    {
        if (_levelQuests.TryGetValue(level, out Quest[] quests))
        {
            int rnd = Random.Range(0, quests.Length);
            QuestManager.Instance.SetCurrentQuest(quests[rnd]);
            QuestManager.Instance.SetQuestCompleted(false);
        }
        else
        {
            Debug.LogError("No quests found for level: " + level);
        }
    }
    

    
}
