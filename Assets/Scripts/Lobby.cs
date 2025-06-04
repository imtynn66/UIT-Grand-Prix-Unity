using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    public InputField roomNameInput;
    public Text statusText;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        statusText.text = "Conecting to Photon";
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        statusText.text = "Conected!";
    }

    public void CreateRoom()
    {
        string roomName = roomNameInput.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 2 });
            statusText.text = $"Creating Room...";
        }
    }

    public void JoinRoom()
    {
        string roomName = roomNameInput.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            PhotonNetwork.JoinRoom(roomName);
            statusText.text = $"Joining Room...";
        }
    }

    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }

    public override void OnJoinedRoom()
    {
        statusText.text = "Joined!";
        PhotonNetwork.LoadLevel("Online"); // Chuyển sang scene đua xe
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        statusText.text = "Can't Join: " + message;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        statusText.text = "Failed!" + message;
    }
}
