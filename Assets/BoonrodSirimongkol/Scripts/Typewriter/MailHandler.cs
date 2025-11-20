using System;
using System.Collections;
using System.Collections.Generic;
using BoonrodSirimongkol.Scripts.Manager;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MailHandler : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI textUI;
    [SerializeField]
    private GameObject questPanel;
    [SerializeField]
    private Image questImage;
    [Header("Typing Settings")]
    public float baseSpeed = 0.05f;   
    public float fastSpeed = 0.01f;   
    
    [SerializeField]
    public Transform questSpawnPoint;
    
    public Quest currentQuest;

    private Coroutine typingCoroutine;

    private void Awake()
    {
        questPanel.SetActive(false);
    }

    private void OnEnable()
    {
        questPanel.SetActive(true);
        SetMail();  
        GameManager.Instance.StartGame();
    }

    private void Start()
    {
        if (currentQuest == null){return;}
        StartTyping(currentQuest.dialogue[0],true);
    }

    private void SetMail()
    {
        currentQuest = QuestManager.Instance.GetCurrentQuest();
        questImage.sprite = currentQuest.icon;
    }
    private void SpawnQuest()
    {
        //Instantiate(currentQuest.questPrefab, questSpawnPoint.position, currentQuest.questPrefab.transform.rotation);
    }
    public void StartTyping(string fullText, bool isFast = false, System.Action onFinished = null)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(fullText, isFast, onFinished));
    }
    private IEnumerator TypeText(string fullText, bool isFast, System.Action onFinished)
    {
        textUI.text = "";
        float speed = isFast ? fastSpeed : baseSpeed;

        foreach (char c in fullText)
        {
            textUI.text += c;
            yield return new WaitForSeconds(speed);
        }

        onFinished?.Invoke();
    }
    public void AcceptQuest()
    {
        SpawnQuest();
        questPanel.SetActive(false);
        GameManager.Instance.SetGameState(Share.GameState.DuringLevelState);
    }
}
