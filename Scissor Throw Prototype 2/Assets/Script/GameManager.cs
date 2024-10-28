using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Play,
        Pause,
    }
    
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> StateChanged;

    public void Awake() {
        Instance = this;
    }

    private void Start() {
        UpdateGameState(GameState.Play);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Play:
                PlayState();
                break;
            case GameState.Pause:
                PauseState();
                break;
            default:
                Debug.Log("ERROR: Unknown game state: " + newState);
                break;
        }
        StateChanged?.Invoke(newState);
    }

    private void PlayState() {  
        Time.timeScale = 1f;
    }

    private void PauseState() {
        Time.timeScale = 0f;   
    }
}

