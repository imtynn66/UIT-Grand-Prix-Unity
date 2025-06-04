using UnityEngine;
using TMPro; // Dùng TextMeshPro

public class PlayerCheckpointTracker : MonoBehaviour
{
    public int totalCheckpoints = 14;
    public int totalLaps = 3;

    public int currentCheckpoint = 0;
    public int currentLap = 0;

    public GameObject endRacePanel;
    public TextMeshProUGUI lapText; // Hiển thị số vòng

    private void Start()
    {
        // Tự động tìm endRacePanel nếu chưa kéo tay
        if (endRacePanel == null)
        {
            GameObject canvas = GameObject.Find("ingame-Canvas");
            if (canvas != null)
            {
                Transform panelTransform = canvas.transform.Find("EndRacePanel");
                if (panelTransform != null)
                {
                    endRacePanel = panelTransform.gameObject;
                    endRacePanel.SetActive(false);
                }
                else
                {
                    Debug.LogError("Không tìm thấy EndRacePanel trong Canvas.");
                }

                // Tự động tìm lapText nếu chưa kéo tay
                Transform lapTextTransform = canvas.transform.Find("LapText");
                if (lapTextTransform != null)
                {
                    lapText = lapTextTransform.GetComponent<TextMeshProUGUI>();
                }
                else
                {
                    Debug.LogWarning("Không tìm thấy LapText trong Canvas.");
                }
            }
            else
            {
                Debug.LogError("Không tìm thấy ingame-Canvas.");
            }
        }

        UpdateLapText(); // Hiển thị lap ban đầu
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        CheckPoint checkpoint = other.GetComponent<CheckPoint>();
        if (checkpoint != null)
        {
            int cpNum = checkpoint.checkPointNumber;

            if (cpNum == currentCheckpoint + 1)
            {
                currentCheckpoint++;

                if (currentCheckpoint == totalCheckpoints)
                {
                    currentLap++;
                    currentCheckpoint = 0;

                    Debug.Log("Lap Completed: " + currentLap);
                    UpdateLapText();

                    if (currentLap >= totalLaps)
                    {
                        EndRace();
                    }
                }
            }
        }
    }

    void UpdateLapText()
    {
        if (lapText != null)
        {
            lapText.text = "Lap: " + currentLap + " / " + totalLaps;
        }
    }

    void EndRace()
    {
        if (endRacePanel != null)
        {
            endRacePanel.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("Race Finished!");
        }
    }
}
