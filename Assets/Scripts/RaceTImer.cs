using UnityEngine;
using TMPro;
using Photon.Pun;

public class RaceTimer : MonoBehaviourPun
{
    public TextMeshProUGUI timerText;

    private float raceTime = 0f;
    private bool isRunning = false;

    void Update()
    {
        if (!CountdownManager.raceStarted) return;

        if (!isRunning)
        {
            isRunning = true;
            raceTime = 0f;
        }

        raceTime += Time.deltaTime;
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(raceTime / 60f);
        int seconds = Mathf.FloorToInt(raceTime % 60f);
        int milliseconds = Mathf.FloorToInt((raceTime * 100f) % 100f);
        timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }

    public float GetFinalTime()
    {
        return raceTime;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
