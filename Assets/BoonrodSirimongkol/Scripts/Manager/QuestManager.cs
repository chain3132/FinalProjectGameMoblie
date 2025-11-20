using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    [SerializeField]
    private Quest _currentQuest;
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
    }

    private void CompleteQuest()
    {
        
    }
    public Quest GetCurrentQuest()
    {
        return _currentQuest;
    }
    public void SetCurrentQuest(Quest quest)
    {
        _currentQuest = quest;
    }
}
