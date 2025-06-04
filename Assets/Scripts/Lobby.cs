using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    public InputField roomNameInput;
    public InputField playerNameInput; // ✅ Input tên người chơi
    public Text statusText;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        statusText.text = "Connecting to Photon...";
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        statusText.text = "Connected!";
    }

    public void CreateRoom()
    {
        string roomName = roomNameInput.text;
        string playerName = playerNameInput.text;

        if (!string.IsNullOrEmpty(roomName) && !string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.NickName = playerName;
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4; 
            PhotonNetwork.CreateRoom(roomName, options);
            statusText.text = $"Creating Room...";
        }
        else
        {
            statusText.text = "Enter Room Name and Player Name!";
        }
    }

    public void JoinRoom()
    {
        string roomName = roomNameInput.text;
        string playerName = playerNameInput.text;

        if (!string.IsNullOrEmpty(roomName) && !string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.NickName = playerName;
            PhotonNetwork.JoinRoom(roomName);
            statusText.text = $"Joining Room...";
        }
        else
        {
            statusText.text = "Enter Room Name and Player Name!";
        }
    }

    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }

    public override void OnJoinedRoom()
    {
        statusText.text = "Joined!";
        PhotonNetwork.LoadLevel("Waiting"); 
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        statusText.text = "Can't Join: " + message;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        statusText.text = "Create Failed: " + message;
    }
}
