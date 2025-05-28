using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints; // 2 điểm spawn

    void Start()
    {
        int actorIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1; // ActorNumber bắt đầu từ 1
        actorIndex = Mathf.Clamp(actorIndex, 0, spawnPoints.Length - 1);

        Vector3 spawnPos = spawnPoints[actorIndex].position;
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
    }
}
