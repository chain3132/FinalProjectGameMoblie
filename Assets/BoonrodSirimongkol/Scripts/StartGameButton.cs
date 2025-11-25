using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public void StartGame()
    {
        AudioManager.Instance.PlaySFX("Click");
        SceneManager.LoadSceneAsync(1);
    }
}
