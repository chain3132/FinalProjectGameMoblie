using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    [SerializeField]
    private Quest _currentQuest;
    private bool _isQuestCompleted;
    
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
    public void SetQuestCompleted(bool isCompleted)
    {
        _isQuestCompleted = isCompleted;
    }
    public bool GetQuestCompleted()
    {
        return _isQuestCompleted;
    }
    
    public Quest GetCurrentQuest()
    {
        return _currentQuest;
    }
    public void SetCurrentQuest(Quest quest)
    {
        _currentQuest = null;
        _currentQuest = quest;
    }
}
