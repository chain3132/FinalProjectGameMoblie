using System;
using UnityEngine;
using UnityEngine.UI;

namespace BoonrodSirimongkol.Scripts.MiniGame.PictureFrameMiniGame
{
    public class PictureFrameMiniGame : MiniGame
    {
        [SerializeField] private RectTransform pictureFrameImage;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private int requiredRotations ;
        private int currentRotations;

        public void OnMiniGameClickEvent()
        {
            
            if (currentRotations >= requiredRotations)
            {
                EndMiniGame();
            }
            currentRotations++;
            pictureFrameImage.Rotate(0,0,-15);
        }

        public void SetMiniGameImg( Sprite img)
        {
            backgroundImage.sprite = img;
            pictureFrameImage.rotation = Quaternion.Euler(0,0,120);
        }

        public override void StartMiniGame()
        {
            throw new NotImplementedException();
        }

        public override void EndMiniGame()
        {
            Debug.Log("MiniGame Completed!");
            gameObject.SetActive(false);
            currentRotations = 0;
            MiniGameProgress.Instance.UpdateProgress(1);
            MiniGameManager.Instance.OpenNextStateOrFinish(); 
        }
    }
}
