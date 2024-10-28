using UnityEngine;

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
            if (GameManager.Instance.State == GameManager.GameState.Pause)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Pause() {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Pause);
            GameUI.SetActive(false);
            MenuUI.SetActive(true);
    }

    public void Resume() {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);
            GameUI.SetActive(true);
            MenuUI.SetActive(false);
    }

}
