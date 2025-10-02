using TMPro;
using UnityEngine;

namespace BoonrodSirimongkol.Scripts.Controller
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] private float startTime = 180f; // 3 นาที = 180 วินาที
        [SerializeField] private TextMeshProUGUI timerText;

        private float currentTime;
        private bool isRunning = false;

        void Start()
        {
            currentTime = startTime;
            isRunning = true;
            UpdateTimerUI();
        }

        void Update()
        {
            if (!isRunning) return;

            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                isRunning = false;
                OnTimeOver();
            }

            UpdateTimerUI();
        }

        private void UpdateTimerUI()
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private void OnTimeOver()
        {
            Debug.Log("Time is up! You Lose!");
            Time.timeScale = 0f; 
            // TODO: เรียก Game Over Panel 
            
        }

        public void ResetTimer(float newTime)
        {
            currentTime = newTime;
            isRunning = true;
        }

        public void StopTimer()
        {
            isRunning = false;
        }
    }
}
