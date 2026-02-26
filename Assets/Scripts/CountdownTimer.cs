using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 600f; // 10 minutes
    public TextMeshProUGUI timerText;

    [Header("Game Over")]
    public string gameOverSceneName = "GameOver";

    private bool timerIsRunning = true;
    private bool gameOverTriggered = false;

    void Update()
    {
        if (timerIsRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else if (!gameOverTriggered)
        {
            timeRemaining = 0;
            timerIsRunning = false;
            DisplayTime(timeRemaining);
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        gameOverTriggered = true;
        SceneManager.LoadScene(gameOverSceneName);
    }

    void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = $"Time Remaining: {minutes:00}:{seconds:00}";
    }
}