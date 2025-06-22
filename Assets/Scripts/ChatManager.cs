using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomChatManager : MonoBehaviourPunCallbacks
{
    public InputField chatInputField;
    public Button sendButton;
    public Transform chatContent;
    public GameObject chatMessagePrefab;

    private void Start()
    {
        sendButton.onClick.AddListener(OnSendClicked);
        chatInputField.onEndEdit.AddListener(OnEndEdit);
    }

    void OnSendClicked()
    {
        SendChatMessage();
    }

    void OnEndEdit(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SendChatMessage();
            chatInputField.ActivateInputField();
        }
    }

    void SendChatMessage()
    {
        string msg = chatInputField.text.Trim();
        if (!string.IsNullOrEmpty(msg))
        {
            photonView.RPC("ReceiveChatMessage", RpcTarget.All, PhotonNetwork.NickName, msg);
            chatInputField.text = "";
        }
    }

    [PunRPC]
    void ReceiveChatMessage(string sender, string message)
    {
        GameObject entry = Instantiate(chatMessagePrefab, chatContent);
        Text text = entry.GetComponent<Text>();
        text.supportRichText = true; // Đảm bảo bật RichText (thường mặc định là true)
        text.text = $"<b><color=#FFD700>{sender}</color>:</b> {message}";
    }
}