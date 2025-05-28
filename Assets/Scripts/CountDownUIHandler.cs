using UnityEngine;
using TMPro;
using Photon.Pun;
using System.Collections;

public class CountdownManager : MonoBehaviourPun
{
    public TextMeshProUGUI countdownText;
    public float countdownTime = 3f;
    public static bool raceStarted = false;

    PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        Debug.Log("IsMaster: " + PhotonNetwork.IsMasterClient);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Calling RPC StartCountdown...");
            photonView.RPC("StartCountdown", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void StartCountdown()
    {
        Debug.Log("StartCountdown RPC called");
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        float current = countdownTime;

        while (current > 0)
        {
            countdownText.text = Mathf.Ceil(current).ToString();
            Debug.Log("Countdown: " + countdownText.text);
            yield return new WaitForSeconds(1f);
            current--;
        }

        countdownText.text = "GO!";
        raceStarted = true;
        Debug.Log("GO!");
        yield return new WaitForSeconds(1f);
        countdownText.text = "";
    }
}
