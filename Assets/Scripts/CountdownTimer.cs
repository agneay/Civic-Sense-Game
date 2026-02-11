using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 600f; // 10 minutes = 600 seconds
    public TextMeshProUGUI timerText;
    private bool timerIsRunning = true;

    void Update()
    {
        if (timerIsRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerIsRunning = false;
            DisplayTime(timeRemaining);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("Time Remaining: {0:00}:{1:00}", minutes, seconds);
    }
}
