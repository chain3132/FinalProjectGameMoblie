using UnityEngine;
using UnityEngine.UI;

namespace BoonrodSirimongkol.Scripts.Manager
{
    public class ToolManager : MonoBehaviour
    {
        public static ToolManager Instance;

        public Sprite CurrentToolIcon { get; private set; }
        public ToolIconType CurrentToolType{ get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void SelectTool(Image icon, ToolIconType toolIcon)
        {
            CurrentToolIcon = icon.sprite;
            CurrentToolType = toolIcon;
            Debug.Log("Selected Tool: " + toolIcon);
        }

        public void ClearTool()
        {
            CurrentToolIcon = null;
            CurrentToolType = ToolIconType.None;
        }
    }
}
