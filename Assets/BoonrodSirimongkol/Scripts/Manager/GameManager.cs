using UnityEngine;

namespace BoonrodSirimongkol.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        
        public Share.GameState CurrentGameState { get; private set; }
        public bool level2Unlocked = false;
        
        private Share.GameState _currentGameState;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        public void StartGame()
        {
            SetGameState(Share.GameState.EnterLevelState);
        }
        public bool IsLevel2Unlocked()
        {
            return level2Unlocked ;
        }
        public void SetGameState(Share.GameState newState)
        {
            _currentGameState = newState;
            CurrentGameState = _currentGameState;
        }
    }
}
