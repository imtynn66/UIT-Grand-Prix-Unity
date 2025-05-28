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
        statusText.text = "Đang kết nối đến Photon...";
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        statusText.text = "Đã kết nối!";
    }

    public void CreateRoom()
    {
        string roomName = roomNameInput.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 2 });
            statusText.text = $"Đang tạo phòng...";
        }
    }

    public void JoinRoom()
    {
        string roomName = roomNameInput.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            PhotonNetwork.JoinRoom(roomName);
            statusText.text = $"Đang tham gia phòng...";
        }
    }

    public override void OnJoinedRoom()
    {
        statusText.text = "Đã vào phòng!";
        PhotonNetwork.LoadLevel("Online"); // Chuyển sang scene đua xe
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        statusText.text = "Không thể tham gia phòng: " + message;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        statusText.text = "Tạo phòng thất bại: " + message;
    }
}
