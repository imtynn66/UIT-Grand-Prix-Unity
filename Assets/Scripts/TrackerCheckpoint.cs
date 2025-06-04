using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerCheckpointTracker : MonoBehaviourPun, IPunObservable
{
    public int totalCheckpoints = 14;
    public int totalLaps = 3;

    public int currentCheckpoint = 0;
    public int currentLap = 0;

    public GameObject endRacePanel;
    public TextMeshProUGUI lapText;
    public TextMeshProUGUI winnerText; // Kéo thả component này trong Inspector

    private bool raceFinished = false;

    private void Start()
    {
        if (!photonView.IsMine) return;

        SetupUI();
        UpdateLapText();
    }

    private void SetupUI()
    {
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

                Transform lapTextTransform = canvas.transform.Find("LapText");
                if (lapTextTransform != null)
                {
                    lapText = lapTextTransform.GetComponent<TextMeshProUGUI>();
                }

                // Tìm winnerText trong EndRacePanel thay vì Canvas root
                if (endRacePanel != null)
                {
                    Transform winnerTextTransform = endRacePanel.transform.Find("WinnerText");
                    if (winnerTextTransform != null)
                    {
                        winnerText = winnerTextTransform.GetComponent<TextMeshProUGUI>();
                        Debug.Log("Found WinnerText component: " + winnerText.name);
                    }
                    else
                    {
                        Debug.LogError("WinnerText not found in EndRacePanel!");
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!photonView.IsMine) return;

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

                    if (currentLap >= totalLaps && !raceFinished)
                    {
                        raceFinished = true;
                        string playerName = PhotonNetwork.LocalPlayer.NickName;

                        Debug.Log("Sending RPC with winner: " + playerName);
                        photonView.RPC("PlayerWins", RpcTarget.All, playerName);
                    }
                }
            }
        }
    }

    [PunRPC]
    void PlayerWins(string playerName)
    {
        Debug.Log("RPC PlayerWins received: " + playerName);
        EndRace(playerName);
    }

    void UpdateLapText()
    {
        if (lapText != null)
        {
            lapText.text = "Lap: " + currentLap + " / " + totalLaps;
        }
    }

    void EndRace(string winnerName)
    {
        if (endRacePanel != null)
        {
            endRacePanel.SetActive(true);

            // Cập nhật text winner
            if (winnerText != null)
            {
                winnerText.text = "WINNER: " + winnerName;
                Debug.Log("Updated winner text to: " + winnerText.text);
            }
            else
            {
                Debug.LogError("winnerText is null!");
            }

            Time.timeScale = 0f;
            Debug.Log("Race Finished! Winner: " + winnerName);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentCheckpoint);
            stream.SendNext(currentLap);
        }
        else
        {
            currentCheckpoint = (int)stream.ReceiveNext();
            currentLap = (int)stream.ReceiveNext();
        }
    }
}