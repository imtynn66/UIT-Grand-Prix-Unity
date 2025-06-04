using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingRoomManager : MonoBehaviourPunCallbacks
{
    public Text roomNameText;
    public Transform playerListParent;
    public GameObject playerNamePrefab;
    public Button startGameButton;

    void Start()
    {
        roomNameText.text = "Room: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
        startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);

        // Đảm bảo Photon tự động sync scene
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void UpdatePlayerList()
    {
        foreach (Transform child in playerListParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject entry = Instantiate(playerNamePrefab, playerListParent);
            Text nameText = entry.GetComponent<Text>();
            nameText.text = player.NickName + (player.IsMasterClient ? " (Host)" : "");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Master Client starting game for all players");

            // Sử dụng PhotonNetwork.LoadLevel sẽ load cho tất cả players
            // nếu AutomaticallySyncScene = true
            PhotonNetwork.LoadLevel("Online");
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Menu");
    }
}