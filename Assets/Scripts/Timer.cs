using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 60f; // Initial timer value in seconds
    [SerializeField] private TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component

    private bool timerIsRunning = false;

    void Start()
    {
        // Start the timer
        timerIsRunning = true;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                UpdateTimerDisplay();
                TimerEnded();
            }
        }
    }

    private void UpdateTimerDisplay()
    {
        // Convert float timeRemaining to minutes and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // Format the time string and update the timerText
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimerEnded()
    {
        // Handle what happens when the timer ends
        Debug.Log("Timer has ended!");
    }
}

