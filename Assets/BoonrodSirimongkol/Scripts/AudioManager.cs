using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM Settings")]
    public AudioSource bgmSource;

    [Header("SFX Settings")]
    public AudioSource sfxSource;

    [Header("Sound List (SFX)")]
    public List<SoundData> sfxList = new List<SoundData>();

    [Header("BGM List")]
    public List<SoundData> bgmList = new List<SoundData>();

    private Dictionary<string, AudioClip> sfxDict;
    private Dictionary<string, AudioClip> bgmDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            BuildDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        PlayBGM("song1");
    }

    private void BuildDictionary()
    {
        sfxDict = new Dictionary<string, AudioClip>();
        foreach (var s in sfxList)
        {
            if (!sfxDict.ContainsKey(s.key))
                sfxDict.Add(s.key, s.clip);
        }

        bgmDict = new Dictionary<string, AudioClip>();
        foreach (var b in bgmList)
        {
            if (!bgmDict.ContainsKey(b.key))
                bgmDict.Add(b.key, b.clip);
        }
    }

   
    public void PlayBGM(string key)
    {
        if (!bgmDict.ContainsKey(key))
        {
            Debug.LogWarning("No BGM with key: " + key);
            return;
        }

        bgmSource.clip = bgmDict[key];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

   
    public void PlaySFX(string key)
    {
        if (!sfxDict.ContainsKey(key))
        {
            Debug.LogWarning("No SFX with key: " + key);
            return;
        }

        sfxSource.PlayOneShot(sfxDict[key]);
    }
}


[System.Serializable]
public class SoundData
{
    public string key;
    public AudioClip clip;
}
