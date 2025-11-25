using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using BoonrodSirimongkol.Scripts.MiniGame;

namespace BoonrodSirimongkol.Scripts.MiniGame.LaundryMiniGame
{
    public class LaundryMiniGame : MiniGame
    {
        [Header("UI References")]
        [SerializeField] private RectTransform[] clothRects;       
        [SerializeField] private RectTransform washingMachineSlot; 
        [SerializeField] private float moveDuration = 0.5f;


        [Header("Raycast")]
        [SerializeField] private Canvas uiCanvas;                  

        private Vector2[] _startPositions;
        private bool _isMoving = false;
        private bool _isFinished = false;

        private GraphicRaycaster _raycaster;
        private EventSystem _eventSystem;

        [SerializeField] private int totalClothes = 6;
        private int collected = 0;

        private void Awake()
        {
            
            if (clothRects != null && clothRects.Length > 0)
            {
                _startPositions = new Vector2[clothRects.Length];
                for (int i = 0; i < clothRects.Length; i++)
                {
                    if (clothRects[i] != null)
                    {
                        _startPositions[i] = clothRects[i].anchoredPosition;
                    }
                }
            }

            
            if (uiCanvas == null)
            {
                uiCanvas = GetComponentInParent<Canvas>();
            }

            if (uiCanvas != null)
            {
                _raycaster = uiCanvas.GetComponent<GraphicRaycaster>();
            }

            _eventSystem = EventSystem.current;
        }

 
        public override void StartMiniGame()
        {
            _isMoving = false;
            _isFinished = false;

            
            if (clothRects != null && clothRects.Length > 0)
            {
                for (int i = 0; i < clothRects.Length; i++)
                {
                    if (clothRects[i] == null) continue;

                    if (_startPositions != null && i < _startPositions.Length)
                    {
                        clothRects[i].anchoredPosition = _startPositions[i];
                    }

                    clothRects[i].gameObject.SetActive(true);
                }
            }
        }

        public override void EndMiniGame()
        {
            gameObject.SetActive(false);
            MiniGameManager.Instance.OpenNextStateOrFinish();
            Debug.Log("EndYong");
        }

        public void OnMiniGameClickEvent()
        {
            if (_isMoving || _isFinished) return;
            if (clothRects == null || clothRects.Length == 0) return;
            if (_raycaster == null || _eventSystem == null) return;

            Vector2 screenPos;

            if (Touchscreen.current != null)
            {
                screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
            }
            else if (Mouse.current != null)
            {
                screenPos = Mouse.current.position.ReadValue();
            }
            else
            {
                return;
            }

            PointerEventData pointerData = new PointerEventData(_eventSystem)
            {
                position = screenPos
            };

            List<RaycastResult> results = new List<RaycastResult>();
            _raycaster.Raycast(pointerData, results);

            RectTransform targetCloth = null;
            int targetIndex = -1;

            foreach (var hit in results)
            {
                for (int i = 0; i < clothRects.Length; i++)
                {
                    RectTransform cloth = clothRects[i];
                    if (cloth == null) continue;
                    if (!cloth.gameObject.activeInHierarchy) continue;

                    // ถ้าคลิกโดนตัวผ้าหรือ child ของมัน
                    if (hit.gameObject == cloth.gameObject || hit.gameObject.transform.IsChildOf(cloth.transform))
                    {
                        targetCloth = cloth;
                        targetIndex = i;
                        break;
                    }
                }

                if (targetCloth != null)
                    break;
            }

            if (targetCloth == null || targetIndex < 0)
                return;
            AudioManager.Instance.PlaySFX("Thorw");
            StartCoroutine(MoveClothToWasher(targetCloth, targetIndex));
        }

        private IEnumerator MoveClothToWasher(RectTransform cloth, int clothIndex)
        {
            _isMoving = true;

            Vector2 start = cloth.anchoredPosition;
            Vector2 end = washingMachineSlot.anchoredPosition;

            float t = 0f;
            while (t < moveDuration)
            {
                t += Time.deltaTime;
                float lerp = Mathf.Clamp01(t / moveDuration);
                cloth.anchoredPosition = Vector2.Lerp(start, end, lerp);
                yield return null;
            }

            cloth.anchoredPosition = end;

            // รอให้เห็นผ้าเข้าเครื่อง
            //yield return new WaitForSeconds(1f);

            //cloth.gameObject.SetActive(false);

            // เช็คว่ามีผ้าเหลืออีกไหม
            collected++;
            _isMoving = false;

            if (collected >= totalClothes) 
            {
                EndMiniGame();
            }

            
        }
    }
}



