using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    private float elapsedTime;
    private bool isRunning;

    void Start() {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameManager.StateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.StateChanged += OnGameStateChanged;
    }

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    private void OnGameStateChanged(GameManager.GameState newState)
    {
        isRunning = newState == GameManager.GameState.Play;
    }

    private void UpdateTimerUI()
    {
        int mins = Mathf.FloorToInt(elapsedTime / 60F);
        int secs = Mathf.FloorToInt(elapsedTime % 60F);
        timerText.text = $"Time: {mins:00}:{secs:00}";
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerUI();
    }
}
