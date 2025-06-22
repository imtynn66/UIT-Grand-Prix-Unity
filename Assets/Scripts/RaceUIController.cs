using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class RaceUIController : MonoBehaviourPun
{
    public GameObject endRacePanel;

    [PunRPC]
    public void ShowEndPanel()
    {
        endRacePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Lobby");
    }

    // Gọi hàm này khi 1 người thắng
    public void TriggerEndGame()
    {
        photonView.RPC("ShowEndPanel", RpcTarget.All);
    }
}
