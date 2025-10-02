using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private Animator panelToClose;
    private bool isPanelOpen = false;
    
    public void HandlePanel()
    {
        isPanelOpen = !isPanelOpen;
        if (!isPanelOpen)
        {
            panelToClose.SetTrigger("ClosePanel");
            return;
        }
        else
        {
            panelToClose.SetTrigger("OpenPanel");

        }
        
        
    }
}
