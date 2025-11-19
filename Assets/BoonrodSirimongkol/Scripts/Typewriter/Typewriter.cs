using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Typewriter : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI textUI;

    [Header("Typing Settings")]
    public float baseSpeed = 0.05f;   
    public float fastSpeed = 0.01f;   
    
    public Quest currentQuest;

    private Coroutine typingCoroutine;

    private void Start()
    {
        StartTyping(currentQuest.dialogue[0],true);
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
}
