using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 60f;
    [SerializeField] private TextMeshProUGUI timerText;

    private bool timerIsRunning = false;

    void Start()
    {
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
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimerEnded()
    {
        Debug.Log("Timer has ended!");
        AudioManager.Instance.StopBGM();
        // Wait for a short delay before loading the end scene
        Invoke("LoadEndScene", 0.5f);
    }

    private void LoadEndScene()
    {
        SceneManager.LoadScene("End"); // Make sure your end scene is named "End" in build settings
    }
}
