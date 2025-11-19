using UnityEngine;

namespace BoonrodSirimongkol.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Share.GameState CurrentGameState { get; private set; }
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
    
    }
}
