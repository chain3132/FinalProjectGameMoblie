using BoonrodSirimongkol.Scripts.Manager;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private Animator panelToClose;
    private bool isPanelOpen = false;
    private Button closeButton;
    private void Awake()
    {
        closeButton = GetComponent<Button>();
    }
    
    public void HandlePanel()
    {
        if (GameManager.Instance.CurrentGameState != Share.GameState.DuringLevelState)
        {
            closeButton.interactable = false;
            return;
        }
        closeButton.interactable = true;
        isPanelOpen = !isPanelOpen;
        if (!isPanelOpen)
        {
            AudioManager.Instance.PlaySFX("Click");
            panelToClose.SetTrigger("ClosePanel");
            return;
        }
        else
        {
            AudioManager.Instance.PlaySFX("Click");
            panelToClose.SetTrigger("OpenPanel");

        }
        
        
    }
}
