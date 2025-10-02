using System;
using BoonrodSirimongkol.Scripts;
using BoonrodSirimongkol.Scripts.MiniGame.PictureFrameMiniGame;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{

    #region Inspactor

    [SerializeField] private PictureFrameMiniGame pictureFrameMiniGamePanel;
    [SerializeField] private TrashcanMiniGame trashcanMiniGamePanel;

    #endregion
    
    #region Properties
    public EventMiniGameType CurrentTMiniGameType { get; private set; }

    #endregion

    #region Fields

    public static MiniGameManager Instance;
    private bool isMiniGameStarted = false;
    private InterectableObject _currentInterectableObject;

    #endregion

    #region LifeCycle

    private void Awake()
    {
        if (Instance == null) Instance = this;
        
    }
    
    private void Update()
    {
        if (isMiniGameStarted)
        {
            if (CurrentTMiniGameType == EventMiniGameType.PictureFrame)
            {
                if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
                {
                    pictureFrameMiniGamePanel.OnMiniGameClickEvent();
                }
            }
            else if (CurrentTMiniGameType == EventMiniGameType.GarbageCollection)
            {
                
            }
        }
    }
    #endregion
    
    #region Controllers

    public void GetCurrentMiniGameType(EventMiniGameType miniGameType , InterectableObject  interectableObject)
    {
        CurrentTMiniGameType = miniGameType;
        _currentInterectableObject = interectableObject;
        Debug.Log(_currentInterectableObject);
        Debug.Log("Current MiniGame Type: " + CurrentTMiniGameType);
        OpenMiniGamePanel();
    }
    
    public void OpenMiniGamePanel()
    {
        switch (CurrentTMiniGameType)
        {
            case EventMiniGameType.None:
                break;
            case EventMiniGameType.PictureFrame:
                pictureFrameMiniGamePanel.gameObject.SetActive(true);
                var sprite = _currentInterectableObject.GetComponent<SpriteRenderer>().sprite;
                Debug.Log(sprite);
                pictureFrameMiniGamePanel.SetMiniGameImg(sprite);
                isMiniGameStarted = true;
                break;
            case EventMiniGameType.GarbageCollection:
                trashcanMiniGamePanel.gameObject.SetActive(true);
                isMiniGameStarted = true;
                break;
            default:
                Debug.Log("No MiniGameType Selected");
                break;
        }
    }
    
    public void OpenNextStateOrFinish()
    {
        isMiniGameStarted = false;
        _currentInterectableObject.SetFixed();
        
        _currentInterectableObject = null;
        CurrentTMiniGameType = EventMiniGameType.None;
    }

    #endregion
    
    
    

}
