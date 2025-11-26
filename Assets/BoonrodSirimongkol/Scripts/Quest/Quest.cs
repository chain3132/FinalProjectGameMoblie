using UnityEngine;
public enum LevelType
{
    None,
    Level1,
    Level2,
    Level3
}
public enum QuestGoal
{
    None,
    Collect
}
[CreateAssetMenu(fileName = "New Quest", menuName = "BoonrodSirimongkol/Quest/New Quest")]
public class Quest : ScriptableObject
{
    public LevelType level;
    [TextArea] 
    public string[] dialogue;  
    public string[] hints;
    public QuestGoal goal; 
    public Sprite icon;
    public GameObject questPrefab;
    
}
