using UnityEngine;
using TMPro;

public class stopwatchControl : MonoBehaviour
{
    
    public TMP_Text stopwatchText;

    private float elapsedTime;
    private bool isRunning;
    public float GetElapsedTime() => elapsedTime;
    void Awake()
    {
        elapsedTime = 0f;
        isRunning = false;
        startStopwatch();

    }

    // Update is called once per frame
    void Update()
    {
        if(isRunning)
        {
            elapsedTime += Time.deltaTime;
            updateDisplay();
        }

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
        int minutes = Mathf.FloorToInt(elapsedTime/60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);

        stopwatchText.text = string.Format("{0:00}:{1:00}:{2:000}",minutes,seconds,milliseconds);
    }
}
