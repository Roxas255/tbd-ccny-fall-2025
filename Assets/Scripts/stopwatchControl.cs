using UnityEngine;
using TMPro;

public class stopwatchControl : MonoBehaviour
{
    public TMP_Text stopwatchText;

    private float elapsedTime;
    private bool isRunning;

    private static stopwatchControl instance;

    public float GetElapsedTime() => elapsedTime;

    void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        elapsedTime = 0f;
        isRunning = false;
        startStopwatch();
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            updateDisplay();
        }
    }

    // Add time penalty on death
    public void AddPenalty(float seconds)
    {
        elapsedTime += seconds;
        updateDisplay();
    }

    public void stopStopwatch()
    {
        isRunning = false;
    }

    public void startStopwatch()
    {
        isRunning = true;
    }

    public void resetStopwatch()
    {
        elapsedTime = 0f;
        isRunning = false;
        updateDisplay();
    }

    private void updateDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);

        if (stopwatchText != null)
        {
            stopwatchText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
    }
}