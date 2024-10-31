using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    public GameObject GameUI;
    public GameObject MenuUI; 

    void Start() {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);

        GameUI.SetActive(true);
        MenuUI.SetActive(false);
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))   // Clicking escape to pause/resume the game
        {
            pauseHandler();
        }
    }

    public void pauseHandler() {
        if (GameManager.Instance.State == GameManager.GameState.Pause)
            {
                Resume();
            } else
            {
                Pause();
            }
    }

    private void Pause() {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Pause);
            GameUI.SetActive(false);
            MenuUI.SetActive(true);
    }

    private void Resume() {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);
            GameUI.SetActive(true);
            MenuUI.SetActive(false);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
