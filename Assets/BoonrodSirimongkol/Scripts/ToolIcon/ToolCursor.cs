using BoonrodSirimongkol.Scripts.Manager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ToolCursor : MonoBehaviour
{
    [SerializeField] private Image toolImage;

    private void Update()
    {
        if (ToolManager.Instance.CurrentToolIcon != null)
        {
            toolImage.enabled = true;
            toolImage.sprite = ToolManager.Instance.CurrentToolIcon;

            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            {
                Vector2 pos = Touchscreen.current.primaryTouch.position.ReadValue();
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    transform.parent as RectTransform, pos, null, out Vector2 localPos);
                toolImage.rectTransform.localPosition = localPos;
            }
        }
        else
        {
            if (!toolImage){return;}
            toolImage.enabled = false;
        }
    }
}
