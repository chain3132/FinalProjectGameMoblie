using System;
using BoonrodSirimongkol.Scripts.Manager;
using UnityEngine;

namespace BoonrodSirimongkol.Scripts
{
    public enum EventMiniGameType
    {
        None,
        PictureFrame,
        GarbageCollection
    }
    public class InterectableObject : MonoBehaviour
    {
        [SerializeField] private ToolIconType requiredTool;
        [SerializeField] private EventMiniGameType miniGameType;
        private bool isFixed = false;
        private InterectableObject thisInterectableObjectObject;

        private void Awake()
        {
            thisInterectableObjectObject = this;
        }

        public void OnClicked()
        {
            if (ToolManager.Instance.CurrentToolType == requiredTool && !isFixed)
            {
                MiniGameManager.Instance.GetCurrentMiniGameType(miniGameType,this.thisInterectableObjectObject);
                Debug.Log("Open MiniGame for " + requiredTool + "!");
            }
        }
        public void SetFixed()
        {
            isFixed = true;
            transform.rotation = Quaternion.Euler(0,0,0);
            if (miniGameType == EventMiniGameType.GarbageCollection)
            {
                SetDestory();
            }
            
        }

        public void SetDestory()
        {
            Destroy(gameObject);
        }
    }
}
