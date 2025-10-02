using System;
using BoonrodSirimongkol.Scripts.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace BoonrodSirimongkol.Scripts.ToolIcon
{
    public class ToolIconView : MonoBehaviour
    {
        #region Inspector
        [SerializeField] private ToolIconData toolIconData;
        [SerializeField] private Image toolIconImage;
        #endregion
        
        #region LifeCycle

        public void Start()
        {
            Init();
        }
        private void Init()
        {
            SettingToolIcon();
        }

        #endregion

        #region Method

        private void SettingToolIcon()
        {
            toolIconImage.sprite = toolIconData.ToolIconSprite;
        }
        public void OnClickSelectTool()
        {
            if (ToolManager.Instance.CurrentToolType == toolIconData.ToolIconType )
            {
                ToolManager.Instance.ClearTool();
                return;
            }

            if (toolIconData.ToolIconType != ToolIconType.None)
            {
                ToolManager.Instance.ClearTool();
                ToolManager.Instance.SelectTool(toolIconImage, toolIconData.ToolIconType);
                Debug.Log(": Select Tool: " + toolIconData.ToolIconType);
            }
            if (ToolManager.Instance.CurrentToolType == ToolIconType.None)
            {
                ToolManager.Instance.SelectTool(toolIconImage, toolIconData.ToolIconType);

            }
        }

        #endregion
        
    }
}
